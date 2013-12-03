CREATE TABLE [dbo].[Badge] (
    [BadgeId]                    INT             IDENTITY (1, 1) NOT NULL CONSTRAINT [pk_Badge] PRIMARY KEY CLUSTERED ([BadgeId] ASC),
    [BadgeName]                  VARCHAR (200)   NOT NULL,
    [BadgeTagLine]               VARCHAR (200)   NULL,
    [BadgeDescription]           VARCHAR (MAX)   NULL,
    [BadgeTypeId]                INT             NULL CONSTRAINT [fk_Badge_BadgeType] FOREIGN KEY ([BadgeTypeId]) REFERENCES [dbo].[BadgeType] ([BadgeTypeId]),
    [BadgeImage]                 VARBINARY (MAX) NULL,
    [BadgeCreated]               DATETIME2 (7)   CONSTRAINT [df_Badge_BadgeCreated] DEFAULT getdate() NOT NULL,
    [BadgeEffectiveStart]        DATETIME2 (7)   NULL,
    [BadgeEffectiveEnd]          DATETIME2 (7)   NULL,
    [BadgePriority]              INT             NULL,
    [MultipleAwardPossible]      BIT             NULL,
    [DisplayOnce]                BIT             NULL,
    [ManagementApprovalRequired] BIT             NULL,
    [ActivityPointsAmount]       INT             NULL,
    [BadgeAwardValueAmount]      INT             NULL,
    [BadgeApprovedBy]            INT             NOT NULL CONSTRAINT [fk_Badge_Employee] FOREIGN KEY ([BadgeApprovedBy]) REFERENCES [dbo].[Employee] ([EmployeeId]),
    [BadgeApprovedDate]          DATETIME2 (7)   CONSTRAINT [df_Badge_BadgeApprovedDate] DEFAULT getdate() NOT NULL
);