using Magenic.BadgeApplication.Common.DTO;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.Common.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface ILeaderboardItemDAL
    {
        /// <summary>
        /// Gets the leaderboard item for user identifier.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        Task<LeaderboardItemDTO> GetLeaderboardItemForUserNameAsync(string userName);
        
        /// <summary>
        /// Deletes a badge from a user.
        /// </summary>
        /// <param name="badgeAwardId">The BadgeAward Id to delete.</param>
        void Delete(int badgeAwardId);

        /// <summary>
        /// Gets the permission level for a user.
        /// </summary>
        /// <param name="employeeId">Id of the user.</param>
        /// <returns></returns>
        int GetAdminUserPermissions(int employeeId);
    }
}
