using Magenic.BadgeApplication.Common.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.DataAccess.EF
{
    public class BadgeCollectionDAL : IBadgeCollectionDAL
    {
        public async Task<IEnumerable<IBadgeItemDTO>> GetBadgesByBadgeTypeAsync(Common.Enums.BadgeType badgeType)
        {
            using (var ctx = new Entities())
            {
                ctx.Database.Connection.Open();
                var badgeList = await(from t in ctx.CurrentActiveBadges
                                      where t.BadgeTypeId == (badgeType == Common.Enums.BadgeType.Unset? t.BadgeTypeId : (int)badgeType)
                                         select new Common.DTO.BadgeItemDTO
                                         {
                                             Id = t.BadgeId,
                                             Name = t.BadgeName,
                                             Type = (Common.Enums.BadgeType)t.BadgeTypeId,
                                             ImagePath = t.BadgePath
                                         }).ToArrayAsync();

                return badgeList;
            }
        }

        public async Task<IEnumerable<IBadgeItemDTO>> GetBadgesForUserByBadgeTypeAsync(string userName, Common.Enums.BadgeType badgeType)
        {
            using (var ctx = new Entities())
            {
                ctx.Database.Connection.Open();
                var badgeList = await (from eb in ctx.EarnedBadges
                                       where eb.ADName == userName
                                       where eb.BadgeTypeId == (badgeType == Common.Enums.BadgeType.Unset ? eb.BadgeTypeId : (int)badgeType)
                                       select new Common.DTO.BadgeItemDTO
                                       {
                                           Id = eb.BadgeId,
                                           Name = eb.BadgeName,
                                           Type = (Common.Enums.BadgeType)eb.BadgeTypeId,
                                           ImagePath = eb.BadgePath
                                       }).ToArrayAsync();
                return badgeList;
            }
        }
    }
}
