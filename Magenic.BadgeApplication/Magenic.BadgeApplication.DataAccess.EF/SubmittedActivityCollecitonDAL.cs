using Magenic.BadgeApplication.Common.DTO;
using Magenic.BadgeApplication.Common.Enums;
using Magenic.BadgeApplication.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Magenic.BadgeApplication.DataAccess.EF
{
    public class SubmittedActivityCollecitonDAL : ISubmittedActivityCollectionDAL
    {
        public async Task<IEnumerable<SubmittedActivityItemDTO>> GetSubmittedActivitiesForEmployeeIdAsync(int employeeId, DateTime? startDate, DateTime? endDate)
        {
            using (var ctx = new Entities())
            {
                ctx.Database.Connection.Open();
                var badgeList = await (from s in ctx.ActivitySubmissions
                                       join a in ctx.Activities on s.ActivityId equals a.ActivityId
                                       where s.EmployeeId == employeeId
                                       where s.SubmissionDate >= (startDate.HasValue ? startDate.Value : DateTime.MinValue)
                                       where s.SubmissionDate <= (endDate.HasValue ? endDate.Value : DateTime.MaxValue)
                                       select new SubmittedActivityItemDTO
                                       {
                                           Id = s.ActivitySubmissionId,
                                           ActivityId = s.ActivityId,
                                           ActivityName = a.ActivityName,
                                           ActivitySubmissionDate = s.SubmissionDate,
                                           ApprovedById = s.SubmissionApprovedById ?? 0,
                                           Status = (ActivitySubmissionStatus)s.SubmissionStatusId,
                                           SubmissionNotes = s.SubmissionDescription,
                                           EmployeeId = s.EmployeeId
                                       }).ToArrayAsync();
                return badgeList;
            }
        }


        public async Task<IEnumerable<SubmittedActivityItemDTO>> GetSubmittedActivitiesForEmployeeIdByActivityIdAsync(int employeeId, int activityId, DateTime? startDate, DateTime? endDate)
        {
            using (var ctx = new Entities())
            {
                ctx.Database.Connection.Open();
                var badgeList = await(from s in ctx.ActivitySubmissions
                                      join a in ctx.Activities on s.ActivityId equals a.ActivityId
                                      where s.EmployeeId == employeeId
                                      where a.ActivityId == activityId
                                      where s.SubmissionDate >= (startDate.HasValue ? startDate.Value : DateTime.MinValue)
                                      where s.SubmissionDate <= (endDate.HasValue ? endDate.Value : DateTime.MaxValue)
                                      select new SubmittedActivityItemDTO
                                      {
                                          Id = s.ActivitySubmissionId,
                                          ActivityId = s.ActivityId,
                                          ActivityName = a.ActivityName,
                                          ActivitySubmissionDate = s.SubmissionDate,
                                          ApprovedById = s.SubmissionApprovedById ?? 0,
                                          Status = (ActivitySubmissionStatus)s.SubmissionStatusId,
                                          SubmissionNotes = s.SubmissionDescription,
                                          EmployeeId = s.EmployeeId
                                      }).ToArrayAsync();
                return badgeList;
            }
        }


        public async Task<IEnumerable<SubmittedActivityItemDTO>> GetSubmittedActivitiesForADNameByActivityIdAsync(string adName, int activityId)
        {
            using (var ctx = new Entities())
            {
                ctx.Database.Connection.Open();
                var activitySubmissions = await (from s in ctx.ActivitySubmissions
                    join a in ctx.Activities on s.ActivityId equals a.ActivityId
                    join e in ctx.Employees on s.EmployeeId equals e.EmployeeId
                    where e.ADName == adName
                    where a.ActivityId == activityId
                    select new SubmittedActivityItemDTO
                    {
                        Id = s.ActivitySubmissionId,
                        ActivityId = s.ActivityId,
                        ActivityName = a.ActivityName,
                        ActivitySubmissionDate = s.SubmissionDate,
                        ApprovedById = s.SubmissionApprovedById ?? 0,
                        Status = (ActivitySubmissionStatus) s.SubmissionStatusId,
                        SubmissionNotes = s.SubmissionDescription,
                        EmployeeId = s.EmployeeId
                    }).ToArrayAsync();
                return activitySubmissions;
            }
        }
    }
}
