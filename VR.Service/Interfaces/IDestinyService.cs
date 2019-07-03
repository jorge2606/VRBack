using System;
using System.Collections.Generic;
using System.Text;
using Service.Common.ServiceResult;
using VR.Data.Model.ModelStoreProcedure;
using VR.Dto;

namespace VR.Service.Interfaces
{
    public interface IDestinyService
    {
        ServiceResult<DestinyBaseDto> Delete(Guid id);
        ServiceResult<List<Get_DestiniesResult>> Get_DestiniesProcedure(Guid solicitationId);
        ServiceResult<List<DestinyBaseDto>> Create(List<DestinyBaseDto> Destinies);
    }
}
