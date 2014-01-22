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
        /// <summary>
        /// Checks to see if a given name already exists in the database for an activity. 
        /// This method is case insensitive.  It will return not found if a matching name is
        /// found with the same id.
        /// </summary>
        /// <param name="id">The activity id.</param>
        /// <param name="name">The name to look for.</param>
        /// <returns>A <see cref="bool"/> indicating if the supplied name was found in the database.</returns>
        bool ActivityNameExists(int id, string name);
    }
}
