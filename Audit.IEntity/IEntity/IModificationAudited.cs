using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VR.Web.IEntity
{
    public interface IModificationAudited
    {
        //
        // Summary:
        //     Last modifier user for this entity.
        Guid LastModifierUserId { get; set; }
    }
}
