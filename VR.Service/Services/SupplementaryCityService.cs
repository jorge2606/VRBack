using System;
using System.Collections.Generic;
using System.Text;
using Service.Common.ServiceResult;
using VR.Data;
using VR.Data.Model;
using VR.Dto;
using VR.Service.Interfaces;

namespace VR.Service.Services
{
    public class SupplementaryCityService : ISupplementaryCityService
    {
        private readonly DataContext _context;

        public SupplementaryCityService(DataContext context)
        {
            _context = context;
        }

        public ServiceResult<SupplementaryCityDto> Create(SupplementaryCityDto supplementary)
        {
            SupplementaryCity newSupplementaryCity = new SupplementaryCity()
            {
                Id = new Guid(),
                CityId = supplementary.CityId,
                DestinyId = supplementary.DestinyId
            };

            _context.SupplementaryCities.Add(newSupplementaryCity);
            _context.SaveChanges();

            return new ServiceResult<SupplementaryCityDto>(supplementary);
        }

    }
}
