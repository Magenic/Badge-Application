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
        public async Task<LeaderboardItemDTO> GetLeaderboardItemForUserNameAsync(string userName)
        {
            using (var dataContext = new Entities())
            {
                dataContext.Database.Connection.Open();
                var leaderBoardItem = await (from emp in dataContext.Employees
                                             join eb in dataContext.EarnedBadges on emp.EmployeeId equals eb.EmployeeId into grp
                                             where emp.ADName == userName && emp.EmploymentEndDate == null
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
                                                     DisplayOnce = b.DisplayOnce,
                                                     BadgeAwardId = b.BadgeAwardId
                                                 })
                                             }).SingleAsync();

                return leaderBoardItem;
            }
        }

        /// <summary>
        /// Deletes a badge from a user.
        /// </summary>
        /// <param name="badgeAwardId">The BadgeAward Id to delete.</param>
        public void Delete(int badgeAwardId)
        {
            using (var ctx = new Entities())
            {
                ctx.Database.Connection.Open();
                var badgeAward = ctx.BadgeAwards.Where(ba => ba.BadgeAwardId == badgeAwardId).FirstOrDefault();
                if (badgeAward != null)
                {
                    ctx.BadgeAwards.Remove(badgeAward);
                    ctx.SaveChanges();
                }
            }
        }
    }
}
