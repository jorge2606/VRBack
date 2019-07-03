using System;
using System.Collections;
using System.Collections.Generic;
using VR.Data.Interfaces;

namespace VR.Data.Model
{
    public class Organism : IsDeletedInterface
    {
        public Guid Id { set; get; }
        public string Name { set; get; }
        public string Description { set; get; }
        public Boolean IsDeleted { set; get; }

        public ICollection<Distribution> Distributions { set; get; }
    }
}
