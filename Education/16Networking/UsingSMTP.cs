using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Education._16Networking
{
    class UsingSMTP
    {
        public void DoSmth()
        {
            SmtpClient client = new SmtpClient();
            client.Host = "mail.myisp.net";
            MailMessage mm = new MailMessage();

            mm.Sender = new MailAddress("kay@domain.com", "Kay");
            mm.From = new MailAddress("kay@domain.com", "Kay");
            mm.To.Add(new MailAddress("bob@domain.com", "Bob"));
            mm.CC.Add(new MailAddress("dan@domain.com", "Dan"));
            mm.Subject = "Hello!";
            mm.Body = "Hi there. Here's the photo!";
            mm.IsBodyHtml = false;
            mm.Priority = MailPriority.High;

            Attachment a = new Attachment("phot.jpg",
                System.Net.Mime.MediaTypeNames.Image.Jpeg);
            mm.Attachments.Add(a);
            client.Send(mm);
        }
    }
}
