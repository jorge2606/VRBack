using System;
using System.Collections.Generic;
using System.Text;

namespace VR.Dto
{
    public class AspNetUserRolesDto
    {
        public Guid UserId { set; get; }
        public Guid RoleId { set; get; }
    }

    public class AllUserRolesDto : AspNetUserRolesDto { }
}
