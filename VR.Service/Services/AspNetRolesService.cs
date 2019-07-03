using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using Service.Common.ServiceResult;
using VR.Data;
using VR.Dto.User;
using VR.Service.Interfaces;

namespace VR.Service.Services
{
    public class AspNetRolesService : IAspNetRolesService
    {
        private DataContext _context;
        private IMapper _mapper;

        public AspNetRolesService(
            DataContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ServiceResult<List<AllRolesDto>> GetAllRoles()
        {
            return new ServiceResult<List<AllRolesDto>>(
                _context.Roles.Select(x => _mapper.Map<AllRolesDto>(x)).ToList()
                );
        }

        public ServiceResult<string> GetNameOfRoleById(Guid roleId)
        {
            return new ServiceResult<string>(
                _context.Roles.FirstOrDefault(x => x.Id == roleId).NormalizedName
            );
        }
    }
}
