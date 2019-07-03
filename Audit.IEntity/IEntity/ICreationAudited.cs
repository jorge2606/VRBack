using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VR.Web.IEntity
{
    //
    // Summary:
    //     This interface is implemented by entities that is wanted to store creation information
    //     (who and when created). Creation time and creator user are automatically set
    //     when saving Abp.Domain.Entities.Entity to database.
    public interface ICreationAudited : IHasCreationTime
    {
        //
        // Summary:
        //     Id of the creator user of this entity.
        Guid CreatorUserId { get; set; }
    }
}
