using System;

namespace Audit.Data
{

    public class AuditBase : IAudit
    {
        public Guid Id { get; set; }
        public string AuditAction { get; set; }
        public DateTime AuditDate { get; set; }
        public Guid AuditUserId { get; set; }
        public string AuditUser { get; set; }
        public Guid EntityId { get; set; }
    }
}   
