using Service.Common.ServiceResult;
using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FluentValidation;
using Service.Common.Extensions;
using VR.Data;
using VR.Data.Model;
using VR.Dto;
using VR.Service.Interfaces;
using NotificationType = Service.Common.ServiceResult.NotificationType;

namespace VR.Service.Services
{
    public class DistributionService : IDistributionService
    {
        private readonly DataContext _distributionContext;
        private readonly IValidator<DistributionBaseDto> _fluentValidatorDistribution;
        private readonly IMapper _mapper;

        public DistributionService(DataContext distributionContext,
            IValidator<DistributionBaseDto> fluentValidatorDistribution,
            IMapper mapper)
        {
            _distributionContext = distributionContext;
            _fluentValidatorDistribution = fluentValidatorDistribution;
            _mapper = mapper;
        }


        public ServiceResult<CreateDistributionDto> CreateDistribution(CreateDistributionDto distribution)
        {
            ServiceResult<CreateDistributionDto> result = new ServiceResult<CreateDistributionDto>();
            NotificationType msg = 0;
            var validation = _fluentValidatorDistribution.Validate(distribution);

            if (!validation.IsValid)
            {
                return _mapper.Map< ServiceResult<CreateDistributionDto> >( validation.ToServiceResult<CreateDistributionDto>(null));
            }

            Distribution newDistribution = new Distribution()
            {
                Id = new Guid(),
                Name = distribution.Name,
                Description = distribution.Description,
                OrganismId = distribution.OrganismId
            };

            _distributionContext.Add(newDistribution);
            _distributionContext.SaveChanges();

            var newOrganism = _distributionContext.Organisms.FirstOrDefault(x =>x.Id == newDistribution.Id);
            List<User> listUsers = _distributionContext.Users.Where(x => x.DistributionId == newDistribution.Id).ToList();
            
            if (newOrganism == null)
            {
                msg = NotificationType.Info;
                result.AddNotification(msg, "No hay organismos para esta distribución");
            }

            if (listUsers.Count == 0)
            {
                msg = NotificationType.Info;
                result.AddNotification(msg, "No hay usuarios para esta distribución");
            }

            newDistribution.Organism = newOrganism;
            newDistribution.Users = listUsers;

            return new ServiceResult<CreateDistributionDto>(distribution);
        }

        public ServiceResult<UpdateDistributionDto> UpdateDistribution(UpdateDistributionDto distributionDto)
        {
            var validateDistributionBaseDto = _fluentValidatorDistribution.Validate(distributionDto);
            var distributionModify = _distributionContext.Distributions.FirstOrDefault(x => x.Id == distributionDto.Id);

            if (!validateDistributionBaseDto.IsValid ||
                distributionModify == null)
            {
                return _mapper.Map< ServiceResult<UpdateDistributionDto> >( validateDistributionBaseDto.ToServiceResult<UpdateDistributionDto>(null) );
            }

            distributionModify.Description = distributionDto.Description;
            distributionModify.Name = distributionDto.Name;
            distributionModify.OrganismId = distributionDto.OrganismId;

            _distributionContext.Distributions.Update(distributionModify);
            _distributionContext.SaveChanges();

            return new ServiceResult<UpdateDistributionDto>(distributionDto);
        }

        public ServiceResult<FindByIdDistributionDto> FindByIdDistribution(Guid id)
        {
            var distribution = _distributionContext.Distributions
                .Where(x => x.IsDeleted != true)
                .FirstOrDefault(x => x.Id == id);

            if (distribution == null)
            {
                return new ServiceResult<FindByIdDistributionDto>(null);
            }

            return new ServiceResult<FindByIdDistributionDto>(_mapper.Map<FindByIdDistributionDto>(distribution));
        }

        public ServiceResult<List<FindByIdOrganismIdDto>> FindByIdOrganism(Guid organismId)
        {
            var distributionFromOrganims = _distributionContext
                .Distributions
                .Select(q => _mapper.Map<FindByIdOrganismIdDto>(q))
                .Where(x => x.IsDeleted != true && x.OrganismId == organismId)
                .ToList();

            if (distributionFromOrganims == null)
            {
                return new ServiceResult<List<FindByIdOrganismIdDto>>(null);
            }

            return new ServiceResult<List<FindByIdOrganismIdDto>>(distributionFromOrganims);
        }

        public ServiceResult<DeleteDistributionDto> DeleteDistribution(Guid distributionId)
        {
            var distribution = _distributionContext.Distributions.FirstOrDefault(x => x.Id == distributionId);

            if (distribution == null)
            {
                return new ServiceResult<DeleteDistributionDto>(null);
            }

            distribution.IsDeleted = true;
            _distributionContext.Update(distribution);
            _distributionContext.SaveChanges();

            return new ServiceResult<DeleteDistributionDto>(_mapper.Map<DeleteDistributionDto>(distribution));
        }

        public ServiceResult< List<AllDistributionDto> > AllDistribution()
        {
            return new ServiceResult<List<AllDistributionDto>>(_mapper.Map<List<AllDistributionDto>>(
                _distributionContext.Distributions.Where(x => x.IsDeleted != true).ToList())
            );
        }

    }
}
