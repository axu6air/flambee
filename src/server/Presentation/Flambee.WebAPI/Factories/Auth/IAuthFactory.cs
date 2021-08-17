using Flambee.Core.Configuration.Email;
using Flambee.Core.Domain.Authentication;
using Flambee.Core.Domain.User;
using Flambee.WebAPI.Models.Authentication;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flambee.WebAPI.Factories.Auth
{
    public interface IAuthFactory
    {
        public RegistrationResponseModel PrepareRegistrationResponseModel(IdentityResult identityResultModel = null, ApplicationUser applicationUser = null);
        public UserInfo PrepareUserInfo(RegistrationModel registrationModel, ApplicationUser applicationUser);
        EmailRequest PrepareRecoveryPasswordEmailRequest(string toEmail, string recoveryPassworLink);
    }
}
