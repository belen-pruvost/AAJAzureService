using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AAJAzureService.DataObjects;
using System.Net.Mail;

namespace AAJAzureService.Utilities
{
    public static class MailsServices
    {
        public static void SendCodeConfirmationEmail(User user)
        {
            //Send Email
            // Create a mail message
            var message = new MailMessage();

            // Add recipient
            message.To.Add(new MailAddress(user.Email, user.FirstName));
            //added an email to test if works fine
            message.To.Add(new MailAddress("calvagna.federico@aajtech.com", "Federico Calvagna"));

            // Set subject
            message.Subject = "Welcome to AAJ Mobile app";

            // Set HTML body
            message.Body = "Your verification code is:"+ user.CodeConfirmation;
            message.IsBodyHtml = true;

            // Create the smtp client
            var smtpClient = new SmtpClient();

            // Send the message
            smtpClient.Send(message);
        }
    }
}
