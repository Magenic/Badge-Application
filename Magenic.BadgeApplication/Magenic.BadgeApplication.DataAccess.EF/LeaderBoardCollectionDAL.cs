using Magenic.BadgeApplication.Common.DTO;
using Magenic.BadgeApplication.Common.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.DataAccess.EF
{
    /// <summary>
    /// 
    /// </summary>
    public class LeaderboardCollectionDAL
        : ILeaderboardCollectionDAL
    {
        /// <summary>
        /// Gets the leader board.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<LeaderboardItemDTO>> GetLeaderBoardAsync()
        {
            using (var dataContext = new Entities())
            {
                dataContext.Database.Connection.Open();
                var leaderBoardItems = await (from emp in dataContext.Employees
                                              join eb in dataContext.EarnedBadges on emp.EmployeeId equals eb.EmployeeId into grp
                                              orderby emp.LastName, emp.FirstName
                                              select new LeaderboardItemDTO
                                              {
                                                  EmployeeId = emp.EmployeeId,
                                                  EmployeeFirstName = emp.FirstName,
                                                  EmployeeLastName = emp.LastName,
                                                  EarnedBadges = grp.Select(b => new EarnedBadgeItemDTO()
                                                  {
                                                      Id = b.BadgeId,
                                                      Name = b.BadgeName,
                                                      Type = (Common.Enums.BadgeType)b.BadgeTypeId,
                                                      ImagePath = b.BadgePath,
                                                      Tagline = b.BadgeTagLine,
                                                      AwardDate = b.AwardDate,
                                                      AwardPoints = b.AwardAmount,
                                                      PaidOut = b.PaidOut,
                                                      BadgePriority = b.BadgePriority,
                                                      DisplayOnce = b.DisplayOnce
                                                  })
                                              }).ToArrayAsync();
                return leaderBoardItems;
            }
        }
    }
}
