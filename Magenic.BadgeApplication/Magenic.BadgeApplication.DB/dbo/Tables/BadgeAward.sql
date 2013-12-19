CREATE TABLE [dbo].[BadgeAward] (
    [BadgeAwardId]    INT           IDENTITY (1, 1) NOT NULL CONSTRAINT [pk_BadgeAward] PRIMARY KEY CLUSTERED ([BadgeAwardId] ASC),
    [BadgeId]         INT           NOT NULL CONSTRAINT [fk_BadgeAward_Badge] FOREIGN KEY ([BadgeId]) REFERENCES [dbo].[Badge] ([BadgeId]),
    [EmployeeADName]  VARCHAR(100)  NOT NULL CONSTRAINT [fk_BadgeAward_Employee] FOREIGN KEY ([EmployeeADName]) REFERENCES [dbo].[Employee] ([ADName]),
    [AwardDate]       DATE          CONSTRAINT [df_BadgeAward_AwardDate] DEFAULT getdate() NOT NULL,
    [AwardAmount]     INT           CONSTRAINT [df_BadgeAward_AwardAmount] DEFAULT 0 NOT NULL,
    [PaidOut]         BIT           CONSTRAINT [df_BadgeAward_PaidOut] DEFAULT 0 NOT NULL,
    [PaidDate]        DATETIME2 (7) NULL,
    [PaidCompletedByADName] VARCHAR(100)           NULL CONSTRAINT [fk_BadgeAward_PaidCompletedBy] FOREIGN KEY ([PaidCompletedByADName]) REFERENCES [dbo].[Employee] ([ADName]),
    [Published]       BIT           NULL
);