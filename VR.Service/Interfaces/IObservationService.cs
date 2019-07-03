using Service.Common.ServiceResult;
using System;
using System.Collections.Generic;
using System.Text;
using VR.Dto;

namespace VR.Service.Interfaces
{
    public interface IObservationService
    {
        ServiceResult<List<ObservationDto>> GetById(Guid solicitationId);
        ServiceResult<PosponeSolicitationDto> Create(PosponeSolicitationDto pospone);
    }
}
