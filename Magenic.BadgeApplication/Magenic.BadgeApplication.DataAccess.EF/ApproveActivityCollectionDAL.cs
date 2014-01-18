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
        public async Task<IEnumerable<IApproveActivityItemDTO>> GetActivitiesToApproveForManagerAsync(string managerUserName)
        {
            using (var ctx = new Entities())
            {
                ctx.Database.Connection.Open();
                var activityList = await (from t in ctx.ActivitySubmissions
                                          join e in ctx.Employees on t.EmployeeADName equals e.ADName
                                          join a in ctx.Activities on t.ActivityId equals a.ActivityId
                                          join m1 in ctx.Employees on e.ApprovingManagerId1 equals m1.EmployeeId into manager1
                                          join m2 in ctx.Employees on e.ApprovingManagerId2 equals m2.EmployeeId into manager2
                                          where t.SubmissionStatusId == (int)ActivitySubmissionStatus.Proposed
                                          where (manager1.DefaultIfEmpty().Any(m1 => m1.ADName == managerUserName)
                                          || manager2.DefaultIfEmpty().Any(m2 => m2.ADName == managerUserName))
                                          from man1 in manager1.DefaultIfEmpty()
                                          from man2 in manager2.DefaultIfEmpty()
                                          select new Common.DTO.ApproveActivityItemDTO
                                          {
                                              SubmissionId = t.ActivitySubmissionId,
                                              SubmissionDate = t.SubmissionDate,
                                              ActivityDescription = a.ActivityDescription,
                                              ActivityName = a.ActivityName,
                                              ApprovedByUserName = t.SubmissionApprovedADName,
                                              EmployeeUserName = t.EmployeeADName,
                                              Status = (ActivitySubmissionStatus)t.SubmissionStatusId,
                                              SubmissionNotes = t.SubmissionDescription
                                          }).ToArrayAsync();

                return activityList;
            }

        }

        public IEnumerable<IApproveActivityItemDTO> Update(IEnumerable<IApproveActivityItemDTO> data)
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
            return list.Where(i => i.Status == ActivitySubmissionStatus.Proposed);
        }

        private static ActivitySubmission LoadData(IApproveActivityItemDTO data)
        {
            var badgeEntity = new ActivitySubmission
            {
                ActivitySubmissionId = data.SubmissionId,
                SubmissionStatusId = (int)data.Status
            };
            return badgeEntity;
        }

    }
}
