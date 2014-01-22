using System.Threading.Tasks;
using Magenic.BadgeApplication.Common.DTO;

namespace Magenic.BadgeApplication.Common.Interfaces
{
    /// <summary>
    /// Data Access Layer interface to load for classes implementing <see cref="ISubmitActivity"/>.
    /// </summary>
    public interface ISubmitActivityDAL
    {
        /// <summary>
        /// Asynchronously returns an <see cref="SubmitActivityDTO"/> for the specified id.
        /// </summary>
        /// <param name="activitySubmissionId">The activity submission id to search for.</param>
        /// <returns>A <see cref="SubmitActivityDTO"/>.</returns>
        Task<SubmitActivityDTO> GetActivitySubmissionByIdAsync(int activitySubmissionId);
        /// <summary>
        /// Updates an existing activity submission based on information passed in via the DTO.
        /// </summary>
        /// <param name="data">The values to update.</param>
        /// <returns>A DTO with updated values after the save.</returns>
        SubmitActivityDTO Update(SubmitActivityDTO data);
        /// <summary>
        /// Inserts a new activity submission based on information passed in via the DTO.
        /// </summary>
        /// <param name="data">The values to update.</param>
        /// <returns>A DTO with updated values after the insert.</returns>
        SubmitActivityDTO Insert(SubmitActivityDTO data);
        /// <summary>
        /// Removes the specified activity submission.
        /// </summary>
        /// <param name="activitySubmissionId">The id of the activity submission to remove.</param>
        void Delete(int activitySubmissionId);
    }
}
