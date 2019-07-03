using System;
using System.Collections.Generic;
using System.Text;

namespace VR.Dto
{
    public class RoleDto
    {

        public virtual Guid Id { get; set; }
        public virtual string Name { get; set; }
        public string NormalizedName { set; get; }
        public string ConcurrencyStamp { set; get; }
    }

    public class RoleWhenModifyUser : RoleDto
    {
        public virtual bool RolBelongUser { get; set; }
    }

    public class RoleUserDto
    {
        public Guid RolId { get; set; }
        public Guid UserId { get; set; }
    }
}
