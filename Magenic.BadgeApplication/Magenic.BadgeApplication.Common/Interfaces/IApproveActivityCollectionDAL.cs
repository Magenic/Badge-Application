using System.Collections.Generic;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.Common.Interfaces
{
    /// <summary>
    /// Data Access Layer interface to load for classes implementing <see cref="IApproveActivityCollection"/>.
    /// </summary>
    public interface IApproveActivityCollectionDAL
    {
        /// <summary>
        /// Asynchronously returns a <see cref="System.Collections.Generic.IEnumerable&lt;IApproveActivityItemDTO&gt;" />
        /// for the specified badge type.
        /// </summary>
        /// <param name="managerEmployeeId">The employee Id of the manager to get badge submission for.</param>
        /// <returns>
        /// A <see cref="System.Collections.Generic.IEnumerable&lt;IApproveActivityItemDTO&gt;" />.
        /// </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
        Task<IEnumerable<IApproveActivityItemDTO>> GetActivitiesToApproveForManagerAsync(int managerEmployeeId);
        /// <summary>
        /// Updates list of activity submissions for a manager's employees 
        /// based on information passed in via the DTO.
        /// </summary>
        /// <param name="data">The values to update.</param>
        /// <returns>A DTO with updated values after the save.</returns>
        IEnumerable<IApproveActivityItemDTO> Update(IEnumerable<IApproveActivityItemDTO> data);
    }
}
