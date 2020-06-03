using Magenic.BadgeApplication.Common.DTO;

namespace Magenic.BadgeApplication.Common.Interfaces
{
    /// <summary>
    /// The interface contract for processing badge request items
    /// </summary>
    public interface IBadgeRequestItemProcessor
    {
        /// <summary>
        /// Processes the input item
        /// </summary>
        /// <param name="publishMessageConfig">The message to publish</param>
        void ProcessItems(PublishBadgeRequestMsgConfigDTO publishMessageConfig);
    }
}
