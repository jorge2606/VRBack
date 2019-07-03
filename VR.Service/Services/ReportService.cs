using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using reportViewerNamespace = AspNetCore.Report;
using reportingNameSpace = AspNetCore.Reporting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Service.Common.ServiceResult;
using VR.Data;
using VR.Data.Model;
using VR.Data.Model.ModelStoreProcedure;
using VR.Dto;
using VR.Dto.User;
using VR.Service.Interfaces;
using VR.Web.Helpers;
using User = VR.Data.Model.User;
using Microsoft.Extensions.Configuration;

namespace VR.Service.Services
{
    
    public class ReportService : IReportService
    {
        private static string StaticFilesDirectory = Path.Combine(Directory.GetCurrentDirectory(), "StaticFiles");
        private readonly ISolicitationSubsidyService _solicitationSubsidyService;
        private readonly IApproveOfAuthorityThatOrderCommissionsService _iApproveOfAuthorityThatOrderCommissionsService;
        private readonly IDestinyService _destinyService;
        private readonly DataContext _context;
        public IConfiguration Configuration { get; }

        public ReportService(
            ISolicitationSubsidyService solicitationSubsidyService,
            IDestinyService destinyService,
            DataContext context,
            IApproveOfAuthorityThatOrderCommissionsService iApproveOfAuthorityThatOrderCommissionsService,
            IConfiguration configuration
            )
        {
            _solicitationSubsidyService = solicitationSubsidyService;
            _destinyService = destinyService;
            _context = context;
            _iApproveOfAuthorityThatOrderCommissionsService = iApproveOfAuthorityThatOrderCommissionsService;
            Configuration = configuration;
        }


        public ServiceResult<reportViewerNamespace.ReportResponse> ExpendituresReport()
        {
            var username = Configuration.GetSection("Reports").GetSection("Username").Value;
            var password = Configuration.GetSection("Reports").GetSection("Password").Value;
            var reportServer = Configuration.GetSection("Reports").GetSection("ReportServer").Value;
            var path = Configuration.GetSection("Reports").GetSection("Path").Value;
            var domain = Configuration.GetSection("Reports").GetSection("Domain").Value;

            var rv = new reportViewerNamespace.ReportViewer(new reportViewerNamespace.ReportSettings()
            {
                ReportServer = reportServer,
                ShowToolBar = true
            });


            var result = rv.Execute(new reportViewerNamespace.ReportRequest()
            {
                ExecuteType = reportViewerNamespace.ReportExecuteType.Export,
                RenderType = reportViewerNamespace.ReportRenderType.Pdf,
                Path = "/"+path+"/Expenditures"
            });
            return new ServiceResult<reportViewerNamespace.ReportResponse>(result);
        }

        public ServiceResult<byte[]> ReportPrintAsync(Guid solicitationId)
        {
            var newDirectory = Path.Combine(StaticFilesDirectory, "Reports", "VR_REPORT.rdl");
            var files = new FileInfo(newDirectory);

            var notif = new ServiceResult<byte[]>();

            if (!files.Exists)
            {
                notif.AddError("Error", "El Reporte no fue encontrado.");
                return notif;
            }
            var rv = new reportingNameSpace.LocalReport(newDirectory);
            var solic = _solicitationSubsidyService.GetByIdSubsidy(solicitationId).Response;
            if (solic == null)
            {
                notif.AddError("Error","La solicitud no existe");
                return notif;
            }
            var destiny = _destinyService.Get_DestiniesProcedure(solicitationId).Response;
            var images = _solicitationSubsidyService.SolicitationApprovedBySupervisorId(solicitationId, solic.UserId).Response;
            var totalLetter = _context.GetLetterNumberTotalSolicitationAsync(destiny.Sum(x => x.Amount).ToString("F").Replace(",", "."), ".");
            rv.AddDataSource("SolicitationDTODataSet", new List<FindByIdSolicitationSubsidyDto>() { solic });
            rv.AddDataSource("UserDataSet",new List<UserDto>(){solic.User});
            rv.AddDataSource("Destination", destiny);
            rv.AddDataSource("SignSupervisorImage",new List<UrlSignHolograph>(){ images });
            rv.AddDataSource("DestinationDataSet", solic.Destinies);
            rv.AddDataSource("ExpenditureDataSet", solic.Expenditures);
            rv.AddDataSource("Common", new List<ReportDto>()
            {
                new ReportDto()
                {
                    TodayDate = DateTime.Today.ToString("d"),
                    TotalLetter = totalLetter.FirstOrDefault() == null ? "" : totalLetter.FirstOrDefault().LetterNumber
                }
            });
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            var result = rv.Execute(reportingNameSpace.RenderType.Pdf);

            return new ServiceResult<byte[]>(result.MainStream);
        }

