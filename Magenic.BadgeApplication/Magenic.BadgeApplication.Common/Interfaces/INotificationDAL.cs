using Magenic.BadgeApplication.Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.Common.Interfaces
{
    /// <summary>
    /// Interface contract for all Notification Item data access
    /// </summary>
    public interface INotificationDAL : IDTORepository<NotificationDTO>
    {
        /// <summary>
        /// Gets the top item off the queue
        /// </summary>
        /// <returns></returns>
        NotificationDTO Peek();
    }
}
