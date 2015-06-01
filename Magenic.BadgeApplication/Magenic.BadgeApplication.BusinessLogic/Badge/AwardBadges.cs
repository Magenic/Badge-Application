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
    public sealed class AwardBadges: IAwardBadges
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "1")]
        public IQueryable<BadgeAwardDTO> GetBadgeAwardsForActivity(ActivityInfoDTO activityInfo,
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

        private BadgeAwardDTO CreateNewBadgeAward(ActivityInfoDTO activity, BadgeEditDTO potentialBadge)
        {
            return new BadgeAwardDTO
            {
                BadgeId = potentialBadge.Id,
                AwardAmount = activity.AwardValue == 0 ? potentialBadge.AwardValueAmount : activity.AwardValue,
                EmployeeId = activity.EmployeeId,
                AwardDate = DateTime.UtcNow
            };
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0")]
        public IQueryable<BadgeAwardDTO> CreateBadges(ActivityInfoDTO activityInfo)
        {
            var activityDal = IoC.Container.Resolve<IBadgeEditDAL>();
            var potentialBadges = activityDal.GetPotentialBadgesForActivity(activityInfo.ActivityId);
            var dal = IoC.Container.Resolve<IAwardBadgesDAL>();
            var earnedBadges = dal.GetAwardedBadgesForUser(activityInfo.EmployeeId);
            var previousActivities = dal.GetPreviousActivitiesForUser(activityInfo.EmployeeId, activityInfo.ActivityId);

            var returnBadges = GetBadgeAwardsForActivity(activityInfo, potentialBadges, earnedBadges, previousActivities,
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
