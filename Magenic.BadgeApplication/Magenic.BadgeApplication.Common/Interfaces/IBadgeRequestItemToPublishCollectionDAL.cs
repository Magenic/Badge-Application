using Magenic.BadgeApplication.Common.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.Common.Interfaces
{
    /// <summary>
    /// Data Access Layer interface to load for classes implementing <see cref="IBadgeRequestItemToPublishCollectionDAL"/>.
    /// </summary>
    public interface IBadgeRequestItemToPublishCollectionDAL
    {
        /// <summary>
        /// Asynchronously returns a <see cref="System.Collections.Generic.IEnumerable&lt;BadgeRequestItemToPublishDTO&gt;" />.
        /// </summary>
        /// <returns>
        /// A <see cref="System.Collections.Generic.IEnumerable&lt;BadgeRequestItemToPublishDTO&gt;" />.
        /// </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
        Task<IEnumerable<BadgeRequestItemToPublishDTO>> GetAllBadgeRequestItemsToPublishAsync();
    }
}
