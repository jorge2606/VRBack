using System;
using System.Collections.Generic;
using System.Text;
using Service.Common.ServiceResult;
using VR.Dto;

namespace VR.Service.Interfaces
{
    public interface IExpenditureTypeService
    {
        ServiceResult<CreateExpenditureTypeDto> CreateExpenditureType(CreateExpenditureTypeDto expenditureTypeDto);
        ServiceResult<UpdateExpenditureTypeDto> UpdateExpenditureType(UpdateExpenditureTypeDto expenditureTypeDto);
        ServiceResult<DeleteExpenditureTypeDto> DeleteExpenditureType(Guid expenditureTypeId);
        ServiceResult<FindByIdExpenditureTypeDto> FindByIdExpenditureType(Guid id);
        ServiceResult<List<AllExpenditureTypeDto>> AllExpenditureType();
    }
}
