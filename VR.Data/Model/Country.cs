using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VR.Data.Model
{
    public class Country
    {
        public Guid Id { set; get; }
        public string Name { set; get; }
        public string PrintableName { set; get; }
        public string Iso3 { set; get; }
        public Int32 NumCode { set; get; }


        [ForeignKey("Place")]
        public Guid? PlaceId { set; get; }

        public Place Place { set; get; }
    }
}
