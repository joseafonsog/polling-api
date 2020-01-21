using Moq;
using PollingApp.Admin;
using PollingApp.Commons.Helpers;
using PollingApp.Commons.Infrastructure;
using PollingApp.Commons.Settings;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using Xunit;

namespace PollingApp.Tests
{
    public class ShareAdminTests
    {
        private readonly IEmailSettings _emailSettings;

        public ShareAdminTests()
        {
            _emailSettings = new EmailSettings
            {
                LocalDirectoryPath = "C:\\temp",
                Sender = "sender@email.com",
                Subject = "Hey see my questions!"
            };
        }

        [Theory]
        [InlineData(null, null)]
        [InlineData("email@domain.com", null)]
        [InlineData(null, "http://google.com")]
        [InlineData("emaildomain.com", "http://google.com")]
        public void SendEmailShouldThrowException(string email, string content)
        {
            // Setup
           ;
            var mock = new Mock<ISmtpClient>();
            mock.Setup(p => p.Send(new MailMessage()));
            var shareAdmin = new ShareAdmin(_emailSettings, mock.Object);

            // Assertion
            Assert.Throws<NullReferenceException>(() => shareAdmin.SendEmail(email, content));
        }
    }
}
