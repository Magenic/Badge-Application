using Magenic.BadgeApplication.Common;
using Magenic.BadgeApplication.Common.Enums;
using Magenic.BadgeApplication.Common.Interfaces;
using Magenic.BadgeApplication.Common.Resources;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Mail;

namespace Magenic.BadgeApplication.DataAccess.EF
{
    /// <summary>
    /// 
    /// </summary>
    public class SendMessageDAL
        : ISendMessageDAL
    {
        /// <summary>
        /// Sends the message.
        /// </summary>
        /// <param name="sendToEmailAddresses">The send to email addresses.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="body">The body.</param>
        public void SendMessage(IEnumerable<string> sendToEmailAddresses, string subject, string body)
        {
            Arg.IsNotNull(() => sendToEmailAddresses);

            using (var mailMessage = new MailMessage())
            {
                foreach (var emailAddress in sendToEmailAddresses)
                {
                    mailMessage.To.Add(emailAddress);
                }

                mailMessage.Subject = subject;
                mailMessage.Body = body;
                mailMessage.IsBodyHtml = true;

                using (var smtpClient = new SmtpClient())
                {
                    smtpClient.Send(mailMessage);
                }
            }
        }

        /// <summary>
        /// Sends the activity notification.
        /// </summary>
        public void SendActivityNotifications()
        {
            using (Entities context = new Entities())
            {
                var activitySubmissions = context.ActivitySubmissions
                    .Where(sub => sub.ItemStatu.ItemStatusId == (int)ActivitySubmissionStatus.AwaitingApproval)
                    .ToList();

                var employees = context.Employees.ToList();
                var peopleToEmail = activitySubmissions
                    .Join(employees, asub => asub.EmployeeId, emp => emp.EmployeeId, (asub, emp) => new { asub = asub, emp = emp });

                var emailAddresses = new List<string>();
                foreach (var person in peopleToEmail)
                {
                    if (person.emp.ApprovingManagerId1.HasValue)
                    {
                        var manager = employees
                            .Where(emp => emp.EmployeeId == person.emp.ApprovingManagerId1.Value)
                            .SingleOrDefault();

                        if (manager != null)
                        {
                            emailAddresses.Add(manager.EmailAddress);
                        }
                    }

                    if (person.emp.ApprovingManagerId2.HasValue)
                    {
                        var manager = employees
                            .Where(emp => emp.EmployeeId == person.emp.ApprovingManagerId2.Value)
                            .SingleOrDefault();

                        if (manager != null)
                        {
                            emailAddresses.Add(manager.EmailAddress);
                        }
                    }
                }

                // TODO: figure out a better way to do this. Maybe using RazorEngine?
                emailAddresses = emailAddresses.Distinct().ToList();
                var emailSubject = String.Format(CultureInfo.CurrentCulture, ApplicationResources.ActivityNotificationSubject);
                var emailBody = String.Format(CultureInfo.CurrentCulture, ApplicationResources.ActivityNotificationBody);

                SendMessage(emailAddresses, emailSubject, emailBody);
            }
        }
    }
}
