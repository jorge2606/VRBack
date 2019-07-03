using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using VR.Data.Interfaces;

namespace VR.Data.Model
{
    public class Holiday : IsDeletedInterface
    {
        public Guid Id { set; get; }
        public string Description { set; get; }
        //[Column(TypeName = "Date")]
        public DateTime Date { set; get; } 
        public Boolean IsDeleted { set; get; }
    }
}
