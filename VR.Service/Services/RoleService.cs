using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Service.Common.ServiceResult;
using VR.Data;
using VR.Data.Model;
using VR.Dto;
using VR.Service.Interfaces;

namespace VR.Service.Services
{
    public class RoleService : IRoleService
    {
        private readonly DataContext _context;
        private readonly IMapper _iMapper;

        public RoleService(
            DataContext context,
            IMapper iMapper
            )
        {
            _context = context;
            _iMapper = iMapper;
        }

        public ServiceResult<RoleDto> Create(RoleDto newRole)
        {
            var exist = _context.Roles.FirstOrDefault(b => b.Id == newRole.Id);
            if (exist != null)
            {
                exist.Name = newRole.Name;
                exist.NormalizedName = newRole.Name.ToUpper();
                _context.Roles.Update(exist);
            }
            else
            {
                _context.Roles.Add(new Role()
                {
                    Id = new Guid(),
                    Name = newRole.Name,
                    NormalizedName = newRole.Name.ToUpper(),
                    ConcurrencyStamp = new Guid().ToString()
                });
            }

            _context.SaveChanges();

            return new ServiceResult<RoleDto>(newRole);
        }

        public ServiceResult<RoleDto> GetById(Guid Id)
        {
            var exist = _context.Roles.FirstOrDefault(b => b.Id == Id);
            if (exist == null)
            {
                var result = new ServiceResult<RoleDto>();
                result.AddError("Error","Este rol no existe");
                return result;
            }
            return new ServiceResult<RoleDto>(
                _context.Roles.Select(n => _iMapper.Map<RoleDto>(n)).FirstOrDefault(b => b.Id == Id));
        }
    }
}
