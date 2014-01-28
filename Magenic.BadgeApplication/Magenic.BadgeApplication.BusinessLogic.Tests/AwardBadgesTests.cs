using Magenic.BadgeApplication.BusinessLogic.Badge;
using Magenic.BadgeApplication.Common.DTO;
using Magenic.BadgeApplication.Common.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Magenic.BadgeApplication.BusinessLogic.Tests
{
    [TestClass]
    public class AwardBadgesTests
    {
        [TestMethod]
        public void AllowedWithMultipleBadges()
        {
            var dto = new BadgeEditDTO
            {
                Id = 1,
                MultipleAwardsPossible = true
            };
            var earnedBadges = new List<BadgeAwardDTO>();
            earnedBadges.Add(new BadgeAwardDTO {BadgeId = 1});

            var result = AwardBadges.AllowedToBeAwarded(dto, earnedBadges);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void NotAllowedWithMultipleBadges()
        {
            var dto = new BadgeEditDTO
            {
                Id = 1,
                MultipleAwardsPossible = false
            };
            var earnedBadges = new List<BadgeAwardDTO>();
            earnedBadges.Add(new BadgeAwardDTO { BadgeId = 1 });

            var result = AwardBadges.AllowedToBeAwarded(dto, earnedBadges);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void AllowedNeverAwarded()
        {
            var dto = new BadgeEditDTO
            {
                Id = 1,
                MultipleAwardsPossible = false
            };
            var earnedBadges = new List<BadgeAwardDTO>();

            var result = AwardBadges.AllowedToBeAwarded(dto, earnedBadges);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void AllowedWithinDateRange()
        {
            var approvalDate = DateTime.Parse("1/1/2014", CultureInfo.CurrentCulture);
            var dto = new BadgeEditDTO
            {
                EffectiveStartDate = DateTime.Parse("1/1/2013", CultureInfo.CurrentCulture),
                EffectiveEndDate = DateTime.Parse("1/1/2015", CultureInfo.CurrentCulture)
            };

            var result = AwardBadges.BadgeValidForDate(approvalDate, dto);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void NotAllowedOutsideDateRange()
        {
            var approvalDate = DateTime.Parse("1/1/2012", CultureInfo.CurrentCulture);
            var dto = new BadgeEditDTO
            {
                EffectiveStartDate = DateTime.Parse("1/1/2013", CultureInfo.CurrentCulture),
                EffectiveEndDate = DateTime.Parse("1/1/2015", CultureInfo.CurrentCulture)
            };

            var result = AwardBadges.BadgeValidForDate(approvalDate, dto);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void AllowedNullEffectiveDates()
        {
            var approvalDate = DateTime.Parse("1/1/2014", CultureInfo.CurrentCulture);
            var dto = new BadgeEditDTO();

            var result = AwardBadges.BadgeValidForDate(approvalDate, dto);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void AllowedNoPreviousOneRequired()
        {
            var dto = new BadgeEditDTO
            {
                ActivityPointsAmount = 1
            };
            var previousActivities = new List<SubmittedActivityItemDTO>();

            var result = AwardBadges.CorrectNumberOfEarnedBadges(1, dto, previousActivities);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void AllowedOnePreviousOneRequired()
        {
            var dto = new BadgeEditDTO
            {
                ActivityPointsAmount = 1
            };
            var previousActivities = new List<SubmittedActivityItemDTO>();
            previousActivities.Add(new SubmittedActivityItemDTO
            {
                ActivityId = 1,
                Status = ActivitySubmissionStatus.Complete
            });

            var result = AwardBadges.CorrectNumberOfEarnedBadges(1, dto, previousActivities);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void NotAllowedTwoPreviousTwoRequired()
        {
            var dto = new BadgeEditDTO
            {
                ActivityPointsAmount = 2
            };
            var previousActivities = new List<SubmittedActivityItemDTO>();
            previousActivities.Add(new SubmittedActivityItemDTO
            {
                ActivityId = 1,
                Status = ActivitySubmissionStatus.Complete
            });
            previousActivities.Add(new SubmittedActivityItemDTO
            {
                ActivityId = 1,
                Status = ActivitySubmissionStatus.Complete
            });

            var result = AwardBadges.CorrectNumberOfEarnedBadges(1, dto, previousActivities);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void AllTogetherIsValidWithApprovedStatus()
        {
            var targetActivityId = 1;
            var activityInfo = new AwardBadges.ActivityInfo
            {
                ActivityId = targetActivityId,
                Status = ActivitySubmissionStatus.Approved,
                EmployeeId = 4
            };
            var potentialBadges = new List<BadgeEditDTO>();
            potentialBadges.Add(new BadgeEditDTO
            {
                Id = 1,
                MultipleAwardsPossible = true,
                EffectiveStartDate = DateTime.Parse("1/1/2013", CultureInfo.CurrentCulture),
                EffectiveEndDate = DateTime.Parse("1/1/2015", CultureInfo.CurrentCulture),
                ApprovedById = 1,
                ActivityPointsAmount = 1,
                BadgeStatus = BadgeStatus.Approved,
                BadgeActivities = new List<BadgeActivityEditDTO> { new BadgeActivityEditDTO { ActivityId = targetActivityId } }
            });
            var earnedBadges = new List<BadgeAwardDTO>();
            var previousActivities = new List<SubmittedActivityItemDTO>();

            var result = AwardBadges.GetBadgeAwardsForActivity(activityInfo, potentialBadges, earnedBadges, previousActivities, DateTime.Parse("1/1/2014", CultureInfo.CurrentCulture));

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());            
        }

        [TestMethod]
        public void AllTogetherIsNotValidWithoutApprovedStatus()
        {
            var targetActivityId = 1;
            var activityInfo = new AwardBadges.ActivityInfo
            {
                ActivityId = targetActivityId,
                Status = ActivitySubmissionStatus.AwaitingApproval,
                EmployeeId = 4
            };
            var potentialBadges = new List<BadgeEditDTO>();
            potentialBadges.Add(new BadgeEditDTO
            {
                Id = 1,
                MultipleAwardsPossible = true,
                EffectiveStartDate = DateTime.Parse("1/1/2013", CultureInfo.CurrentCulture),
                EffectiveEndDate = DateTime.Parse("1/1/2015", CultureInfo.CurrentCulture),
                ApprovedById = 1,
                ActivityPointsAmount = 1,
                BadgeActivities = new List<BadgeActivityEditDTO> { new BadgeActivityEditDTO { ActivityId = targetActivityId } }
            });
            var earnedBadges = new List<BadgeAwardDTO>();
            var previousActivities = new List<SubmittedActivityItemDTO>();

            var result = AwardBadges.GetBadgeAwardsForActivity(activityInfo, potentialBadges, earnedBadges, previousActivities, DateTime.Parse("1/1/2014", CultureInfo.CurrentCulture));

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        public void AllTogetherIsNotValidWithoutPotentialBadgeApproved()
        {
            var targetActivityId = 1;
            var activityInfo = new AwardBadges.ActivityInfo
            {
                ActivityId = targetActivityId,
                Status = ActivitySubmissionStatus.Approved,
                EmployeeId = 4
            };
            var potentialBadges = new List<BadgeEditDTO>();
            potentialBadges.Add(new BadgeEditDTO
            {
                Id = 1,
                MultipleAwardsPossible = true,
                EffectiveStartDate = DateTime.Parse("1/1/2013", CultureInfo.CurrentCulture),
                EffectiveEndDate = DateTime.Parse("1/1/2015", CultureInfo.CurrentCulture),
                ApprovedById = 0,
                ActivityPointsAmount = 1,
                BadgeActivities = new List<BadgeActivityEditDTO> { new BadgeActivityEditDTO { ActivityId = targetActivityId } }
            });
            var earnedBadges = new List<BadgeAwardDTO>();
            var previousActivities = new List<SubmittedActivityItemDTO>();

            var result = AwardBadges.GetBadgeAwardsForActivity(activityInfo, potentialBadges, earnedBadges, previousActivities, DateTime.Parse("1/1/2014", CultureInfo.CurrentCulture));

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        public void AllTogetherMultiplePotentialBadgesAwardsMultipleBadges()
        {
            var targetActivityId = 1;
            var activityInfo = new AwardBadges.ActivityInfo
            {
                ActivityId = targetActivityId,
                Status = ActivitySubmissionStatus.Approved,
                EmployeeId = 4
            };
            var potentialBadges = new List<BadgeEditDTO>();
            potentialBadges.Add(new BadgeEditDTO
            {
                Id = 1,
                MultipleAwardsPossible = true,
                EffectiveStartDate = DateTime.Parse("1/1/2013", CultureInfo.CurrentCulture),
                EffectiveEndDate = DateTime.Parse("1/1/2015", CultureInfo.CurrentCulture),
                ApprovedById = 1,
                ActivityPointsAmount = 1,
                BadgeStatus = BadgeStatus.Approved,
                BadgeActivities = new List<BadgeActivityEditDTO> { new BadgeActivityEditDTO { ActivityId = targetActivityId } }
            });
            potentialBadges.Add(new BadgeEditDTO
            {
                Id = 2,
                MultipleAwardsPossible = true,
                EffectiveStartDate = DateTime.Parse("1/1/2013", CultureInfo.CurrentCulture),
                EffectiveEndDate = DateTime.Parse("1/1/2015", CultureInfo.CurrentCulture),
                ApprovedById = 1,
                ActivityPointsAmount = 1,
                BadgeStatus = BadgeStatus.Approved,
                BadgeActivities = new List<BadgeActivityEditDTO> { new BadgeActivityEditDTO { ActivityId = targetActivityId } }
            });

            var earnedBadges = new List<BadgeAwardDTO>();
            var previousActivities = new List<SubmittedActivityItemDTO>();

            var result = AwardBadges.GetBadgeAwardsForActivity(activityInfo, potentialBadges, earnedBadges, previousActivities, DateTime.Parse("1/1/2014", CultureInfo.CurrentCulture));

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
        }
    }
}
