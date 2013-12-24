CREATE VIEW dbo.EarnedBadges
AS
SELECT     TOP (100) PERCENT dbo.Employee.EmployeeId, dbo.Employee.FirstName, dbo.Employee.LastName, dbo.Employee.AwardPayoutThreshold, dbo.Employee.ADName, 
                      dbo.Badge.BadgeId, dbo.Badge.BadgeName, dbo.Badge.BadgeTagLine, dbo.Badge.BadgeDescription, dbo.Badge.BadgeTypeId, dbo.Badge.BadgePath, 
                      dbo.Badge.BadgeCreated, dbo.Badge.BadgeEffectiveStart, dbo.Badge.BadgeEffectiveEnd, dbo.Badge.BadgePriority, dbo.Badge.MultipleAwardPossible, 
                      dbo.Badge.DisplayOnce, dbo.Badge.ManagementApprovalRequired, dbo.Badge.ActivityPointsAmount, dbo.Badge.BadgeAwardValueAmount, 
                      dbo.Badge.BadgeApprovedBy, dbo.Badge.BadgeApprovedDate, dbo.BadgeType.BadgeTypeName, dbo.BadgeType.PayrollEligible,
					  dbo.BadgeAward.AwardDate, dbo.BadgeAward.AwardAmount, dbo.BadgeAward.PaidOut
FROM         dbo.BadgeAward INNER JOIN
					  dbo.Badge ON dbo.BadgeAward.BadgeId = dbo.Badge.BadgeId INNER JOIN
                      dbo.BadgeType ON dbo.Badge.BadgeTypeId = dbo.BadgeType.BadgeTypeId INNER JOIN
                      dbo.Employee ON dbo.BadgeAward.EmployeeADName = dbo.Employee.ADName
GO