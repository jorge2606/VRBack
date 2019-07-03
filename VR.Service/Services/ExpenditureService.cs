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
    public class ExpenditureService : IExpenditureService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public ExpenditureService(
            DataContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ServiceResult<List<ExpenditureDto>> GetByIdSolicitationSubsidy(Guid id)
        {
            return new ServiceResult<List<ExpenditureDto>>(
                _context.Expenditures.Select(x => _mapper.Map<ExpenditureDto>(x))
                    .Where(x => x.SolicitationSubsidyId == id)
                    .ToList()
                );
        }

        public ServiceResult<ExpenditureDto> Delete(Guid id)
        {
            var expDelete = _context.Expenditures.FirstOrDefault(x => x.Id == id);
            if (expDelete == null)
            {
                return new ServiceResult<ExpenditureDto>(null);
            }
            _context.Expenditures.Remove(expDelete);
            _context.SaveChanges();

            return new ServiceResult<ExpenditureDto>();
        }
    }
}
