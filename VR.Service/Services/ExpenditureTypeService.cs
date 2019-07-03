using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using FluentValidation;
using Service.Common.Extensions;
using Service.Common.ServiceResult;
using VR.Data;
using VR.Data.Model;
using VR.Dto;
using VR.Service.Interfaces;

namespace VR.Service.Services
{
    public class ExpenditureTypeService : IExpenditureTypeService
    {
        private readonly DataContext _dataContext;
        private readonly IValidator<ExpenditureTypeBaseDto> _fluentValidator;
        private IMapper _mapper;

        public ExpenditureTypeService(DataContext dataContext, IValidator<ExpenditureTypeBaseDto> fluentValidator,
            IMapper mapper)
        {
            _dataContext = dataContext;
            _fluentValidator = fluentValidator;
            _mapper = mapper;
        }

        public ServiceResult<CreateExpenditureTypeDto> CreateExpenditureType(CreateExpenditureTypeDto expenditure)
        {
            var validation = _fluentValidator.Validate(expenditure);

            if (!validation.IsValid)
            {
                return _mapper.Map<ServiceResult<CreateExpenditureTypeDto>>(validation.ToServiceResult<CreateExpenditureTypeDto>(null));
            }

            ExpenditureType newExpenditure = new ExpenditureType()
            {
                Id = new Guid(),
                Name = expenditure.Name,
                CanRepeat = expenditure.CanRepeat,
                IsImportant = expenditure.IsImportant,
                OutsideProvince = expenditure.OutsideProvince,
                Percentage = expenditure.Percentage
            };

            _dataContext.ExpenditureTypes.Add(newExpenditure);
            _dataContext.SaveChanges();

            return new ServiceResult<CreateExpenditureTypeDto>(expenditure);

        }

        public ServiceResult<UpdateExpenditureTypeDto> UpdateExpenditureType(UpdateExpenditureTypeDto expenditure)
        {
            var validate = _fluentValidator.Validate(expenditure);

            var updateExpenditure = _dataContext.ExpenditureTypes.FirstOrDefault(x => x.Id == expenditure.Id);

            if (!validate.IsValid || updateExpenditure == null || updateExpenditure.IsDeleted == true)
            {
                return _mapper.Map<ServiceResult<UpdateExpenditureTypeDto>>(
                    validate.ToServiceResult<UpdateExpenditureTypeDto>(null));
            }

            updateExpenditure.Name = expenditure.Name;
            updateExpenditure.CanRepeat = expenditure.CanRepeat;
            updateExpenditure.IsImportant = expenditure.IsImportant;
            updateExpenditure.OutsideProvince = expenditure.OutsideProvince;
            updateExpenditure.Percentage = expenditure.Percentage;

            _dataContext.ExpenditureTypes.Update(updateExpenditure);
            _dataContext.SaveChanges();

            return new ServiceResult<UpdateExpenditureTypeDto>(expenditure);

        }

        public ServiceResult<FindByIdExpenditureTypeDto> FindByIdExpenditureType(Guid id)
        {
            var expenditure = _dataContext.ExpenditureTypes
                .Where(x => x.IsDeleted != true)
                .FirstOrDefault(x => x.Id == id);

            if (expenditure == null)
            {
                return new ServiceResult<FindByIdExpenditureTypeDto>(null);
            }

            return new ServiceResult<FindByIdExpenditureTypeDto>( _mapper.Map<FindByIdExpenditureTypeDto>(expenditure) );
        }

        public ServiceResult<DeleteExpenditureTypeDto> DeleteExpenditureType(Guid expenditureId)
        {
            var delete = _dataContext.ExpenditureTypes.FirstOrDefault(x => x.Id == expenditureId);

            if (delete == null)
            {
                return new ServiceResult<DeleteExpenditureTypeDto>(null);
            }

            delete.IsDeleted = true;
            _dataContext.ExpenditureTypes.Update(delete);
            _dataContext.SaveChanges();

            return new ServiceResult<DeleteExpenditureTypeDto>( _mapper.Map<DeleteExpenditureTypeDto>(delete));
        }

        public ServiceResult<List<AllExpenditureTypeDto>> AllExpenditureType()
        {
            return new ServiceResult<List<AllExpenditureTypeDto>>(
             _dataContext.ExpenditureTypes.Select(x => _mapper.Map<AllExpenditureTypeDto>(x))
                 .Where(x => x.IsDeleted != true)
                 .ToList()   
             );
        }

    }
}
