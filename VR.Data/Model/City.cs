using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VR.Data.Model
{
    public class City
    {
        public Guid Id { set; get; }
        public string Name { set; get; }
        public DateTime CreatedDate { set; get; }
        public Boolean IsDeleted { set; get; }

        [ForeignKey("Province")]
        public Guid ProvinceId { set; get; }
        public Province Province { set; get; }

    }
}
