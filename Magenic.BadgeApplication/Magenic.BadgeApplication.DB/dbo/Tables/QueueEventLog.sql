CREATE TABLE [dbo].[QueueEventLog]
(
	[QueueEventLogId] INT IDENTITY (1, 1) NOT NULL CONSTRAINT [pk_QueueEventLog] PRIMARY KEY CLUSTERED ([QueueEventLogId] ASC),
    [QueueEventId] INT NOT NULL CONSTRAINT [fk_QueueEventLog_QueueEvent] FOREIGN KEY ([QueueEventId]) REFERENCES [dbo].[QueueEvent] ([QueueEventId]),  
    [QueueItemId] INT NOT NULL CONSTRAINT [fk_QueueEventLog_QueueItem] FOREIGN KEY ([QueueItemId]) REFERENCES [dbo].[QueueItem] ([QueueItemId]),   
    [QueueEventCreated] DATETIME NOT NULL, 
    [Message] NVARCHAR(200) NULL
)
