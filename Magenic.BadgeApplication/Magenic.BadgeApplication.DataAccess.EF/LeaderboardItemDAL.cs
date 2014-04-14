using Magenic.BadgeApplication.Common.DTO;
using Magenic.BadgeApplication.Common.Interfaces;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.DataAccess.EF
{
    /// <summary>
    /// 
    /// </summary>
    public class LeaderboardItemDAL
        : ILeaderboardItemDAL
    {
        /// <summary>
        /// Gets the leaderboard item for user identifier.
        /// </summary>
        /// <param name="employeeId">The user identifier.</param>
        /// <returns></returns>
        public async Task<LeaderboardItemDTO> GetLeaderboardItemForEmployeeIdAsync(int employeeId)
        {
            using (var dataContext = new Entities())
            {
                dataContext.Database.Connection.Open();
                var leaderBoardItem = await (from emp in dataContext.Employees
                                             join eb in dataContext.EarnedBadges on emp.EmployeeId equals eb.EmployeeId into grp
                                             where emp.EmployeeId == employeeId
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
                                             }).SingleAsync();

                return leaderBoardItem;
            }
        }
    }
}
