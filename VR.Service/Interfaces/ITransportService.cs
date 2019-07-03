using System;
using System.Collections.Generic;
using Service.Common.ServiceResult;
using VR.Dto;

namespace VR.Service.Interfaces
{
    public interface ITransportService
    {
        ServiceResult<CreateTransportDto> CreateTransport(CreateTransportDto transportDto);
        ServiceResult<UpdateTransportDto> UpdateTransport(UpdateTransportDto transportDto);
        ServiceResult<TransportBaseDto> DeleteTransport(Guid idTransport);
        ServiceResult<TransportBaseDto> FindByIdTransport(Guid idTransport);
        ServiceResult<List<ListTransports>> GetAllTransport();
    }
}
