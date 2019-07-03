using Service.Common.ServiceResult;
using System;
using System.Collections.Generic;
using System.Text;
using VR.Dto;
using VR.Web.Helpers;

namespace VR.Service.Interfaces
{
    public interface ISupervisorUserAgentService
    {
        ServiceResult<List<CreateSupervisorAgentDto>> Create(List<CreateSupervisorAgentDto> createSupervisor);
        ServiceResult<List<AllSupervisorAgentDto>> AllSupervisorAgent();
        ServiceResult<Boolean> IsAgent(Guid myUserId, Guid otherUserId);
        ServiceResult<Guid> DeleteRelationshipBetweenAgentAndSupervisor(Guid supervisorId, Guid AgentId);
        ServiceResult<List<SupervisorsDto>> AllSupervisors();
        ServiceResult<PagedResult<AllSupervisorAgentDto>> GetPageUserAgent(UserAgentFilterDto filters);
    }
}
