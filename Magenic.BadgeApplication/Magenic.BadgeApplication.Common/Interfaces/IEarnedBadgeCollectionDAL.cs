using Magenic.BadgeApplication.Common.DTO;
using Magenic.BadgeApplication.Common.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.Common.Interfaces
{
    /// <summary>
    /// Data Access Layer interface to load for classes implementing <see cref="IEarnedBadgeCollection"/>.
    /// </summary>
    public interface IEarnedBadgeCollectionDAL
    {
        /// <summary>
        /// Asynchronously returns a <see cref="System.Collections.Generic.IEnumerable&lt;IBadgeItemDTO&gt;" />
        /// for the specified badge type for a given user's awarded badges.
        /// </summary>
        /// <param name="employeeId">The employee id to return earned badges for.</param>
        /// <param name="badgeType">Type of the badge.</param>
        /// <returns>
        /// A <see cref="System.Collections.Generic.IEnumerable&lt;IBadgeItemDTO&gt;" />.
        /// </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
        Task<IEnumerable<EarnedBadgeItemDTO>> GetBadgesForUserByBadgeTypeAsync(int employeeId, BadgeType badgeType);
    }
}
