using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VR.Dto
{

    public class LoginValidator : AbstractValidator<LoginDto>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Usuario).NotEmpty().WithName("Usuario");
            RuleFor(x => x.Password).NotEmpty().WithName("Contraseña");
        }
    }

    public class LoginDto
    {
        public string Usuario { set; get; }
        public string Password { set; get; }
    }
}
