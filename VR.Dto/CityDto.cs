using System;
using System.Collections.Generic;
using System.Text;

namespace VR.Dto
{
    public class CityBaseDto
    {
        public Guid Id { set; get; }
        public string Name { set; get; }
        public DateTime CreatedDate { set; get; }
        public Boolean IsDeleted { set; get; }
        public Guid ProvinceId { set; get; }
    }


    public class CreateCityDto : CityBaseDto { }

    public class UpdateCityDto : CityBaseDto { }

    public class DeleteCityDto : CityBaseDto { }

    public class FindByIdCityDto : CityBaseDto { }

    public class AllCityDto : CityBaseDto { }
}
