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
    public class ApproveOfAuthorityThatOrderCommissionController : ControllerBase
    {
        private readonly IApproveOfAuthorityThatOrderCommissionsService _IApproveOfAuthorityThatOrderCommissionsService;
        public ApproveOfAuthorityThatOrderCommissionController(
            IApproveOfAuthorityThatOrderCommissionsService IApproveOfAuthorityThatOrderCommissionsService
            )
        {
            _IApproveOfAuthorityThatOrderCommissionsService = IApproveOfAuthorityThatOrderCommissionsService;
        }
        [HttpGet("GetApproved/{Id}")]
        [Authorize]
        public IActionResult GetApproved(Guid Id)
        {
            var result = _IApproveOfAuthorityThatOrderCommissionsService.GetApproved(Id);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
        public void Post([FromBody] string value)
        {
        }
    }
}
