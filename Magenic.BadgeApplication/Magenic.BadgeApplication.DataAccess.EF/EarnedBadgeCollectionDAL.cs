using System.Data.Entity;
using Magenic.BadgeApplication.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.DataAccess.EF
{
    public class EarnedBadgeCollectionDAL : IEarnedBadgeCollectionDAL
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
        public async Task<IEnumerable<IEarnedBadgeItemDTO>> GetBadgesForUserByBadgeTypeAsync(string userADName, Common.Enums.BadgeType badgeType)
        {
            using (var ctx = new Entities())
            {
                ctx.Database.Connection.Open();
                var badgeList = await (from eb in ctx.EarnedBadges
                                       where eb.ADName == userADName
                                       where eb.BadgeTypeId == (badgeType == Common.Enums.BadgeType.Unset ? eb.BadgeTypeId : (int)badgeType)
                                       select new Common.DTO.EarnedBadgeItemDTO
                                       {
                                           Id = eb.BadgeId,
                                           Name = eb.BadgeName,
                                           Type = (Common.Enums.BadgeType)eb.BadgeTypeId,
                                           ImagePath = eb.BadgePath,
                                           Tagline = eb.BadgeTagLine,
                                           AwardDate = eb.AwardDate,
                                           AwardPoints = eb.AwardAmount
                                       }).ToArrayAsync();
                return badgeList;
            }
        }

    }
}
