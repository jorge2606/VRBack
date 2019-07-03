using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VR.Data.Model
{
    public class CodeLiquidation
    {
        public Guid Id { set; get; }
        public Double Percentage { set; get; }
        public string TextPercentage { set; get; }

        [ForeignKey("Place")]
        public Guid PlaceId { set; get; }

        public Place Place { set; get; }
    }
}
