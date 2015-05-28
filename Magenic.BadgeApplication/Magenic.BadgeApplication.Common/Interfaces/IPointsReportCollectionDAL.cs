using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Magenic.BadgeApplication.Common.DTO;

namespace Magenic.BadgeApplication.Common.Interfaces
{
    /// <summary>
    /// Data Access Layer interface to load for classes implementing <see cref="IPointsReportCollection"/>.
    /// </summary>
    public interface IPointsReportCollectionDAL
    {
        /// <summary>
        /// Asynchronously returns a <see cref="System.Collections.Generic.IEnumerable&lt;PointsReportItemDTO&gt;" />
        /// for the specified badge type for a given user's awarded badges.
        /// </summary>
        /// <returns>
        /// A <see cref="System.Collections.Generic.IEnumerable&lt;PointsReportItemDTO&gt;" />.
        /// </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
        Task<IEnumerable<PointsReportItemDTO>> GetPointsReportItemsAsync(bool displayThreshold = false);

        /// <summary>
        /// Updates list of approved points for payout.
        /// </summary>
        /// <param name="data">The values to update.</param>
        /// <returns>A DTO with updated values after the save.</returns>
        IEnumerable<PointsReportItemDTO> Update(IEnumerable<PointsReportItemDTO> data);
    }
}
