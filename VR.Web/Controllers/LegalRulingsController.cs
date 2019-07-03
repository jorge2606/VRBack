using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VR.Common.Security;
using VR.Dto;
using VR.Service.Interfaces;
using VR.Web.Helpers;

namespace VR.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LegalRulingsController : ControllerBase
    {
        private readonly ILegalRulings _IlegalRuling;

        public LegalRulingsController(ILegalRulings IlegalRuling)
        {
            _IlegalRuling = IlegalRuling;
        }

        [HttpDelete("Delete/{id}")]
        [Authorize(Policy = SolicitationSubsidyClaims.CanDeleteLegalRuling, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Delete(Guid Id)
        {
            var result = _IlegalRuling.Delete(Id);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet("GetById/{id}")]
        [Authorize(Policy = SolicitationSubsidyClaims.CanViewLegalRuling, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult GetById(Guid id)
        {
            var result = _IlegalRuling.FindById(id);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
        
        [HttpPost]
        [Authorize(Policy = SolicitationSubsidyClaims.CanCreateLegalRuling, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Create([FromBody] LegalRulingsBaseDto legalRuling)
        {
            var result = _IlegalRuling.Create(legalRuling);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet("page")]
        [Authorize(Policy = SolicitationSubsidyClaims.CanViewLegalRuling, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public PagedResult<LegalRulingsBaseDto> Pagination([FromQuery] FilterLegalRulingsBaseDto param)
        {
            const int pageSize = 10;
            var queryPaginator = _IlegalRuling.GetAll().Response;

            var result = new List<LegalRulingsBaseDto>();

            result = queryPaginator
            .Where(
                    x => 
                        ( string.IsNullOrEmpty(param.Number) || x.Number.ToString().Contains(param.Number) )
                         &&
                        ( string.IsNullOrEmpty(param.Description) || x.Description.ToUpper().Contains(param.Description.ToUpper()) )
                         &&
                        (!x.IsDeleted)
                ).Skip((param.Page ?? 0) * pageSize)
                .Take(pageSize)
                .ToList();
            if (result.Count() == 0 && param.Page > 0)
            {
                result = queryPaginator.Where(
                        x =>
                            (string.IsNullOrEmpty(param.Number) || x.Number.ToString().Contains(param.Number))
                            &&
                            (string.IsNullOrEmpty(param.Description) || x.Description.ToUpper().Contains(param.Description.ToUpper()))
                            &&
                            (!x.IsDeleted)
                    ).Skip(((param.Page ?? 0) - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();
            }
            return new PagedResult<LegalRulingsBaseDto>
            {
                List = result,
                TotalRecords = queryPaginator.Count()
            };
        }
    }
}
