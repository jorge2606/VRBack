using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VR.Dto;
using VR.Service.Interfaces;

namespace VR.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ObservationController : ControllerBase
    {
        private readonly IObservationService _IobservationService;

        public ObservationController(
            IObservationService IobservationService
            )
        {
            _IobservationService = IobservationService;
        }

        [HttpGet("getById/{solicitationId}")]
        public IActionResult GetById(Guid solicitationId)
        {
            var result = _IobservationService.GetById(solicitationId);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost("create")]
        public IActionResult create([FromBody] PosponeSolicitationDto pospone)
        {
            var result = _IobservationService.Create(pospone);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
