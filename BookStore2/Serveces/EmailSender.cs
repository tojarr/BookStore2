using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace BookStore2.Serveces
{
    public class EmailSender : IEmailSender
    {
        public void SendEmail(string to, string subjMail, string bodyMail)
        {
            // создаем объект сообщения
            MailMessage m = new MailMessage();
            m.To.Add(new MailAddress(to));
            // тема письма
            m.Subject = subjMail;
            // текст письма
            m.Body = bodyMail;
            // письмо представляет код html
            m.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient();
            smtp.EnableSsl = true;
            smtp.Send(m);
        }
    }
}