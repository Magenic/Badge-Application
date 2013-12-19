using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.Common.Interfaces
{
    /// <summary>
    /// Data Access Layer interface to load for classes implementing <see cref="ISubmitActivity"/>.
    /// </summary>
    public interface ISubmitActivityDAL
    {
        /// <summary>
        /// Asynchronously returns an <see cref="ISubmitActivityDTO"/> for the specified id.
        /// </summary>
        /// <param name="activitySubmissionId">The activity submission id to search for.</param>
        /// <returns>A <see cref="ISubmitActivityDTO"/>.</returns>
        Task<ISubmitActivityDTO> GetActivitySubmissionByIdAsync(int activitySubmissionId);
        /// <summary>
        /// Updates an existing activity submission based on information passed in via the DTO.
        /// </summary>
        /// <param name="data">The values to update.</param>
        /// <returns>A DTO with updated values after the save.</returns>
        ISubmitActivityDTO Update(ISubmitActivityDTO data);
        /// <summary>
        /// Inserts a new activity submission based on information passed in via the DTO.
        /// </summary>
        /// <param name="data">The values to update.</param>
        /// <returns>A DTO with updated values after the insert.</returns>
        ISubmitActivityDTO Insert(ISubmitActivityDTO data);
        /// <summary>
        /// Removes the specified activity submission.
        /// </summary>
        /// <param name="activitySubmissionId">The id of the activity submission to remove.</param>
        void Delete(int activitySubmissionId);
    }
}
