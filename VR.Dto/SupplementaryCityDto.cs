using System;
using System.Collections.Generic;
using System.Text;

namespace VR.Dto
{
    public class SupplementaryCityDto
    {
        public Guid Id { set; get; }
        public Guid CityId { set; get; }
        public Guid DestinyId { set; get; }
    }
    

    public class SupplementaryCityOnlyId : SupplementaryCityDto
    {
        public string Name { set; get; }
    }
}
