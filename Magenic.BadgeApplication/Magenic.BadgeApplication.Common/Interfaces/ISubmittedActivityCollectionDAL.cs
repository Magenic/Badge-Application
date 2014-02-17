using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Magenic.BadgeApplication.Common.DTO;

namespace Magenic.BadgeApplication.Common.Interfaces
{
    /// <summary>
    /// Data Access Layer interface to load for classes implementing <see cref="ISubmittedActivityCollection"/>.
    /// </summary>
    public interface ISubmittedActivityCollectionDAL
    {
        /// <summary>
        /// Asynchronously returns a <see cref="System.Collections.Generic.IEnumerable&lt;SubmittedActivityItemDTO&gt;" />
        /// for the specified employee.
        /// </summary>
        /// <param name="employeeId">The employee id.</param>
        /// <param name="startDate">The start date to search for submitted activities for the
        /// user based on when the activity was submitted.  Send in null to search for submitted
        /// activities from the beginning of time.</param>
        /// <param name="endDate">The end date to search for submitted activities for the
        /// user based on when the activity was submitted.  Send in null to search for submitted
        /// activities unit the end of time.</param>
        /// <returns>
        /// A <see cref="System.Collections.Generic.IEnumerable&lt;SubmittedActivityItemDTO&gt;" />.
        /// </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
        Task<IEnumerable<SubmittedActivityItemDTO>> GetSubmittedActivitiesForEmployeeIdAsync(int employeeId, DateTime? startDate, DateTime? endDate);

        /// <summary>
        /// Asynchronously returns a <see cref="System.Collections.Generic.IEnumerable&lt;SubmittedActivityItemDTO&gt;" />
        /// for the specified employee for a given activity id.
        /// </summary>
        /// <param name="employeeId">The employee id.</param>
        /// <param name="activityId">The activity id to look for.</param>
        /// <param name="startDate">The start date to search for submitted activities for the
        /// user based on when the activity was submitted.  Send in null to search for submitted
        /// activities from the beginning of time.</param>
        /// <param name="endDate">The end date to search for submitted activities for the
        /// user based on when the activity was submitted.  Send in null to search for submitted
        /// activities unit the end of time.</param>
        /// <returns>
        /// A <see cref="System.Collections.Generic.IEnumerable&lt;SubmittedActivityItemDTO&gt;" />.
        /// </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
        Task<IEnumerable<SubmittedActivityItemDTO>> GetSubmittedActivitiesForEmployeeIdByActivityIdAsync(int employeeId, int activityId, DateTime? startDate, DateTime? endDate);
    }
}