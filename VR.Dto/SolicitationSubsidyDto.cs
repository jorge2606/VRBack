using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using VR.Data.Model;
using VR.Data.Model.ModelStoreProcedure;
using VR.Dto.User;

namespace VR.Dto
{

    public class SolicitationSubsidyValidator : AbstractValidator<SolicitationSubsidyBaseDto>
    {
        public SolicitationSubsidyValidator()
        {
            RuleFor(x => x.Motive).NotEmpty().WithName("Motivo");
            RuleFor(x => x.Total).NotNull().WithName("Total");
        }
    }

    public class SolicitationSubsidyBaseDto
    {
        public Guid Id { set; get; }
        public string Motive { set; get; }
        public decimal Total { set; get; }
        public List<ExpenditureDto> Expenditures { set; get; }
        public List<DestinyBaseDto> Destinies { set; get; }
        public Decimal AdvanceCategory { set; get; }
        public Decimal PercentageCodeLiquidation { set; get; }

        public DateTime CreateDate { set; get; }
        public Guid UserId { set; get; }
        public UserDto User { set; get; }
        public int DaysPay { set; get; }
    }


    public class SolicitationObjectsSubsidyBaseDto : SolicitationSubsidyBaseDto
    {
        public string UserUserName { set; get; }
    }

    public class CreateSolicitationSubsidyDto : SolicitationSubsidyBaseDto
    {
        public Boolean IsRefund { set; get; }
        public DateDto FinalizeDate { set; get; }
        public int DaysWeekEnd { set; get; }
        public Boolean IsCommission { set; get; }
        public string RandomKey { set; get; }
        public string FileNumber { set; get; }
        public List<ApproveOfAuthorityThatOrderCommissionsDto> AllApproved { set; get; }
        public List<ObservationDto> Observations { set; get; }
    }

    public class UpdateComissionDto : SolicitationSubsidyBaseDto
    {
        public Boolean IsRefund { set; get; }
        //public DateDto FinalizeDate { set; get; }
        public int DaysWeekEnd { set; get; }
        public Boolean IsCommission { set; get; }
        public string RandomKey { set; get; }
    }

    public class UpdateSolicitationSubsidyDto : SolicitationSubsidyBaseDto
    {
        public int DaysWeekEnd { set; get; }
    }

    public class DeleteSolicitationSubsidyDto
    {
        public Guid Id { set; get; }
        public string Motive { set; get; }
        public decimal Total { set; get; }
        public Guid UserId { set; get; }

    }

    //accountFor
    public class FindByIdSolicitationSubsidyDto
    {
        public FindByIdSolicitationSubsidyDto()
        {
            FileNumber = "";
            LegalRulingDescription = "";
        }
        public Guid Id { set; get; }
        public string Motive { set; get; }
        public decimal Total { set; get; }
        public List<ExpenditureFromSolicitationSubsidyByIdDto> Expenditures { set; get; }
        public List<DestinyFromSolicitationSubsidyFindByIdDto> Destinies { set; get; }
        public Guid UserId { set; get; }
        public UserDto User { set; get; }
        public Boolean IsRefund { set; get; }
        public DateTime FinalizeDate { set; get; }
        public Boolean? IsCommission { set; get; }
        public string RandomKey { set; get; }
        public Boolean IsDeleted { set; get; }
        public LegalRulingsBaseDto LegalRuling { set; get; }
        public string LegalRulingDescription {set; get; }
        public List<ObservationDto> Observations { set; get; }
        public List<SolicitationStateDto> SolicitationStates { get; set; }
        public SolicitationStateDto LatestSolicitationStates { get; set; }
        public DateTime BeginDate { get; set; }
        public string FileNumber { set; get; }
    }

    public class FindByIdOnlySolicitationSubsidyDto
    {
        public Guid Id { set; get; }
        public string Motive { set; get; }
        public decimal Total { set; get; }
        public List<ExpenditureFromOnlySolicitationSubsidyByIdDto> Expenditures { set; get; }
        public List<DestinyFromOnlySolicitationSubsidyFindByIdDto> Destinies { set; get; }
        public Guid UserId { set; get; }
        public UserDto User { set; get; }
        public Boolean IsRefund { set; get; }
        public DateTime FinalizeDate { set; get; }
        public Boolean? IsCommission { set; get; }
        public string RandomKey { set; get; }
    }

