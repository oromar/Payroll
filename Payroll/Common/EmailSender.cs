using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Payroll.Common
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var mail = new MailMessage("payrollapp123@gmail.com", email);
            var client = new SmtpClient
            {
                Port = 587,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                EnableSsl = true,
                Credentials = new System.Net.NetworkCredential("payrollapp123@gmail.com", "321654cba"),
                Host = "smtp.gmail.com"
            };
            mail.Subject = subject;
            mail.Body = htmlMessage;
            return client.SendMailAsync(mail);
        }
    }
}
