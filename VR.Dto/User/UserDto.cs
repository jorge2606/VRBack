using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using VR.Data.Model;

namespace VR.Dto.User
{

    public class UserValidator : AbstractValidator<SaveUserDto>
    {
        public UserValidator()
        {
            RuleFor(x => x.Dni).NotEmpty().WithName("Dni");
            RuleFor(x => x.Dni).NotEmpty().WithName("Dni");
            RuleFor(x => x.UserName).NotEmpty().WithName("Usuario");
            RuleFor(x => x.Password).NotEmpty().WithName("Contraseña");
            RuleFor(x => x.Email)
                .EmailAddress()
                .MaximumLength(256).WithName("Email");
            RuleFor(x => x.PhoneNumber).NotEmpty().MinimumLength(8).WithName("Telefóno");
            RuleFor(x => x.PhoneNumber).NotEmpty().MaximumLength(8).WithName("Telefóno");
        }
    }

    public class UserCreateValidator : AbstractValidator<CreateUserDto>
    {
        public UserCreateValidator()
        {
            RuleFor(x => x.Dni.ToString()).NotEmpty().WithName("Dni");
            RuleFor(x => x.UserName).NotEmpty().WithName("Usuario");
            RuleFor(x => x.Password).NotEmpty().WithName("Contraseña");
            RuleFor(x => x.PhoneNumber).NotEmpty().WithName("Telefóno");
        }
    }

    public class SaveUserDto : UserDto
    {
        [Required]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "PASSWORD_MIN_LENGTH", MinimumLength = 6)]
        public string Password { get; set; }
    }

    public class UserDto
    {
        public Guid Id { set; get; }
        public string Dni { set; get; }
        public string UserName { set; get; }
        public string FirstName { set; get; }
        public string LastName { set; get; }
        public string Email { set; get; }
        public string PhoneNumber { set; get; }
        public string Token { get; set; }
        public string Path { get; set; }
        public Guid DistributionId { set; get; }
        public string DistributionName { set; get; }
        public string DistributionDescription { set; get; }
        //public DistributionBaseDto Distribution { set; get; }
        public Guid CategoryId { set; get; }
        public string CategoryName { set; get; }
        public string CategoryDescription { set; get; }
        public Decimal CategoryAdvance { set; get; }
        public Boolean IsDeleted { set; get; }
        public IList<ClaimDto> Roles { set; get; }
        public List<RolNameDto> RolesNames { set; get; }
    }

    public class UserDistribution {
        public string Dni { set; get; }
    }
    


    public class ModifyUserDto
    {
        public Guid Id { get; set; }
        public string Dni { set; get; }
        public string UserName { set; get; }
        public string FirstName { set; get; }
        public string LastName { set; get; }
        public string Password { get; set; }
        public string PhoneNumber { set; get; }
        public List<RoleWhenModifyUser> RolesUser { set; get; }
        public Guid? DistributionId { set; get; }
        public Distribution Distribution { set; get; }
        public Guid CategoryId { set; get; }
        public Guid? OrganismId { set; get; }
        public Guid? SupervisorAgentId { set; get; }
        public Guid? SupervisorAgentId2 { set; get; }
    }

    //admin and common user can use this Dto, because they update its own the same way 
    public class UpdateMyProfile : ModifyUserDto {}

    public class UpdateProfileAsAdmin : CreateUserDto {}

    public class CreateUserDto
    {
        public Guid Id { get; set; }
        public string Dni { set; get; }
        public string UserName { set; get; }
        public string FirstName { set; get; }
        public string LastName { set; get; }
        public string Password { get; set; }
        public string PhoneNumber { set; get; }
        public List<RoleWhenModifyUser> RolesUser { set; get; }
        public Guid DistributionId { set; get; }
        public Guid CategoryId { set; get; }
        public Guid SupervisorAgentId { set; get; }
        public Guid SupervisorAgentId2 { set; get; }
    }

    public class CreateUserHtmlDto
    {
        public string FirstName { set; get; }
        public string LastName { set; get; }
        public string UserName { set; get; }
        public string Password { set; get; }
        public string CallbackUrl { set; get; }
    }

    public class AllUserDto
    {
        public Guid Id { get; set; }
        public string Dni { set; get; }
        public string FirstName { set; get; }
        public string LastName { set; get; }
        public string UserName { set; get; }
        public string PhoneNumber { set; get; }
        public Guid DistributionId { set; get; }
        public Distribution Distribution { set; get; }
    }


    public class UserAuthenticationDto
    {
        public string Token { get; set; }
    }

    public class UserOfSolicitationSubsidy
    {
        public Guid Id { set; get; }
        public string Dni { set; get; }
        public string UserName { set; get; }
        public string PhoneNumber { set; get; }
    }
}
