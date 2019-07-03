using System;

namespace Audit.Data
{
    public class Audit_Notification : AuditBase
    {
        public string TextData { set; get; }
        public string Tittle { set; get; }
        public Guid UserId { set; get; }
        //public NotificationType NotificationType { set; get; }
        public bool Read { set; get; }
        public Guid CreatorUserId { set; get; }
    }
}   
