using Magenic.BadgeApplication.Common.DTO;

namespace Magenic.BadgeApplication.Common.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBadgeAwardEditDAL
    {
        /// <summary>
        /// Updates an existing activity based on information passed in via the DTO.
        /// </summary>
        /// <param name="data">The values to update.</param>
        /// <returns>A DTO with updated values after the save.</returns>
        BadgeAwardEditDTO Update(BadgeAwardEditDTO data);
    }
}
