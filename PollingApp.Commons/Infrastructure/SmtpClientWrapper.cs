using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace PollingApp.Commons.Infrastructure
{
    public class SmtpClientWrapper : ISmtpClient
    {
        public SmtpClient SmtpClient { get; set; }
        public SmtpDeliveryMethod DeliveryMethod { get; set; }
        public string PickupDirectoryLocation { get; set; }

        public SmtpClientWrapper(string host, int port)
        {
            SmtpClient = new SmtpClient(host, port);
        }

        public SmtpClientWrapper()
        {
            SmtpClient = new SmtpClient();
        }

        public void Send(MailMessage mailMessage)
        {
            SmtpClient.DeliveryMethod = DeliveryMethod;
            SmtpClient.PickupDirectoryLocation = PickupDirectoryLocation;
            SmtpClient.Send(mailMessage);
        }
    }
}
