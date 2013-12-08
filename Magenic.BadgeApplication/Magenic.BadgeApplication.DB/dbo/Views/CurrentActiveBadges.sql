CREATE VIEW dbo.CurrentActiveBadges
AS
SELECT     dbo.BadgeType.BadgeTypeName, dbo.BadgeType.PayrollEligible, dbo.Badge.BadgeId, dbo.Badge.BadgeName, dbo.Badge.BadgeTagLine, 
                      dbo.Badge.BadgeDescription, dbo.Badge.BadgeTypeId, dbo.Badge.BadgeImage, dbo.Badge.BadgeCreated, dbo.Badge.BadgeEffectiveStart, 
                      dbo.Badge.BadgeEffectiveEnd, dbo.Badge.BadgePriority, dbo.Badge.MultipleAwardPossible, dbo.Badge.DisplayOnce, dbo.Badge.ManagementApprovalRequired, 
                      dbo.Badge.ActivityPointsAmount, dbo.Badge.BadgeAwardValueAmount, dbo.Badge.BadgeApprovedBy, dbo.Badge.BadgeApprovedDate
FROM         dbo.Badge INNER JOIN
                      dbo.BadgeType ON dbo.Badge.BadgeTypeId = dbo.BadgeType.BadgeTypeId
WHERE     (GETDATE() <= dbo.Badge.BadgeEffectiveEnd) AND (GETDATE() >= dbo.Badge.BadgeEffectiveStart) AND (dbo.Badge.BadgeApprovedBy IS NOT NULL)
GO
