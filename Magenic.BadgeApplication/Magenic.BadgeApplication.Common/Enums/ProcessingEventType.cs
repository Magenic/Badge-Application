namespace Magenic.BadgeApplication.Common.Enums
{
    /// <summary>
    /// Represents the state of a queue item
    /// </summary>
    public enum ProcessingEventType
    {
        /// <summary>
        /// The item has not been processed
        /// </summary>
        AwaitingProcessing = 0,
        /// <summary>
        /// The item has been completely processed successfully with no errors
        /// </summary>
        Processed = 1,
        /// <summary>
        /// The item is currently being processed
        /// </summary>
        Processing = 2,
        /// <summary>
        /// The processing of the item failed
        /// </summary>
        Failed = 3
    }
}
