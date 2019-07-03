using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VR.Service.Interfaces;

namespace VR.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AspNetRolesController : ControllerBase
    {
        private readonly IAspNetRolesService _aspNetRolesService;

        public AspNetRolesController(
            IAspNetRolesService aspNetRolesService)
        {
            _aspNetRolesService = aspNetRolesService;
        }
        // GET: api/AspNetRoles
        [HttpGet("AllRoles")]
        [Authorize]
        public IActionResult GetAllRoles()
        {
            var result = _aspNetRolesService.GetAllRoles();
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result.Response);
        }
    }
}
