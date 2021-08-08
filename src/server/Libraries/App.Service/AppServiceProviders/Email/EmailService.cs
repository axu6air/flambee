using App.Core.Configuration.Email;
using App.Core.Domain.Authentication;
using App.Service.AppServiceProviders.User;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace App.Service.AppServiceProviders.Email
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        private readonly IUserInfoService _userDetailsService;

        public EmailService(IConfiguration configuration,
            IUserInfoService userDetailsService)
        {
            _configuration = configuration;
            _userDetailsService = userDetailsService;
        }

        public EmailConfiguration GetEmailConfiguration(EmailType emailType)
        {
            var emailConfiguration = _configuration.GetSection("EmailConfigurations")
                .Get<List<EmailConfiguration>>()
                .Where(x => x.Type == emailType).FirstOrDefault();

            return emailConfiguration;
        }

        public async Task<bool> SendPasswordRecoveryMessageAsync(ApplicationUser applicationUser, string emailTemplate = null)
        {
            return true;
        }

        public async Task<bool> SendEmailAsync(EmailRequest emailRequest)
        {
            EmailConfiguration emailConfiguration = GetEmailConfiguration(emailRequest.EmailType);
            return await SendEmailAsync(emailRequest, emailConfiguration);
        }

        private static async Task<bool> SendEmailAsync(EmailRequest emailRequest, EmailConfiguration emailConfiguration)
        {
            try
            {
                var email = new MimeMessage();
                email.Sender = MailboxAddress.Parse(emailConfiguration.Email);
                email.To.Add(MailboxAddress.Parse(emailRequest.ToEmail));
                email.Subject = emailRequest.Subject;
                var builder = new BodyBuilder();
                if (emailRequest.Attachments != null)
                {
                    byte[] fileBytes;
                    foreach (var file in emailRequest.Attachments)
                    {
                        if (file.Length > 0)
                        {
                            using (var ms = new MemoryStream())
                            {
                                file.CopyTo(ms);
                                fileBytes = ms.ToArray();
                            }
                            builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
                        }
                    }
                }
                builder.HtmlBody = emailRequest.Body;
                email.Body = builder.ToMessageBody();
                using var smtp = new SmtpClient();
                smtp.Connect(emailConfiguration.Host, emailConfiguration.Port, SecureSocketOptions.StartTls);
                smtp.Authenticate(emailConfiguration.Email, emailConfiguration.Password);
                await smtp.SendAsync(email);
                smtp.Disconnect(true);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
