using System;

namespace Magenic.BadgeApplication.Common.DTO
{
    /// <summary>
    /// Represents a badge award item in the integration queue
    /// </summary>
    public class QueueItemToPublishDTO : DTOBase
    {
        /// <summary>
        /// Initializes a default instance
        /// </summary>
        public QueueItemToPublishDTO()
        { }

        /// <summary>
        /// The ID of the QueueItem
        /// </summary>
        public int QueueItemId { get; set; }

        /// <summary>
        /// The ID of the awarded badge
        /// </summary>
        public int BadgeAwardId { get; set; }

        /// <summary>
        /// The date the queue item was created
        /// </summary>
        public DateTime QueueItemCreated { get; set; }

        /// <summary>
        /// The ID of the Badge
        /// </summary>
        public int BadgeId { get; set; }

        /// <summary>
        /// The Name of the Badge
        /// </summary>
        public string BadgeName { get; set; }

        /// <summary>
        /// The TagLine of the Badge
        /// </summary>
        public string BadgeTagline { get; set; }

        /// <summary>
        /// The Path of the Badge image
        /// </summary>
        public string BadgePath { get; set; }

        /// <summary>
        /// The Description of the Badge
        /// </summary>
        public string BadgeDescription { get; set; }

        /// <summary>
        /// The ID of the Enployee
        /// </summary>
        public int EmployeeId { get; set; }

        /// <summary>
        /// The FirstName of the Employee
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// The LastName of the Employee
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// The EmailAddress of the Employee
        /// </summary>
        public string EmailAddress { get; set; }

        /// <summary>
        /// The ADName of the Employee
        /// </summary>
        public string ADName { get; set; }
    }
}
