using Autofac;
using Magenic.BadgeApplication.BusinessLogic.Framework;
using Magenic.BadgeApplication.Common.DTO;
using Magenic.BadgeApplication.Common.Enums;
using Magenic.BadgeApplication.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Magenic.BadgeApplication.BusinessLogic.Badge
{
    public static class AwardBadges
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible")]
        public class ActivityInfo
        {
            public ActivitySubmissionStatus Status { get; set; }
            public int ActivityId { get; set; }
            public int EmployeeId { get; set; }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "1")]
        public static IQueryable<BadgeAwardDTO> GetBadgeAwardsForActivity(ActivityInfo activityInfo,
            IList<BadgeEditDTO> potentialBadges, IList<BadgeAwardDTO> earnedBadges,
            IList<SubmittedActivityItemDTO> previousActivities, DateTime activityApprovalTime)
        {
            var returnValue = new List<BadgeAwardDTO>();

            if (activityInfo.Status == ActivitySubmissionStatus.Approved)
            {
                foreach (var potentialBadge in potentialBadges)
                {
                    if (potentialBadge.BadgeActivities.Any(ba => ba.ActivityId == activityInfo.ActivityId)
                        & AllowedToBeAwarded(potentialBadge, earnedBadges)
                        & BadgeValidForDate(activityApprovalTime, potentialBadge)
                        & potentialBadge.BadgeStatus == BadgeStatus.Approved
                        & CorrectNumberOfEarnedBadges(activityInfo.ActivityId, potentialBadge, previousActivities))
                    {
                        returnValue.Add(CreateNewBadgeAward(activityInfo, potentialBadge));
                    }
                }
            }

            return returnValue.AsQueryable();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0")]
        public static bool AllowedToBeAwarded(BadgeEditDTO potentialBadge, IList<BadgeAwardDTO> earnedBadges)
        {
            var returnValue = potentialBadge.MultipleAwardsPossible
                   || !earnedBadges.Any(eb => eb.BadgeId == potentialBadge.Id);
            return returnValue;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "1")]
        public static bool BadgeValidForDate(DateTime activityApprovalTime, BadgeEditDTO potentialBadge)
        {
            var returnValue = (!potentialBadge.EffectiveStartDate.HasValue
                    || potentialBadge.EffectiveStartDate.Value <= activityApprovalTime)
                   && (!potentialBadge.EffectiveEndDate.HasValue
                       || potentialBadge.EffectiveEndDate.Value >= activityApprovalTime);
            return returnValue;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "1")]
        public static bool CorrectNumberOfEarnedBadges(int activityId, BadgeEditDTO potentialBadge, IList<SubmittedActivityItemDTO> previousActivities)
        {
            var returnValue = false;
            if (potentialBadge.ActivityPointsAmount != 0)
            {
                returnValue = ((previousActivities.Count(pa => pa.Status == ActivitySubmissionStatus.Complete
                                                         && pa.ActivityId == activityId) + 1) %
                         potentialBadge.ActivityPointsAmount == 0);
            }

            return returnValue;
        }

        private static BadgeAwardDTO CreateNewBadgeAward(ActivityInfo activity, BadgeEditDTO potentialBadge)
        {
            return new BadgeAwardDTO
            {
                BadgeId = potentialBadge.Id,
                AwardAmount = potentialBadge.AwardValueAmount,
                EmployeeId = activity.EmployeeId,
                AwardDate = DateTime.UtcNow
            };
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0")]
        public static IQueryable<BadgeAwardDTO> CreateBadges(ActivityInfo activityInfo)
        {
            var activityDal = IoC.Container.Resolve<IBadgeEditDAL>();
            var potentialBadges = activityDal.GetPotentialBadgesForActivity(activityInfo.ActivityId);
            var dal = IoC.Container.Resolve<IAwardBadgesDAL>();
            var earnedBadges = dal.GetAwardedBadgesForUser(activityInfo.EmployeeId);
            var previousActivities = dal.GetPreviousActivitiesForUser(activityInfo.EmployeeId, activityInfo.ActivityId);

            var returnBadges = AwardBadges.GetBadgeAwardsForActivity(activityInfo, potentialBadges, earnedBadges, previousActivities,
                DateTime.UtcNow);
            activityInfo.Status = ActivitySubmissionStatus.Complete;

            return returnBadges;
        }

        public static void SaveBadges(IQueryable<BadgeAwardDTO> badges)
        {
            if (badges != null && badges.Any())
            {
                var dal = IoC.Container.Resolve<IAwardBadgesDAL>();
                dal.SaveEarnedBadges(badges);
            }
        }

    }
}
