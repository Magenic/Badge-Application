using Magenic.BadgeApplication.Common.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.Common.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface ILeaderboardCollectionDAL
    {
        /// <summary>
        /// Gets the leader board.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<LeaderboardItemDTO>> GetLeaderBoard();
    }
}
