using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VR.Common.Security;
using VR.Data;
using VR.Data.Model;
using VR.Dto;
using VR.Service.Interfaces;
using VR.Web.Helpers;

namespace VR.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DistributionController : ControllerBase
    {
        private readonly IDistributionService _distributionService;
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public DistributionController(IDistributionService distributionService,
            DataContext dataContext, IMapper mapper)
        {
            _distributionService = distributionService;
            _dataContext = dataContext;
            _mapper = mapper;
        }

        [HttpPost("CreateDistribution")]
        [Authorize(SolicitationSubsidyClaims.CanCreateDistribution, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult CreateDistribution(CreateDistributionDto createDistribution)
        {
            var result = _distributionService.CreateDistribution(createDistribution);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result.Response);
        }

        [HttpPut("UpdateDistribution")]
        [Authorize(SolicitationSubsidyClaims.CanEditDistribution, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult UpdateDistribution(UpdateDistributionDto updateDistribution)
        {
            var result = _distributionService.UpdateDistribution(updateDistribution);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result.Response);
        }


        [HttpGet("FindByIdDistribution/{findByIdDistribution}")]
        [Authorize]
        public IActionResult FindByIdCategory(Guid findByIdDistribution)
        {
            var response = _distributionService.FindByIdDistribution(findByIdDistribution);

            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }

            return Ok(response.Response);
        }

        [HttpGet("FindByIdOrganism/{organismId}")]
        [Authorize]
        public IActionResult FindByIdOrganism(Guid organismId)
        {
            var result = _distributionService.FindByIdOrganism(organismId);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result.Response);
        }

        [HttpDelete("DeleteDistribution/{distributionId}")]
        [Authorize(SolicitationSubsidyClaims.CanDeleteDistribution, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult DeleteDistribution(Guid distributionId)
        {
            var result = _distributionService.DeleteDistribution(distributionId);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result.Response);
        }

        [HttpGet("AllDistributions")]
        [Authorize]
        public IActionResult AllDistribution()
        {
            var result = _distributionService.AllDistribution();
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result.Response);

        }


        public IQueryable<AllDistributionDto> queryableUser()
        {
            var Paginator = _dataContext.Distributions
                .Include(x => x.Users)
                .Include(q => q.Organism)
                .Select(dist => _mapper.Map<AllDistributionDto>(dist))
                .Where(y => y.IsDeleted != true);
            return Paginator;
        }

        [HttpGet("page")]
        [Authorize(SolicitationSubsidyClaims.CanViewDistribution, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public PagedResult<AllDistributionDto> userPagination([FromQuery] FilterDistributionDto filters)
        {       
            const int pageSize = 10;
            var queryPaginator = queryableUser();

            var result = queryPaginator
                .Where(
                    x => (string.IsNullOrEmpty(filters.Name) || x.Name.ToUpper().Contains(filters.Name.ToUpper()))
                         &&
                         (!filters.OrganismId.HasValue || x.OrganismId == filters.OrganismId)
            );
            

            if (filters.OrganismId.HasValue)
            {
                return new PagedResult<AllDistributionDto>
                {
                    List = result.Skip((filters.Page ?? 0) * pageSize).Take(pageSize).ToList(),
                    TotalRecords = result.Count()
                };
            }

            var lista = result.Skip((filters.Page ?? 0) * pageSize).Take(pageSize).ToList();
            if (lista.Count() == 0 && filters.Page > 0)
            {
                lista = result.Skip( ((filters.Page ?? 0) - 1) * pageSize).Take(pageSize).ToList();
            }
            return new PagedResult<AllDistributionDto>
            {
                List = lista,
                TotalRecords = queryPaginator.Count()
            };
        }
    }
}