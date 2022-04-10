using System.Threading.Tasks;

namespace WebApi.Domain.Interfaces.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}