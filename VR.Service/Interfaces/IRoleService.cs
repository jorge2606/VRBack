using Service.Common.ServiceResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VR.Dto;

namespace VR.Service.Interfaces
{
    public interface IRoleService
    {

        ServiceResult<RoleDto> Create(RoleDto newRole);
        ServiceResult<RoleDto> GetById(Guid Id);
    }
}
