using Service.Common.ServiceResult;
using System;
using System.Collections.Generic;
using System.Text;
using VR.Dto;

namespace VR.Service.Interfaces
{
    public interface ISolicitationStateService
    {
        ServiceResult<AddFielNumberDto> AddFielNumber(AddFielNumberDto fields);
        ServiceResult<bool> ItHasNumberFile(Guid solId);
    }
}
