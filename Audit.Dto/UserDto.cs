using Audit.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Audit.Dto
{
    public class UserAuditDto
    {
        public string Dni { set; get; }
        public string UserName { set; get; }
        public string PhoneNumber { set; get; }
    }

    public class AuditDto<T> : AuditBase
    {
        public T Current { get; set; }
        public T Previous { get; set; }
    }
}
