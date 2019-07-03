using System;
using System.Collections.Generic;
using System.Text;
using Service.Common.ServiceResult;
using VR.Dto;

namespace VR.Service.Interfaces
{
    public interface ICityService
    {
        /**ServiceResult<CreateCityDto> CreateTransport(CreateCityDto transportDto);
        ServiceResult<UpdateCityDto> UpdateCity(UpdateCityDto CityDto);
        ServiceResult<DeleteCityDto> DeleteCity(Guid idCity);**/
        ServiceResult<List<FindByIdCityDto>> FindByIdCity(Guid provinceId);

        ServiceResult<List<AllCityDto>> GetAllCity();
    }
}
