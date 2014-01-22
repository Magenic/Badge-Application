
namespace Magenic.BadgeApplication.Common.Enums
{
    /// <summary>
    /// Represents the state of a queue item
    /// </summary>
    public enum QueueEventType
    {
        /// <summary>
        /// The queue item has been completely processed successfully with no errors
        /// </summary>
        Processed = 1,

        /// <summary>
        /// The queue item is currently being processed
        /// </summary>
        Processing = 2,

        /// <summary>
        /// The processing of the queue item failed
        /// </summary>
        Failed = 3
    }
}
