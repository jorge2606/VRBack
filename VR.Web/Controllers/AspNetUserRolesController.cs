using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VR.Dto;
using VR.Dto.User;
using VR.Service.Interfaces;

namespace VR.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AspNetUserRolesController : ControllerBase
    {
        private readonly IAspNetRolesService _aspNetRolesService;
        private readonly IAspNetUserRolesService _aspNetUserRoles;

        public AspNetUserRolesController(
            IAspNetRolesService aspNetRolesService,
            IAspNetUserRolesService aspNetUserRoles)
        {
            _aspNetRolesService = aspNetRolesService;
            _aspNetUserRoles = aspNetUserRoles;
        }

        [HttpGet("AllRoles")]
        [Authorize]
        public IActionResult GetAllRoles()
        {
            var Supervisor = new List<AllUserRolesDto>();

            var rolesType = _aspNetRolesService.GetAllRoles();
            var usersRoles = _aspNetUserRoles.GetAllUserRoles();

            if (!usersRoles.IsSuccess)
            {
                return BadRequest(usersRoles);
            }

            if (!rolesType.IsSuccess)
            {
                return BadRequest(rolesType);
            }

            var rolSupervisor = rolesType.Response.FirstOrDefault(x => x.NormalizedName.Equals("SUPERVISOR"));
            Supervisor = usersRoles.Response.FindAll(x => x.RoleId == rolSupervisor.Id);

            return Ok(Supervisor);
        }


        [HttpGet("OnlyRoles")]
        [Authorize]
        public IActionResult OnlyRoles()
        {
            
            var usersRoles = _aspNetUserRoles.GetAllUserRoles();

            if (!usersRoles.IsSuccess)
            {
                return BadRequest(usersRoles);
            }

            return Ok(usersRoles.Response);
        }
    
    }


}
