CREATE VIEW [dbo].[NotificationItemsToPublish]
AS
SELECT        n.NotificationId, n.ActivitySubmissionId, n.CreatedDate, n.NotificationStatusId, n.NotificationSentDate, n.UpdatedDate, acts.ActivityId, acts.EmployeeId, acts.SubmissionDescription, acts.SubmissionApprovedById, 
                         acts.SubmissionDate, acts.SubmissionStatusId, acts.AwardValue, act.ActivityName, act.ActivityDescription, e.FirstName, e.LastName, e.EmailAddress, e.ADName
FROM            dbo.Notification AS n INNER JOIN
                         dbo.ActivitySubmission AS acts ON acts.ActivitySubmissionId = n.ActivitySubmissionId INNER JOIN
                         dbo.Activity AS act ON act.ActivityId = acts.ActivityId INNER JOIN
                         dbo.Employee AS e ON e.EmployeeId = acts.EmployeeId
GO