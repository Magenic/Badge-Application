using Magenic.BadgeApplication.Common;
using Magenic.BadgeApplication.Common.Enums;
using Magenic.BadgeApplication.Common.Interfaces;
using Magenic.BadgeApplication.Common.Resources;
using Magenic.BadgeApplication.Common.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;


namespace Magenic.BadgeApplication.DataAccess.EF
{
    /// <summary>
    /// 
    /// </summary>
    public partial class SendMessageDAL : ISendMessageDAL
    {
        private static string DataService
        {
            get { return ConfigurationManager.AppSettings["ITDataServiceURL"]; }
        }

        /// <summary>
        /// Sends the message.
        /// </summary>
        /// <param name="sendToEmailAddresses">The send to email addresses.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="body">The body.</param>
        public void SendMessage(IEnumerable<string> sendToEmailAddresses, string subject, string body)
        {
            Common.Utils.Emails.SendMessage(new MailAddress("no-reply@magenic.com", "Magenic Badge Application"), sendToEmailAddresses, subject, body);
        }

        /// <summary>
        /// Sends the activity notification.
        /// </summary>
        public void SendActivityNotifications()
        {
            using (Entities context = new Entities())
            {
                IList<Employee> peopleToEmail = getPeopleToEmail( context );

                if ( 0 == peopleToEmail.Count )
                    return;

                IList<Employee> employees = getEmployees( context );

                IList<string> emailAddresses = getEmailAddresses( peopleToEmail, employees );
                string emailSubject;
                var environment = ConfigurationManager.AppSettings["Environment"];
                if (!string.IsNullOrWhiteSpace(environment) && environment.Trim().ToLower(CultureInfo.CurrentCulture) == "prod")
                {
                    emailSubject = String.Format(CultureInfo.CurrentCulture, ApplicationResources.ActivityNotificationSubject);
                }
                else
                {
                    emailSubject = String.Format(CultureInfo.CurrentCulture, ApplicationResources.ActivityNotificationSubjectTest);
                }

                var emailBody = String.Format(CultureInfo.CurrentCulture, ApplicationResources.ActivityNotificationBody);

                SendMessage(emailAddresses, emailSubject, emailBody);
            }
        }

        private static IList<string> getEmailAddresses( IList<Employee> peopleToEmail, IList<Employee> employees )
        {
            IList<string> emailAddresses = new List<string>();
            foreach ( var person in peopleToEmail )
            {
                if (person.ApprovingManagerId1.HasValue)
                {
                    addEmailAddress(employees, person.ApprovingManagerId1, emailAddresses);
                }
                if (person.ApprovingManagerId2.HasValue)
                {
                    addEmailAddress(employees, person.ApprovingManagerId2, emailAddresses);
                }
            }
            return emailAddresses.Distinct().ToList();
        }

        private static void addEmailAddress( IList<Employee> employees, int? managerId, IList<string> emailAddresses )
        {
            if ( false == managerId.HasValue ) return;

            Employee manager = employees.SingleOrDefault( emp => emp.EmployeeId == managerId );

            if ( manager != null)
            {
                if (String.IsNullOrWhiteSpace(manager.EmailAddress))
                {
                    var dataServiceUri = new Uri(DataService, UriKind.Absolute);
                    var context = new MagenicDataEntities(dataServiceUri)
                    {
                        Credentials = CredentialCache.DefaultCredentials
                    };
                    var employee = context.vwODataEmployees.Where(e => e.NetworkAlias == manager.ADName).FirstOrDefault();
                    if (employee != null)
                    {
                        emailAddresses.Add(employee.EMailAddress);
                    }
                }
                else
                {
                    emailAddresses.Add(manager.EmailAddress);
                }
            }
        }

        private static IList<Employee> getEmployees( Entities context )
        {
            IList<Employee> employees = context.Employees.ToList();
            return employees;
        }

        private static IList<Employee> getPeopleToEmail( Entities context )
        {
            IList<Employee> peopleToEmail = ( from asub in context.ActivitySubmissions
                join emp in context.Employees on asub.EmployeeId equals emp.EmployeeId
                where asub.ItemStatu.ItemStatusId == (int)ActivitySubmissionStatus.AwaitingApproval
                select emp ).ToList();

            return peopleToEmail;
        }
    }
}
