using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;
using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using C3xPAWM.Models.Options;
using System;

namespace C3xPAWM.Models.Services.Infrastructure
{
    public class MailKitEmailSender : IEmailSender
    {

        private readonly IOptionsMonitor<SmtpOptions> smtpOptionsMonitor;
        private readonly ILogger<MailKitEmailSender> logger;
        public MailKitEmailSender(IOptionsMonitor<SmtpOptions> smtpOptionsMonitor, ILogger<MailKitEmailSender> logger)
        {
            this.logger = logger;
            this.smtpOptionsMonitor = smtpOptionsMonitor;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            try
            {
                var options = this.smtpOptionsMonitor.CurrentValue;
                using var client = new SmtpClient();
                await client.ConnectAsync("smtp.mailtrap.io", options.Port, options.Security);
                if (!string.IsNullOrEmpty(options.Username))
                {
                    await client.AuthenticateAsync(options.Username, options.Password);
                }
                var message = new MimeMessage();
                message.From.Add(MailboxAddress.Parse(options.Sender));
                message.To.Add(MailboxAddress.Parse(email));
                message.Subject = subject;
                message.Body = new TextPart("html")
                {
                    Text = htmlMessage
                };
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
            catch (Exception exc)
            {
                logger.LogError(exc, "Non riesco a inivare a {email} con messaggio > {message}", email, htmlMessage);
            }
        }


    }
}