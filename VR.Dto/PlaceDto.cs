using System;
using System.Collections.Generic;
using System.Text;

namespace VR.Dto
{
    public class PlaceBaseDto
    {
        public Guid Id { set; get; }
        public string Description { set; get; }
    }

    public class AllPlaceDto : PlaceBaseDto
    {
        public Boolean Checked { set; get; }
        public int? Order { set; get; }
    }

}
