using System;
using System.Collections.Generic;
using Magenic.BadgeApplication.Common.DTO;
using Magenic.BadgeApplication.Common.Enums;
using Magenic.BadgeApplication.Common.Interfaces;
using System.Linq;

namespace Magenic.BadgeApplication.DataAccess.EF
{
    public class AwardBadgesDAL : IAwardBadgesDAL
    {
        public IList<BadgeAwardDTO> GetAwardedBadgesForUser(int employeeId)
        {
            using (var ctx = new Entities())
            {
                ctx.Database.Connection.Open();
                var badgeList = from t in ctx.BadgeAwards
                                where t.EmployeeId == employeeId
                                select new BadgeAwardDTO
                                {
                                    BadgeAwardId = t.BadgeAwardId,
                                    BadgeId = t.BadgeId,
                                    EmployeeId = t.EmployeeId,
                                    AwardAmount = t.AwardAmount,
                                    AwardDate = t.AwardDate
                                };

                return badgeList.ToList();
            }
        }

        public IList<SubmittedActivityItemDTO> GetPreviousActivitiesForUser(int employeeId, int activityId)
        {
            using (var ctx = new Entities())
            {
                ctx.Database.Connection.Open();
                var submittedActivityList = from t in ctx.ActivitySubmissions
                                            where t.EmployeeId == employeeId
                                            where t.ActivityId == activityId
                                            select new SubmittedActivityItemDTO
                                            {
                                                Id = t.ActivitySubmissionId,
                                                ActivityId = t.ActivityId,
                                                ActivitySubmissionDate = t.SubmissionDate,
                                                ApprovedById = t.SubmissionApprovedById.HasValue ? t.SubmissionApprovedById.Value : 0,
                                                EmployeeId = t.EmployeeId,
                                                Status = (ActivitySubmissionStatus)t.SubmissionStatusId,
                                                SubmissionNotes = t.SubmissionDescription
                                            };

                return submittedActivityList.ToList();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0")]
        public void SaveEarnedBadges(IQueryable<BadgeAwardDTO> badges)
        {
            using (var ctx = new Entities())
            {
                ctx.Database.Connection.Open();
                var badgeAwards = new List<BadgeAward>();
                foreach (var badge in badges)
                {
                    var saveBadge = LoadData(badge);
                    ctx.BadgeAwards.Add(saveBadge);
                    badgeAwards.Add(saveBadge);
                }
                ctx.SaveChanges();
                foreach (var badgeAward in badgeAwards)
                {
                    var queueItem = new QueueItem
                    {
                        BadgeAwardId = badgeAward.BadgeAwardId,
                        QueueItemCreated = badgeAward.AwardDate
                    };
                    ctx.QueueItems.Add(queueItem);
                }
                ctx.SaveChanges();
            }
        }

        private BadgeAward LoadData(BadgeAwardDTO badge)
        {
            var earnedBadge = new BadgeAward
            {
                BadgeAwardId = badge.BadgeAwardId,
                BadgeId = badge.BadgeId,
                AwardAmount = badge.AwardAmount,
                AwardDate = badge.AwardDate,
                EmployeeId = badge.EmployeeId,
                PaidOut = false
            };
            return earnedBadge;
        }
    }
}
