using Magenic.BadgeApplication.Common.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.Common.Interfaces
{
    /// <summary>
    /// Data Access Layer interface to load for classes implementing <see cref="IQueueItemToPublishCollection"/>.
    /// </summary>
    public interface IQueueItemToPublishCollectionDAL
    {
        /// <summary>
        /// Asynchronously returns a <see cref="System.Collections.Generic.IEnumerable&lt;QueueItemToPublishDTO&gt;" />.
        /// </summary>
        /// <returns>
        /// A <see cref="System.Collections.Generic.IEnumerable&lt;QueueItemToPublishDTO&gt;" />.
        /// </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
        Task<IEnumerable<QueueItemToPublishDTO>> GetAllQueueItemsToPublishAsync();
    }
}