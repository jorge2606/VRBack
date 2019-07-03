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
    public class CityController : ControllerBase
    {
        private readonly ICityService _cityService;

        public CityController(ICityService cityService)
        {
            _cityService = cityService;
        } 

        [HttpGet("GetAll")]
        [Authorize]
        public IActionResult GetAll()
        {
            var result = _cityService.GetAllCity();
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result.Response);
        }
        

        // PUT: api/City/5
        [HttpGet("GetByIdCity/{provinceId}")]
        [Authorize]
        public IActionResult GetByIdCity(Guid provinceId)
        {
            var result = _cityService.FindByIdCity(provinceId);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result.Response);
        }
    }
}
