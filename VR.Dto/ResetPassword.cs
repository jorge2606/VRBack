using System;
using System.Linq;
using FluentValidation;
using FluentValidation.Validators;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Internal;

namespace VR.Dto
{
    public class ResetPasswordValidator : AbstractValidator<ResetPassword> {
        public ResetPasswordValidator()
        {
            RuleFor(x => x.Password)
                .MinimumLength(6)
                .WithName("Contraseña")
                .WithMessage("La contraseña debe ser al menos de 6 caracteres.");
            //RuleFor(x => x.Password)
            //    .WithName("Contraseña")
            //    .WithMessage("La contrseña debe contener al menos una mayúscula ('A','Z')");
            //RuleFor(x => x.Password).Equal(x => x.PasswordConfirm)
            //    .WithName("Contraseña")
            //    .WithMessage("La contrseña no coinciden");
        }
    }
    public class ResetPassword
    {
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
        public Guid UserId { get; set; }
        public string PasswordResetToken { get; set; }
    }
}
