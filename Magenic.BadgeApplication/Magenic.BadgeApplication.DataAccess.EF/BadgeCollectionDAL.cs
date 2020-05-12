﻿using Magenic.BadgeApplication.Common.DTO;
using Magenic.BadgeApplication.Common.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.DataAccess.EF
{
    public class BadgeCollectionDAL : IBadgeCollectionDAL
    {
        public async Task<IEnumerable<BadgeItemDTO>> GetBadgesByBadgeTypeAsync(Common.Enums.BadgeType badgeType)
        {
            using (var ctx = new Entities())
            {
                ctx.Database.Connection.Open();
                var badgeList = await (from t in ctx.CurrentActiveBadges
                                       where t.BadgeTypeId == (badgeType == Common.Enums.BadgeType.Unset ? t.BadgeTypeId : (int)badgeType)
                                       select new BadgeItemDTO
                                       {
                                           Id = t.BadgeId,
                                           Name = t.BadgeName,
                                           Type = (Common.Enums.BadgeType)t.BadgeTypeId,
                                           ImagePath = t.BadgePath,
                                           BadgePriority = t.BadgePriority,
                                           BadgeAwardValue = t.BadgeAwardValueAmount,
                                           BadgeAwardValueMax = t.BadgeAwardValueAmountMax
                                       }).ToArrayAsync();

                return badgeList;
            }
        }

        public async Task<IEnumerable<BadgeItemDTO>> GetBadgesByActivityIdsAsync(IEnumerable<int> activityIds)
        {
            using (var ctx = new Entities())
            {
                ctx.Database.Connection.Open();
                var badgeList = await (from t in ctx.CurrentActiveBadges
                                       join ba in ctx.BadgeActivities on t.BadgeId equals ba.BadgeId
                                       where activityIds.Contains(ba.ActivityId)
                                       select new BadgeItemDTO
                                       {
                                           Id = t.BadgeId,
                                           ActivityId = ba.ActivityId,
                                           Name = t.BadgeName,
                                           Type = (Common.Enums.BadgeType)t.BadgeTypeId,
                                           ImagePath = t.BadgePath,
                                           BadgePriority = t.BadgePriority,
                                           BadgeAwardValue = t.BadgeAwardValueAmount,
                                           BadgeAwardValueMax = t.BadgeAwardValueAmountMax
                                       }).ToArrayAsync();

                return badgeList;
            }
        }

        public async Task<IEnumerable<BadgeItemDTO>> GetInactiveBadgesAsync(InactiveBadgeSearchDTO searchDTO)
        {
            using (var ctx = new Entities())
            {
                ctx.Database.Connection.Open();
                var badgeList = await (from t in ctx.Badges
                                       where t.BadgeTypeId == (int)searchDTO.BadgeType
                                       && searchDTO.BadgeStatusList.Contains((Common.Enums.BadgeStatus)t.BadgeStatusId)
                                       && t.BadgeEffectiveEnd.HasValue
                                       && t.BadgeEffectiveEnd < searchDTO.InactiveEffectiveDate
                                       select new BadgeItemDTO
                                       {
                                           Id = t.BadgeId,
                                           Name = t.BadgeName,
                                           Type = (Common.Enums.BadgeType)t.BadgeTypeId,
                                           ImagePath = t.BadgePath,
                                           BadgePriority = t.BadgePriority,
                                           BadgeAwardValue = t.BadgeAwardValueAmount,
                                           BadgeAwardValueMax = t.BadgeAwardValueAmountMax
                                       }).ToArrayAsync();

                return badgeList;
            }
        }
    }
}
