using System;

namespace Magenic.BadgeApplication.Common.DTO
{
    /// <summary>
    /// Represents a record of an activity performed on a queue item
    /// </summary>
    public class QueueEventLogDTO
    {
        /// <summary>
        /// The ID of the queue event log
        /// </summary>
        public int QueueEventLogId { get; set; }

        /// <summary>
        /// The ID of the queue event
        /// </summary>
        public int QueueEventId { get; set; }

        /// <summary>
        /// The ID of the queue item
        /// </summary>
        public int QueueItemId { get; set; }

        /// <summary>
        /// The date the Queue Event Log was created
        /// </summary>
        public DateTime QueueEventCreated { get; set; }

        /// <summary>
        /// Additional information about the event
        /// </summary>
        public string Message { get; set; }
    }
}
