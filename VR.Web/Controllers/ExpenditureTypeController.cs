using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VR.Common.Security;
using VR.Data;
using VR.Data.Model;
using VR.Dto;
using VR.Service.Interfaces;
using VR.Web.Helpers;

namespace VR.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenditureTypeController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IExpenditureTypeService _expenditureService;

        public ExpenditureTypeController(DataContext context, IExpenditureTypeService expenditureService)
        {
            _context = context;
            _expenditureService = expenditureService;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var result = _expenditureService.AllExpenditureType();
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result.Response);
        }

        [HttpPut("Update")]
        [Authorize(Policy = SolicitationSubsidyClaims.CanEditExpenditure, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult UpdateExpenditureType(UpdateExpenditureTypeDto expenditure)
        {
            var result = _expenditureService.UpdateExpenditureType(expenditure);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result.Response);
        }

        [HttpDelete("Delete/{id}")]
        [Authorize(Policy = SolicitationSubsidyClaims.CanDeleteExpenditure, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult DeleteExpenditureType(Guid id)
        {
            var result = _expenditureService.DeleteExpenditureType(id);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result.Response);
        }

        [HttpGet("FindById/{id}")]
        [Authorize(Policy = SolicitationSubsidyClaims.CanEditExpenditure, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult FindById(Guid id)
        {
            var result = _expenditureService.FindByIdExpenditureType(id);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result.Response);
        }

        [HttpPost("Create")]
        [Authorize(Policy = SolicitationSubsidyClaims.CanCreateExpenditure, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult CreateExpenditureType(CreateExpenditureTypeDto createExpenditure)
        {
            var result = _expenditureService.CreateExpenditureType(createExpenditure);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result.Response);
        }


        public IQueryable<ExpenditureType> queryableUser()
        {
            var usersPaginator = _context.ExpenditureTypes
                .Where(x => x.IsDeleted != true)
                .OrderBy(x => x.Name);
            return usersPaginator;
        }

        [HttpGet("page/{page}")]
        [Authorize(Policy = SolicitationSubsidyClaims.CanViewExpenditure, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public PagedResult<ExpenditureType> userPagination(int? page)
        {
            const int pageSize = 10;
            var queryPaginator = queryableUser();

            var result = queryPaginator.Skip((page ?? 0) * pageSize)
                .Take(pageSize)
                .ToList();

            if (result.Count() == 0 && page > 0)
            {
                result = queryPaginator.Skip(( (page ?? 0) - 1 ) * pageSize)
                    .Take(pageSize)
                    .ToList();
            }

            return new PagedResult<ExpenditureType>
            {
                List = result,
                TotalRecords = queryPaginator.Count()
            };
        }
    }
}