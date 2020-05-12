using Magenic.BadgeApplication.Common.DTO;
using Magenic.BadgeApplication.Common.Enums;

namespace Magenic.BadgeApplication.Common.Interfaces
{
    /// <summary>
    /// The interface contract for processing queue items
    /// </summary>
    public interface IQueueItemProcessor
    {
        /// <summary>
        /// Processes the input item
        /// </summary>
        /// <param name="publishMessageConfig">The message to publish</param>
        void ProcessItems(PublishBadgeMsgConfigDTO publishMessageConfig);

        /// <summary>
        /// Publishes updates related to the input item
        /// </summary>
        /// <param name="publishMessageConfig">The item to publish updates about</param>
        void PublishUpdates(PublishBadgeMsgConfigDTO publishMessageConfig);

        /// <summary>
        /// Logs progress related to the processing of a queue item
        /// </summary>
        /// <param name="eventType">The type of event we are recording information about</param>
        /// <param name="publishMessageConfig">The items that has been updated</param>
        void RegisterQueueItemProgress(ProcessingEventType eventType, PublishBadgeMsgConfigDTO publishMessageConfig);
    }
}
