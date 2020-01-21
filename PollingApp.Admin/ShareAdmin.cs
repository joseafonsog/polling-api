using PollingApp.Commons.Helpers;
using PollingApp.Commons.Infrastructure;
using PollingApp.Commons.Settings;
using System;
using System.Globalization;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace PollingApp.Admin
{
    public class ShareAdmin : IShareAdmin
    {
        private readonly IEmailSettings _emailSettings;
        private readonly ISmtpClient _smtpClient;

        public ShareAdmin(IEmailSettings emailSettings, ISmtpClient smtpClient)
        {
            _emailSettings = emailSettings;
            _smtpClient = smtpClient;
        }
        public void SendEmail(string to, string content)
        {
            if (string.IsNullOrEmpty(content) || !IsValidEmail(to))
            {
                throw new NullReferenceException("parameters to and content are required!");
            }

            var message = new MailMessage(_emailSettings.Sender,to)
            {
                Subject = _emailSettings.Subject,
                Body = HtmlHelper.GetHtmlBody(content, "Questions"),
                IsBodyHtml = true
            };
            
            _smtpClient.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
            _smtpClient.PickupDirectoryLocation = _emailSettings.LocalDirectoryPath;

            try
            {
                _smtpClient.Send(message);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // Normalize the domain
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    var domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
            catch (ArgumentException)
            {
                return false;
            }
            catch(Exception)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                    @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
    }
}
