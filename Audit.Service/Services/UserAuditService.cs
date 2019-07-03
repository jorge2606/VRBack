using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Audit.Data;
using Audit.Dto;
using Audit.Service.Interfaces;
using AutoMapper;
using Service.Common.ServiceResult;

namespace Audit.Service.Services
{
    public class UserAuditService : IUserAuditService
    {
        private AuditContext _auditContext;
        private IMapper _mapper;

        public UserAuditService(AuditContext auditContext, IMapper mapper)
        {
            _auditContext = auditContext;
            _mapper = mapper;
        }

        public ServiceResult<List<AuditDto<UserAuditDto>>> GetUserAudit(Guid userId)
        {
            var user = _auditContext.Audit_Users
                .Where(x => x.EntityId == userId)
                .OrderByDescending(x => x.AuditDate)
                .ToList();

            if (user == null)
            {
               return new ServiceResult<List<AuditDto<UserAuditDto>>>(null);
            }

            UserAuditDto userDtoService = new UserAuditDto();
            List<AuditDto<UserAuditDto>> listRegisters = new List<AuditDto<UserAuditDto>>();
            
            foreach (var userIndex in user)
            {
                var p = user.FirstOrDefault(x => x.AuditDate < userIndex.AuditDate);
                var previous = _mapper.Map<UserAuditDto>(p);
                if (previous == null)
                {
                    previous = new UserAuditDto();
                }

               listRegisters.Add(
                    new AuditDto<UserAuditDto>()
                    {
                        AuditDate = userIndex.AuditDate,
                        AuditAction = userIndex.AuditAction,
                        Id = userIndex.Id,
                        AuditUser = userIndex.AuditUser,
                        Current = _mapper.Map<UserAuditDto>(userIndex),
                        Previous = previous
                    }
                );
            }
            
            return new ServiceResult<List<AuditDto<UserAuditDto>>>(listRegisters.OrderByDescending(x => x.AuditDate).ToList());


        }

        //public ServiceResult<UserAuditDto> userModified(Guid userId)
        //{
        //    var user = _auditContext.Audit_Users.ToList().OrderBy(x => x.AuditDate).Take(2).ToList();

        //    if (user == null)
        //    {
        //        return new ServiceResult<UserAuditDto>(null);
        //    }

        //    UserAuditDto userDtoService = new UserAuditDto();
        //    List<UserDtoBase> listRegisters = new List<UserDtoBase>(); 
        //    foreach (var userIndex in user)
        //    {
        //        listRegisters.Add(
        //            new UserDtoBase()
        //            {
        //                Dni = userIndex.Dni,
        //                UserName = userIndex.UserName
        //            }
        //        );
        //    }

        //   userDtoService.FieldModified = listRegisters;


        //    return new ServiceResult<UserAuditDto>(userDtoService);
        //}

    }
}
