CREATE TABLE [dbo].[QueueEventLog]
(
	[QueueEventLogId] INT IDENTITY (1, 1) NOT NULL CONSTRAINT [pk_QueueEventLog] PRIMARY KEY CLUSTERED ([QueueEventLogId] ASC),
    [QueueEventId] INT NOT NULL CONSTRAINT [fk_QueueEventLog_QueueEvent] FOREIGN KEY ([QueueEventId]) REFERENCES [dbo].[QueueEvent] ([QueueEventId]),  
    [BadgeAwardId] INT NOT NULL CONSTRAINT [fk_QueueEventLog_BadgeAward] FOREIGN KEY ([BadgeAwardId]) REFERENCES [dbo].[BadgeAward] ([BadgeAwardId]), 
    [QueueEventCreated] DATETIME NOT NULL, 
    [Message] NVARCHAR(200) NULL
)
