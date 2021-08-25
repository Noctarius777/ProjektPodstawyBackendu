using Microsoft.Extensions.Configuration;
using ProjektPodstawyBackendu.Domain;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;

namespace ProjektPodstawyBackendu.Services
{
  
    public class EmailService : IEmailService
    {
        private readonly string _apiKey;
        public EmailService(IConfiguration configuration) // konstruktor
        {
            _apiKey = configuration["Application:APIKeyForSendGrid"];
        }
        public void SendMessageEmail(string email, string message)
        {
            var client = new SendGridClient(_apiKey);
            var from = new EmailAddress("marszalapi@gmailcom", "Mateusz Marszałek");
            var subject = "Dostałeś nową wiadomość";
            var to = new EmailAddress(email);
            var plainTextContent = $"Masz nową wiadomość: {message}";
            var htmlContent = $"<strong>Masz nową wiadomość: <br /> {message}</strong>";

            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = client.SendEmailAsync(msg).Result;
            if (!response.IsSuccessStatusCode)
            {
                throw new System.Exception("Something Get Wrong");
            }
        }
    }
}
