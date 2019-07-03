using System;
using System.Collections.Generic;
using System.Text;

namespace VR.Dto.User
{
    public class AspNetRolesDto
    {
        public Guid Id { set; get; }
        public string Name { set; get; }
        public string NormalizedName { set; get; }
        public string ConcurrencyStamp { set; get; }
    }

    public class AllRolesDto : AspNetRolesDto{}
}
