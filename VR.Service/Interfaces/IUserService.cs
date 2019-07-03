using Service.Common.ServiceResult;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VR.Dto;
using VR.Dto.User;
using VR.Web.Helpers;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace VR.Service.Interfaces
{

    public interface IUserService
    {
        Task<ServiceResult<UserDto>> Authenticate(LoginDto p_LoginDto);
        Task<ServiceResult<UpdateMyProfile>> UpdateMyProfile(UpdateMyProfile user);
        Task UpdateProfileAsAdmin(UpdateProfileAsAdmin user);
        Task<ServiceResult<CreateUserDto>> CreateAsync(CreateUserDto user);
        void Delete(Guid id);
        Task<ServiceResult<UserDto>> Register(SaveUserDto model);
        Task UpdateUserRole(Guid idUser, Guid id);
        Task<ServiceResult<string>> ForgotPassword(ForgotPasswordDto model);
        Task<ServiceResult<ResetPassword>> ResetPassword(ResetPassword model);
        ServiceResult<PagedResult<AllUserDto>> GetPageUser(UserFilterDto filters);
        ServiceResult<List<AllUserDto>> GetAll();
    }
    
}
