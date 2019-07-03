using System;
using System.Collections.Generic;
using System.Text;

namespace VR.Dto
{
    public class CountryBaseDto
    {
        public Guid Id { set; get; }
        public string Name { set; get; }
        public string PrintableName { set; get; }
        public string Iso3 { set; get; }
        public Int32 NumCode { set; get; }
        public Guid PlaceId { set; get; }
    }

    public class GetAllCountryDto : CountryBaseDto {}
}
