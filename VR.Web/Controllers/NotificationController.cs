using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using VR.Web.Helpers;
using System.Linq;
using AutoMapper;
using VR.Dto;
using VR.Service.Interfaces;

namespace VR.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NotificationController : ControllerBase
    {
        private INotificationService _notificationService;
        private IMapper _mapper;

        public NotificationController(INotificationService notificationService, IMapper mapper)
        {
            _notificationService = notificationService;
            _mapper = mapper;
        }

        [HttpGet("page/{page}")]
        [Authorize]
        public PagedResult<NotificationDto> NotificationPagination(int? page)
        {
            const int pageSize = 5;
            Guid idUser = GetIdUser();
            var queryPaginator = _notificationService.GetPaginator(idUser);

            if (!queryPaginator.IsSuccess)
            {
                return new PagedResult<NotificationDto>()
                {
                    List = _mapper.Map<List<NotificationDto>>(queryPaginator.Response.ToList()),
                    TotalRecords = queryPaginator.Response.ToList().Count()
                };
            }
            var result = queryPaginator.Response.Skip((page ?? 0) * pageSize)
                .Take(pageSize)
                .ToList();
            return new PagedResult<NotificationDto>
            {
                List = _mapper.Map<List<NotificationDto>>(result),
                TotalRecords = result.Count()
            };
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

        [HttpGet("GetSomeNotifications")]
        [Authorize]
        public ActionResult<List<NotificationDto>> GetSomeNotifications()
        {
            var userId = GetIdUser();
            return _notificationService.GetSomeNotifications(userId);
        }

        [HttpGet("GetAllNotifications")]
        public ActionResult<List<NotificationDto>> GetAllNotifications()
        {
            return _notificationService.GetAllNotifications();
        }


        [HttpGet("{id}")]
        [AllowAnonymous]
        public ActionResult<List<NotificationDto>> GetNotificationsById(Guid id)
        {
            return _notificationService.GetNotificationsById(id);
        }

        [HttpDelete("Delete/{id}")]
        public ActionResult<List<NotificationDto>> DeleteNotification(Guid id)
        {
            var response = _notificationService.DeleteNotification(id);
            if (!response.IsSuccess)
            {
                return BadRequest(response.Response);
            }

            return Ok(response.Response);
        }

        [HttpPut("NotificationRidden")]
        public IActionResult NotificationRidden(NotificationDto notif)
        {
            var response = _notificationService.NotificationRidden(notif.Id);
            if (!response.IsSuccess)
            {
                return BadRequest(response.Response);
            }

            //response.Response.Colapse = true;
            return Ok(response.Response);

        }


    }

}