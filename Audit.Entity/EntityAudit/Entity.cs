using VR.Web.IEntity;

namespace VR.Web.EntityAudit
{
    //
    // Summary:
    //     Basic implementation of IEntity interface. An entity can inherit this class of
    //     directly implement to IEntity interface.
    //
    // Type parameters:
    //   TPrimaryKey:
    //     Type of the primary key of the entity
    public abstract class Entity<TPrimaryKey> : IEntity<TPrimaryKey>
    {
        //
        // Summary:
        //     Unique identifier for this entity.
        public virtual TPrimaryKey Id { get; set; }
    }
}
