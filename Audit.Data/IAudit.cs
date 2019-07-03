using System;

namespace Audit.Data
{
    public interface IAudit
    {
       Guid Id { get; set; }
       string AuditAction { get; set; }
       DateTime AuditDate { get; set; }
       string AuditUser { get; set; }
       Guid AuditUserId { get; set; }
       Guid EntityId { get; set; }
    }
}   
