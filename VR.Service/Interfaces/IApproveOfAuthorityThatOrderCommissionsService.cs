using Service.Common.ServiceResult;
using System;
using System.Collections.Generic;
using System.Text;
using VR.Dto;

namespace VR.Service.Interfaces
{
    public interface IApproveOfAuthorityThatOrderCommissionsService
    {
        ServiceResult<List<ApproveOfAuthorityThatOrderCommissionsDto>> GetApproved(Guid Id);
    }
}
