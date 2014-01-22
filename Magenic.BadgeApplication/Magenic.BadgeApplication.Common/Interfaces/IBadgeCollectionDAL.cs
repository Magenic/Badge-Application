using Magenic.BadgeApplication.Common.DTO;
using Magenic.BadgeApplication.Common.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.Common.Interfaces
{
    /// <summary>
    /// Data Access Layer interface to load for classes implementing <see cref="IBadgeCollection"/>.
    /// </summary>
    public interface IBadgeCollectionDAL
    {
        /// <summary>
        /// Asynchronously returns a <see cref="System.Collections.Generic.IEnumerable&lt;IBadgeItemDTO&gt;" />
        /// for the specified badge type.
        /// </summary>
        /// <param name="badgeType">Type of the badge.</param>
        /// <returns>
        /// A <see cref="System.Collections.Generic.IEnumerable&lt;IBadgeItemDTO&gt;" />.
        /// </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
        Task<IEnumerable<BadgeItemDTO>> GetBadgesByBadgeTypeAsync(BadgeType badgeType);

        /// <summary>
        /// Gets the badges by activity identifier asynchronous.
        /// </summary>
        /// <param name="activityIds">The activity ids.</param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
        Task<IEnumerable<BadgeItemDTO>> GetBadgesByActivityIdsAsync(IEnumerable<int> activityIds);
    }
}
