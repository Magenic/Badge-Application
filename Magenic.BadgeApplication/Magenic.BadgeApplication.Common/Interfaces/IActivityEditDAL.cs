using System.Threading.Tasks;

namespace Magenic.BadgeApplication.Common.Interfaces
{
    /// <summary>
    /// Data Access Layer interface to load for classes implementing <see cref="IActivityEdit"/>.
    /// </summary>
    public interface IActivityEditDAL
    {
        /// <summary>
        /// Asynchronously returns an <see cref="IActivityEditDTO"/> for the specified id.
        /// </summary>
        /// <param name="activityEditId">The activity id to search for.</param>
        /// <returns>An <see cref="IActivityEditDTO"/>.</returns>
        Task<IActivityEditDTO> GetActivityByIdAsync(int activityEditId);
        /// <summary>
        /// Updates an existing activity based on information passed in via the DTO.
        /// </summary>
        /// <param name="data">The values to update.</param>
        /// <returns>A DTO with updated values after the save.</returns>
        IActivityEditDTO Update(IActivityEditDTO data);
        /// <summary>
        /// Inserts a new activity based on information passed in via the DTO.
        /// </summary>
        /// <param name="data">The values to update.</param>
        /// <returns>A DTO with updated values after the insert.</returns>
        IActivityEditDTO Insert(IActivityEditDTO data);
        /// <summary>
        /// Removes the specified activity.
        /// </summary>
        /// <param name="activityId">The id of the activity to remove.</param>
        void Delete(int activityId);
    }
}
