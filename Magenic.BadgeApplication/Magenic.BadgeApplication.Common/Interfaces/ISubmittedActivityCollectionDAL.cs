using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.Common.Interfaces
{
    /// <summary>
    /// Data Access Layer interface to load for classes implementing <see cref="ISubmittedActivityCollection"/>.
    /// </summary>
    public interface ISubmittedActivityCollectionDAL
    {
        /// <summary>
        /// Asynchronously returns a <see cref="System.Collections.Generic.IEnumerable&lt;ISubmittedActivityItemDTO&gt;" />
        /// for the specified badge type for a given user's awarded badges.
        /// </summary>
        /// <param name="userADName">The user name.</param>
        /// <param name="startDate">The start date to search for submitted activities for the
        /// user based on when the activity was submitted.  Send in null to search for submitted
        /// activities from the beginning of time.</param>
        /// <param name="endDate">The end date to search for submitted activities for the
        /// user based on when the activity was submitted.  Send in null to search for submitted
        /// activities unit the end of time.</param>
        /// <returns>
        /// A <see cref="System.Collections.Generic.IEnumerable&lt;ISubmittedActivityItemDTO&gt;" />.
        /// </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
        Task<IEnumerable<ISubmittedActivityItemDTO>> GetSubmittedActivitiesForUserAsync(string userADName, DateTime? startDate, DateTime? endDate);
    }
}