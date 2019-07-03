using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VR.Data.Model
{
    public class Province
    {
        public Guid Id { set; get; }
        public string Name { set; get; }
        public DateTime CreatedDate { set; get; }
        public Boolean IsDeleted { set; get; }
        public string DistrictCity { set; get; }
        public long Poblation { set; get; }
        public string PrintableName { set; get; }

        [ForeignKey("Place")]
        public Guid? PlaceId { set; get; }
        [ForeignKey("Country")]
        public Guid? CountryId { set; get; }

        public Place Place { set; get; }
        public Country Country { set; get; }
    }
}
