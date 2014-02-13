using Magenic.BadgeApplication.Common.DTO;
using Magenic.BadgeApplication.Common.Enums;
using Magenic.BadgeApplication.Common.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.DataAccess.EF
{
    public class ApproveBadgeCollectionDAL : IApproveBadgeCollectionDAL
    {
        public async Task<IEnumerable<ApproveBadgeItemDTO>> GetBadgesToApproveAsync()
        {
            using (var ctx = new Entities())
            {
                ctx.Database.Connection.Open();
                var activityList = await(from t in ctx.Badges
                                         where t.BadgeStatusId == (int)BadgeStatus.AwaitingApproval
                                         select new ApproveBadgeItemDTO
                                         {
                                             BadgeId = t.BadgeId,
                                             Description = t.BadgeDescription,
                                             Name = t.BadgeName,
                                             ImagePath = t.BadgePath,
                                             Tagline = t.BadgeTagLine,
                                             Type = (Common.Enums.BadgeType)t.BadgeTypeId,
                                             AwardValueAmount = t.BadgeAwardValueAmount,
                                             ApprovedById = t.BadgeApprovedById ?? 0,
                                             ApprovedDate = t.BadgeApprovedDate,
                                             BadgeStatus = (BadgeStatus)t.BadgeStatusId
                                         }).ToArrayAsync();

                return activityList;
            }
        }

        public async Task<ApproveBadgeItemDTO> GetBadgeToApproveByIdAsync(int badgeId)
        {
            using (var ctx = new Entities())
            {
                ctx.Database.Connection.Open();
                var activityList = await(from t in ctx.Badges
                                         where t.BadgeStatusId == (int)BadgeStatus.AwaitingApproval
                                         where t.BadgeId == badgeId
                                         select new ApproveBadgeItemDTO
                                         {
                                             BadgeId = t.BadgeId,
                                             Description = t.BadgeDescription,
                                             Name = t.BadgeName,
                                             ImagePath = t.BadgePath,
                                             Tagline = t.BadgeTagLine,
                                             Type = (Common.Enums.BadgeType)t.BadgeTypeId,
                                             AwardValueAmount = t.BadgeAwardValueAmount,
                                             ApprovedById = t.BadgeApprovedById ?? 0,
                                             ApprovedDate = t.BadgeApprovedDate,
                                             BadgeStatus = (BadgeStatus)t.BadgeStatusId
                                         }).ToArrayAsync();

                return activityList.Single();
            }
        }

        public ApproveBadgeItemDTO Update(ApproveBadgeItemDTO data)
        {
            using (var ctx = new Entities())
            {
                ctx.Database.Connection.Open();
                ctx.Configuration.ValidateOnSaveEnabled = false;
                var saveBadge = LoadData(data);
                ctx.Badges.Attach(saveBadge);
                var objectState = ((IObjectContextAdapter)ctx).ObjectContext.ObjectStateManager;
                objectState.GetObjectStateEntry(saveBadge).SetModifiedProperty("BadgeApprovedById");
                objectState.GetObjectStateEntry(saveBadge).SetModifiedProperty("BadgeStatusId");
                objectState.GetObjectStateEntry(saveBadge).SetModifiedProperty("BadgeApprovedDate");
                ctx.SaveChanges();
            }
            return data;
        }

        private static Badge LoadData(ApproveBadgeItemDTO data)
        {
            var badge = new Badge
            {
                BadgeId = data.BadgeId,
                BadgeApprovedById = data.ApprovedById == 0 ? null : (int?)data.ApprovedById,
                BadgeStatusId = (int)data.BadgeStatus,
                BadgeApprovedDate = data.ApprovedDate
            };
            return badge;
        }
    }
}
