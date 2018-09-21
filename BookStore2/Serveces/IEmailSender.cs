using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore2.Serveces
{
    public interface IEmailSender
    {
        void SendEmail(string to, string subjMail, string body);
    }
}
