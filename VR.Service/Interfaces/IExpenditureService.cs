using System;
using System.Collections.Generic;
using System.Text;
using Service.Common.ServiceResult;
using VR.Dto;

namespace VR.Service.Interfaces
{
    public interface IExpenditureService
    {
        ServiceResult<List<ExpenditureDto>> GetByIdSolicitationSubsidy(Guid id);
        ServiceResult<ExpenditureDto> Delete(Guid id);
    }
}
