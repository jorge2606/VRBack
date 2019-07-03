using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using Service.Common.ServiceResult;
using VR.Data;
using VR.Dto;
using VR.Service.Interfaces;

namespace VR.Service.Services
{
    public class ProvinceService : IProvinceService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ProvinceService(
            DataContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ServiceResult<List<AllProvinceDto>> GetAllProvince()
        {
            return new ServiceResult<List<AllProvinceDto>>(
               _context.Provinces.Select(x => _mapper.Map<AllProvinceDto>(x)).OrderBy(x=>x.Name).ToList() 
             );
        }

        public ServiceResult<List<FindByProvinceIdDto>> FindByPlaceId(Guid placeId)
        {
            return new ServiceResult<List<FindByProvinceIdDto>>(
                _context.Provinces.Select(x => _mapper.Map<FindByProvinceIdDto>(x))
                    .Where(x => x.PlaceId == placeId)
                    .OrderBy(x => x.Name).ToList()
            );
        }

        public ServiceResult<List<FindByProvinceIdDto>> FindByDistrictCity(string districtCity)
        {
            return new ServiceResult<List<FindByProvinceIdDto>>(
                _context.Provinces.Select(x => _mapper.Map<FindByProvinceIdDto>(x))
                    .Where(x => x.DistrictCity == districtCity)
                    .OrderBy(x => x.Name).ToList()
            );
        }

        public ServiceResult<List<FindByProvinceIdDto>> FindByCountryId(Guid countryId)
        {
            return new ServiceResult<List<FindByProvinceIdDto>>(
                _context.Provinces.Select(x => _mapper.Map<FindByProvinceIdDto>(x))
                    .Where(x => x.CountryId == countryId)
                    .OrderBy(x => x.Name).ToList()
            );
        }
    }
}
