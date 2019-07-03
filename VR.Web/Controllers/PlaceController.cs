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
    public class PlaceController : ControllerBase
    {
        private readonly IPlaceService _placeService;
        public PlaceController(
            IPlaceService placeService)
        {
            _placeService = placeService;
        }
        // GET: api/Place
        [HttpGet("GetAll")]
        [Authorize]
        public IActionResult GetAll()
        {
            var result = _placeService.GetAllPlace();
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result.Response);
        }
        
    }
}
