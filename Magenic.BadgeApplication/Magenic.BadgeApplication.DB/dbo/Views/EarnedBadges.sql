CREATE VIEW dbo.EarnedBadges
AS
SELECT     TOP (100) PERCENT dbo.Employee.EmployeeId, dbo.Employee.FirstName, dbo.Employee.LastName, dbo.Employee.AwardPayoutThreshhold, dbo.Employee.ADName, 
                      dbo.Badge.BadgeId, dbo.Badge.BadgeName, dbo.Badge.BadgeTagLine, dbo.Badge.BadgeDescription, dbo.Badge.BadgeTypeId, dbo.Badge.BadgePath, 
                      dbo.Badge.BadgeCreated, dbo.Badge.BadgeEffectiveStart, dbo.Badge.BadgeEffectiveEnd, dbo.Badge.BadgePriority, dbo.Badge.MultipleAwardPossible, 
                      dbo.Badge.DisplayOnce, dbo.Badge.ManagementApprovalRequired, dbo.Badge.ActivityPointsAmount, dbo.Badge.BadgeAwardValueAmount, 
                      dbo.Badge.BadgeApprovedBy, dbo.Badge.BadgeApprovedDate, dbo.BadgeType.BadgeTypeName, dbo.BadgeType.PayrollEligible
FROM         dbo.Badge INNER JOIN
                      dbo.BadgeType ON dbo.Badge.BadgeTypeId = dbo.BadgeType.BadgeTypeId INNER JOIN
                      dbo.Employee ON dbo.Badge.BadgeApprovedBy = dbo.Employee.EmployeeId
GO