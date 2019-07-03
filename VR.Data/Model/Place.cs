using System;
using System.Collections.Generic;
using System.Text;

namespace VR.Data.Model
{
    public class Place
    {
        public Guid Id { set; get; }
        public string Description { set; get; }
        public int? Order { set; get; }
    }
}
