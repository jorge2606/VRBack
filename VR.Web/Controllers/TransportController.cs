using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Common.ServiceResult;
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
    public class TransportController : ControllerBase
    {
        private readonly ITransportService _transportService;
        private readonly DataContext _dataContext;

        public TransportController(ITransportService transportService, DataContext dataContext)
        {
            _transportService = transportService;
            _dataContext = dataContext;
        }

        [HttpPost("Create")]
        [Authorize(Policy = SolicitationSubsidyClaims.CanCreateTransport, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult CreateTransport(CreateTransportDto createTransport)
        {
            var result = _transportService.CreateTransport(createTransport);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result.Response);
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var response = _transportService.GetAllTransport();
            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }

            return Ok(response.Response);
        }

        [HttpPut("Update")]
        [Authorize(Policy = SolicitationSubsidyClaims.CanEditTransport, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult UpdateTransport(UpdateTransportDto updateTransport)
        {
            var result = _transportService.UpdateTransport(updateTransport);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result.Response);
        }

        [HttpGet("FindByIdTransport/{findByIdTransport}")]
        [Authorize(Policy = SolicitationSubsidyClaims.CanEditTransport, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult FindByIdTransport(Guid findByIdTransport)
        {
            var result = _transportService.FindByIdTransport(findByIdTransport);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result.Response);
        }

        [HttpDelete("Delete/{id}")]
        [Authorize(Policy = SolicitationSubsidyClaims.CanDeleteTransport, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Delete(Guid id)
        {
            var result = _transportService.DeleteTransport(id);

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
        
        [HttpGet("page")]
        [Authorize(Policy = SolicitationSubsidyClaims.CanViewTransport, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public PagedResult<TransportBaseDto> userPagination([FromQuery] FilterTransportDto filters)
        {
            const int pageSize = 10;
            var resultFull = _dataContext.Transports
                .Where(x => x.IsDeleted != true)
                .OrderBy(x => x.Brand)
                .Where(
                    x =>
                        (string.IsNullOrEmpty(filters.Model) || x.Model.ToUpper().Contains(filters.Model.ToUpper()))
                        &&
                        (string.IsNullOrEmpty(filters.Brand) || x.Brand.ToUpper().Contains(filters.Brand.ToUpper()))
                        &&
                        (string.IsNullOrEmpty(filters.CarPlate) || x.CarPlate.ToUpper().Contains(filters.CarPlate.ToUpper()))
                        &&
                        (!x.IsDeleted)
                );

            var resultPage = resultFull.Skip((filters.Page ?? 0) * pageSize)
                .Take(pageSize)
                .ProjectTo<TransportBaseDto>()
                .ToList();

            if (resultPage.Count() == 0 && filters.Page > 0)
            {
                resultPage = resultFull.Skip(((filters.Page ?? 0) - 1) * pageSize)
                    .Take(pageSize)
                    .ProjectTo<TransportBaseDto>()
                    .ToList();
            }

            return new PagedResult<TransportBaseDto>()
            {
                List = resultPage,
                TotalRecords = resultFull.Count()
            };
        }

    }
}