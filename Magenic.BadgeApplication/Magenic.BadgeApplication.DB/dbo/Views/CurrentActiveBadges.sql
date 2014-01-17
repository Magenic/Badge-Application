CREATE VIEW dbo.CurrentActiveBadges
AS
SELECT     dbo.BadgeType.BadgeTypeName, dbo.BadgeType.PayrollEligible, dbo.Badge.BadgeId, dbo.Badge.BadgeName, dbo.Badge.BadgeTagLine, 
                      dbo.Badge.BadgeDescription, dbo.Badge.BadgeTypeId, dbo.Badge.BadgePath, dbo.Badge.BadgeCreated, dbo.Badge.BadgeEffectiveStart, 
                      dbo.Badge.BadgeEffectiveEnd, dbo.Badge.BadgePriority, dbo.Badge.MultipleAwardPossible, dbo.Badge.DisplayOnce, dbo.Badge.ManagementApprovalRequired, 
                      dbo.Badge.ActivityPointsAmount, dbo.Badge.BadgeAwardValueAmount, dbo.Badge.BadgeApprovedByADName, dbo.Badge.BadgeApprovedDate
FROM         dbo.Badge INNER JOIN
                      dbo.BadgeType ON dbo.Badge.BadgeTypeId = dbo.BadgeType.BadgeTypeId
WHERE     (GETDATE() <= dbo.Badge.BadgeEffectiveEnd OR dbo.Badge.BadgeEffectiveEnd IS NULL) AND (GETDATE() >= dbo.Badge.BadgeEffectiveStart OR dbo.Badge.BadgeEffectiveEnd IS NULL) AND (dbo.Badge.BadgeApprovedByADName IS NOT NULL)
GO