using Magenic.BadgeApplication.Common;
using Magenic.BadgeApplication.Common.Enums;
using Magenic.BadgeApplication.Common.Interfaces;
using Magenic.BadgeApplication.Common.Resources;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;


namespace Magenic.BadgeApplication.DataAccess.EF
{
    /// <summary>
    /// 
    /// </summary>
    public partial class SendMessageDAL : ISendMessageDAL
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
	            foreach ( var emailAddress in sendToEmailAddresses )
	            {
					if ( String.IsNullOrWhiteSpace( emailAddress ) )
			            continue;

					bool isValidEmailAddr = Regex.IsMatch(emailAddress,
								@"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
								@"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
								RegexOptions.IgnoreCase);

		            if ( false == isValidEmailAddr )
			            continue;

					mailMessage.To.Add(emailAddress);
				}
		        
	            mailMessage.Subject = subject;
                mailMessage.Body = body;
                mailMessage.IsBodyHtml = true;
	            mailMessage.From = new MailAddress( "no-reply@magenic.com", "Magenic Badge Application" );

                using (var smtpClient = new SmtpClient())
                {
	                smtpClient.EnableSsl = Config.EnableSslForSMTP;
	                smtpClient.Port = Config.SMTPPort;
	                smtpClient.Host = Config.SMTPAddress;
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
	            IList<Employee> peopleToEmail = getPeopleToEmail( context );

	            if ( 0 == peopleToEmail.Count )
		            return;

				IList<Employee> employees = getEmployees( context );

	            IList<string> emailAddresses = getEmailAddresses( peopleToEmail, employees );

                var emailSubject = String.Format(CultureInfo.CurrentCulture, ApplicationResources.ActivityNotificationSubject);
                var emailBody = String.Format(CultureInfo.CurrentCulture, ApplicationResources.ActivityNotificationBody);

                SendMessage(emailAddresses, emailSubject, emailBody);
            }
        }

	    private static IList<string> getEmailAddresses(IList<Employee> peopleToEmail, IList<Employee> employees)
	    {
			IList<string> emailAddresses = new List<string>();
			foreach (Employee person in peopleToEmail)
			{
				Debug.Assert(person.ApprovingManagerId1.HasValue || person.ApprovingManagerId2.HasValue);

				addEmailAddress( employees, person.ApprovingManagerId1, emailAddresses );
				addEmailAddress( employees, person.ApprovingManagerId2, emailAddresses );
			}
			return emailAddresses.Distinct().ToList();
		}

		private static void addEmailAddress(IList<Employee> employees, int? managerId, IList<string> emailAddresses  )
		{
			if ( false == managerId.HasValue ) return;

			Employee manager = employees.SingleOrDefault(emp => emp.EmployeeId == managerId);

			if (manager != null)
				emailAddresses.Add(manager.EmailAddress);
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
