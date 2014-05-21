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
        public async Task<IEnumerable<LeaderboardItemDTO>> GetLeaderboardAsync()
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
                                                  EmployeeADName = emp.ADName,
                                                  EmployeeLocation = emp.Location,
                                                  EmployeeDepartment = emp.Department,
                                                  EarnedBadges = grp.Select(b => new EarnedBadgeItemDTO()
                                                  {
                                                      Id = b.BadgeId,
                                                      Name = b.BadgeName,
                                                      Description = b.BadgeDescription,
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

        /// <summary>
        /// Gets the leader board based on a search term.
        /// </summary>
        /// <param name="searchTerm">The search term.</param>
        /// <returns></returns>
        public async Task<IEnumerable<LeaderboardItemDTO>> SearchLeaderboardAsync(string searchTerm)
        {
            using (var dataContext = new Entities())
            {
                dataContext.Database.Connection.Open();
                var leaderBoardItems = await (from emp in dataContext.Employees
                                              join eb in dataContext.EarnedBadges on emp.EmployeeId equals eb.EmployeeId into grp
                                              where emp.ADName.Contains(searchTerm) || emp.FirstName.Contains(searchTerm) || emp.LastName.Contains(searchTerm)
                                              orderby emp.LastName, emp.FirstName
                                              select new LeaderboardItemDTO
                                              {
                                                  EmployeeId = emp.EmployeeId,
                                                  EmployeeFirstName = emp.FirstName,
                                                  EmployeeLastName = emp.LastName,
                                                  EmployeeADName = emp.ADName,
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
