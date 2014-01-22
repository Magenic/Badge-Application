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
                var badgeList = await (from t in ctx.Badges
                                       where t.BadgeId == badgeEditId
                                       select new BadgeEditDTO
                                       {
                                           Id = t.BadgeId,
                                           Name = t.BadgeName,
                                           Tagline = t.BadgeTagLine,
                                           Description = t.BadgeDescription,
                                           Type = (Common.Enums.BadgeType)t.BadgeTypeId,
                                           ImagePath = t.BadgePath,
                                           Created = t.BadgeCreated,
                                           EffectiveStartDate = t.BadgeEffectiveStart,
                                           EffectiveEndDate = t.BadgeEffectiveEnd,
                                           Priority = t.BadgePriority,
                                           MultipleAwardsPossible = t.MultipleAwardPossible,
                                           DisplayOnce = t.DisplayOnce,
                                           ManagementApprovalRequired = t.ManagementApprovalRequired,
                                           ActivityPointsAmount = t.ActivityPointsAmount,
                                           AwardValueAmount = t.BadgeAwardValueAmount,
                                           ApprovedById = t.BadgeApprovedById ?? 0,
                                           ApprovedDate = t.BadgeApprovedDate
                                       }).ToArrayAsync();

                var badge = badgeList.Single();

                return badge;
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0")]
        public BadgeEditDTO Update(BadgeEditDTO data)
        {
            this.SaveToBlobStorage(data);

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
                objectState.GetObjectStateEntry(saveBadge).SetModifiedProperty("BadgeApprovedByADName");
                objectState.GetObjectStateEntry(saveBadge).SetModifiedProperty("BadgeApprovedDate");

                ctx.SaveChanges();
                data.Id = saveBadge.BadgeId;
            }
            return data;
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
                BadgeApprovedDate = data.ApprovedDate
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
                data.Id = saveBadge.BadgeId;
            }
            return data;
        }

        public void Delete(int badgeId)
        {
            using (var ctx = new Entities())
            {
                ctx.Database.Connection.Open();
                var deleteBadge = new Badge
                {
                    BadgeId = badgeId
                };
                ctx.Badges.Attach(deleteBadge);
                ctx.Badges.Remove(deleteBadge);
                ctx.SaveChanges();
            }
        }
    }
}
