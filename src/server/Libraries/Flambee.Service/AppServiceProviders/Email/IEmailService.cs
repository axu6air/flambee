using Flambee.Core.Configuration.Email;
using Flambee.Core.Domain.Authentication;
using System.Threading.Tasks;

namespace Flambee.Service.AppServiceProviders.Email
{
    public interface IEmailService
    {
        EmailConfiguration GetEmailConfiguration(EmailType emailType);
        Task<bool> SendPasswordRecoveryMessageAsync(ApplicationUser applicationUser, string emailTemplate = null);
        Task<bool> SendEmailAsync(EmailRequest emailRequest);
    }

}
