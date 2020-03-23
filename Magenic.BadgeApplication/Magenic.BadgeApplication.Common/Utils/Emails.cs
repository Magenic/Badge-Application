using System.Collections.Generic;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace Magenic.BadgeApplication.Common.Utils
{
    /// <summary>
    /// Utility for email messages
    /// </summary>
    public static class Emails
    {
        /// <summary>
        /// Sends the message.
        /// </summary>
        /// <param name="fromEmail">The send from email address.</param>
        /// <param name="sendToEmailAddresses">The send to email addresses.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="body">The body.</param>
        public static void SendMessage(MailAddress fromEmail, IEnumerable<string> sendToEmailAddresses, string subject, string body)
        {
            SendMessage(fromEmail, sendToEmailAddresses, subject, body, true);
        }

        /// <summary>
        /// Sends the message.
        /// </summary>
        /// <param name="fromEmail">The send from email address.</param>
        /// <param name="sendToEmailAddresses">The send to email addresses.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="body">The body.</param>
        /// <param name="asHTML">Email as html.</param>
        public static void SendMessage(MailAddress fromEmail, IEnumerable<string> sendToEmailAddresses, string subject, string body, bool asHTML)
        {
            Arg.IsNotNull(() => sendToEmailAddresses);

            using (MailMessage mailMessage = new MailMessage())
            {
#pragma warning disable CA1062 // Validate arguments of public methods
                foreach (string emailAddress in sendToEmailAddresses)
#pragma warning restore CA1062 // Validate arguments of public methods
                {
                    if (string.IsNullOrWhiteSpace(emailAddress))
                        continue;

                    bool isValidEmailAddr = Regex.IsMatch(emailAddress,
                        @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                        @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                        RegexOptions.IgnoreCase);

                    if (false == isValidEmailAddr)
                        continue;

                    mailMessage.To.Add(emailAddress);
                }

                // default settings for email from address, host, port, and enable https is on the smtp configuration
                mailMessage.Subject = subject;
                mailMessage.Body = body;
                mailMessage.IsBodyHtml = asHTML;
                if (fromEmail != null)
                {
                    mailMessage.From = fromEmail;
                }

                //Add this line to bypass the certificate validation
                System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate (object s,
                        System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                        System.Security.Cryptography.X509Certificates.X509Chain chain,
                        System.Net.Security.SslPolicyErrors sslPolicyErrors)
                {
                    return true;
                };

                using (var smtpClient = new SmtpClient())
                {
                    smtpClient.Send(mailMessage);
                }
            }
        }
    }
}
