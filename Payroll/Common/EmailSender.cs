using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Payroll.Common
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var mail = new MailMessage("oromar.melo@gmail.com", email);
            var client = new SmtpClient
            {
                Port = 587,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                EnableSsl = true,
                Credentials = new System.Net.NetworkCredential("oromar.melo@gmail.com", "085471li#"),
                Host = "smtp.gmail.com"
            };
            mail.Subject = subject;
            mail.Body = htmlMessage;
            return client.SendMailAsync(mail);
        }
    }
}
