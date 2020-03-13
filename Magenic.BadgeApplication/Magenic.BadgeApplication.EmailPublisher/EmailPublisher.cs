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

        public void Publish(PublishMessageConfigDTO publishMessageConfig)
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
