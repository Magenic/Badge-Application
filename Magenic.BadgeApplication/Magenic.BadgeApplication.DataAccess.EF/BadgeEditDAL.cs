using System.Collections.Generic;
using Magenic.BadgeApplication.Common.DTO;
using Magenic.BadgeApplication.Common.Interfaces;
using Microsoft.WindowsAzure.Storage;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.DataAccess.EF
{
    public class BadgeEditDAL : IBadgeEditDAL
    {
        public async Task<BadgeEditDTO> GetBadgeByIdAsync(int badgeEditId)
        {
            using (var ctx = new Entities())
            {
                ctx.Database.Connection.Open();
                
                var badgeList = await (from t in ctx.Badges.Include("BadgeActivities")
                                       where t.BadgeId == badgeEditId
                                       select t).ToListAsync();
                var badge = badgeList.Single();
                var returnValue = LoadReturnData(badge);

                return returnValue;
            }
        }

        private BadgeEditDTO LoadReturnData(Badge badge)
        {
            var returnValue = new BadgeEditDTO
            {
                Id = badge.BadgeId,
                Name = badge.BadgeName,
                Tagline = badge.BadgeTagLine,
                Description = badge.BadgeDescription,
                Type = (Common.Enums.BadgeType)badge.BadgeTypeId,
                ImagePath = badge.BadgePath,
                Created = badge.BadgeCreated,
                EffectiveStartDate = badge.BadgeEffectiveStart,
                EffectiveEndDate = badge.BadgeEffectiveEnd,
                Priority = badge.BadgePriority,
                MultipleAwardsPossible = badge.MultipleAwardPossible,
                DisplayOnce = badge.DisplayOnce,
                ManagementApprovalRequired = badge.ManagementApprovalRequired,
                ActivityPointsAmount = badge.ActivityPointsAmount,
                AwardValueAmount = badge.BadgeAwardValueAmount,
                ApprovedById = badge.BadgeApprovedById ?? 0,
                ApprovedDate = badge.BadgeApprovedDate,
                BadgeStatus = (Common.Enums.BadgeStatus)badge.BadgeStatusId,
                CreateEmployeeId = badge.CreateEmployeeId,
                BadgeActivities = new List<BadgeActivityEditDTO>()
            };
            foreach (var badgeActivity in badge.BadgeActivities)
            {
                returnValue.BadgeActivities.Add(new BadgeActivityEditDTO
                {
                    BadgeActivityId = badgeActivity.BadgeActivityId,
                    ActivityId = badgeActivity.ActivityId
                });
            }
            return returnValue;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0")]
        public BadgeEditDTO Update(BadgeEditDTO data)
        {
            using (var ctx = new Entities())
            {
                ctx.Database.Connection.Open();
                var saveBadge = LoadData(data);
                ctx.Badges.Attach(saveBadge);
                var objectState = ((IObjectContextAdapter)ctx).ObjectContext.ObjectStateManager;
                objectState.GetObjectStateEntry(saveBadge).SetModifiedProperty("BadgeName");
                objectState.GetObjectStateEntry(saveBadge).SetModifiedProperty("BadgeTagLine");
                objectState.GetObjectStateEntry(saveBadge).SetModifiedProperty("BadgeDescription");
                objectState.GetObjectStateEntry(saveBadge).SetModifiedProperty("BadgeTypeId");
                objectState.GetObjectStateEntry(saveBadge).SetModifiedProperty("BadgePath");
                objectState.GetObjectStateEntry(saveBadge).SetModifiedProperty("BadgeCreated");
                objectState.GetObjectStateEntry(saveBadge).SetModifiedProperty("BadgeEffectiveStart");
                objectState.GetObjectStateEntry(saveBadge).SetModifiedProperty("BadgeEffectiveEnd");
                objectState.GetObjectStateEntry(saveBadge).SetModifiedProperty("BadgePriority");
                objectState.GetObjectStateEntry(saveBadge).SetModifiedProperty("MultipleAwardPossible");
                objectState.GetObjectStateEntry(saveBadge).SetModifiedProperty("DisplayOnce");
                objectState.GetObjectStateEntry(saveBadge).SetModifiedProperty("ManagementApprovalRequired");
                objectState.GetObjectStateEntry(saveBadge).SetModifiedProperty("ActivityPointsAmount");
                objectState.GetObjectStateEntry(saveBadge).SetModifiedProperty("BadgeAwardValueAmount");
                objectState.GetObjectStateEntry(saveBadge).SetModifiedProperty("BadgeApprovedById");
                objectState.GetObjectStateEntry(saveBadge).SetModifiedProperty("BadgeApprovedDate");
                objectState.GetObjectStateEntry(saveBadge).SetModifiedProperty("BadgeStatusId");
                objectState.GetObjectStateEntry(saveBadge).SetModifiedProperty("CreateEmployeeId");

                AttachChildren(ctx, data, saveBadge.BadgeId);
                ctx.SaveChanges();

                this.SaveToBlobStorage(data);

                var badge = GetRefreshedBadgeInfo(ctx, saveBadge.BadgeId);
                data = LoadReturnData(badge);
            }

            return data;
        }

        private void AttachChildren(Entities ctx, BadgeEditDTO data, int badgeId)
        {
            foreach (var badgeActivity in data.BadgeActivities)
            {
                if (badgeActivity.IsDeleted && badgeActivity.BadgeActivityId > 0) // Delete
                {
                    var deleteBadgeActivity = new BadgeActivity
                    {
                        BadgeActivityId = badgeActivity.BadgeActivityId
                    };
                    ctx.BadgeActivities.Attach(deleteBadgeActivity);
                    ctx.BadgeActivities.Remove(deleteBadgeActivity);
                }
                else if (!badgeActivity.IsDeleted && badgeActivity.BadgeActivityId == 0) // Insert
                {
                    var insertBadgeActivity = new BadgeActivity
                    {
                        ActivityId = badgeActivity.ActivityId,
                        BadgeId = badgeId
                    };
                    ctx.BadgeActivities.Add(insertBadgeActivity);
                }
                else if (!badgeActivity.IsDeleted && badgeActivity.BadgeActivityId > 0) // Update
                {
                    var updateBadgeActivity = new BadgeActivity
                    {
                        BadgeActivityId = badgeActivity.BadgeActivityId,
                        ActivityId = badgeActivity.ActivityId,
                        BadgeId = badgeId
                    };
                    ctx.BadgeActivities.Attach(updateBadgeActivity);
                    var objectState = ((IObjectContextAdapter)ctx).ObjectContext.ObjectStateManager;
                    objectState.GetObjectStateEntry(updateBadgeActivity).SetModifiedProperty("ActivityId");
                    objectState.GetObjectStateEntry(updateBadgeActivity).SetModifiedProperty("BadgeId");
                }
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        private void SaveToBlobStorage(BadgeEditDTO data)
        {
            if (data.BadgeImage != null && data.BadgeImage.Length > 0)
            {
                var storageAccount =
                    CloudStorageAccount.Parse(ConfigurationManager.AppSettings["StorageAccountConnectionString"]);

                var blobClient = storageAccount.CreateCloudBlobClient();
                var container = blobClient.GetContainerReference(ConfigurationManager.AppSettings["StorageAccountBlobContainer"]);
                var blockBlob = container.GetBlockBlobReference(string.Format(CultureInfo.CurrentCulture, "badgeimage{0}", data.Id));

                blockBlob.UploadFromByteArray(data.BadgeImage, 0, data.BadgeImage.Length, null, null, null);

                data.ImagePath = blockBlob.Uri.ToString();
            }
        }

        private static Badge LoadData(BadgeEditDTO data)
        {
            var badgeEntity = new Badge
            {
                BadgeId = data.Id,
                BadgeName = data.Name,
                BadgeTagLine = data.Tagline,
                BadgeDescription = data.Description,
                BadgeTypeId = (int)data.Type,
                BadgePath = data.ImagePath,
                BadgeCreated = data.Created,
                BadgeEffectiveStart = data.EffectiveStartDate,
                BadgeEffectiveEnd = data.EffectiveEndDate,
                BadgePriority = data.Priority,
                MultipleAwardPossible = data.MultipleAwardsPossible,
                DisplayOnce = data.DisplayOnce,
                ManagementApprovalRequired = data.ManagementApprovalRequired,
                ActivityPointsAmount = data.ActivityPointsAmount,
                BadgeAwardValueAmount = data.AwardValueAmount,
                BadgeApprovedById = data.ApprovedById == 0 ? null : (int?)data.ApprovedById,
                BadgeStatusId = (int)data.BadgeStatus,
                BadgeApprovedDate = data.ApprovedDate,
                CreateEmployeeId = data.CreateEmployeeId
            };
            return badgeEntity;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0")]
        public BadgeEditDTO Insert(BadgeEditDTO data)
        {
            using (var ctx = new Entities())
            {
                ctx.Database.Connection.Open();
                var saveBadge = LoadData(data);
                ctx.Badges.Add(saveBadge);

                ctx.SaveChanges();
                AttachChildren(ctx, data, saveBadge.BadgeId);
                ctx.SaveChanges();

                data.Id = saveBadge.BadgeId;
                this.SaveToBlobStorage(data);
                saveBadge.BadgePath = data.ImagePath;
                var objectState = ((IObjectContextAdapter)ctx).ObjectContext.ObjectStateManager;
                objectState.GetObjectStateEntry(saveBadge).SetModifiedProperty("BadgePath");
                ctx.SaveChanges();

                var badge = GetRefreshedBadgeInfo(ctx, saveBadge.BadgeId);
                data = LoadReturnData(badge);
            }


            return data;
        }

        private static Badge GetRefreshedBadgeInfo(Entities ctx, int badgeId)
        {
            var badgeList = from t in ctx.Badges.Include("BadgeActivities")
                where t.BadgeId == badgeId
                select t;
            return badgeList.Single();
        }

        public void Delete(int badgeId)
        {
            using (var ctx = new Entities())
            {
                ctx.Database.Connection.Open();
                var badgeActivities = ctx.BadgeActivities.Where(ba => ba.BadgeId == badgeId).ToList();
                ctx.BadgeActivities.RemoveRange(badgeActivities);
                ctx.SaveChanges();
                
                var deleteBadge = new Badge
                {
                    BadgeId = badgeId
                };
                ctx.Badges.Attach(deleteBadge);
                ctx.Badges.Remove(deleteBadge);
                ctx.SaveChanges();
            }
        }

        public IList<BadgeEditDTO> GetPotentialBadgesForActivity(int activityId)
        {
            using (var ctx = new Entities())
            {
                ctx.Database.Connection.Open();
                var returnList = new List<BadgeEditDTO>();

                var badgeList = (from t in ctx.Badges.Include("BadgeActivities")
                                join ba in ctx.BadgeActivities on t.BadgeId equals ba.BadgeId
                                where ba.ActivityId == activityId
                                where t.BadgeStatusId == (int)Common.Enums.BadgeStatus.Approved
                                select t).Distinct();

                foreach (var badge in badgeList)
                {
                    returnList.Add(LoadReturnData(badge));
                }
                return returnList.ToList();
            }
        }
    }
}
