CREATE TABLE [dbo].[QueueEvent]
(
	[QueueEventId] INT IDENTITY (1, 1) NOT NULL CONSTRAINT [pk_QueueEvent] PRIMARY KEY CLUSTERED ([QueueEventId] ASC),     
    [QueueEventName] NVARCHAR(30) NOT NULL, 
    [QueueEventDescription] NVARCHAR(50) NOT NULL
)
