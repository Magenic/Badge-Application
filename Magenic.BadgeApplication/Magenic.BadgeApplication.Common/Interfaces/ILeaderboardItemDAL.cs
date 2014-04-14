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
        /// <param name="employeeId">The user identifier.</param>
        /// <returns></returns>
        Task<LeaderboardItemDTO> GetLeaderboardItemForEmployeeIdAsync(int employeeId);
    }
}
