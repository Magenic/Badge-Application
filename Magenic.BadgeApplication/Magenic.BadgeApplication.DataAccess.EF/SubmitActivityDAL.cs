using Magenic.BadgeApplication.Common.DTO;
using Magenic.BadgeApplication.Common.Interfaces;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.DataAccess.EF
{
    public class SubmitActivityDAL : ISubmitActivityDAL
    {
        public async Task<SubmitActivityDTO> GetActivitySubmissionByIdAsync(int activitySubmissionId)
        {
            using (var ctx = new Entities())
            {
                ctx.Database.Connection.Open();
                var badgeList = await (from t in ctx.ActivitySubmissions
                                       where t.ActivitySubmissionId == activitySubmissionId
                                       select new SubmitActivityDTO
                                       {
                                           Id = t.ActivitySubmissionId,
                                           ActivityId = t.ActivityId,
                                           ActivitySubmissionDate = t.SubmissionDate,
                                           ApprovedById = t.SubmissionApprovedById ?? 0,
                                           Notes = t.SubmissionDescription,
                                           Status = (Common.Enums.ActivitySubmissionStatus)t.SubmissionStatusId,
                                           EmployeeId = t.EmployeeId
                                       }).ToArrayAsync();

                var badge = badgeList.Single();

                return badge;
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0")]
        public SubmitActivityDTO Update(SubmitActivityDTO data)
        {
            using (var ctx = new Entities())
            {
                ctx.Database.Connection.Open();
                var saveActivitySubmission = LoadData(data);
                ctx.ActivitySubmissions.Attach(saveActivitySubmission);
                var objectState = ((IObjectContextAdapter)ctx).ObjectContext.ObjectStateManager;
                objectState.GetObjectStateEntry(saveActivitySubmission).SetModifiedProperty("ActivityId");
                objectState.GetObjectStateEntry(saveActivitySubmission).SetModifiedProperty("EmployeeId");
                objectState.GetObjectStateEntry(saveActivitySubmission).SetModifiedProperty("SubmissionStatusId");
                objectState.GetObjectStateEntry(saveActivitySubmission).SetModifiedProperty("SubmissionApprovedById");
                objectState.GetObjectStateEntry(saveActivitySubmission).SetModifiedProperty("SubmissionDate");
                objectState.GetObjectStateEntry(saveActivitySubmission).SetModifiedProperty("SubmissionDescription");

                ctx.SaveChanges();
                data.Id = saveActivitySubmission.ActivitySubmissionId;
            }
            return data;
        }

        private static ActivitySubmission LoadData(SubmitActivityDTO data)
        {
            var activitySubmission = new ActivitySubmission
            {
                ActivitySubmissionId = data.Id,
                ActivityId = data.ActivityId,
                EmployeeId = data.EmployeeId,
                SubmissionStatusId = (int)data.Status,
                SubmissionApprovedById = data.ApprovedById == 0 ? null : (int?)data.ApprovedById,
                SubmissionDate = data.ActivitySubmissionDate,
                SubmissionDescription = data.Notes,
                AwardValue = data.AwardValue
            };
            return activitySubmission;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0")]
        public SubmitActivityDTO Insert(SubmitActivityDTO data)
        {
            using (var ctx = new Entities())
            {
                ctx.Database.Connection.Open();
                var saveActivitySubmission = LoadData(data);
                ctx.ActivitySubmissions.Add(saveActivitySubmission);

                ctx.SaveChanges();
                data.Id = saveActivitySubmission.ActivitySubmissionId;
            }
            return data;
        }

        public void Delete(int activitySubmissionId)
        {
            using (var ctx = new Entities())
            {
                ctx.Database.Connection.Open();
                var deleteActivitySubmission = new ActivitySubmission
                {
                    ActivitySubmissionId = activitySubmissionId
                };
                ctx.ActivitySubmissions.Attach(deleteActivitySubmission);
                ctx.ActivitySubmissions.Remove(deleteActivitySubmission);
                ctx.SaveChanges();
            }
        }
    }
}
