CREATE TABLE [dbo].[BadgeActivity] (
    [BadgeActivityId] INT IDENTITY (1, 1) NOT NULL CONSTRAINT [pk_BadgeActivity] PRIMARY KEY CLUSTERED ([BadgeActivityId] ASC),
    [BadgeId]         INT NOT NULL CONSTRAINT [fk_BadgeActivity_Badge] FOREIGN KEY ([BadgeId]) REFERENCES [dbo].[Badge] ([BadgeId]),
    [ActivityId]      INT NOT NULL CONSTRAINT [fk_BadgeActivity_Activity] FOREIGN KEY ([ActivityId]) REFERENCES [dbo].[Activity] ([ActivityId]),
    [PointsAwarded]   INT CONSTRAINT [df_BadgeActivity_PointsAwarded] DEFAULT 0 NOT NULL
);