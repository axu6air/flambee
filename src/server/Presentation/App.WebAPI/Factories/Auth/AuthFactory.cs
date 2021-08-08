﻿using App.Core.Configuration.Email;
using App.Core.Domain.Authentication;
using App.Core.Domain.User;
using App.WebAPI.Models.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;
using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace App.WebAPI.Factories.Auth
{
    public class AuthFactory : IAuthFactory
    {
        private readonly IWebHostEnvironment _environment;

        public AuthFactory(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public RegistrationResponseModel PrepareRegistrationResponseModel(IdentityResult identityResultModel = null)
        {
            if (identityResultModel == null)
                return new RegistrationResponseModel
                {
                    Succeeded = false,
                    Errors = new List<string> { "User already exists" },
                    Status = StatusCodes.Status400BadRequest
                };

            var registrationResponseModel = new RegistrationResponseModel
            {
                Succeeded = identityResultModel.Succeeded,
                Errors = identityResultModel.Errors.Select(x => x.Description).ToList(),
                Status = identityResultModel.Succeeded ? StatusCodes.Status200OK : StatusCodes.Status400BadRequest,
                Message = identityResultModel.Succeeded ? "Successfully registered" : "Registration unsuccessful"
            };

            return registrationResponseModel;
        }

        public UserInfo PrepareUserInfo(RegistrationModel registrationModel, ApplicationUser applicationUser)
        {
            return new UserInfo
            {
                FirstName = registrationModel.FirstName,
                LastName = registrationModel.LastName,
                PhoneNumber = registrationModel.PhoneNumber,
                DateOfBirth = registrationModel.DateOfBirth,
                ApplicationUser = applicationUser,
                ApplicationUserId = applicationUser.Id
            };
        }

        public EmailRequest PrepareRecoveryPasswordEmailRequest(string toEmail, string recoveryPassworLink)
        {
            try
            {
                var pathToFile = _environment.WebRootPath
                                + Path.DirectorySeparatorChar.ToString()
                                + "UI"
                                + Path.DirectorySeparatorChar.ToString()
                                + "EmailTemplates"
                                + Path.DirectorySeparatorChar.ToString()
                                + "ForgetPassword.html";

                var builder = new BodyBuilder();
                using (StreamReader SourceReader = System.IO.File.OpenText(pathToFile))
                {
                    builder.HtmlBody = SourceReader.ReadToEnd();
                    string emailTemplateText = File.ReadAllText(pathToFile);

                    //string messageBody = string.Format(emailTemplateText,
                    //            DateTime.Now.Date.ToShortDateString());

                    string messageBody = emailTemplateText.Replace("[resetLink]", recoveryPassworLink);

                    EmailRequest emailRequest = new EmailRequest
                    {
                        EmailType = EmailType.ForgetPassword,
                        Subject = "Forget Password",
                        ToEmail = toEmail,
                        Body = messageBody
                    };
                    return emailRequest;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
