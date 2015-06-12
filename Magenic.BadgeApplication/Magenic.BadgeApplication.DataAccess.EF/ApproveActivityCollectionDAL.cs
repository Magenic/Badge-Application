using System.Threading;
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
    public class ApproveActivityCollectionDAL : IApproveActivityCollectionDAL
    {
        public async Task<IEnumerable<ApproveActivityItemDTO>> GetActivitiesToApproveForAdministratorAsync(IApproveActivityCollectionCriteria criteria)
        {
            using (var ctx = new Entities())
            {
                ApproveActivityItemDTO[] activityList;
                activityList = await (from t in ctx.ActivitySubmissions
                                      join e in ctx.Employees on t.EmployeeId equals e.EmployeeId
                                      join a in ctx.Activities on t.ActivityId equals a.ActivityId
                                      where t.SubmissionStatusId == (int)ActivitySubmissionStatus.AwaitingApproval
                                      select new ApproveActivityItemDTO
                                      {
                                          SubmissionId = t.ActivitySubmissionId,
                                          SubmissionDate = t.SubmissionDate,
                                          ActivityId = t.ActivityId,
                                          ActivityDescription = a.ActivityDescription,
                                          ActivityName = a.ActivityName,
                                          ApprovedById = t.SubmissionApprovedById ?? 0,
                                          EmployeeId = t.EmployeeId,
                                          EmployeeADName = e.ADName,
                                          EmployeeFirstName = e.FirstName,
                                          EmployeeLastName = e.LastName,
                                          Status = (ActivitySubmissionStatus)t.SubmissionStatusId,
                                          SubmissionNotes = t.SubmissionDescription
                                      }).ToArrayAsync();

                foreach (var activity in activityList)
                {
                    var activityInfo = new ActivityInfoDTO
                    {
                        EmployeeId = activity.EmployeeId,
                        ActivityId = activity.ActivityId,
                        Status = ActivitySubmissionStatus.Approved
                    };
                    var badgeIds = criteria.AwardBadges.CreateBadges(activityInfo).Select(b => b.BadgeId).ToArray();

                    activity.ApproveActivityBadgeItemCollection = await (from t in ctx.Badges
                                                                         where badgeIds.Contains(t.BadgeId)
                                                                         select new ApproveActivityBadgeItemDTO
                                                                         {
                                                                             BadgeId = t.BadgeId,
                                                                             Name = t.BadgeName,
                                                                             Type = (Common.Enums.BadgeType)t.BadgeTypeId,
                                                                             ImagePath = t.BadgePath,
                                                                             BadgePriority = t.BadgePriority,
                                                                             AwardValueAmount = t.BadgeAwardValueAmount
                                                                         }).ToArrayAsync();
                }

                return activityList;
            }
        }

        public async Task<IEnumerable<ApproveActivityItemDTO>> GetActivitiesToApproveForManagerAsync(IApproveActivityCollectionCriteria criteria)
        {
            using (var ctx = new Entities())
            {
                ApproveActivityItemDTO[] activityList;
                activityList = await (from t in ctx.ActivitySubmissions
                                      join e in ctx.Employees on t.EmployeeId equals e.EmployeeId
                                      join a in ctx.Activities on t.ActivityId equals a.ActivityId
                                      where t.SubmissionStatusId == (int)ActivitySubmissionStatus.AwaitingApproval
                                      where (e.ApprovingManagerId1 == criteria.ManagerEmployeeId
                                      || e.ApprovingManagerId2 == criteria.ManagerEmployeeId)
                                      select new ApproveActivityItemDTO
                                      {
                                          SubmissionId = t.ActivitySubmissionId,
                                          SubmissionDate = t.SubmissionDate,
                                          ActivityId = t.ActivityId,
                                          ActivityDescription = a.ActivityDescription,
                                          ActivityName = a.ActivityName,
                                          ApprovedById = t.SubmissionApprovedById ?? 0,
                                          EmployeeId = t.EmployeeId,
                                          EmployeeADName = e.ADName,
                                          EmployeeFirstName = e.FirstName,
                                          EmployeeLastName = e.LastName,
                                          Status = (ActivitySubmissionStatus)t.SubmissionStatusId,
                                          SubmissionNotes = t.SubmissionDescription
                                      }).ToArrayAsync();

                foreach (var activity in activityList)
                {
                    var activityInfo = new ActivityInfoDTO
                    {
                        EmployeeId = activity.EmployeeId,
                        ActivityId = activity.ActivityId,
                        Status = ActivitySubmissionStatus.Approved
                    };
                    var badgeIds = criteria.AwardBadges.CreateBadges(activityInfo).Select(b => b.BadgeId).ToArray();

                    activity.ApproveActivityBadgeItemCollection = await (from t in ctx.Badges
                                                                         where badgeIds.Contains(t.BadgeId)
                                                                         select new ApproveActivityBadgeItemDTO
                                                                         {
                                                                             BadgeId = t.BadgeId,
                                                                             Name = t.BadgeName,
                                                                             Type = (Common.Enums.BadgeType)t.BadgeTypeId,
                                                                             ImagePath = t.BadgePath,
                                                                             BadgePriority = t.BadgePriority,
                                                                             AwardValueAmount = t.BadgeAwardValueAmount
                                                                         }).ToArrayAsync();
                }

                return activityList;
            }
        }

        public IEnumerable<ApproveActivityItemDTO> Update(IEnumerable<ApproveActivityItemDTO> data)
        {
            var list = data.ToList();
            using (var ctx = new Entities())
            {
                ctx.Database.Connection.Open();
                ctx.Configuration.ValidateOnSaveEnabled = false;
                foreach (var item in list)
                {
                    var saveSubmission = LoadData(item);
                    ctx.ActivitySubmissions.Attach(saveSubmission);
                    var objectState = ((IObjectContextAdapter)ctx).ObjectContext.ObjectStateManager;
                    objectState.GetObjectStateEntry(saveSubmission).SetModifiedProperty("ActivitySubmissionId");
                    objectState.GetObjectStateEntry(saveSubmission).SetModifiedProperty("SubmissionStatusId");
                }
                ctx.SaveChanges();
            }
            return list.Where(i => i.Status == ActivitySubmissionStatus.AwaitingApproval);
        }

        private static ActivitySubmission LoadData(ApproveActivityItemDTO data)
        {
            var badgeEntity = new ActivitySubmission
            {
                ActivitySubmissionId = data.SubmissionId,
                SubmissionStatusId = (int)data.Status,
                SubmissionApprovedById = data.ApprovedById,
            };
            return badgeEntity;
        }
    }
}