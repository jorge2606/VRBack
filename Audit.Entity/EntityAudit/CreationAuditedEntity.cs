using System;
using VR.Web.IEntity;
using VR.Web.EntityAudit;


namespace VR.Web.EntityAudit
{
    //
    // Summary:
    //     This class can be used to simplify implementing Abp.Domain.Entities.Auditing.ICreationAudited.
    //
    // Type parameters:
    //   TPrimaryKey:
    //     Type of the primary key of the entity
    public abstract class CreationAuditedEntity<TPrimaryKey> : Entity<TPrimaryKey>, ICreationAudited, IHasCreationTime
    {
        //
        // Summary:
        //     Creation time of this entity.
        public virtual DateTime CreationTime { get; set; }
        //
        // Summary:
        //     Creator of this entity.
        public virtual Guid CreatorUserId { get; set; }
    }
}
