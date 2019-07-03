using Service.Common.ServiceResult;
using System;
using System.Collections.Generic;
using System.Text;
using VR.Dto;

namespace VR.Service.Interfaces
{
    public interface IApproveOfAuthority_SolicitationService
    {
        ServiceResult<ApproveOfAuthority_SolicitationDto> SaveChangesOfApprove(
            List<ApproveOfAuthorityThatOrderCommissionsDto> ApprovedList, Guid solicitationId);
    }
}
