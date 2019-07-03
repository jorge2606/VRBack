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
    public class CodeLiquidationService : ICodeLiquidationService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public CodeLiquidationService(
            DataContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ServiceResult<List<CodeLiquidationBaseDto>> GetAll()
        {
            return new ServiceResult<List<CodeLiquidationBaseDto>>(
                _context.CodeLiquidations.Select(x => _mapper.Map<CodeLiquidationBaseDto>(x))
                    .OrderBy(x => x.Percentage)
                    .ToList()
            );
        }
    }
}
