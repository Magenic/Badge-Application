using Magenic.BadgeApplication.Common.DTO;
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
    }
}