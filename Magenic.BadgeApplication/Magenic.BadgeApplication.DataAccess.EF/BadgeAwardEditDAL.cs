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
    public class BadgeAwardEditDAL
        : IBadgeAwardEditDAL
    {
        /// <summary>
        /// Gets the badge award by identifier asynchronous.
        /// </summary>
        /// <param name="badgeAwardEditId">The badge award edit identifier.</param>
        /// <returns></returns>
        public async Task<BadgeAwardEditDTO> GetBadgeAwardByIdAsync(int badgeAwardEditId)
        {
            using (var ctx = new Entities())
            {
                ctx.Database.Connection.Open();
                var badgeAward = await (from ba in ctx.BadgeAwards
                                        where ba.BadgeAwardId == badgeAwardEditId
                                        select new BadgeAwardEditDTO
                                        {
                                            Id = ba.BadgeAwardId,
                                            EmployeeId = ba.Employee.EmployeeId,
                                            EmployeeADName = ba.Employee.ADName,
                                            BadgeId = ba.BadgeId,
                                            BadgeName = ba.Badge.BadgeName,
                                            AwardDate = ba.AwardDate,
                                            AwardAmount = ba.AwardAmount,
                                        }).SingleAsync();


                return badgeAward;
            }
        }

        /// <summary>
        /// Updates the asynchronous.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0")]
        public BadgeAwardEditDTO Update(BadgeAwardEditDTO data)
        {
            using (var ctx = new Entities())
            {
                ctx.Database.Connection.Open();
                var saveBadgeAward = ctx.BadgeAwards.Where(ba => ba.BadgeAwardId == data.Id).Single();
                saveBadgeAward.AwardAmount = data.AwardAmount;
                ctx.SaveChanges();
            }
            return data;
        }
    }
}
