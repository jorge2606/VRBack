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

namespace VR.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupervisorUserAgentController : ControllerBase
    {
        private readonly ISupervisorUserAgentService _service;

        public SupervisorUserAgentController(
            ISupervisorUserAgentService service
            )
        {
            _service = service;
        }
        [HttpPost("Create")]
        [Authorize(Policy = SolicitationSubsidyClaims.CanCreateRelationshipBeetwenSupervisorAndAgent, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Create([FromBody] List<CreateSupervisorAgentDto> createSupervisorAgent)
        {
            var result = _service.Create(createSupervisorAgent);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result.Response);
        }

        [HttpGet("AllSupervisorAgents")]
        [Authorize(Policy = SolicitationSubsidyClaims.CanToSeeRelationshipBeetwenSupervisorAndAgent, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult AllSupervisorAgents()
        {
            var result = _service.AllSupervisorAgent();
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result.Response);
        }


        [HttpGet("AllSupervisors")]
        [Authorize]
        public IActionResult AllSupervisors()
        {
            var result = _service.AllSupervisors();
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result.Response);
        }

        [HttpGet("page")]
        [Authorize]
        public IActionResult PageAgentSupervisor([FromQuery] UserAgentFilterDto filters)
        {
            var result = _service.GetPageUserAgent(filters);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result.Response);
        }

        public Guid GetIdUser()
        {
            var currentUser = Helpers.HttpContext.Current.User.Claims;
            var result = Guid.Empty;
            foreach (var i in currentUser)
            {
                if (i.Type.Equals("NameIdentifier"))
                {
                    result = Guid.Parse(i.Value);
                }
            }

            return result;
        }
        [HttpGet("IsAgent/{otherId}")]
        [Authorize]
        public IActionResult IsAgent(Guid otherId)
        {
            var result = _service.IsAgent(GetIdUser(), otherId);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result.Response);
        }

        [HttpDelete("DeleteRelationshipBetweenAgentAndSupervisor/{supervisorId}/{AgentId}")]
        [Authorize(Policy = SolicitationSubsidyClaims.CanDeleteRelationshipBeetwenSupervisorAndAgent, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult DeleteRelationshipBetweenAgentAndSupervisor(Guid supervisorId, Guid AgentId)
        {
            var result = _service.DeleteRelationshipBetweenAgentAndSupervisor(supervisorId, AgentId);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result.Response);
        }
    }
}
