using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.Common.DTO
{
    /// <summary>
    /// Class for data transfer persist operations.
    /// </summary>
    public class PublishQueueItemDTO
    {
        /// <summary>
        /// The Queue Item Id to be published
        /// </summary>
        public int QueueItemId { get; set; }
        /// <summary>
        /// The Badge Award Id to be published
        /// </summary>
        public int BadgeAwardId { get; set; }
        /// <summary>
        /// The date when Queue Item Id was created to be published
        /// </summary>
        public System.DateTime QueueItemCreated { get; set; }
        /// <summary>
        /// The Id of Badge to be published
        /// </summary>
        public int BadgeId { get; set; }
        /// <summary>
        /// The Name of Badge to be published
        /// </summary>
        public string BadgeName { get; set; }
        /// <summary>
        /// The Tag Line of Badge to be published
        /// </summary>
        public string BadgeTagline { get; set; }
        /// <summary>
        /// The image path of Badge to be published
        /// </summary>
        public string BadgePath { get; set; }
        /// <summary>
        /// The decription of Badge to be published
        /// </summary>
        public string BadgeDescription { get; set; }
    }
}
