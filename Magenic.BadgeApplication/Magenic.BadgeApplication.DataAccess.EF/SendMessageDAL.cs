using Magenic.BadgeApplication.Common.DTO;
using Magenic.BadgeApplication.Common.Interfaces;
using Magenic.BadgeApplication.Common.Resources;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

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
            var mailMessage = new MailMessage();
            foreach (var emailAddress in sendToEmailAddresses)
            {
                mailMessage.To.Add(emailAddress);
            }

            mailMessage.Subject = subject;
            mailMessage.Body = body;
            mailMessage.IsBodyHtml = true;

            var smtpClient = new SmtpClient();
            smtpClient.Send(mailMessage);
        }

        /// <summary>
        /// Sends the activity notification.
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        /// <param name="activityId">The activity identifier.</param>
        public void SendActivityNotification(int employeeId, int activityId)
        {
            using (Entities context = new Entities())
            {
                var employee = context.Employees
                    .Where(emp => emp.EmployeeId == employeeId)
                    .Single();

                var activity = context.Activities
                    .Where(a => a.ActivityId == activityId)
                    .Single();

                var emailAddresses = new List<string>();
                if (employee.ApprovingManagerId1.HasValue)
                {
                    var manager = context.Employees
                        .Where(emp => emp.EmployeeId == employee.ApprovingManagerId1.Value)
                        .SingleOrDefault();

                    if (manager != null)
                    {
                        emailAddresses.Add(manager.EmailAddress);
                    }
                }

                if (employee.ApprovingManagerId2.HasValue)
                {
                    var manager = context.Employees
                        .Where(emp => emp.EmployeeId == employee.ApprovingManagerId2)
                        .SingleOrDefault();

                    if (manager != null)
                    {
                        emailAddresses.Add(manager.EmailAddress);
                    }
                }

                // TODO: figure out a better way to do this. Maybe using RazorEngine?
                var emailSubject = String.Format(CultureInfo.CurrentCulture, ApplicationResources.ActivityNotificationSubjectFormat, employee.FirstName, employee.LastName);
                var emailBody = String.Format(CultureInfo.CurrentCulture, ApplicationResources.ActivityNotificationBodyFormat, activity.ActivityName);

                SendMessage(emailAddresses, emailSubject, emailBody);
            }
        }
    }
}
