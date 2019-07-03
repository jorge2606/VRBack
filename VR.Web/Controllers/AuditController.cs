using System;
using Audit.Service.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VR.Common.Security;

namespace VR.Web.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class AuditController : ControllerBase
    {
        private IUserAuditService _userService;
        private INotificationAuditService _notificationAuditService;

        public AuditController(
            IUserAuditService userService,
            INotificationAuditService notificationAuditService
        )
        {
            _userService = userService;
            _notificationAuditService = notificationAuditService;
        }

        [HttpGet("userAudits/{userId}")]
        [Authorize(SolicitationSubsidyClaims.CanAudits, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult UserAudits(Guid userId)
        {
            var result = _userService.GetUserAudit(userId);

            if (!result.IsSuccess)
            {
                return BadRequest();
            }
            return Ok(result.Response);
        }

        [HttpGet("GetNotifications/{userId}")]
        [Authorize(Policy = SolicitationSubsidyClaims.CanAudits, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult GetNotifications(Guid userId)
        {
            var result = _notificationAuditService.GetNotificationAudit(userId);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result.Response);
        }

    }
}
