using Service.Common.ServiceResult;
using System;
using System.Collections.Generic;
using System.Text;
using VR.Dto.User;

namespace VR.Service.Interfaces
{
    public interface IAspNetRolesService
    {
        ServiceResult<List<AllRolesDto>> GetAllRoles();
        ServiceResult<string> GetNameOfRoleById(Guid roleId);
    }
}
