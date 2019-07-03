using System;
using System.Collections.Generic;
using System.Text;

namespace VR.Data.Model
{
    public class ApproveOfAuthorityThatOrderCommission
    {
        public Guid Id { set; get; }
        public string Description { set; get; }
        public int Order { set; get; }
        public int OrderReport { set; get; }
        public bool Default { set; get; }
    }
}
