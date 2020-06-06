using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.Common.DTO
{
    public class PublishBadgeRequestMsgConfigDTO
    {
        /// <summary>
        /// The Environemnt to be published
        /// </summary>
        public string Environment { get; set; }
        /// <summary>
        /// The title of the message to be published
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// The emsil address of the message to be published
        /// </summary>
        public string SendToEmailAddress { get; set; }
        /// <summary>
        /// The URL of the Magenic DataService
        /// </summary>
        public string MagenicDataService { get; set; }
        /// <summary>
        /// The list of the QueueItemIds
        /// </summary>
        public IList<PublishBadgeRequestItemDTO> BadgeRequestItems { get; set; }
    }
}
