using System.Collections.Generic;

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
        void SendActivityNotifications();
    }
}
