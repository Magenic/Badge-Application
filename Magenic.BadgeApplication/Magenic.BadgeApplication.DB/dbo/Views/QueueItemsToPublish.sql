CREATE VIEW [dbo].[QueueItemsToPublish]
AS
SELECT        dbo.QueueItem.QueueItemId, dbo.QueueItem.BadgeAwardId, dbo.QueueItem.QueueItemCreated, dbo.Badge.BadgeId, dbo.Badge.BadgeName, dbo.Badge.BadgeTagLine AS BadgeTagline, dbo.Badge.BadgePath, 
                         dbo.Badge.BadgeDescription, dbo.Employee.EmployeeId, dbo.Employee.FirstName, dbo.Employee.LastName, dbo.Employee.EmailAddress, dbo.Employee.ADName
FROM            dbo.QueueItem INNER JOIN
                         dbo.BadgeAward ON dbo.QueueItem.BadgeAwardId = dbo.BadgeAward.BadgeAwardId INNER JOIN
                         dbo.Employee ON dbo.BadgeAward.EmployeeId = dbo.Employee.EmployeeId INNER JOIN
                         dbo.Badge ON dbo.BadgeAward.BadgeId = dbo.Badge.BadgeId
GO

