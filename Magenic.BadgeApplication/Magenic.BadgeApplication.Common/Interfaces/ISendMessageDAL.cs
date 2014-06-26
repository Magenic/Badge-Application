using Magenic.BadgeApplication.Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.Common.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISendMessageDAL
    {
        /// <summary>
        /// Sends the message.
        /// </summary>
        /// <param name="sendToEmailAddresses">The send to email addresses.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="body">The body.</param>
        void SendMessage(IEnumerable<string> sendToEmailAddresses, string subject, string body);

        /// <summary>
        /// Sends the activity notification.
        /// </summary>
        /// <param name="employeeId">The employee identifier.</param>
        /// <param name="activityId">The activity identifier.</param>
        void SendActivityNotification(int employeeId, int activityId);
    }
}
