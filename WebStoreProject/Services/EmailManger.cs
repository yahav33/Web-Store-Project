using MailKit.Net.Smtp;
using MimeKit;
using System.IO;

namespace WebStoreProject.Services
{
    public class EmailManger : IEmailManger
    {
        public void SendEmail(string content, string email, string subject, string path = null)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("WebStore", "yad2selaproject@gmail.com"));
            message.To.Add(new MailboxAddress("Me", email));
            message.Subject = subject;

            var body = new TextPart("html")
            {
                Text = content
            };

            if(path != null)
            {
                var attachment = new MimePart("image","jpg")
                {
                    Content = new MimeContent(File.OpenRead(path)),
                    ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                    ContentTransferEncoding = ContentEncoding.Base64,
                    FileName = Path.GetFileName(path)
                };

               
                var multipart = new Multipart("mixed");
                multipart.Add(body);
                multipart.Add(attachment);

                // now set the multipart/mixed as the message body
                message.Body = multipart;
            }
            else message.Body = body;
            
            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, false);
                client.Authenticate("yad2selaproject@gmail.com", "203216395");
                client.Send(message);
                client.Disconnect(true);
            }
        }
    }
}
