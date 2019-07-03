using SendGrid;
using System.Threading.Tasks;

namespace VR.Service.Services
{
    public interface IEmailService
    {
        Task<Response> SendEmail(string email, string subject, string message);
    }
}