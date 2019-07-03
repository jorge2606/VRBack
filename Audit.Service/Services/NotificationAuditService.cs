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
    public class NotificationAuditService : INotificationAuditService
    {
        private AuditContext _auditContext;
        private IMapper _mapper;

        public NotificationAuditService(
            AuditContext auditContext,
            IMapper mapper
            )
        {
            _auditContext = auditContext;
            _mapper = mapper;
        }

        public ServiceResult<List<AuditDto<NotificationsAuditDto>>> GetNotificationAudit(Guid userId)
        {
            var notificationList = _auditContext.Audit_Notifications
                .Where(x => x.CreatorUserId == userId)
                .ToList();
            if (notificationList == null)
            {
                return new ServiceResult<List<AuditDto<NotificationsAuditDto>>>(null);
            }

            NotificationsAuditDto userDtoService = new NotificationsAuditDto();
            List<AuditDto<NotificationsAuditDto>> listRegisters = new List<AuditDto<NotificationsAuditDto>>();

            foreach (var notifIndex in notificationList)
            {
                var p = notificationList.FirstOrDefault(x => x.AuditDate < notifIndex.AuditDate);
                var previous = _mapper.Map<NotificationsAuditDto>(p);
                if (previous == null)
                {
                    previous = new NotificationsAuditDto();
                }

                listRegisters.Add(
                    new AuditDto<NotificationsAuditDto>()
                    {
                        AuditDate = notifIndex.AuditDate,
                        AuditAction = notifIndex.AuditAction,
                        Id = notifIndex.Id,
                        AuditUser = notifIndex.AuditUser,
                        Current = _mapper.Map<NotificationsAuditDto>(notifIndex),
                        Previous = previous
                    }
                );
            }
            return new ServiceResult<List<AuditDto<NotificationsAuditDto>>>(listRegisters.OrderByDescending(x => x.AuditDate).ToList());
        }

    }
}
