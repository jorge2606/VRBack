using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VR.Web.Helpers
{
    public class MyService
    {
        public string getCurrentUserName()
        {
            var result = "";
            foreach (var claim in HttpContext.Current.User.Claims)
            {
                if (claim.Type.ToString().Equals("Name"))
                {
                    return claim.Value;
                }
            }

            return result;
        }

        public Guid getCurrentUserId()
        {
            Guid result = Guid.Empty;
            foreach (var claim in HttpContext.Current.User.Claims)
            {
                if (claim.Type.ToString().Equals("NameIdentifier"))
                {
                    return new Guid(claim.Value); 
                }
            }

            return result;
        }
    }
}
