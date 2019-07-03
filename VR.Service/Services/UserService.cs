using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Text;
using Service.Common.ServiceResult;
using Microsoft.Extensions.Configuration;
using FluentValidation;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Service.Common.Extensions;
using VR.Data;
using VR.Data.Model;
using VR.Dto;
using VR.Dto.User;
using VR.Service.Interfaces;
using VR.Identity.Identities;
using VR.Service.Helpers.WebApi.Helpers;
using VR.Web.Helpers;
using NotificationType = Service.Common.ServiceResult.NotificationType;
using System.IO;
using Microsoft.AspNetCore.Identity;
using RazorLight;
using VR.Common.Security;
using System.Net;
using CreateUserDto = VR.Dto.User.CreateUserDto;

namespace VR.Service.Services
{
    public class UserService : IUserService
    {
        private readonly DataContext _context;
        private readonly UserManager _userManager;
        private readonly RoleManager _roleManager;
        private readonly IEmailService _emailSender;
        private readonly IFileService _fileService;
        private readonly ISupervisorUserAgentService _supervisorUserAgentService;
        private readonly IMapper _mapper;
        private readonly IValidator<SaveUserDto> _fluentValidatorUser;
        private readonly IValidator<LoginDto> _fluentValidatorLogin;
        private readonly IValidator<CreateUserDto> _fluentValidatorCreateUser;
        private readonly IValidator<ResetPassword> _fluentValidationResetPassword;
        private readonly SignInManager _signInManager;
        private IConfiguration _configuration;
        private IAspNetUserRolesService _rolesService;
        public static string StaticFilesDirectory = Path.Combine(Directory.GetCurrentDirectory(), "StaticFiles");

        public UserService(DataContext context,
            UserManager userManager,
            RoleManager roleManager,
            ISupervisorUserAgentService supervisorUserAgentService,
            IConfiguration configuration,
            SignInManager signInManager,
            IEmailService emailSender,
            IFileService fileService,
            IMapper mapper,
            IValidator<SaveUserDto> fluentValidatorUser,
            IValidator<LoginDto> fluentValidatorLogin,
            IValidator<CreateUserDto> fluentValidatorCreateUser,
            IValidator<ResetPassword> fluentValidationResetPassword,
            IAspNetUserRolesService rolesService)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _fileService = fileService;
            _mapper = mapper;
            _fluentValidatorUser = fluentValidatorUser;
            _fluentValidatorLogin = fluentValidatorLogin;
            _fluentValidatorCreateUser = fluentValidatorCreateUser;
            _fluentValidationResetPassword = fluentValidationResetPassword;
            _rolesService = rolesService;
            _supervisorUserAgentService = supervisorUserAgentService;
        }

        public ServiceResult<List<AllUserDto>> GetAll()
        {
            var result = _context.Users.Select(x => x)
                .Where(x => x.IsDeleted != true)
                .ProjectTo<AllUserDto>().ToList();

            return new ServiceResult<List<AllUserDto>>(result);
        }

        public ServiceResult<PagedResult<AllUserDto>> GetPageUser(UserFilterDto filters)
        {
            const int pageSize = 10;

            var resultFull = _context.Users
                .Include(dist => dist.Distribution)
                .Where(
                    x =>
                        (!filters.DistributionId.HasValue || x.DistributionId == filters.DistributionId)
                        &&
                        (string.IsNullOrEmpty(filters.FirstName) || x.FirstName.ToUpper().Contains(filters.FirstName.ToUpper()) )
                        &&
                        (string.IsNullOrEmpty(filters.Dni) || x.Dni.ToString().ToUpper().Contains(filters.Dni.ToString().ToUpper()) )
                        &&
                        (!x.IsDeleted)
                        &&
                        (!x.SuperAdmin)
                );

            var resultPage = resultFull.Skip((filters.Page ?? 0) * pageSize)
                .Take(pageSize)
                .ProjectTo<AllUserDto>()
                .ToList();

            if (resultPage.Count() == 0 && filters.Page > 0)
            {
                resultPage = resultFull.Skip( ( (filters.Page ?? 0) - 1) * pageSize)
                    .Take(pageSize)
                    .ProjectTo<AllUserDto>()
                    .ToList();
            }
            return new ServiceResult<PagedResult<AllUserDto>>(
                     new PagedResult<AllUserDto>()
                     {
                         List = resultPage,
                         TotalRecords = resultFull.Count()
                     }
            );
        }

