CREATE TABLE [dbo].[BadgePrerequisite] (
    [PrerequisiteID]  INT IDENTITY (1, 1) NOT NULL,
    [BadgeId]         INT NULL,
    [RequiredBadgeId] INT NULL,
    CONSTRAINT [PK_BadgePrerequisite] PRIMARY KEY CLUSTERED ([PrerequisiteID] ASC),
    CONSTRAINT [FK_BadgePrerequisite_Badge] FOREIGN KEY ([BadgeId]) REFERENCES [dbo].[Badge] ([BadgeId]),
    CONSTRAINT [FK_BadgePrerequisite_Badge1] FOREIGN KEY ([RequiredBadgeId]) REFERENCES [dbo].[Badge] ([BadgeId])
);

