using Magenic.BadgeApplication.Common.DTO;
using Magenic.BadgeApplication.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.Common.Interfaces
{
    /// <summary>
    /// The interface contract for processing notification items
    /// </summary>
    public interface ISubmissionNotifyItemProcessor
    {
        /// <summary>
        /// Processes the input item
        /// </summary>
        /// <param name="publishMessageConfig">The message to publish</param>
        void ProcessItems(PublishNotificationMsgConfigDTO publishMessageConfig);
    }
}