        public ServiceResult<byte[]> PrintAccountFor(Guid solicitationId)
        {
            var newDirectory = Path.Combine(StaticFilesDirectory, "Reports", "Rendición.rdl");
            var files = new FileInfo(newDirectory);

            var notif = new ServiceResult<byte[]>();

            if (!files.Exists)
            {
                notif.AddError("Error", "El Reporte no fue encontrado.");
                return notif;
            }
            var solic = _solicitationSubsidyService.GetByIdSubsidy(solicitationId).Response;
            if (solic == null)
            {
                notif.AddError("Error","La solicitud no existe.");
                return notif;
            }
            var rv = new reportingNameSpace.LocalReport(newDirectory);
            rv.AddDataSource("SolicitationDataset", new List<FindByIdSolicitationSubsidyDto>(){ solic } );
            rv.AddDataSource("UserDataSet", new List<UserDto>() { solic.User });
            rv.AddDataSource("SolicitationStateDataSet", new List<SolicitationStateDto>() { solic.SolicitationStates.FirstOrDefault(x => x.FileNumber != null) });
            rv.AddDataSource("OrganismDataSet", new List<OrganismBaseDto>() { new OrganismBaseDto()
            {
                Name = _context.Distributions.FirstOrDefault(x => x.Id == solic.User.DistributionId).Name
            } });
            rv.AddDataSource("DestinationDataSet", solic.Destinies);
            solic.Expenditures.Add(new ExpenditureFromSolicitationSubsidyByIdDto()
            {
                SupportingDateTime = new Nullable<DateTime>(),
                OrderNumber = new Nullable<int>(),
                Description = solic.Motive,
                AccountedForAmount = new Nullable<decimal>(),
                ExpenditureTypeName = ""
            });
            rv.AddDataSource("ExpenditureDataSet", solic.Expenditures);
            var adv = solic.Destinies.Sum(x => x.Days * x.PercentageCodeLiquidation * x.AdvanceCategory) +
                  solic.Expenditures.Where(x => x.IsRequested).Sum(j => j.Amount);
            var supp = solic.Destinies.Sum(x => x.DaysPay * x.PercentageCodeLiquidation * x.AdvanceCategory) +
                   solic.Expenditures.Sum(j => j.AccountedForAmount ?? 0);

            var newCommon = new ReportDto()
            {
                Advance = adv,
                SupportingPresent = supp,
                Total = adv - supp,
                LegalRulingDescription = solic.LegalRulingDescription == null ? "" : solic.LegalRulingDescription
            };
            newCommon.TotalBool();
            var secondCommon = new ReportDto()
            {
                Advance = adv,
                SupportingPresent = supp,
                Total = adv - supp
            };
            rv.AddDataSource("CommonDataSet", new List<ReportDto>()
            {
                newCommon,
                secondCommon
            });
            
            var result1 = _iApproveOfAuthorityThatOrderCommissionsService.GetApproved(solic.Id);
            var listApprove = new VerificationCommissionDto();
            listApprove.SetValues(result1.Response);
            rv.AddDataSource("VerificationCommissionDataSet", new List<VerificationCommissionDto>()
            {
                listApprove
            });

            rv.AddDataSource("ObservationList", new List<ObservationDto>(solic.Observations));
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            var result = rv.Execute(reportingNameSpace.RenderType.Pdf);

            return new ServiceResult<byte[]>(result.MainStream);
        }

