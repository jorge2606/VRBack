using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using Service.Common.ServiceResult;
using VR.Data;
using VR.Data.Model;
using VR.Dto;
using VR.Service.Interfaces;

namespace VR.Service.Services
{
    public class LegalRulingsService : ILegalRulings
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public LegalRulingsService(
            DataContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ServiceResult<LegalRulingsBaseDto> Create(LegalRulingsBaseDto legalRuling)
        {
            var find = _context.LegalRulings.FirstOrDefault(x => x.Id == legalRuling.Id);
            if (find == null)
            {
                _context.LegalRulings.Add(new LegalRuling()
                {
                    Id = new Guid(),
                    Description = legalRuling.Description,
                    Date = legalRuling.Date.ToDateTime(),
                    Number = legalRuling.Number,
                    IsDeleted = legalRuling.IsDeleted
                });
            }
            else
            {
                find.Description = legalRuling.Description;
                find.Date = legalRuling.Date.ToDateTime();
                find.Number = legalRuling.Number;
                _context.LegalRulings.Update(find);
            }


            _context.SaveChanges();

            return new ServiceResult<LegalRulingsBaseDto>(legalRuling);
        }

        public ServiceResult<LegalRulingsBaseDto> Update(LegalRulingsBaseDto legalRuling)
        {
            _context.LegalRulings.Update(new LegalRuling()
            {
                Number = legalRuling.Number,
                Description = legalRuling.Description,
                Date = legalRuling.Date.ToDateTime()
            });

            _context.SaveChanges();

            return new ServiceResult<LegalRulingsBaseDto>(legalRuling);
        }

        public ServiceResult<DeleteLegalDto> Delete(Guid Id)
        {
            var legalRuling = _context.LegalRulings.FirstOrDefault(x => x.Id == Id);
            legalRuling.IsDeleted = true;
            _context.LegalRulings.Update(legalRuling);
            _context.SaveChanges();

            return new ServiceResult<DeleteLegalDto>(new DeleteLegalDto()
            {
                Id = legalRuling.Id
            });
        }

        public ServiceResult<IQueryable<LegalRulingsBaseDto>> GetAll()
        {
            return new ServiceResult<IQueryable<LegalRulingsBaseDto>>(_context.LegalRulings
                .Select(x => _mapper.Map<LegalRulingsBaseDto>(x)));
        }

        public ServiceResult<LegalRulingsBaseDto> FindById(Guid id)
        {
            return new ServiceResult<LegalRulingsBaseDto>(_mapper.Map<LegalRulingsBaseDto>(_context.LegalRulings.FirstOrDefault(x => x.Id == id)));
        }
        
    }
}
