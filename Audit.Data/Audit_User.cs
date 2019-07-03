using System;
using System.Collections.Generic;
using System.Text;

namespace Audit.Data
{
    public class Audit_User : AuditBase
    {
        public string Dni { set; get; }
        public string UserName { set; get; }
        public string PhoneNumber { get; set; }
    }
}
