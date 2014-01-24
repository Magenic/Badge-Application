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
                                       select new Common.DTO.EarnedBadgeItemDTO
                                       {
                                           Id = eb.BadgeId,
                                           Name = eb.BadgeName,
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


        public EarnedBadgeItemDTO GetEarnedBadge(int badgeAwardId)
        {
            using (var ctx = new Entities())
            {
                BadgeAward badgeAward = ctx.BadgeAwards.SingleOrDefault(badges => badges.BadgeAwardId == badgeAwardId);
                if (badgeAward == null)
                { 
                    throw new NotFoundException(string.Format("Badge award with id {0} was not found", badgeAwardId));
                }
               
                EarnedBadgeItemDTO earnedBadge = new EarnedBadgeItemDTO()
                {
                    AwardDate = badgeAward.AwardDate,
                    AwardPoints = badgeAward.Badge.BadgeAwardValueAmount,
                    BadgePriority = badgeAward.Badge.BadgePriority,
                    DisplayOnce = badgeAward.Badge.DisplayOnce,
                    EmployeeADName = badgeAward.Employee.ADName,
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
    }
}