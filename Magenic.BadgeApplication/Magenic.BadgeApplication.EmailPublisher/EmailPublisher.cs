using Autofac;
using Magenic.BadgeApplication.BusinessLogic.Framework;
using Magenic.BadgeApplication.Common;
using Magenic.BadgeApplication.Common.DTO;
using Magenic.BadgeApplication.Common.Enums;
using Magenic.BadgeApplication.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.EmailPublisher
{  
    public class EmailPublisher : IPublisher
    {
        private IContainer _factory;

        public EmailPublisher() : this(IoC.Container)
        {
        }

        public EmailPublisher(IContainer factory)
        {
            _factory = factory;
        }

        public void PublishBadge(PublishBadgeMsgConfigDTO publishMessageConfig)
        {
            try
            {
                var msgEmp = $"{publishMessageConfig.EmployeeFullName} has been awarded the following Magenic Badges:";

                StringBuilder sb = new StringBuilder();
                if (!publishMessageConfig.Environment.ToLower().Equals("prod"))
                {
                    sb.Append($" ({publishMessageConfig.Environment} test)<br/>");
                }
                sb.Append($"{msgEmp}<br/>");
                sb.Append("<table>");
                foreach (var item in publishMessageConfig.QueueItems)
                {
                    sb.Append("<tr>");
                    sb.Append($"<td colspan=3>&nbsp;{item.BadgeName}</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append($"<td><img src='{item.BadgePath}' width='70%' height='70%' alt='{item.BadgeName}' /></td>");
                    sb.Append($"<td>&nbsp;{item.BadgeTagline}&nbsp;&nbsp;&nbsp;</td>");
                    sb.Append($"<td>{item.BadgeDescription}</td>");
                    sb.Append("</tr>");
                    sb.Append("<tr>");
                    sb.Append($"<td colspan=3>&nbsp;</td>");
                    sb.Append("</tr>");
                }
                sb.Append("</table>");
                sb.Append("<p>");

                var msgBody = sb.ToString();

                if (string.IsNullOrWhiteSpace(publishMessageConfig.EmployeeEmailAddress))
                {
                    Logger.Error<EmailPublisher>($"No email address for {publishMessageConfig.EmployeeFullName} - {publishMessageConfig.EmployeeADName}");
                }
                else
                {
                    Common.Utils.Emails.SendMessage(
                        new MailAddress("no-reply@magenic.com", "Magenic Badge Application"),
                                        new List<string>() { publishMessageConfig.EmployeeEmailAddress }, publishMessageConfig.Title, msgBody);
                }
            }
            catch (Exception ex)
            {
                Logger.Error<EmailPublisher>(ex.Message, ex);
                throw;
            }
        }

        public void PublishSubmitNotify(PublishNotificationMsgConfigDTO publishMessageConfig)
        {
            try
            {
                var msgEmp = $"Status of the following Badge activities submitted for {publishMessageConfig.EmployeeFullName}:";

                StringBuilder sb = new StringBuilder();
                if (!publishMessageConfig.Environment.ToLower().Equals("prod"))
                {
                    sb.Append($" ({publishMessageConfig.Environment} test)<br/>");
                }
                sb.Append($"{msgEmp}<br/>");
                sb.Append("<table style=\"width:100%\">");
                sb.Append("<tr align='left'>");
                sb.Append("<th width=\"5%\" style=\"border: 1px solid black;\">Date Submitted</th>");
                sb.Append("<th width=\"10%\" style=\"border: 1px solid black;\">Status</th>");
                sb.Append("<th width=\"20%\" style=\"border: 1px solid black;\">Description</th>");
                sb.Append("<th width=\"20%\" style=\"border: 1px solid black;\">Activity Name</th>");
                sb.Append("<th width=\"40%\" style=\"border: 1px solid black;\">Activity Description</th>");
                sb.Append("<th width=\"5%\" style=\"border: 1px solid black;\">Award Value</th>");
                sb.Append("</tr>");
                foreach (var item in publishMessageConfig.NotificationItems)
                {
                    sb.Append("<tr>");
                    sb.Append($"<td>{item.SubmissionDate.ToString("MM/dd/yyyy")}</td>");
                    var redStyle = string.Empty;
                    var submissionStatus = "Approved";
                    if (item.SubmissionStatusId == (int)ActivitySubmissionStatus.Denied)
                    {
                        redStyle = " style=\"color:red;\"";
                        submissionStatus = "Not Approved";
                    }
                    sb.Append($"<td{redStyle}>{submissionStatus}</td>");
                    sb.Append($"<td>{item.SubmissionDescription}</td>");
                    sb.Append($"<td>{item.ActivityName}</td>");
                    sb.Append($"<td>{item.ActivityDescription}</td>");
                    sb.Append($"<td>{(item.AwardValue.HasValue ? item.AwardValue.ToString() : string.Empty)}</td>");
                    sb.Append("</tr>");
                }
                sb.Append("</table>");
                sb.Append("<p>");

                var msgBody = sb.ToString();

                if (string.IsNullOrWhiteSpace(publishMessageConfig.EmployeeEmailAddress))
                {
                    Logger.Error<EmailPublisher>($"No email address for {publishMessageConfig.EmployeeFullName} - {publishMessageConfig.EmployeeADName}");
                }
                else
                {
                    Common.Utils.Emails.SendMessage(
                        new MailAddress("no-reply@magenic.com", "Magenic Badge Application"),
                                        new List<string>() { publishMessageConfig.EmployeeEmailAddress }, publishMessageConfig.Title, msgBody);
                }
            }
            catch (Exception ex)
            {
                Logger.Error<EmailPublisher>(ex.Message, ex);
                throw;
            }
        }

        public void PublishBadgeRequest(PublishBadgeRequestMsgConfigDTO publishMessageConfig)
        {
            try
            {
                var msgEmp = $"The following Badge Requests were submitted by {publishMessageConfig.EmployeeFullName}:";

                StringBuilder sb = new StringBuilder();
                if (!publishMessageConfig.Environment.ToLower().Equals("prod"))
                {
                    sb.Append($" ({publishMessageConfig.Environment} test)<br/>");
                }
                sb.Append($"{msgEmp}<br/>");
                sb.Append("<table style=\"width:100%\">");
                sb.Append("<tr align='left'>");
                sb.Append("<th width=\"5%\" style=\"border: 1px solid black;\">Date Submitted</th>");
                sb.Append("<th width=\"10%\" style=\"border: 1px solid black;\">Badge Name</th>");
                sb.Append("<th width=\"20%\" style=\"border: 1px solid black;\">Description</th>");
                sb.Append("</tr>");
                foreach (var item in publishMessageConfig.BadgeRequestItems)
                {
                    sb.Append("<tr>");
                    sb.Append($"<td>{item.CreatedDate.ToString("MM/dd/yyyy")}</td>");
                    sb.Append($"<td>{item.BadgeName}</td>");
                    sb.Append($"<td>{item.BadgeDescription}</td>");
                    sb.Append("</tr>");
                }
                sb.Append("</table>");
                sb.Append("<p>");

                var msgBody = sb.ToString();

                if (string.IsNullOrWhiteSpace(publishMessageConfig.EmployeeEmailAddress))
                {
                    Logger.Error<EmailPublisher>($"No email address for {publishMessageConfig.EmployeeFullName} - {publishMessageConfig.EmployeeADName}");
                }
                else
                {
                    Common.Utils.Emails.SendMessage(
                        new MailAddress("no-reply@magenic.com", "Magenic Badge Application"),
                                        new List<string>() { publishMessageConfig.EmployeeEmailAddress }, publishMessageConfig.Title, msgBody);
                }
            }
            catch (Exception ex)
            {
                Logger.Error<EmailPublisher>(ex.Message, ex);
                throw;
            }
        }
    }
}
