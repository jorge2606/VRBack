using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VR.Service.Interfaces;
using VR.Common.Security;

namespace VR.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CodeLiquidationController : ControllerBase
    {
        private readonly ICodeLiquidationService _ICodeLiquidationServiceservice;

        public CodeLiquidationController(
            ICodeLiquidationService ICodeLiquidationServiceservice)
        {
            _ICodeLiquidationServiceservice = ICodeLiquidationServiceservice;
        }
        // GET: api/CodeLiquidation
        [HttpGet]
        [Authorize]
        public IActionResult GetAll()
        {
            var result = _ICodeLiquidationServiceservice.GetAll();

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result.Response);
        }
    }
}
