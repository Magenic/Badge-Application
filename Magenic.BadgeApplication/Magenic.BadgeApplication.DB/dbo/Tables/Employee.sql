CREATE TABLE [dbo].[Employee] (
    [EmployeeId]            INT           IDENTITY (1, 1) NOT NULL CONSTRAINT [pk_User] PRIMARY KEY CLUSTERED ([EmployeeId] ASC),
    [FirstName]             NVARCHAR (100) NOT NULL,
    [LastName]              NVARCHAR (100) NOT NULL,
    [EmailAddress]			NVARCHAR(255) NULL,
	[Location]				VARCHAR (100) NULL,
	[Department]			VARCHAR (100) NULL,
    [ADName]                NVARCHAR (100) NOT NULL,
    [EmploymentStartDate]   DATE          NULL,
    [EmploymentEndDate]     DATE          NULL,
    [ApprovingManagerId1]   INT           NULL CONSTRAINT [fk_Employee_ApprovingManager1] FOREIGN KEY ([ApprovingManagerId1]) REFERENCES [dbo].[Employee] ([EmployeeId]),
    [ApprovingManagerId2]   INT           NULL CONSTRAINT [fk_Employee_ApprovingManager2] FOREIGN KEY ([ApprovingManagerId2]) REFERENCES [dbo].[Employee] ([EmployeeId]),
    [AwardPayoutThreshold]	INT           CONSTRAINT [df_Employee_AwardPayoutThreshhold] DEFAULT 50 NULL, 
);
GO

CREATE UNIQUE INDEX [IX_Employee_ADName] ON [dbo].[Employee] ([ADName])
