using Service.Common.ServiceResult;
using System;
using System.Collections.Generic;
using System.Text;
using VR.Dto;

namespace VR.Service.Interfaces
{
    public interface IDistributionService
    {
        ServiceResult<CreateDistributionDto> CreateDistribution(CreateDistributionDto categoryDto);
        ServiceResult<UpdateDistributionDto> UpdateDistribution(UpdateDistributionDto categoryDto);
        ServiceResult<DeleteDistributionDto> DeleteDistribution(Guid distributionId);
        ServiceResult<List<AllDistributionDto>> AllDistribution();
        ServiceResult<FindByIdDistributionDto> FindByIdDistribution(Guid id);
        ServiceResult<List<FindByIdOrganismIdDto>> FindByIdOrganism(Guid organismId);
    }
}
