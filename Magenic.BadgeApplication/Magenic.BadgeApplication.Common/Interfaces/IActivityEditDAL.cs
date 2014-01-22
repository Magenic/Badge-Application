using Magenic.BadgeApplication.Common.DTO;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.Common.Interfaces
{
    /// <summary>
    /// Data Access Layer interface to load for classes implementing <see cref="IActivityEdit"/>.
    /// </summary>
    public interface IActivityEditDAL
    {
        /// <summary>
        /// Asynchronously returns an <see cref="ActivityEditDTO"/> for the specified id.
        /// </summary>
        /// <param name="activityEditId">The activity id to search for.</param>
        /// <returns>An <see cref="ActivityEditDTO"/>.</returns>
        Task<ActivityEditDTO> GetActivityByIdAsync(int activityEditId);
        /// <summary>
        /// Updates an existing activity based on information passed in via the DTO.
        /// </summary>
        /// <param name="data">The values to update.</param>
        /// <returns>A DTO with updated values after the save.</returns>
        ActivityEditDTO Update(ActivityEditDTO data);
        /// <summary>
        /// Inserts a new activity based on information passed in via the DTO.
        /// </summary>
        /// <param name="data">The values to update.</param>
        /// <returns>A DTO with updated values after the insert.</returns>
        ActivityEditDTO Insert(ActivityEditDTO data);
        /// <summary>
        /// Removes the specified activity.
        /// </summary>
        /// <param name="activityId">The id of the activity to remove.</param>
        void Delete(int activityId);
    }
}
