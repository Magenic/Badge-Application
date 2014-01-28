using System;

namespace Magenic.BadgeApplication.Common.DTO
{
    /// <summary>
    /// Represents a badge award item in the integration queue
    /// </summary>
    public sealed class QueueItemDTO
    {
        /// <summary>
        /// Initializes a default instance
        /// </summary>
        public QueueItemDTO()
        { }

        /// <summary>
        /// Initializes an instance of QueueItemDTO with the input ids
        /// </summary>
        /// <param name="queueItemId"></param>
        /// <param name="badgeAwardId"></param>
        public QueueItemDTO(int queueItemId, int badgeAwardId)
        {
            QueueItemId = queueItemId;
            BadgeAwardId = badgeAwardId;
        }

        /// <summary>
        /// Initializes an instance of QueueItemDTO with the input ids and date
        /// </summary>
        /// <param name="queueItemId"></param>
        /// <param name="badgeAwardId"></param>
        /// <param name="queueItemCreated"></param>
        public QueueItemDTO(int queueItemId, int badgeAwardId, DateTime queueItemCreated)
        {
            QueueItemId = queueItemId;
            BadgeAwardId = badgeAwardId;
            QueueItemCreated = queueItemCreated;
        }

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
    }
}