        public async Task<ServiceResult<UserDto>> Authenticate(LoginDto p_LoginDto)
        {
            var firstValidation = _fluentValidatorLogin.Validate(p_LoginDto);

            if (!firstValidation.IsValid)
            {
                return firstValidation.ToServiceResult<UserDto>(null);
            }

            ServiceResult<UserDto> result = new ServiceResult<UserDto>();

            var user = await _userManager.Users.SingleOrDefaultAsync(x => x.UserName == p_LoginDto.Usuario);
            var passwordResult = await _userManager.CheckPasswordAsync(user, p_LoginDto.Password);

            if (user == null || !passwordResult || user.IsDeleted == true)
            {
                const NotificationType notificationType = NotificationType.Error;
                result.AddNotification(notificationType, "Usuario y/o Contraseña Incorrecto");
                return result;
            }
            

            UserDto userDto = new UserDto();

            var token = GenerateJwtTokenHandlerAsync(user.Email, user);
            var filePath = _fileService.GetByIdFile(user.Id);

            userDto.Id = user.Id;
            userDto.Dni = user.Dni;
            userDto.FirstName = user.FirstName;
            userDto.LastName = user.LastName;
            userDto.UserName = user.UserName;
            userDto.PhoneNumber = user.PhoneNumber;
            userDto.Token = await token;
            userDto.Path = filePath.Response.Paths;
            userDto.CategoryId = user.CategoryId;
            userDto.Roles = _rolesService.FindByIdRoles(user.Id).Result.Response;
            userDto.RolesNames = _rolesService.RolesNames(user.Id).Response;
            result.Response = userDto;
            return result;
        }

        public void Delete(Guid id)
        {
            var userToDelete = _userManager.Users.FirstOrDefault(x => x.Id == id);

            if (userToDelete != null)
            {
                //primero remuevo los permisos del usuario
                var userRoles = _context.UserRoles.ToList();
                userRoles.RemoveAll(x => x.UserId == id);
                var userAgent = _context.SupervisorUserAgents.FirstOrDefault(x => x.AgentId == userToDelete.Id);
                userAgent.IsDeleted = true;
                _context.SupervisorUserAgents.Update(userAgent);
                userToDelete.IsDeleted = true;
                //luego elimino el usuario
                _context.Users.Update(userToDelete);

                _context.SaveChanges();
            }
        }

        public async Task UpdateUserRoleWhenModify(Guid userId, Guid roleId, bool rolBelongUser)
        {

            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);
            var role = await _roleManager.Roles.FirstOrDefaultAsync(x => x.Id == roleId);

