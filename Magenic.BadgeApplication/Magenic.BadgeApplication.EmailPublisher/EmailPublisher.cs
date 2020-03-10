using Autofac;
using Magenic.BadgeApplication.BusinessLogic.Framework;
using Magenic.BadgeApplication.Common;
using Magenic.BadgeApplication.Common.DTO;
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

        public void Publish(PublishMessageConfigDTO publishMessageConfig) //(EarnedBadgeItemDTO earnedBadge, BadgeAwardPublishMessageDTO awardMessage)
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
                    sb.Append($"<td><img src='{item.BadgePath}' width='50%' height='50%' alt='{item.BadgeName}' /></td>");
                    sb.Append($"<td>&nbsp;{item.BadgeName}</td>");
                    sb.Append("<td>");
                    if (!string.IsNullOrWhiteSpace(item.BadgeTagline))
                    {
                        sb.Append($" - {item.BadgeTagline}</td>");
                    }
                    sb.Append("</td>");
                    sb.Append("</tr>");
                }
                sb.Append("</table>");
                sb.Append("<p>");

                var msgBody = sb.ToString();

                Common.Utils.Emails.SendMessage(
                    new MailAddress("no-reply@magenic.com", "Magenic Badge Application"),
                                    new List<string>() { publishMessageConfig.EmployeeEmailAddress }, publishMessageConfig.Title, msgBody);
            }
            catch (Exception ex)
            {
                Logger.Error<EmailPublisher>(ex.Message, ex);
            }
        }
    }
}
