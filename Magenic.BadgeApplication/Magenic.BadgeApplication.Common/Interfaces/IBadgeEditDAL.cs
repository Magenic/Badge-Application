using System.Threading.Tasks;
using Magenic.BadgeApplication.Common.DTO;

namespace Magenic.BadgeApplication.Common.Interfaces
{
    /// <summary>
    /// Data Access Layer interface to load for classes implementing <see cref="IBadgeEdit"/>.
    /// </summary>
    public interface IBadgeEditDAL
    {
        /// <summary>
        /// Asynchronously returns an <see cref="BadgeEditDTO"/> for the specified id.
        /// </summary>
        /// <param name="badgeEditId">The activity id to search for.</param>
        /// <returns>An <see cref="BadgeEditDTO"/>.</returns>
        Task<BadgeEditDTO> GetBadgeByIdAsync(int badgeEditId);
        /// <summary>
        /// Updates an existing badge based on information passed in via the DTO.
        /// </summary>
        /// <param name="data">The values to update.</param>
        /// <returns>A DTO with updated values after the save.</returns>
        BadgeEditDTO Update(BadgeEditDTO data);
        /// <summary>
        /// Inserts a new badge based on information passed in via the DTO.
        /// </summary>
        /// <param name="data">The values to update.</param>
        /// <returns>A DTO with updated values after the insert.</returns>
        BadgeEditDTO Insert(BadgeEditDTO data);
        /// <summary>
        /// Removes the specified badge.
        /// </summary>
        /// <param name="badgeId">The id of the badge to remove.</param>
        void Delete(int badgeId);
    }
}
