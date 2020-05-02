using Magenic.BadgeApplication.Common.DTO;
using Magenic.BadgeApplication.Common.Exceptions;
using Magenic.BadgeApplication.Common.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.DataAccess.EF
{
    public class EarnedBadgeCollectionDAL : IEarnedBadgeCollectionDAL
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
        public async Task<IEnumerable<EarnedBadgeItemDTO>> GetBadgesForUserByBadgeTypeAsync(int employeeId, Common.Enums.BadgeType badgeType)
        {
            using (var ctx = new Entities())
            {
                ctx.Database.Connection.Open();
                var badgeList = await (from eb in ctx.EarnedBadges
                                       where eb.EmployeeId == employeeId
                                       where eb.BadgeTypeId == (badgeType == Common.Enums.BadgeType.Unset ? eb.BadgeTypeId : (int)badgeType)
                                       select new EarnedBadgeItemDTO
                                       {
                                           Id = eb.BadgeId,
                                           Name = eb.BadgeName,
                                           Description = eb.BadgeDescription,
                                           Type = (Common.Enums.BadgeType)eb.BadgeTypeId,
                                           ImagePath = eb.BadgePath,
                                           Tagline = eb.BadgeTagLine,
                                           AwardDate = eb.AwardDate,
                                           AwardPoints = eb.AwardAmount,
                                           PaidOut = eb.PaidOut,
                                           BadgePriority = eb.BadgePriority,
                                           DisplayOnce = eb.DisplayOnce
                                       }).ToArrayAsync();
                return badgeList;
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
        public async Task<IEnumerable<EarnedBadgeItemDTO>> GetBadgesAsync()
        {
            using (var ctx = new Entities())
            {
                ctx.Database.Connection.Open();
                var badgeList = await (from eb in ctx.EarnedBadges
                                       select new EarnedBadgeItemDTO
                                       {
                                           BadgeAwardId = eb.BadgeAwardId,
                                           Id = eb.BadgeId,
                                           Name = eb.BadgeName,
                                           Description = eb.BadgeDescription,
                                           Type = (Common.Enums.BadgeType)eb.BadgeTypeId,
                                           ImagePath = eb.BadgePath,
                                           Tagline = eb.BadgeTagLine,
                                           AwardDate = eb.AwardDate,
                                           AwardPoints = eb.AwardAmount,
                                           PaidOut = eb.PaidOut,
                                           BadgePriority = eb.BadgePriority,
                                           DisplayOnce = eb.DisplayOnce,
                                           EmployeeName = eb.FirstName + " " + eb.LastName,
                                           BadgeEffectiveEnd = eb.BadgeEffectiveEnd,
                                           AwardAmount = eb.AwardAmount
                                       }).ToArrayAsync();
                return badgeList;
            }
        }

        public async Task<EarnedBadgeItemDTO> GetEarnedBadge(int badgeAwardId)
        {
            using (var ctx = new Entities())
            {
                BadgeAward badgeAward = await (ctx.BadgeAwards.Include(b => b.Badge)).SingleOrDefaultAsync(badges => badges.BadgeAwardId == badgeAwardId);
                if (badgeAward == null)
                { 
                    throw new NotFoundException(string.Format("Badge award with id {0} was not found", badgeAwardId));
                }

                var earnedBadge = new EarnedBadgeItemDTO
                {
                    BadgeAwardId = badgeAward.BadgeAwardId,
                    AwardDate = badgeAward.AwardDate,
                    AwardPoints = badgeAward.Badge.BadgeAwardValueAmount,
                    BadgePriority = badgeAward.Badge.BadgePriority,
                    DisplayOnce = badgeAward.Badge.DisplayOnce,
                    EmployeeADName = badgeAward.Employee.ADName,
                    EmployeeEmailAddr = badgeAward.Employee.EmailAddress,
                    Id = badgeAward.BadgeAwardId,
                    ImagePath = badgeAward.Badge.BadgePath,
                    Name = badgeAward.Badge.BadgeName,
                    PaidOut = badgeAward.PaidOut,
                    Tagline = badgeAward.Badge.BadgeTagLine,
                    Type = (Common.Enums.BadgeType)badgeAward.Badge.BadgeTypeId
                };

                return earnedBadge;
            }
        }

        public void Delete(int badgeAwardId)
        {
            using (var ctx = new Entities())
            {
                ctx.Database.Connection.Open();

                var badgeAward = new BadgeAward
                {
                    BadgeAwardId = badgeAwardId
                };

                ctx.BadgeAwards.Attach(badgeAward);
                ctx.BadgeAwards.Remove(badgeAward);
                ctx.SaveChanges();
            }
        }

        public void DeleteQueueEventLogs(int badgeAwardId)
        {
            using (var ctx = new Entities())
            {
                ctx.Database.Connection.Open();
                var records = ctx.QueueEventLogs.Where(item => item.BadgeAwardId == badgeAwardId).ToList();
                ctx.QueueEventLogs.RemoveRange(records);
                ctx.SaveChanges();
            }
        }

        public void DeleteQueueItems(int badgeAwardId)
        {
            using (var ctx = new Entities())
            {
                ctx.Database.Connection.Open();
                var records = ctx.QueueItems.Where(item => item.BadgeAwardId == badgeAwardId).ToList();
                ctx.QueueItems.RemoveRange(records);
                ctx.SaveChanges();
            }
        }
    }
}