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
    public class ObservationService : IObservationService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ObservationService(
            DataContext context,
            IMapper mapper
        )
        {
            _context = context;
            _mapper = mapper;
        }

        public ServiceResult<List<ObservationDto>> GetById(Guid solicitationId)
        {
            var sol = _context.SolicitationSubsidies.FirstOrDefault(x => x.Id == solicitationId);
            var result = new ServiceResult<List<ObservationDto>>();
            if (sol == null)
            {
                result.AddError(NotificationType.Error.ToString(),"Esta solicitud no existe.");
                return result;
            }

            return new ServiceResult<List<ObservationDto>>(
                    _context.Observations
                        .Select(c => _mapper.Map<ObservationDto>(c))
                        .Where(x => x.SolicitationId == solicitationId).ToList()
                ); 
            
        }

        public ServiceResult<PosponeSolicitationDto> Create(PosponeSolicitationDto pospone)
        {
            var solic = _context.SolicitationSubsidies.FirstOrDefault(x =>
                x.Id == pospone.Id);

            var result = new ServiceResult<PosponeSolicitationDto>();
            if (solic == null)
            {
                result.AddError(NotificationType.Error.ToString(),"La solicitud no existe");
                return result;
            }

            var observationThisSolicitation = _context.Observations
                .Where(x => x.SolicitationId == pospone.Id)
                .ToList();

            if (pospone.Observations.Count() == 0)
            {
                _context.Observations.RemoveRange(observationThisSolicitation);
            }
            else
            {
                foreach (var p in pospone.Observations)
                {
                    var exist = observationThisSolicitation.FirstOrDefault(v => v.Id == p.Id);

                    if (p.Id.Equals(Guid.Empty))
                    {
                        _context.Observations.Add(new Observation()
                        {
                            Id = new Guid(),
                            SolicitationId = pospone.Id,
                            Description = p.Description
                        });
                    }
                    else if (exist != null)
                    {
                        exist.Description = p.Description;
                        exist.SolicitationId = pospone.Id;
                        _context.Observations.Update(exist);

                        observationThisSolicitation.Remove(exist);
                    }

                }

                _context.Observations.RemoveRange(observationThisSolicitation);

            }
            _context.SaveChanges();
            return result;
        }
        
    }
}
