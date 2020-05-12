CREATE TABLE [dbo].[Notification]
(
	[NotificationId] INT IDENTITY (1, 1) NOT NULL CONSTRAINT [pk_Notification] PRIMARY KEY CLUSTERED ([NotificationId] ASC),
    [ActivitySubmissionId] INT NOT NULL, 
    [CreatedDate] DATETIME NOT NULL, 
    [NotificationStatusId] INT NOT NULL DEFAULT 0, 
    [NotificationSentDate] DATETIME NULL, 
    [UpdatedDate] DATETIME NULL
)
