using Service.Common.ServiceResult;
using System;
using System.Collections.Generic;
using System.Text;
using VR.Dto;

namespace StoresProcedures.IStoresProcedures
{
    public interface ISolicitationProcedure
    {
        ServiceResult<List<AllSolicitationSubsidyDto>> Get_agents_solicitation_by_supervisor(
            Guid SupervisorId, Guid AgentId, FilterSolicitationSubsidyDto filters);
    }
}
