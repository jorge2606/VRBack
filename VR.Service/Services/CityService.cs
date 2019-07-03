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
    public class CityService : ICityService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public CityService(
            DataContext context,
            IMapper mapper
        )
        {
            _context = context;
            _mapper = mapper;
        }

        
        public ServiceResult<List<AllCityDto>> GetAllCity()
        {
            return new ServiceResult<List<AllCityDto>>(
                _context.Cities.Select(x => _mapper.Map<AllCityDto>(x)).OrderBy(x => x.Name).ToList()
            );
        }

        public ServiceResult<List<FindByIdCityDto>> FindByIdCity(Guid provinceId)
        {
            return new ServiceResult<List<FindByIdCityDto>>(
                _context.Cities.Select(x => _mapper.Map<FindByIdCityDto>(x))
                    .Where(x=> x.ProvinceId == provinceId)
                    .OrderBy(x => x.Name).ToList()
            );
        }




    }
}
