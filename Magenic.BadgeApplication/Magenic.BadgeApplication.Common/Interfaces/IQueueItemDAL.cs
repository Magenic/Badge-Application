using Magenic.BadgeApplication.Common.DTO;

namespace Magenic.BadgeApplication.Common.Interfaces
{
    /// <summary>
    /// Interface contract for all Queue Item data access
    /// </summary>
    public interface IQueueItemDAL : IDTORepository<QueueItemDTO>
    {
        /// <summary>
        /// Gets the top item off the queue
        /// </summary>
        /// <returns></returns>
        QueueItemDTO Peek();       
    }
}