    public class FindRandomKeySolicitationSubsidyDto
    {
        public Guid Id { set; get; }
        public string Motive { set; get; }
        public decimal Total { set; get; }
        public List<ExpenditureFromSolicitationSubsidyByIdDto> Expenditures { set; get; }
        public List<DestinyFromOnlySolicitationSubsidyFindByIdDto> Destinies { set; get; }
        public Guid UserId { set; get; }
        public UserDto User { set; get; }
        public Boolean IsRefund { set; get; }
        public Boolean? IsCommission { set; get; }
        public string RandomKey { set; get; }
    }

    public class FindByIdSolicitationSubsidyWhitStateDto
    {
        public string State { set; get; }
        public DateTime? FinalizeDate { set; get; }
    }

    public class FindByIdRefundDto
    {
        public Guid Id { set; get; }
        public string Motive { set; get; }
        public decimal Total { set; get; }
        public List<ExpenditureFromSolicitationSubsidyByIdDto> Expenditures { set; get; }
        public List<DestinyFromSolicitationSubsidyFindByIdDto> Destinies { set; get; }
        public Guid UserId { set; get; }
        public UserDto User { set; get; }
    }

    public class AllSolicitationSubsidyDto
    {
        public Guid Id { set; get; }
        public string Motive { set; get; }
        public decimal Total { set; get; }
        public List<ExpenditureFromSolicitationSubsidyByIdDto> Expenditures { set; get; }
        public List<DestinyFromSolicitationSubsidyByIdDto> Destinies { set; get; }
        public Guid UserId { set; get; }
        public UserDto User { set; get; }
        public DateTime CreateDate { set; get; }
        public string State { set; get; }
        public Guid StateId { set; get; }
        public string MotiveReject { set; get; }
        public string FileNumber { set; get; }
        public string FullName { set; get; }
        public string Localities { set; get; }
        public Boolean IsDeleted {set; get; }
        public Boolean IsRefund { set; get; }
        public DateTime? FinalizeDate { set; get; }
        public DateTime? BeginDate { set; get; }
        public DateTime? EndDate { set; get; }
        public Guid AprovedByFirstInstance { set; get; }
    }

    public class SolicitationIdDto
    {
        public Guid Id { set; get; }
        public string MotiveReject { set; get; }
        public string FileNumber { set; get; }
        public Guid SupervisorId { set; get; }
        public UserDto Supervisor { set; get; }
        public List<DestinationDateSolicitationPosponed> Destinies { set; get; }
    }

    public class FilterSolicitationSubsidyDto
    {
        public int Page { set; get; }
        public string FirstName { set; get; }
        public string LastName { set; get; }
        public string Dni { set; get; }
        public Boolean IsRefund { set; get; }
    }

    public class OverlapingDatesDto
    {
        public Guid Id { set; get; }
        public DateDto StartDate { set; get; }
        public DateDto EndDate { set; get; }
        public int Days { set; get; }
    }

    public class OverlapingDatesAndTransportsDto : OverlapingDatesDto
    {

        public DateTime StartDateDatetime { set; get; }
        public DateTime EndDateDatetime { set; get; }
        public Guid UserId { set; get; }
    }

    public class GetByIdSubsidyRpt
    {
        public List<Get_all_solicitationResult> SolicitationResult { set; get; }
        public List<Get_expendituresResult> ExpendituresResults { set; get; }
        
    }

    public class SolicitationSubsidyForTemplateDto
    {
        public SolicitationSubsidyForTemplateDto()
        {
            Destinies = new List<DestinyBaseDto>();
            Expenditures = new List<ExpenditureFromSolicitationSubsidyByIdDto>();
            SolicitationStates = new List<SolicitationStateDto>();
        }

        public Guid Id { set; get; }
        public string Motive { set; get; }
        public decimal Total { set; get; }
        public DateTime CreateDate { set; get; }
        public DateTime? FinalizeDate { set; get; }
        public Boolean IsRefund { set; get; }
        public Boolean IsDeleted { set; get; }
        
        public Guid UserId { set; get; }

        public UserDto User { set; get; }

        public List<DestinyBaseDto> Destinies { get; set; }

        public List<ExpenditureFromSolicitationSubsidyByIdDto> Expenditures { get; set; }

        public List<SolicitationStateDto> SolicitationStates { get; set; }
        public string Url { set; get; }
        public string SupervisorsFirstName { set; get; }
        public string SupervisorsLastName { set; get; }
    }

    public class PosponeSolicitationDto
    {
        public Guid Id { set; get; }
        public List<ObservationDto> Observations { set; get; }
    }
}
