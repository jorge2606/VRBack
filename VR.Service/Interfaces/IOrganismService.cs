using System;
using System.Collections.Generic;
using System.Text;
using Service.Common.ServiceResult;
using VR.Dto;

namespace VR.Service.Interfaces
{
    public interface IOrganismService
    {
        ServiceResult<List<GetallOrganismDto>> GetAllOrganism();
        ServiceResult<CreateOrganismDto> CreateOrganism(CreateOrganismDto organismDto);
        ServiceResult<UpdateOrganismDto> UpdateOrganism(UpdateOrganismDto organismDto);
        ServiceResult<DeleteOrganismDto> DeleteOrganism(Guid organismId);
        ServiceResult<FindByIdOrganismDto> FindByIdOrganism(Guid id);

    }
}
