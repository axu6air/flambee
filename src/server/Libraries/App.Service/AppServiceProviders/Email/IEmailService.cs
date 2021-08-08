using App.Core.Configuration.Email;
using App.Core.Domain.Authentication;
using System.Threading.Tasks;

namespace App.Service.AppServiceProviders.Email
{
    public interface IEmailService
    {
        EmailConfiguration GetEmailConfiguration(EmailType emailType);
        Task<bool> SendPasswordRecoveryMessageAsync(ApplicationUser applicationUser, string emailTemplate = null);
        Task<bool> SendEmailAsync(EmailRequest emailRequest);
    }

}
