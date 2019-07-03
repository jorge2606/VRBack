using System.ComponentModel.DataAnnotations;

namespace VR.Dto
{
    public class ForgotPasswordDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
