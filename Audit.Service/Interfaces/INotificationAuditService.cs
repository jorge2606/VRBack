using System;
using System.Collections.Generic;
using Audit.Dto;
using Service.Common.ServiceResult;

namespace Audit.Service.Interfaces
{
    public interface INotificationAuditService
    {
        ServiceResult<List<AuditDto<NotificationsAuditDto>>> GetNotificationAudit(Guid userId);
    }
}
