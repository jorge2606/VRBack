using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VR.Web.IEntity
{
    //
    // Summary:
    //     This interface is implemented by entities which must be audited. Related properties
    //     automatically set when saving/updating Abp.Domain.Entities.Entity objects.
    public interface IAudited : ICreationAudited, IHasCreationTime, IModificationAudited, IHasModificationTime
    {
    }
}
