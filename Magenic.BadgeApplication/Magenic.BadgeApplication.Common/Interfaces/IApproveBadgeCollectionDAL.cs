using Magenic.BadgeApplication.Common.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.Common.Interfaces
{
    /// <summary>
    /// Data Access Layer interface to load for classes implementing <see cref="IApproveBadgeCollection"/>.
    /// </summary>
    public interface IApproveBadgeCollectionDAL
    {
        /// <summary>
        /// Asynchronously returns a <see cref="System.Collections.Generic.IEnumerable&lt;ApproveBadgeItemDTO&gt;" />.
        /// </summary>
        /// <returns>
        /// A <see cref="System.Collections.Generic.IEnumerable&lt;ApproveBadgeItemDTO&gt;" />.
        /// </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
        Task<IEnumerable<ApproveBadgeItemDTO>> GetBadgesToApproveAsync();
        /// <summary>
        /// Asynchronously returns a <see cref="ApproveBadgeItemDTO" />
        /// for the specified badge id.
        /// </summary>
        /// <param name="badgeId">The badge id to look for</param>
        /// <returns>A <see cref="ApproveBadgeItemDTO"/> with the badge information.</returns>
        Task<ApproveBadgeItemDTO> GetBadgeToApproveByIdAsync(int badgeId);
        /// <summary>
        /// Updates an approved or denied community badge 
        /// based on information passed in via the DTO.
        /// </summary>
        /// <param name="data">The badge to update.</param>
        /// <returns>A DTO with updated values after the save.</returns>
        ApproveBadgeItemDTO Update(ApproveBadgeItemDTO data);
    }
}