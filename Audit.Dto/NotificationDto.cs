using System;
using System.Collections.Generic;
using System.Text;

namespace Audit.Dto
{
    public class NotificationsAuditDto
    {
        public string TextData { set; get; }
        public string Tittle { set; get; }
        public Guid UserId { set; get; }
        //public NotificationType NotificationType { set; get; }
        public bool Read { set; get; }
        public Guid CreatorUserId { set; get; }
    }
}
