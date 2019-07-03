using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VR.Data.Model
{
    public class SupplementaryCity
    {
        public Guid Id { set; get; }

        [ForeignKey("City")]
        public Guid CityId { set; get; }

        [ForeignKey("Destiny")]
        public Guid DestinyId {set; get; }

        public List<City> Cities { set; get; }
        public Destiny Destiny { set; get; }

        public City City { set; get; }
    }
}
