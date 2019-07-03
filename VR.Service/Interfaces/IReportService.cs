using AspNetCore.Report;
using Service.Common.ServiceResult;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VR.Data.Model.ModelStoreProcedure;
using VR.Dto;

namespace VR.Service.Interfaces
{
    public interface IReportService
    {
        ServiceResult<byte[]> ReportPrintAsync(Guid solicitationId);
        ServiceResult<byte[]> PrintAccountFor(Guid solicitationId);
        ServiceResult<byte[]> PrintReportSolicitationSubsidyByUser(Guid userId);
        ServiceResult<byte[]> PrintReportSolicitationSubsidyByOrganism(Guid organismId);
        ServiceResult<byte[]> PrintReport_SolicitationByDestiniesAndDates(ReportByDestiniesAndDatesDto reportByDestiniesAndDates);
        ServiceResult<byte[]> SolicitationsPendingProcedure();
        ServiceResult<byte[]> SolicitationsExpireProcedure();
        ServiceResult<byte[]> ExpenditureProcedure();
        ServiceResult<byte[]> PrintReportCommission(Guid solicitationId);
        ServiceResult<ReportResponse> ExpendituresReport();
    }
}
