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
    public class ProvinceController : ControllerBase
    {
        private readonly IProvinceService _provinceService;

        public ProvinceController(IProvinceService provinceService)
        {
            _provinceService = provinceService;
        }

        [HttpGet("GetAll")]
        [Authorize]
        public IActionResult GetAll()
        {
            var result = _provinceService.GetAllProvince();

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result.Response);
        }

        [HttpGet("DistrictCity/{district}")]
        [Authorize]
        public IActionResult GetAll(string district)
        {
            var result = _provinceService.FindByDistrictCity(district);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result.Response);
        }

        // GET: api/Province/5
        [HttpGet("PlaceId/{id}")]
        [Authorize]
        public IActionResult PlaceId(Guid id)
        {
            var result = _provinceService.FindByPlaceId(id);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result.Response);

        }

        [HttpGet("FindByCountryId/{countryId}")]
        [Authorize]
        public IActionResult FindByCountryId(Guid countryId)
        {
            var result = _provinceService.FindByCountryId(countryId);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result.Response);

        }

    }
}
