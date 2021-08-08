using App.Core.Configuration.Email;
using App.Core.Domain.Authentication;
using App.Core.Domain.User;
using App.WebAPI.Models.Authentication;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.WebAPI.Factories.Auth
{
    public interface IAuthFactory
    {
        public RegistrationResponseModel PrepareRegistrationResponseModel(IdentityResult identityResultModel = null);
        public UserInfo PrepareUserInfo(RegistrationModel registrationModel, ApplicationUser applicationUser);
        EmailRequest PrepareRecoveryPasswordEmailRequest(string toEmail, string recoveryPassworLink);
    }
}