            if (user != null && role != null)
            {
                var currentRoles = await _context.UserRoles.FirstOrDefaultAsync(x => x.RoleId == roleId && x.UserId == userId);

                if (currentRoles != null)
                {
                    //si el rol existe y esta destildado
                    if (!rolBelongUser)
                    {
                        var result = await _userManager.RemoveFromRoleAsync(user, role.Name);
                    }
                }
                else
                {
                    if (rolBelongUser)
                    {
                        //no tiene roles por ende se le agrega
                        var result = await _userManager.AddToRoleAsync(user, role.Name);
                    }
                }

            }
        }

        public async Task UpdateProfileAsAdmin(UpdateProfileAsAdmin userParam)
          {
              var user = _context.Users.Find(userParam.Id);

              if (user == null)
                  throw new AppException("User not found");

              if (userParam.UserName != user.UserName)
              {
                  // username has changed so check if the new username is already taken
                  if (_context.Users.Any(x => x.UserName == userParam.UserName))
                      throw new AppException("El usuario " + userParam.UserName + " ya existe en la db.");
              }
               
              // update user properties
              user.Dni = userParam.Dni;
              user.UserName = userParam.UserName;
              user.Email = userParam.UserName;
              user.PhoneNumber = userParam.PhoneNumber;
              user.DistributionId = userParam.DistributionId;
              user.FirstName = userParam.FirstName;
              user.LastName = userParam.LastName;
              user.CategoryId = userParam.CategoryId;

              //actualizo los roles del usuario
            foreach (var role in userParam.RolesUser)
              {
                  await UpdateUserRoleWhenModify(user.Id, role.Id, role.RolBelongUser);
              }


              // update password if it was entered
              if (!string.IsNullOrWhiteSpace(userParam.Password))
              {
                  user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, userParam.Password);
              }

              var ministro = userParam.RolesUser.FirstOrDefault(x => x.Name == Role.Ministro && x.RolBelongUser == true);
              var admin = userParam.RolesUser.FirstOrDefault(x => x.Name == Role.Admin && x.RolBelongUser == true);
              var newrelationshipAgentsupervisor = new CreateSupervisorAgentDto();
              var RelationshipTodelete = _context.SupervisorUserAgents.FirstOrDefault(sup => sup.AgentId == user.Id);

            newrelationshipAgentsupervisor.AgentId = user.Id;

            if (ministro == null && admin == null)
              {
                  if (userParam.SupervisorAgentId.CompareTo(Guid.Empty) != 0)
                  {
                      newrelationshipAgentsupervisor.SupervisorId = userParam.SupervisorAgentId;
                  }

                  if (userParam.SupervisorAgentId2.CompareTo(Guid.Empty) != 0)
                  {
                      newrelationshipAgentsupervisor.SupervisorId2 = userParam.SupervisorAgentId2;
                  }

                  _supervisorUserAgentService.Create(new List<CreateSupervisorAgentDto>()
                  {
                      new CreateSupervisorAgentDto()
                      {
                          SupervisorId = newrelationshipAgentsupervisor.SupervisorId,
                          SupervisorId2 = newrelationshipAgentsupervisor.SupervisorId2,
                          AgentId = newrelationshipAgentsupervisor.AgentId
                      }
                  });
              }
            //Elimino la relación actual entre supervisor y agente para crear una nueva
              if (RelationshipTodelete != null)
              {
                  _context.SupervisorUserAgents.Remove(RelationshipTodelete);
              }
            _context.Users.Update(user);
            _context.SaveChanges();
          }

        public async Task<ServiceResult<UpdateMyProfile>> UpdateMyProfile(UpdateMyProfile userParam)
        {
            var user = _context.Users.Find(userParam.Id);

            if (user == null)
                throw new AppException("User not found");

            if (userParam.UserName != user.UserName)
            {
                // username has changed so check if the new username is already taken
                if (_context.Users.Any(x => x.UserName == userParam.UserName))
                    throw new AppException("El usuario " + userParam.UserName + " ya existe en la db.");
            }

            // update user properties
            user.Dni = userParam.Dni;
            user.UserName = userParam.UserName;
            user.Email = userParam.UserName;
            user.PhoneNumber = userParam.PhoneNumber;
            user.FirstName = userParam.FirstName;
            user.LastName = userParam.LastName;
            user.DistributionId = userParam.DistributionId;

            //actualizo los roles del usuario
            foreach (var role in userParam.RolesUser)
            {
                await UpdateUserRoleWhenModify(user.Id, role.Id, role.RolBelongUser);
            }


            // update password if it was entered
            if (!string.IsNullOrWhiteSpace(userParam.Password))
            {
                user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, userParam.Password);
            }
            _context.Users.Update(user);
            _context.SaveChanges();
            return new ServiceResult<UpdateMyProfile>(userParam);
        }

        public async Task<ServiceResult<CreateUserDto>> CreateAsync(CreateUserDto user)
        {
            var valid = _fluentValidatorCreateUser.Validate(user);

            if (!valid.IsValid)
            {
                return _mapper.Map<ServiceResult<CreateUserDto>>(valid.ToServiceResult<CreateUserDto>(null));
            }

            var notifications = new ServiceResult<CreateUserDto>();
            var userExistOrNot = await _userManager.FindByNameAsync(user.UserName);
            var distribution = _context.Distributions.FirstOrDefault(x => x.Id == user.DistributionId);
            if (userExistOrNot == null)
            {
                var NewUser = new User
                {
                    Id = Guid.NewGuid(),
                    Dni = user.Dni,
                    UserName = user.UserName,
                    Email = user.UserName,
                    PhoneNumber = user.PhoneNumber,
                    Distribution = distribution,
                    DistributionId = user.DistributionId,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    CategoryId = user.CategoryId
                };
                var result = await _userManager.CreateAsync(NewUser, user.Password);

                // update password if it was entered
                if (!string.IsNullOrWhiteSpace(user.Password))
                {
                    NewUser.PasswordHash = _userManager.PasswordHasher.HashPassword(NewUser, user.Password);
                }

                //actualizo los roles del usuario
                foreach (var role in user.RolesUser)
                {
                    await UpdateUserRoleWhenModify(NewUser.Id, role.Id, role.RolBelongUser);
                }

                if (!result.Succeeded)
                {
                    return result.ToServiceResult<CreateUserDto>(null);
                }

                var newSupervisor = new SupervisorUserAgent()
                {
                    Id = new Guid(),
                    Agents = NewUser,
                };

                if (user.SupervisorAgentId.CompareTo(Guid.Empty) != 0)
                {
                    newSupervisor.SupervisorId = user.SupervisorAgentId;
                }

                if (user.SupervisorAgentId2.CompareTo(Guid.Empty) != 0)
                {
                    newSupervisor.SupervisorId2 = user.SupervisorAgentId2;
                }

                _context.SupervisorUserAgents.Add(newSupervisor);
                _context.SaveChanges();

                string template = Path.Combine(StaticFilesDirectory, "Templates");
                var engine = new RazorLightEngineBuilder()
                    .UseFilesystemProject(template)
                    .UseMemoryCachingProvider()
                    .Build();

                var callbackUrl = string.Format(_configuration["AppSettings:baseUrl"] + "/login");

                var html = new CreateUserHtmlDto()
                {
                    LastName = user.LastName,
                    FirstName = user.FirstName,
                    UserName = user.UserName,
                    Password = user.Password,
                    CallbackUrl = callbackUrl
                };

                string resultHtml = await engine.CompileRenderAsync("Email/whenAnUserIsCreated.cshtml", html);

                var emailSended = await _emailSender.SendEmail(NewUser.Email,"Alta al sistema", resultHtml);

                if (!(emailSended.StatusCode == HttpStatusCode.Accepted))
                {
                    if (emailSended.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        notifications.AddError("error", "La clave api expiró , está mal escrito o es errónea");
                    }
                    else
                    {
                        notifications.AddError("error", "La solicitud no pudo ser enviada al correo del supervisor.");
                    }

                    return notifications;
                }
            }



            
            return new ServiceResult<CreateUserDto>(user);
        }

        //Create JWToken
        private async Task<string> GenerateJwtTokenHandlerAsync(string email, User user)
        {
            var Token = "";
            if (user != null)
            {
                // authentication successful so generate jwt token
                var tokenHandler = new JwtSecurityTokenHandler();

                //Settings
                string set = _configuration.GetSection("AppSettings").GetSection("Secret").Value;
                var key = Encoding.ASCII.GetBytes(set);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("NameIdentifier", user.Id.ToString()),
                        new Claim("Name", user.UserName),
                        new Claim("SerialNumber", user.Dni.ToString()),
                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var rolesUser = _rolesService.FindClaimsByIdRoles(user.Id).Result;
                if (rolesUser.IsSuccess)
                {
                    tokenDescriptor.Subject.AddClaims(rolesUser.Response.Distinct());
                }
                var token = tokenHandler.CreateToken(tokenDescriptor);

                Token = tokenHandler.WriteToken(token);
            }

            return Token;
        }

        public async Task<ServiceResult<UserDto>> Register(SaveUserDto model)
        {
            var firstValidation = _fluentValidatorUser.Validate(model);

            if (!firstValidation.IsValid)
            {
                return firstValidation.ToServiceResult<UserDto>(null);
            }

            var user = new User
            {
                Id = Guid.NewGuid(),
                Dni = model.Dni,
                UserName = model.Email,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber
            };
            
            var result = await _userManager.CreateAsync(user, model.Password);
            
            if (!result.Succeeded)
            {
                return result.ToServiceResult<UserDto>(null);
            }

            await _signInManager.SignInAsync(user, false);

            var token = GenerateJwtTokenHandlerAsync(model.Email, user);
            var userDto = new UserDto
            {
                Id = user.Id,
                Dni = user.Dni,
                UserName = user.UserName,
                PhoneNumber = user.PhoneNumber,
                Token = await token,
                Path = ""
            };
            return new ServiceResult<UserDto>(userDto);

        }

        public async Task UpdateUserRole(Guid userId, Guid roleId)
        {

            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);
            var role = await _roleManager.Roles.FirstOrDefaultAsync(x => x.Id == roleId);

            if (user != null && role != null)
            {
                var currentRoles = await _userManager.GetRolesAsync(user);

                foreach (var currentRole in currentRoles)
                {
                    await _userManager.RemoveFromRoleAsync(user, currentRole);
                }

                var result = await _userManager.AddToRoleAsync(user, role.Name);

                if (!result.Succeeded)
                {

                }
            }
        }

        public async Task<ServiceResult<string>> ForgotPassword(ForgotPasswordDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            var notif = new ServiceResult<string>();

            if (user == null)
            {
                notif.AddError("Error","Su correo no se encuentra registrado.");
                return notif;
            }

            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            var codeScaped = Uri.EscapeDataString(code);

            //var callbackUrl = string.Format(_configuration["AppSettings:"]+"/CambiarPassword/{0}/{1}", Uri.EscapeDataString(code), user.Id);
            var callbackUrl =
                string.Format(_configuration["AppSettings:baseUrl"] + "/CambiarPassword?code={0}&userId={1}",codeScaped,
                    user.Id);
            UserRecoveryPassword userRecoveryPasswordModel = new UserRecoveryPassword()
            {
                LastName = user.LastName,
                FirstName = user.FirstName,
                UrlCallBack = callbackUrl
            };
            string template = Path.Combine(StaticFilesDirectory, "Templates");
            var engine = new RazorLightEngineBuilder()
                .UseFilesystemProject(template)
                .UseMemoryCachingProvider()
                .Build();

            string result = await engine.CompileRenderAsync("Email/recoveryPassword.cshtml", userRecoveryPasswordModel);

            await _emailSender.SendEmail(model.Email, "Reset Password",result);

            return new ServiceResult<string>(model.Email);
        }

        public async Task<ServiceResult<ResetPassword>> ResetPassword(ResetPassword model)
        {
            var notif = new ServiceResult<ResetPassword>();
            var valid = _fluentValidationResetPassword.Validate(model);
            if (!valid.IsValid)
            {
                return valid.ToServiceResult<ResetPassword>(model); 
            }

            if (!model.Password.Any(char.IsUpper))
            {
                notif.AddError("Error", "La contraseña debe contener al menos una mayuscula ('A','Z')");
            }

            if (!model.Password.Any(char.IsLower))
            {
                notif.AddError("Error", "La contraseña debe contener al menos una minuscula ('a','z')");
            }

            if (notif.Errors.Count() > 0)
            {
                return notif;
            }

            var user = _userManager.Users.FirstOrDefault(x => x.Id == model.UserId);
            if (user != null)
            {
                var unescapedString = Uri.UnescapeDataString(model.PasswordResetToken);
                var result = await _userManager.ResetPasswordAsync(user, unescapedString, model.Password);

                if (!result.Succeeded)
                {
                   var newResult = new ServiceResult<ResetPassword>();
                    foreach (var identityError in result.Errors)
                    {
                        var error = (identityError.Code.Equals("InvalidToken"))
                            ? "El token expiró o es erróneo."
                            : "";
                        newResult.AddError("Error", error);
                    }
                    return newResult;
                }
            }

            return new ServiceResult<ResetPassword>();
        }


    }
}
