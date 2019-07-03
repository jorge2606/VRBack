using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VR.Web.IEntity
{
    public interface IEntity<TPrimaryKey>
    {
        //
        // Summary:
        //     Unique identifier for this entity.
        TPrimaryKey Id { get; set; }
    }
}
