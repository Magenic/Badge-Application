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
    public class BadgeAwardEditCollectionDAL
        : IBadgeAwardEditCollectionDAL
    {
        /// <summary>
        /// Gets all badge awards for user asynchronous.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        public async Task<IEnumerable<BadgeAwardEditDTO>> GetAllBadgeAwardsForUserAsync(string userName)
        {
            using (var ctx = new Entities())
            {
                ctx.Database.Connection.Open();
                var badgeAwardList = await (from ba in ctx.BadgeAwards
                                            where ba.Employee.ADName == userName
                                                && !ba.PaidOut
                                            select new BadgeAwardEditDTO
                                            {
                                                Id = ba.BadgeAwardId,
                                                EmployeeId = ba.Employee.EmployeeId,
                                                EmployeeADName = ba.Employee.ADName,
                                                BadgeId = ba.BadgeId,
                                                BadgeName = ba.Badge.BadgeName,
                                                AwardDate = ba.AwardDate,
                                                AwardAmount = ba.AwardAmount,
                                            }).ToArrayAsync();

                return badgeAwardList;
            }
        }

        /// <summary>
        /// Gets all badge awards for user asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<BadgeAwardEditDTO>> GetAllBadgeAwardsAsync()
        {
            using (var ctx = new Entities())
            {
                ctx.Database.Connection.Open();
                var badgeAwardList = await (from ba in ctx.BadgeAwards
                                            where !ba.PaidOut
                                            select new BadgeAwardEditDTO
                                            {
                                                Id = ba.BadgeAwardId,
                                                EmployeeId = ba.Employee.EmployeeId,
                                                EmployeeADName = ba.Employee.ADName,
                                                BadgeId = ba.BadgeId,
                                                BadgeName = ba.Badge.BadgeName,
                                                AwardDate = ba.AwardDate,
                                                AwardAmount = ba.AwardAmount,
                                            }).ToArrayAsync();

                return badgeAwardList;
            }
        }
    }
}
