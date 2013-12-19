using Magenic.BadgeApplication.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        /// <param name="userADName">The user name.</param>
        /// <param name="badgeType">Type of the badge.</param>
        /// <returns>
        /// A <see cref="System.Collections.Generic.IEnumerable&lt;IBadgeItemDTO&gt;" />.
        /// </returns>
        Task<IEnumerable<IEarnedBadgeItemDTO>> GetBadgesForUserByBadgeTypeAsync(string userADName, BadgeType badgeType);
    }
}
