CREATE TABLE [dbo].[BadgeAward] (
    [BadgeAwardId]    INT           IDENTITY (1, 1) NOT NULL CONSTRAINT [pk_BadgeAward] PRIMARY KEY CLUSTERED ([BadgeAwardId] ASC),
    [BadgeId]         INT           NOT NULL CONSTRAINT [fk_BadgeAward_Badge] FOREIGN KEY ([BadgeId]) REFERENCES [dbo].[Badge] ([BadgeId]),
    [EmployeeId]      INT           NOT NULL,
    [AwardDate]       DATE          CONSTRAINT [df_BadgeAward_AwardDate] DEFAULT getdate() NOT NULL,
    [AwardAmount]     INT           CONSTRAINT [df_BadgeAward_AwardAmount] DEFAULT 0 NOT NULL,
    [PaidOut]         BIT           CONSTRAINT [df_BadgeAward_PaidOut] DEFAULT 0 NOT NULL,
    [PaidDate]        DATETIME2 (7) NULL,
    [PaidCompletedById] INT           NULL,
    [Published]       BIT           NULL,
	 CONSTRAINT [fk_BadgeAward_Employee] FOREIGN KEY ([EmployeeId]) REFERENCES [dbo].[Employee] ([EmployeeId]),
	 CONSTRAINT [fk_BadgeAward_PaidCompletedBy] FOREIGN KEY ([PaidCompletedById]) REFERENCES [dbo].[Employee] ([EmployeeId])
);