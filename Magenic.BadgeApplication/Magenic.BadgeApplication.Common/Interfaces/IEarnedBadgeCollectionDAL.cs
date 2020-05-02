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
        /// Asynchronously returns a <see cref="System.Collections.Generic.IEnumerable&lt;BadgeItemDTO&gt;" />
        /// for the specified badge type for a given user's awarded badges.
        /// </summary>
        /// <param name="employeeId">The employee id to return earned badges for.</param>
        /// <param name="badgeType">Type of the badge.</param>
        /// <returns>
        /// A <see cref="System.Collections.Generic.IEnumerable&lt;BadgeItemDTO&gt;" />.
        /// </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
        Task<IEnumerable<EarnedBadgeItemDTO>> GetBadgesForUserByBadgeTypeAsync(int employeeId, BadgeType badgeType);

        /// <summary>
        /// Gets the earned badge for the input badge award id
        /// </summary>
        /// <param name="badgeAwardId">The badge award id</param>
        /// <returns>The earned badge</returns>
        Task<EarnedBadgeItemDTO> GetEarnedBadge(int badgeAwardId);

        /// <summary>
        /// Get all earned badges
        /// </summary>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
        Task<IEnumerable<EarnedBadgeItemDTO>> GetBadgesAsync();

        /// <summary>
        /// Delete badge awarded
        /// </summary>
        /// <param name="badgeAwardId"></param>
        void Delete(int badgeAwardId);

        /// <summary>
        /// Delete queue event logs
        /// </summary>
        /// <param name="badgeAwardId"></param>
        void DeleteQueueEventLogs(int badgeAwardId);

        /// <summary>
        /// Delete queue items
        /// </summary>
        /// <param name="badgeAwardId"></param>
        void DeleteQueueItems(int badgeAwardId);
    }
}
