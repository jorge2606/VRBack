using Service.Common.ServiceResult;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using VR.Dto;

namespace VR.Service.Interfaces
{
    public interface IAspNetUserRolesService
    {
        ServiceResult<List<AllUserRolesDto>> GetAllUserRoles();
        Task<ServiceResult<IList<ClaimDto>>> FindByIdRoles(Guid userId);
        ServiceResult<List<RolNameDto>> RolesNames(Guid userId);
        Task<ServiceResult<IEnumerable<Claim>>> FindClaimsByIdRoles(Guid userId);
    }
}
