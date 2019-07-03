using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Service.Common.Extensions;
using Service.Common.ServiceResult;
using VR.Data;
using VR.Data.Model;
using VR.Dto;
using VR.Service.Interfaces;

namespace VR.Service.Services
{
    public class TransportService : ITransportService
    {
        private readonly DataContext _dataContext;
        private readonly IValidator<TransportBaseDto> _fluentValidator;
        private readonly IMapper _mapper;

        public TransportService(
            DataContext dataContext, 
            IValidator<TransportBaseDto> fluentValidator,
            IMapper mapper)
        {
            _dataContext = dataContext;
            _fluentValidator = fluentValidator;
            _mapper = mapper;
        }

        public ServiceResult<CreateTransportDto> CreateTransport(CreateTransportDto transportDto)
        {
            var validator = _fluentValidator.Validate(transportDto);

            if (!validator.IsValid)
            {
                return _mapper.Map<ServiceResult<CreateTransportDto>>(validator.ToServiceResult<CreateTransportDto>(null));
            }

            Transport newTransport = new Transport()
            {
                Id = new Guid(),
                Model = transportDto.Model,
                CarPlate = transportDto.CarPlate,
                Type = transportDto.Type,
                Brand = transportDto.Brand
            };

            _dataContext.Transports.Add(newTransport);
            _dataContext.SaveChanges();

            return new ServiceResult<CreateTransportDto>(transportDto);
        }

        public ServiceResult<UpdateTransportDto> UpdateTransport(UpdateTransportDto updateTransport)
        {
            var validator = _fluentValidator.Validate(updateTransport);

            if (!validator.IsValid)
            {
                return _mapper.Map< ServiceResult<UpdateTransportDto> >( validator.ToServiceResult<UpdateTransportDto>(null) );
            }

            Transport updateTran = new Transport()
            {
                Id = updateTransport.Id,
                Model = updateTransport.Model,
                Brand = updateTransport.Brand,
                CarPlate = updateTransport.CarPlate,
                Type = updateTransport.Type
            };

            _dataContext.Transports.Update(updateTran);
            _dataContext.SaveChanges();

            return new ServiceResult<UpdateTransportDto>(updateTransport);
        }

        public ServiceResult<TransportBaseDto> FindByIdTransport(Guid transportId)
        {
            var findTransport = _dataContext.Transports
                .Where(x => x.IsDeleted != true)
                .FirstOrDefault(x =>x.Id == transportId);

            if(findTransport == null)
            {
                return new ServiceResult<TransportBaseDto>(null);
            }

            return new ServiceResult<TransportBaseDto>(_mapper.Map<TransportBaseDto>(findTransport));
        }

        public ServiceResult<TransportBaseDto> DeleteTransport(Guid idTransport)
        {
            var deleteTransport = _dataContext.Transports.FirstOrDefault(x => x.Id == idTransport);

            if (deleteTransport == null)
            {
                return new ServiceResult<TransportBaseDto>(null);
            }

            deleteTransport.IsDeleted = true;
            _dataContext.Transports.Update(deleteTransport);
            _dataContext.SaveChanges();

            return new ServiceResult<TransportBaseDto>( _mapper.Map<TransportBaseDto>(deleteTransport) );
        }

        public ServiceResult<List<ListTransports>> GetAllTransport()
        {
            return new ServiceResult<List<ListTransports>>(
               _mapper.Map<List<ListTransports>>(
                   _dataContext.Transports
                       .Where(x => x.IsDeleted != true)
                       .OrderBy(x => x.Brand)
                       .ToList()
                   )
               );
        }


    }
}
