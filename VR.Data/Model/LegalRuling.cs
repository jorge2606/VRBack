using System;
using System.Collections.Generic;
using System.Text;

namespace VR.Data.Model
{
    public class LegalRuling
    {
        public LegalRuling()
        {
            IsCreated = DateTime.Now;
        }
        public Guid Id { set; get; }
        public int Number { set; get; }
        public DateTime Date { set; get; }
        public string Description { set; get; }
        public bool IsDeleted { set; get; }
        public DateTime IsCreated { set; get; }
    }
}
