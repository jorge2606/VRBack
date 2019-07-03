using Service.Common.ServiceResult;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using VR.Web.EntityAudit;

namespace VR.Data.Model

{
    public class Notification : AuditedEntity<Guid>
    {
        public string TextData { set; get; }
        public string Tittle { set; get; }
        [ForeignKey("User")]
        public Guid UserId { set; get; }
        public Guid EntityId { set; get; }
        public int NotificationType { set; get; }
        public bool Read { set; get; }
        [ForeignKey("SolicitationSubsidy")]
        public Guid SolicitationSubsidyId { set; get; }

        public SolicitationSubsidy SolicitationSubsidy { set; get; }
        public User User { set; get; }
    }
    
}
