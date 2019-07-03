using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Service.Common.ServiceResult;
using VR.Data;
using VR.Data.Model;
using VR.Dto;
using VR.Service.Interfaces;

namespace VR.Service.Services
{
    public class ApproveOfAuthority_SolicitationService : IApproveOfAuthority_SolicitationService
    {
        private readonly DataContext _context;

        public ApproveOfAuthority_SolicitationService(
            DataContext context
        )
        {
            _context = context;
        }

        public ServiceResult<ApproveOfAuthority_SolicitationDto> SaveChangesOfApprove(
            List<ApproveOfAuthorityThatOrderCommissionsDto> ApprovedList, Guid solicitationId
         )
        {
            var result = _context.ApproveOfAuthoritySolicitations.Where(x => x.SolicitationSubsidyId == solicitationId).ToList();
            //si result es 0 quieere decir que el reintegro/rendición recien se enta creando, simplemente debemos guardar
            if (result.Count() != 0)
            {
                foreach (var approve in ApprovedList)
                {
                    var exist = result.FirstOrDefault(x => x.AprApproveOfAuthorityThatOrderCommissionId == approve.Id);
                    if (approve.Checked)
                    {
                        if (exist == null)
                        {
                            _context.ApproveOfAuthoritySolicitations.Add(new ApproveOfAuthority_Solicitation()
                            {
                                Id = new Guid(),
                                SolicitationSubsidyId = solicitationId,
                                AprApproveOfAuthorityThatOrderCommissionId = approve.Id
                            });
                        }
                    }
                    else
                    {
                        if (exist != null)
                        {
                            _context.ApproveOfAuthoritySolicitations.Remove(exist);
                        }
                    }
                }
            }
            else
            {
                foreach (var approve in ApprovedList)
                {
                    if (approve.Checked)
                    {
                        _context.ApproveOfAuthoritySolicitations.Add(new ApproveOfAuthority_Solicitation()
                        {
                            Id = new Guid(),
                            SolicitationSubsidyId = solicitationId,
                            AprApproveOfAuthorityThatOrderCommissionId = approve.Id
                        });
                    }
                }
            }
            

            _context.SaveChanges();
            return new ServiceResult<ApproveOfAuthority_SolicitationDto>();
        }

    }
}
