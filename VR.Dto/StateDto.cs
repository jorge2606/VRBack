using System;
using System.Collections.Generic;
using System.Text;

namespace VR.Dto
{
    public class StateDto
    {
        public Guid Id { set; get; }
        public string Description { set; get; }
    }

    public class StateDescriptionDto
    {
        public string Description { set; get; }
    }
}
