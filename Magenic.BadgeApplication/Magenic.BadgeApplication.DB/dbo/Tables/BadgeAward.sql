CREATE TABLE [dbo].[BadgeAward] (
    [BadgeAwardId]    INT           IDENTITY (1, 1) NOT NULL CONSTRAINT [pk_BadgeAward] PRIMARY KEY CLUSTERED ([BadgeAwardId] ASC),
    [BadgeId]         INT           NOT NULL CONSTRAINT [fk_BadgeAward_Badge] FOREIGN KEY ([BadgeId]) REFERENCES [dbo].[Badge] ([BadgeId]),
    [EmployeeId]      INT           NOT NULL CONSTRAINT [fk_BadgeAward_Employee] FOREIGN KEY ([EmployeeId]) REFERENCES [dbo].[Employee] ([EmployeeId]),
    [AwardDate]       DATE          CONSTRAINT [df_BadgeAward_AwardDate] DEFAULT getdate() NOT NULL,
    [AwardAmount]     INT           CONSTRAINT [df_BadgeAward_AwardAmount] DEFAULT 0 NOT NULL,
    [PaidOut]         BIT           CONSTRAINT [df_BadgeAward_PaidOut] DEFAULT 0 NOT NULL,
    [PaidDate]        DATETIME2 (7) NULL,
    [PaidCompletedBy] INT           NULL CONSTRAINT [fk_BadgeAward_PaidCompletedBy] FOREIGN KEY ([PaidCompletedBy]) REFERENCES [dbo].[Employee] ([EmployeeId]),
    [Published]       BIT           NULL
);