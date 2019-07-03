using System.Collections.Generic;
using VR.Data.Model;
namespace VR.Identity.Identities
{
    public class RoleManager : Microsoft.AspNetCore.Identity.RoleManager<Role>
    {
        public RoleManager(Microsoft.AspNetCore.Identity.IRoleStore<Role> store, IEnumerable<Microsoft.AspNetCore.Identity.IRoleValidator<Role>> roleValidators, Microsoft.AspNetCore.Identity.ILookupNormalizer keyNormalizer, Microsoft.AspNetCore.Identity.IdentityErrorDescriber errors, Microsoft.Extensions.Logging.ILogger<Microsoft.AspNetCore.Identity.RoleManager<Role>> logger) : base(store, roleValidators, keyNormalizer, errors, logger)
        {
        }
    }
}
