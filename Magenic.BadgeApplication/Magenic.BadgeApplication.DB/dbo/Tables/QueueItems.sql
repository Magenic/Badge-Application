CREATE TABLE [dbo].[QueueItem]
(
	[QueueItemId] INT IDENTITY (1, 1) NOT NULL CONSTRAINT [pk_QueueItem] PRIMARY KEY CLUSTERED ([QueueItemId] ASC),    
    [BadgeAwardId] INT NOT NULL CONSTRAINT [fk_QueueItem_BadgeAward] FOREIGN KEY ([BadgeAwardId]) REFERENCES [dbo].[BadgeAward] ([BadgeAwardId]), 
    [QueueItemCreated] DATETIME NOT NULL
)
