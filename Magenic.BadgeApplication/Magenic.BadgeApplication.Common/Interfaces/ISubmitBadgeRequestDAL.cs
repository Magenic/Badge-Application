using System.Threading.Tasks;
using Magenic.BadgeApplication.Common.DTO;

namespace Magenic.BadgeApplication.Common.Interfaces
{
    /// <summary>
    /// Data Access Layer interface to load for classes implementing <see cref="ISubmitBadgeRequest"/>.
    /// </summary>
    public interface ISubmitBadgeRequestDAL
    {
        /// <summary>
        /// Asynchronously returns an <see cref="SubmitBadgeRequestDTO"/> for the specified id.
        /// </summary>
        /// <param name="badgeRequestSubmissionId">The badge request submission id to search for.</param>
        /// <returns>A <see cref="SubmitActivityDTO"/>.</returns>
        Task<SubmitBadgeRequestDTO> GetActivitySubmissionByIdAsync(int badgeRequestSubmissionId);
        /// <summary>
        /// Updates an existing badge request submission based on information passed in via the DTO.
        /// </summary>
        /// <param name="data">The values to update.</param>
        /// <returns>A DTO with updated values after the save.</returns>
        SubmitBadgeRequestDTO Update(SubmitBadgeRequestDTO data);
        /// <summary>
        /// Inserts a new badge request submission based on information passed in via the DTO.
        /// </summary>
        /// <param name="data">The values to update.</param>
        /// <returns>A DTO with updated values after the insert.</returns>
        SubmitBadgeRequestDTO Insert(SubmitBadgeRequestDTO data);
        /// <summary>
        /// Removes the specified activity submission.
        /// </summary>
        /// <param name="badgeRequestSubmissionId">The id of the badge request submission to remove.</param>
        void Delete(int badgeRequestSubmissionId);
    }
}
