using Magenic.BadgeApplication.Common.DTO;

namespace Magenic.BadgeApplication.Common.Interfaces
{
    /// <summary>
    /// Interface contract for all Queue Item data access
    /// </summary>
    public interface IQueueItemDAL
    {
        /// <summary>
        /// Gets the top item off the queue
        /// </summary>
        /// <returns></returns>
        QueueItemDTO GetTopItem();

        /// <summary>
        /// Gets the item with the input id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        QueueItemDTO GetItem(int id);

        /// <summary>
        /// Adds the input item
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        QueueItemDTO AddItem(QueueItemDTO item);

        /// <summary>
        /// Updates the input item
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        QueueItemDTO UpdateItem(QueueItemDTO item);

        /// <summary>
        /// Deletes the item with the input id
        /// </summary>
        /// <param name="id"></param>
        void DeleteItem(int id);
    }
}
