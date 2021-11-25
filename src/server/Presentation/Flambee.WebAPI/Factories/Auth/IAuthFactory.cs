using Flambee.Core.Configuration.Email;
using Flambee.Core.Domain.UserDetails;
using Flambee.WebAPI.Models.Authentication;
using Microsoft.AspNetCore.Identity;

namespace Flambee.WebAPI.Factories.Auth
{
    public interface IAuthFactory
    {
        public RegistrationResponseModel PrepareRegistrationResponseModel(IdentityResult identityResultModel = null, User applicationUser = null);
        public UserInfo PrepareUserInfo(RegistrationModel registrationModel, User applicationUser);
        public EmailRequest PrepareRecoveryPasswordEmailRequest(string toEmail, string recoveryPassworLink);
        public (bool isUsername, bool isEmail, bool isPhoneNumber) DetermineLoginMethod(string appliedUsername);
    }
}
