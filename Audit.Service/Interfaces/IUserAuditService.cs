using Audit.Dto;
using Service.Common.ServiceResult;
using System;
using System.Collections.Generic;

namespace Audit.Service.Interfaces
{
    public interface IUserAuditService
    {
        ServiceResult<List<AuditDto<UserAuditDto>>> GetUserAudit(Guid userId);
    }
}
