using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Service.Common.ServiceResult;
using VR.Dto;

namespace VR.Service.Interfaces
{
    public interface ISolicitationSubsidyService
    {
        ServiceResult<CreateSolicitationSubsidyDto> Create(CreateSolicitationSubsidyDto subsidy);
        ServiceResult<CreateSolicitationSubsidyDto> CreateAccountFor(CreateSolicitationSubsidyDto subsidy);
        ServiceResult<FindByIdSolicitationSubsidyDto> GetByIdSubsidy(Guid id);
        ServiceResult<DeleteSolicitationSubsidyDto> Delete(Guid id);
        ServiceResult<DeleteSolicitationSubsidyDto> FinalizeSubsidy(Guid id);
        ServiceResult<UpdateSolicitationSubsidyDto> Update(UpdateSolicitationSubsidyDto subsidy, Guid sessionUserId);
        Task<ServiceResult<string>> SendSolicitationAsync(SolicitationIdDto solicitationdDto);
        Task<ServiceResult<string>> SendAccuountForSolicitationToSupervisorAsync(SolicitationIdDto accountForSolicitation);
        Task<ServiceResult<SolicitationIdDto>> AceptedSolicitationAsync(SolicitationIdDto solicitationDto);
        ServiceResult<SolicitationIdDto> RefusedSolicitation(SolicitationIdDto solicitationDto);
        ServiceResult<Boolean> OverlapingDates(OverlapingDatesAndTransportsDto overlapingDates);
        ServiceResult<GetByIdSubsidyRpt> GetByIdSubsidyRpt(Guid solicitationId);
        ServiceResult<UrlSignHolograph> SolicitationApprovedBySupervisorId(Guid id, Guid currentUserId);
        ServiceResult<SolicitationSubsidyStateDto> GetSolicitationState(Guid solicitationId);
        Task<ServiceResult<SolicitationIdDto>> RefusedAccountForSolicitationAsync(SolicitationIdDto solicitationDto);
        Task<ServiceResult<SolicitationIdDto>> AceptedAccountForSolicitationAsync(SolicitationIdDto solicitationDto);
        ServiceResult<FindByIdOnlySolicitationSubsidyDto> GetByIdSolicitationSubsidySubsidy(Guid id);
        ServiceResult<FindByIdSolicitationSubsidyWhitStateDto> GetByIdSubsidyWhitState(Guid id);
        ServiceResult<SolicitationIdDto> AceptedMySolicitation(SolicitationIdDto solicitationDto);
        ServiceResult<SolicitationIdDto> AceptedMyAccountForSolicitation(SolicitationIdDto solicitationDto);
        ServiceResult<bool> ValidateBeforeSendAccountFor(Guid solcitationId);
        ServiceResult<bool> ValidateBeforeSendAccountForFinalizeNormally(Guid solicitationId);
        ServiceResult<FindRandomKeySolicitationSubsidyDto> GetByRandomKey(string randomKey, Guid currentUserId);
        ServiceResult<CreateSolicitationSubsidyDto> CreateComission(CreateSolicitationSubsidyDto subsidy);
        ServiceResult<UpdateComissionDto> UpdateComission(UpdateComissionDto subsidy);
        ServiceResult<bool> SomeSolicitationHasThisExpenditure(string key, Guid expenditureId);
        ServiceResult<PosponeSolicitationDto> PosponeSolicitation(PosponeSolicitationDto pospone);
    }
}