        public ServiceResult<byte[]> PrintReportSolicitationSubsidyByUser(Guid userId)
        {
            var newDirectory = Path.Combine(StaticFilesDirectory, "Reports", "SolicitationSubsidyByUser.rdl");
            var files = new FileInfo(newDirectory);

            var notif = new ServiceResult<byte[]>();

            if (!files.Exists)
            {
                notif.AddError("Error", "El Reporte no fue encontrado.");
                return notif;
            }
            var rv = new reportingNameSpace.LocalReport(newDirectory);
            var user = _context.Users.FirstOrDefault(c => c.Id == userId);
            var solicitation = _context.Report_SolicitationByUserProcedure(userId);

            if (solicitation.Count() > 0)
            {
                rv.AddDataSource("ReportSolicitationByUser", new List<SolicitationSubsidyByUser>(solicitation));
            }
            else
            {
                rv.AddDataSource("ReportSolicitationByUser", new List<SolicitationSubsidyByUser>());
            }
            
            rv.AddDataSource("CommonDataSet", new List<ReportDto>()
            {
                new ReportDto()
                {
                    TodayDate = DateTime.Today.ToShortDateString()
                }
            });
            rv.AddDataSource("UserDataSet", new List<User>(){user});
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            var result = rv.Execute(reportingNameSpace.RenderType.Pdf);

            return new ServiceResult<byte[]>(result.MainStream);
        }

        public ServiceResult<byte[]> PrintReportSolicitationSubsidyByOrganism(Guid organismId)
        {
            var newDirectory = Path.Combine(StaticFilesDirectory, "Reports", "SolicitationSubsidyByOrganism.rdl");
            var files = new FileInfo(newDirectory);

            var notif = new ServiceResult<byte[]>();

            if (!files.Exists)
            {
                notif.AddError("Error", "El Reporte no fue encontrado.");
                return notif;
            }
            var rv = new reportingNameSpace.LocalReport(newDirectory);
            var organismResult = _context.Organisms.FirstOrDefault(x => x.Id == organismId);

            var solicitation = _context.Report_SolicitationByOrganism(organismId);

            
            if (solicitation.Count() > 0)
            {
                rv.AddDataSource("ReportSolicitationByUser", new List<SolicitationSubsidyByOrganism>(solicitation));
            }
            else
            {
                rv.AddDataSource("ReportSolicitationByUser", new List<SolicitationSubsidyByOrganism>());
            }
            rv.AddDataSource("OrganismDataSet", new List<Organism>(){ organismResult });
            rv.AddDataSource("CommonDataSet", new List<ReportDto>()
            {
                new ReportDto()
                {
                    TodayDate = DateTime.Today.ToShortDateString()
                }
            });
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            var result = rv.Execute(reportingNameSpace.RenderType.Pdf);

            return new ServiceResult<byte[]>(result.MainStream);
        }

