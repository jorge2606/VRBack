using System;
using System.Collections.Generic;
using System.Text;
using Service.Common.ServiceResult;
using VR.Dto;

namespace VR.Service.Interfaces
{
    public interface IProvinceService
    {
        ServiceResult<List<FindByProvinceIdDto>> FindByDistrictCity(string districtCity);
        ServiceResult<List<FindByProvinceIdDto>> FindByPlaceId(Guid placeId);
        ServiceResult<List<AllProvinceDto>> GetAllProvince();
        ServiceResult<List<FindByProvinceIdDto>> FindByCountryId(Guid countryId);
    }
}
