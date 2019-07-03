using System;
using System.Collections.Generic;
using System.Text;

namespace VR.Dto
{
    public class ApproveOfAuthorityThatOrderCommissionsDto
    {
        public Guid Id { set; get; }
        public string Description { set; get; }
        public bool Checked { set; get; }
        public int Order { set; get; }
        public bool Default { set; get; }
        public int OrderReport { set; get; }

    }
}
