using System;
using System.Collections.Generic;
using System.Text;

namespace VR.Dto
{
    public class CodeLiquidationBaseDto
    {
        public Guid Id { set; get; }
        public Double Percentage { set; get; }
        public Guid PlaceId { set; get; }
    }
}
