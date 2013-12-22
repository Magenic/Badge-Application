using System.Threading.Tasks;

namespace Magenic.BadgeApplication.Common.Interfaces
{
    /// <summary>
    /// Data Access Layer interface to load for classes implementing <see cref="IBadgeEdit"/>.
    /// </summary>
    public interface IBadgeEditDAL
    {
        /// <summary>
        /// Asynchronously returns an <see cref="IActivityEditDTO"/> for the specified id.
        /// </summary>
        /// <param name="badgeEditId">The activity id to search for.</param>
        /// <returns>An <see cref="IBadgeEditDTO"/>.</returns>
        Task<IBadgeEditDTO> GetBadgeByIdAsync(int badgeEditId);
        /// <summary>
        /// Updates an existing badge based on information passed in via the DTO.
        /// </summary>
        /// <param name="data">The values to update.</param>
        /// <returns>A DTO with updated values after the save.</returns>
        IBadgeEditDTO Update(IBadgeEditDTO data);
        /// <summary>
        /// Inserts a new badge based on information passed in via the DTO.
        /// </summary>
        /// <param name="data">The values to update.</param>
        /// <returns>A DTO with updated values after the insert.</returns>
        IBadgeEditDTO Insert(IBadgeEditDTO data);
        /// <summary>
        /// Removes the specified badge.
        /// </summary>
        /// <param name="badgeId">The id of the badge to remove.</param>
        void Delete(int badgeId);
    }
}
