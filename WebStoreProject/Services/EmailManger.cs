using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebStoreProject.Services
{
    public class EmailManger : IEmailManger
    {
        public void SendEmail(string content, string email, string subject)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("WebStore", "yad2selaproject@gmail.com"));
            message.To.Add(new MailboxAddress("Me", email));
            message.Subject = subject;  
            
            message.Body = new TextPart("plain")
            {
                Text = content
            };
            using(var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, false);
                client.Authenticate("yad2selaproject@gmail.com", "203216395");
                client.Send(message);
                client.Disconnect(true);
            }
        }
    }
}
