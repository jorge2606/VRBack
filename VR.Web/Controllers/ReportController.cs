using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VR.Common.Extensions;
using VR.Common.Security;
using VR.Dto;
using VR.Service.Interfaces;

namespace VR.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportController(
            IReportService reportService
        )
        {
            _reportService = reportService;
        }
        // GET: api/Report
        [HttpGet("Report_SolicitationByUser/{userId}")]
        [Authorize(Policy = SolicitationSubsidyClaims.CanViewReportByUsers, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Get_Report_SolicitationByUser(Guid userId)
        {
            var result = _reportService.PrintReportSolicitationSubsidyByUser(userId);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet("Report_SolicitationByOrganism/{organismId}")]
        [Authorize(Policy = SolicitationSubsidyClaims.CanViewReportByOrganism, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Get_Report_SolicitationByOrganism(Guid organismId)
        {
            var result = _reportService.PrintReportSolicitationSubsidyByOrganism(organismId);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }


        [HttpGet("Report_SolicitationByDestiniesAndDates")]
        [Authorize(Policy = SolicitationSubsidyClaims.CanViewReport, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Report_SolicitationByDestiniesAndDates([FromQuery] ReportByDestiniesAndDatesDto param)
        {
            var result = _reportService.PrintReport_SolicitationByDestiniesAndDates(param);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            
            return Ok(result);
        }

        [HttpGet("SolicitationsPendingProcedure")]
        [Authorize(Policy = SolicitationSubsidyClaims.CanViewPendingSolicitations, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Report_SolicitationsPendingProcedure()
        {
            var result = _reportService.SolicitationsPendingProcedure();
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet("SolicitationsExpireProcedure")]
        [Authorize(Policy = SolicitationSubsidyClaims.CanViewSolicitationsExpire, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Report_SolicitationsExpireProcedure()
        {
            var result = _reportService.SolicitationsExpireProcedure();
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet("ExpenditureProcedure")]
        [Authorize(Policy = SolicitationSubsidyClaims.CanViewExpendituresReport, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Report_ExpenditureProcedure()
        {
            var result = _reportService.ExpenditureProcedure();
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet("reportCommission/{id}")]
        [Authorize]
        public IActionResult ReportCommission(Guid id)
        {
            var result = _reportService.PrintReportCommission(id);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result.Response);
        }

        [HttpGet("ExpendituresReport")]
        [Authorize]
        public IActionResult Example()
        {
            var result = _reportService.ExpendituresReport();
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result.Response);
        }
    }
}
