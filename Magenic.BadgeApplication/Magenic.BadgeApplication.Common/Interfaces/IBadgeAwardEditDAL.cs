using Magenic.BadgeApplication.Common.DTO;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.Common.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBadgeAwardEditDAL
    {
        /// <summary>
        /// Gets the badge award by identifier asynchronous.
        /// </summary>
        /// <param name="badgeAwardEditId">The badge award edit identifier.</param>
        /// <returns></returns>
        Task<BadgeAwardEditDTO> GetBadgeAwardByIdAsync(int badgeAwardEditId);

        /// <summary>
        /// Updates an existing activity based on information passed in via the DTO.
        /// </summary>
        /// <param name="data">The values to update.</param>
        /// <returns>A DTO with updated values after the save.</returns>
        BadgeAwardEditDTO Update(BadgeAwardEditDTO data);
    }
}
