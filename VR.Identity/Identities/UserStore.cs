using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using VR.Data;
using VR.Data.Model;

namespace VR.Identity.Identities
{
    public class UserStore : UserStore<User, Role, DataContext, Guid>
    {
        public UserStore(DataContext context) : base(context)
        {
        }
    }

}
