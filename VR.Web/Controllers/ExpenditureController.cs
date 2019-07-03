using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VR.Common.Security;
using VR.Service.Interfaces;

namespace VR.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenditureController : ControllerBase
    {
        private readonly IExpenditureService _expenditureService;

        public ExpenditureController(IExpenditureService expenditureService)
        {
            _expenditureService = expenditureService;
        }
        // GET: api/Expenditure
        [HttpGet("GetByIdSolicitationSubsidy/{id}")]
        [Authorize]
        public IActionResult GetByIdSolicitationSubsidy(Guid id)
        {
            var result = _expenditureService.GetByIdSolicitationSubsidy(id);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result.Response);
        }

        [HttpDelete("Delete/{id}")]
        [Authorize]
        public IActionResult Delete(Guid id)
        {
            var result = _expenditureService.Delete(id);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result.Response);
        }
    }
}
