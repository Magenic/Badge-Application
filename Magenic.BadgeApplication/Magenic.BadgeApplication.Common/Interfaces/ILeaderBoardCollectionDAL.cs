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
        Task<IEnumerable<LeaderboardItemDTO>> GetLeaderBoardAsync();

        /// <summary>
        /// Gets the leader board based on a search term.
        /// </summary>
        /// <param name="searchTerm">The search term.</param>
        /// <returns></returns>
        Task<IEnumerable<LeaderboardItemDTO>> SearchLeaderboardAsync(string searchTerm);
    }
}
