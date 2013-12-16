using Csla;
using Magenic.BadgeApplication.Common.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.Common.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBadgeCollectionDAL
    {
        /// <summary>
        /// Asychronously returns a <see cref="System.Collections.Generic.IEnumerable&lt;IBadgeItemDTO&gt;" />
        /// for the specified badge type.
        /// </summary>
        /// <param name="badgeType">Type of the badge.</param>
        /// <returns>
        /// A <see cref="System.Collections.Generic.IEnumerable&lt;IBadgeItemDTO&gt;" />.
        /// </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
        Task<IEnumerable<IBadgeItemDTO>> GetBadgesByBadgeTypeAsync(BadgeType badgeType);

        /// <summary>
        /// Asychronously returns a <see cref="System.Collections.Generic.IEnumerable&lt;IBadgeItemDTO&gt;" />
        /// for the specified badge type for a given user's awarded badges.
        /// </summary>
        /// <param name="userName">The user name.</param>
        /// <param name="badgeType">Type of the badge.</param>
        /// <returns>
        /// A <see cref="System.Collections.Generic.IEnumerable&lt;IBadgeItemDTO&gt;" />.
        /// </returns>
        Task<IEnumerable<IBadgeItemDTO>> GetBadgesForUserByBadgeTypeAsync(string userName, BadgeType badgeType);
    }
}