        public ServiceResult<byte[]> PrintReport_SolicitationByDestiniesAndDates(ReportByDestiniesAndDatesDto reportByDestiniesAndDates)
        {
            var newDirectory = Path.Combine(StaticFilesDirectory, "Reports", "SolicitationSubsidyByDestinationAndDate.rdl");
            var files = new FileInfo(newDirectory);

            var notif = new ServiceResult<byte[]>();

            if (!files.Exists)
            {
                notif.AddError("Error", "El Reporte no fue encontrado.");
                return notif;
            }

            reportByDestiniesAndDates.CityId = reportByDestiniesAndDates.CityId.Equals(Guid.Empty) ? null : reportByDestiniesAndDates.CityId;
            reportByDestiniesAndDates.CountryId = reportByDestiniesAndDates.CountryId.Equals(Guid.Empty) ? null : reportByDestiniesAndDates.CountryId;
            reportByDestiniesAndDates.ProvinceId = reportByDestiniesAndDates.ProvinceId.Equals(Guid.Empty) ? null : reportByDestiniesAndDates.ProvinceId;
            reportByDestiniesAndDates.StartDate = new DateDto()
            {
                Day = reportByDestiniesAndDates.StartDateDay,
                Month = reportByDestiniesAndDates.StartDateMonth,
                Year = reportByDestiniesAndDates.StartDateYear
            };
            reportByDestiniesAndDates.EndDate = new DateDto()
            {
                Day = reportByDestiniesAndDates.EndDay,
                Month = reportByDestiniesAndDates.EndMonth,
                Year = reportByDestiniesAndDates.EndYear
            };

            var rv = new reportingNameSpace.LocalReport(newDirectory);
            DateTime? startDate = null;
            DateTime? endDate = null;
            if (reportByDestiniesAndDates.StartDate != null 
                && !reportByDestiniesAndDates.StartDate.Day.Equals(0)
                && !reportByDestiniesAndDates.StartDate.Month.Equals(0)
                && !reportByDestiniesAndDates.StartDate.Year.Equals(0))
            {
                startDate = reportByDestiniesAndDates.StartDate.ToDateTime();
            }

            if (reportByDestiniesAndDates.EndDate != null
                && !reportByDestiniesAndDates.EndDate.Day.Equals(0)
                && !reportByDestiniesAndDates.EndDate.Month.Equals(0)
                && !reportByDestiniesAndDates.EndDate.Year.Equals(0))
            {
                endDate = reportByDestiniesAndDates.EndDate.ToDateTime();
            }
            var solicitation = _context.Report_SolicitationByDestiniesAndDates(
                startDate,
                endDate,
                reportByDestiniesAndDates.CountryId,
                reportByDestiniesAndDates.CityId,
                reportByDestiniesAndDates.ProvinceId
                );

            
            if (solicitation.Count() > 0)
            {
                rv.AddDataSource("ReportSolicitationByUser", new List<SolicitationSubsidyByOrganism>(solicitation));
            }
            else
            {
                rv.AddDataSource("ReportSolicitationByUser", new List<SolicitationSubsidyByOrganism>());
            }
            rv.AddDataSource("CommonDataSet", new List<ReportDto>()
            {
                new ReportDto()
                {
                    TodayDate = DateTime.Today.ToShortDateString()
                }
            });
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            var result = rv.Execute(reportingNameSpace.RenderType.Pdf);

            return new ServiceResult<byte[]>(result.MainStream);
        }

        public ServiceResult<byte[]> SolicitationsPendingProcedure()
        {
            var newDirectory = Path.Combine(StaticFilesDirectory, "Reports", "SolicitationsPendingProcedure.rdl");
            var files = new FileInfo(newDirectory);

            var notif = new ServiceResult<byte[]>();

            if (!files.Exists)
            {
                notif.AddError("Error", "El Reporte no fue encontrado.");
                return notif;
            }
            var rv = new reportingNameSpace.LocalReport(newDirectory);

            var solicitation = _context.SolicitationsPendingProcedure();

            if (solicitation.Count() > 0)
            {
                rv.AddDataSource("ReportSolicitationByUser", new List<SolicitationSubsidyByOrganism>(solicitation));
            }
            else
            {
                rv.AddDataSource("ReportSolicitationByUser", new List<SolicitationSubsidyByOrganism>());
            }
            rv.AddDataSource("CommonDataSet", new List<ReportDto>()
            {
                new ReportDto()
                {
                    TodayDate = DateTime.Today.ToShortDateString()
                }
            });
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            var result = rv.Execute(reportingNameSpace.RenderType.Pdf);

            return new ServiceResult<byte[]>(result.MainStream);
        }

