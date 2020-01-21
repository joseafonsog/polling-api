using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace PollingApp.Commons.Infrastructure
{
    public interface ISmtpClient
    {
        SmtpDeliveryMethod DeliveryMethod { get; set; }
        string PickupDirectoryLocation { get; set; }
        void Send(MailMessage mailMessage);
    }
}
