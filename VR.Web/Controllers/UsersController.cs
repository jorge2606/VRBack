using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using VR.Web.Helpers;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Expressions;
using Service.Common.ServiceResult;
using VR.Data;
using VR.Data.Model;
using VR.Dto;
using VR.Dto.User;
using VR.Service.Interfaces;
using VR.Common.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace VR.Web.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public UserController(DataContext context, IUserService userService, IConfiguration configuration,
            IMapper mapper)
        {
            _context = context;
            _userService = userService;
            _configuration = configuration;
            _mapper = mapper;
        }

        [HttpPost("SaveRolUser")]
        [Authorize(Policy = SolicitationSubsidyClaims.CanCreateUsers, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> SaveRolUser([FromBody]RoleUserDto rolUser)
        {
            await _userService.UpdateUserRole(rolUser.UserId, rolUser.RolId);
            return Ok(rolUser);
        }

        [HttpPost("Save")]
        [Authorize(Policy = SolicitationSubsidyClaims.CanCreateUsers, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Save([FromBody]CreateUserDto createUser)
        {
            var newUser = await _userService.CreateAsync(createUser);

            if (!newUser.IsSuccess)
            {
                return BadRequest(newUser);
            }
            return Ok(newUser);
        }

        [HttpPost("Auth")]
        [AllowAnonymous]
        public async Task<IActionResult> Authentication([FromBody]LoginDto p_LoginDto)
        {
            var result = await _userService.Authenticate(p_LoginDto);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result.Response);
        }


        [AllowAnonymous]
        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromBody]ForgotPasswordDto forgotPassword)
        {
            var result = await _userService.ForgotPassword(forgotPassword);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(forgotPassword);
        }

        [AllowAnonymous]
        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody]ResetPassword resetPassword)
        {
            var result = await _userService.ResetPassword(resetPassword);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(resetPassword);
        }

        [HttpGet("getall")]
        [Authorize]
        public ActionResult<List<User>> GetAll()
        {
            var result = _userService.GetAll();

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result.Response);
        }

        /*[AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]SaveUserDto userDto)
        {
            var result = await _userService.Register(userDto);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            /* UserAuthenticationDto ObjToken = new UserAuthenticationDto
             {
                 Token = result.Response
             };
             */
        /*return Ok(result.Response);
     }
        */
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

        [HttpGet("getbyidAdministrator/{id}")]
        [Authorize(Policy = SolicitationSubsidyClaims.CanEditUsers, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult<ModifyUserDto> GetById(Guid id)
        {
            var user = _context.Users.Find(id);
            if (user == null)
            {
                return null;
            }

            var supervisorId = _context.SupervisorUserAgents.FirstOrDefault(x => x.AgentId == user.Id);
            var supervisorId1Nulleable = supervisorId == null ? Guid.Empty : supervisorId.SupervisorId;
            var supervisorId2Nulleable = supervisorId == null ? Guid.Empty : supervisorId.SupervisorId2;

            ModifyUserDto modifyUserDto = new ModifyUserDto
            {
                Dni = user.Dni,
                UserName = user.UserName,
                PhoneNumber = user.PhoneNumber,
                FirstName = user.FirstName,
                LastName = user.LastName,
                DistributionId = user.DistributionId,
                CategoryId = user.CategoryId,
                SupervisorAgentId = supervisorId1Nulleable == null ? Guid.Empty : supervisorId1Nulleable,
                SupervisorAgentId2 = supervisorId2Nulleable == null ? Guid.Empty : supervisorId2Nulleable
            };

            var RolesUser = _context.UserRoles.ToList();
            var AllRoles = _context.Roles.ToList();

            List<RoleWhenModifyUser> ListRolesBelongsToUser = new List<RoleWhenModifyUser>();
            foreach (var role in AllRoles)
            {
                if (RolesUser.Exists(x => x.UserId == user.Id && x.RoleId == role.Id))
                {
                    RoleWhenModifyUser roleWhenModifyUser = new RoleWhenModifyUser
                    {
                        Id = role.Id,
                        Name = role.Name,
                        NormalizedName = role.NormalizedName,
                        RolBelongUser = true
                    };

                    ListRolesBelongsToUser.Add(roleWhenModifyUser);
                }
                else
                {
                    RoleWhenModifyUser roleWhenModifyUser = new RoleWhenModifyUser
                    {
                        Id = role.Id,
                        Name = role.Name,
                        NormalizedName = role.NormalizedName,
                        RolBelongUser = false
                    };

                    ListRolesBelongsToUser.Add(roleWhenModifyUser);

                }
            }
            modifyUserDto.RolesUser = ListRolesBelongsToUser;
            return modifyUserDto;
        }

        [HttpGet("getbyid")]
        [Authorize]
        public ActionResult<ModifyUserDto> GetById()
        {
            var userId = GetIdUser();
            var user = _context.Users
                .Include(x => x.Distribution)
                .FirstOrDefault(j => j.Id == userId);
            if (user == null)
            {
                return null;
            }
            ModifyUserDto modifyUserDto = new ModifyUserDto
            {
                Dni = user.Dni,
                UserName = user.UserName,
                PhoneNumber = user.PhoneNumber,
                FirstName = user.FirstName,
                LastName = user.LastName,
                DistributionId = user.DistributionId,
                OrganismId = user.Distribution.OrganismId
            };

            var RolesUser = _context.UserRoles.ToList();
            var AllRoles = _context.Roles.ToList();

            List<RoleWhenModifyUser> ListRolesBelongsToUser = new List<RoleWhenModifyUser>();
            foreach (var role in AllRoles)
            {
                if (RolesUser.Exists(x => x.UserId == user.Id && x.RoleId == role.Id))
                {
                    RoleWhenModifyUser roleWhenModifyUser = new RoleWhenModifyUser
                    {
                        Id = role.Id,
                        Name = role.Name,
                        RolBelongUser = true
                    };

                    ListRolesBelongsToUser.Add(roleWhenModifyUser);
                }
                else
                {
                    RoleWhenModifyUser roleWhenModifyUser = new RoleWhenModifyUser
                    {
                        Id = role.Id,
                        Name = role.Name,
                        RolBelongUser = false
                    };

                    ListRolesBelongsToUser.Add(roleWhenModifyUser);

                }
            }
            modifyUserDto.RolesUser = ListRolesBelongsToUser;
            return modifyUserDto;
        }

        [HttpPut("UpdateProfileAsAdmin")]
        [Authorize(Policy = SolicitationSubsidyClaims.CanEditUsers, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> UpdateProfileAsAdmin([FromBody]UpdateProfileAsAdmin userDto)
        {
            await _userService.UpdateProfileAsAdmin(userDto);
            return Ok();
        }

        [HttpPut("UpdateMyProfile")]
        [Authorize]
        public async Task<IActionResult> UpdateMyProfile([FromBody]UpdateMyProfile userDto)
        {
            userDto.Id = GetIdUser();
            var result = await _userService.UpdateMyProfile(userDto);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            
            return Ok(result.Response);
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = SolicitationSubsidyClaims.CanDeleteUsers, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Delete(Guid id)
        {
            _userService.Delete(id);
            return Ok();
        }

        public IQueryable<AllUserDto> queryableUser()
        {
            var usersPaginator = _context.Users.Select(x => _mapper.Map<AllUserDto>(x)).OrderBy(x => x.UserName);
            return usersPaginator;
        }
        
        [HttpGet("page")]
        [Authorize(Policy = SolicitationSubsidyClaims.CanViewUsers, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult GetPageUser([FromQuery] UserFilterDto param)
        {
            var result = _userService.GetPageUser(param);

            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result.Response);
        }
    }

}