        public ServiceResult<byte[]> SolicitationsExpireProcedure()
        {
            var newDirectory = Path.Combine(StaticFilesDirectory, "Reports", "SolicitationsExpireProcedure.rdl");
            var files = new FileInfo(newDirectory);

            var notif = new ServiceResult<byte[]>();

            if (!files.Exists)
            {
                notif.AddError("Error", "El Reporte no fue encontrado.");
                return notif;
            }
            var rv = new reportingNameSpace.LocalReport(newDirectory);

            var solicitation = _context.SolicitationsPendingProcedure();

            if (solicitation.Count() > 0)
            {
                rv.AddDataSource("ReportSolicitationByUser", new List<SolicitationSubsidyByOrganism>(solicitation));
            }
            else
            {
                rv.AddDataSource("ReportSolicitationByUser", new List<SolicitationSubsidyByOrganism>());
            }
            rv.AddDataSource("CommonDataSet", new List<ReportDto>()
            {
                new ReportDto()
                {
                    TodayDate = DateTime.Today.ToShortDateString()
                }
            });
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            var result = rv.Execute(reportingNameSpace.RenderType.Pdf);

            return new ServiceResult<byte[]>(result.MainStream);
        }

        public ServiceResult<byte[]> ExpenditureProcedure()
        {
            var newDirectory = Path.Combine(StaticFilesDirectory, "Reports", "Expenditures.rdl");
            var files = new FileInfo(newDirectory);

            var notif = new ServiceResult<byte[]>();

            if (!files.Exists)
            {
                notif.AddError("Error", "El Reporte no fue encontrado.");
                return notif;
            }
            var rv = new reportingNameSpace.LocalReport(newDirectory);

            var solicitation = _context.ExpenditureProcedure();

            if (solicitation.Count() > 0)
            {
                rv.AddDataSource("ExpenditureDataSet", new List<ExpenditureProcedureDto>(solicitation));
            }
            else
            {
                rv.AddDataSource("ExpenditureDataSet", new List<ExpenditureProcedureDto>());
            }
            rv.AddDataSource("CommonDataSet", new List<ReportDto>()
            {
                new ReportDto()
                {
                    TodayDate = DateTime.Today.ToShortDateString()
                }
            });
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            var result = rv.Execute(reportingNameSpace.RenderType.Pdf);

            return new ServiceResult<byte[]>(result.MainStream);
        }

        public ServiceResult<byte[]> PrintReportCommission(Guid solicitationId)
        {
            var newDirectory = Path.Combine(StaticFilesDirectory, "Reports", "Informe_de_comision_de_servicio.rdl");
            var files = new FileInfo(newDirectory);

            var notif = new ServiceResult<byte[]>();

            if (!files.Exists)
            {
                notif.AddError("Error", "El Reporte no fue encontrado.");
                return notif;
            }

            var solic = _solicitationSubsidyService.GetByIdSubsidy(solicitationId).Response;
            if (solic == null)
            {
                notif.AddError("Error", "La solicitud no existe.");
                return notif;
            }

            var rv = new reportingNameSpace.LocalReport(newDirectory);
            var iscommission = solic.IsCommission == null ? true : false;
            if (!iscommission)
            {
                rv.AddDataSource("solicitationDataSet", new List<FindByIdSolicitationSubsidyDto>() { solic });
            }
            else
            {
                var commisionList = _context.SolicitationSubsidies.Where(v => v.RandomKey == solic.RandomKey)
                    .ToList();
                rv.AddDataSource("solicitationDataSet", commisionList);
            }
            
            rv.AddDataSource("DestinationDataSet", new List< DestinyFromSolicitationSubsidyFindByIdDto>(solic.Destinies));
            rv.AddDataSource("UsersDataSet",new List<UserDto>(){ solic.User });
            rv.AddDataSource("ObservationList", new List<ObservationDto>(solic.Observations));

            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            var result = rv.Execute(reportingNameSpace.RenderType.Pdf);
            return new ServiceResult<byte[]>(result.MainStream);
        }

    }
}
