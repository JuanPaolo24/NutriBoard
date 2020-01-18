using Microsoft.AspNetCore.Hosting;
using MimeKit;
using System;
using System.Threading.Tasks;
using MailKit.Net.Smtp;

namespace NutriBoard.Web.Services
{
    public class EmailService : IEmailService
    {
        private readonly IHostingEnvironment _environment;
        public EmailService(IHostingEnvironment environment)
        {
            _environment = environment;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            try
            {
                var mimeMessage = new MimeMessage();

                mimeMessage.From.Add(new MailboxAddress("NutriBoard", "azurecloud24@gmail.com"));
                mimeMessage.To.Add(new MailboxAddress(email));
                mimeMessage.Subject = subject;
                mimeMessage.Body = new TextPart("html")
                {
                    Text = message
                };


                using (var client = new SmtpClient())
                {
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                    if (_environment.IsDevelopment())
                    {
                        await client.ConnectAsync("smtp.gmail.com", 465, true);
                    }
                    else
                    {
                        await client.ConnectAsync("smtp.gmail.com");
                    }

                    // TODO: Add a proper email for sending the confirmation link from
                    await client.AuthenticateAsync("username", "password");

                    await client.SendAsync(mimeMessage);

                    await client.DisconnectAsync(true);
                }



            } catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }
    }
}
