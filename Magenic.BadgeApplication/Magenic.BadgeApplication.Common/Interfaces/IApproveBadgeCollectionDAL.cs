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
        /// Asynchronously returns a <see cref="System.Collections.Generic.IEnumerable&lt;ApproveBadgeItemDTO&gt;" />
        /// for the specified badge type.
        /// </summary>
        /// <returns>
        /// A <see cref="System.Collections.Generic.IEnumerable&lt;ApproveBadgeItemDTO&gt;" />.
        /// </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
        Task<IEnumerable<ApproveBadgeItemDTO>> GetActivitiesToApproveForAdministratorAsync();
        /// <summary>
        /// Updates list of approved or denied community badges 
        /// based on information passed in via the DTO.
        /// </summary>
        /// <param name="data">The values to update.</param>
        /// <returns>A DTO with updated values after the save.</returns>
        IEnumerable<ApproveBadgeItemDTO> Update(IEnumerable<ApproveBadgeItemDTO> data);
    }
}
