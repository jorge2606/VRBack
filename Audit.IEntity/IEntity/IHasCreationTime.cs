using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VR.Web.IEntity
{
    //
    // Summary:
    //     An entity can implement this interface if Abp.Domain.Entities.Auditing.IHasCreationTime.CreationTime
    //     of this entity must be stored. Abp.Domain.Entities.Auditing.IHasCreationTime.CreationTime
    //     is automatically set when saving Abp.Domain.Entities.Entity to database.
    public interface IHasCreationTime
    {
        //
        // Summary:
        //     Creation time of this entity.
        DateTime CreationTime { get; set; }
    }
}
