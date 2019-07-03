using System;
using System.Collections.Generic;
using System.Text;

namespace VR.Dto
{
    public class MotiveBaseDto
    {
        public Guid Id { set; get; }
        public string Description { set; get; }
    }

    public class AllMotiveDto : MotiveBaseDto{ }
}
