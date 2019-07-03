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
    public class HolidayController : ControllerBase
    {
        private readonly IHolidayService _holidayService;

        public HolidayController(
            IHolidayService holidayService
            )
        {
            _holidayService = holidayService;
        }
        // GET: api/Holiday
        [HttpGet("GetPageHoliday")]
        [Authorize(SolicitationSubsidyClaims.CanViewHoliday, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult GetPageHoliday([FromQuery] FilterHolidayDto param)
        {
            FilterHolidayDto filters = new FilterHolidayDto()
            {
                Description = param.Description,
                Date = new DateDto()
                {
                    Day = param.Day,
                    Month = param.Month,
                    Year = param.Year
                },
                Page = param.Page
            };
            var response = _holidayService.Pagination(filters);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }

            return Ok(response.Response);
        }

        // GET: api/Holiday/5
        [HttpGet("getById/{id}")]
        [Authorize(Policy = SolicitationSubsidyClaims.CanEditHoliday, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult GetById(Guid id)
        {
            var result = _holidayService.FindByIdHoliday(id);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result.Response);
        }

        // POST: api/Holiday
        [HttpPost("create")]
        [Authorize(SolicitationSubsidyClaims.CanCreateHoliday, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Post([FromBody] CreateHolidayDto newHoliday)
        {
            var result = _holidayService.CreateHoliday(newHoliday);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result.Response);
        }

        // PUT: api/Holiday/5
        [HttpPut("update")]
        [Authorize(SolicitationSubsidyClaims.CanEditHoliday, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Update([FromBody] UpdateHolidayDto holiday)
        {
            var result = _holidayService.UpdateHoliday(holiday);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result.Response);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("Delete/{id}")]
        [Authorize(SolicitationSubsidyClaims.CanDeleteHoliday, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Delete(Guid id)
        {
            var response = _holidayService.DeleteHoliday(id);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }

            return Ok(response.Response);
        }

        [HttpGet("haveHolidays")]
        [Authorize]
        public IActionResult IsHolidays(int day, int month, int year, int amountDays)
        {

            var response = _holidayService.HaveHoliday(new DateTime(year,month,day), amountDays);
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }

            return Ok(response.Response);
        }
    }
}
