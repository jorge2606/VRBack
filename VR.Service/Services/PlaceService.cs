using Service.Common.ServiceResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using VR.Data;
using VR.Dto;
using VR.Service.Interfaces;

namespace VR.Service.Services
{
    public class PlaceService : IPlaceService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public PlaceService(
            DataContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ServiceResult<List<AllPlaceDto>> GetAllPlace()
        {
            var list = _context.Places.ToList();
            var result = new List<AllPlaceDto>();
            foreach (var i in list)
            {
                result.Add(new AllPlaceDto()
                {
                    Id = i.Id,
                    Description = i.Description,
                    Checked = false,
                    Order = i.Order
                });
            }
            return new ServiceResult<List<AllPlaceDto>>(result);
        }
    }
}
