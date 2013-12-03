CREATE TABLE [dbo].[Employee] (
    [EmployeeId]            INT           IDENTITY (1, 1) NOT NULL CONSTRAINT [pk_User] PRIMARY KEY CLUSTERED ([EmployeeId] ASC),
    [FirstName]             VARCHAR (100) NOT NULL,
    [LastName]              VARCHAR (100) NOT NULL,
    [ADName]                VARCHAR (100) NOT NULL,
    [EmploymentStartDate]   DATE          NOT NULL,
    [EmploymentEndDate]     DATE          NULL,
    [EmploymentStartDate2]  DATE          NULL,
    [EmploymentEndDate2]    DATE          NULL,
    [ApprovingManagerId1]   INT           NOT NULL CONSTRAINT [fk_Employee_ApprovingManager1] FOREIGN KEY ([ApprovingManagerId1]) REFERENCES [dbo].[Employee] ([EmployeeId]),
    [ApprovingManagerId2]   INT           NULL CONSTRAINT [fk_Employee_ApprovingManager2] FOREIGN KEY ([ApprovingManagerId2]) REFERENCES [dbo].[Employee] ([EmployeeId]),
    [AwardPayoutThreshhold] INT           CONSTRAINT [df_Employee_AwardPayoutThreshhold] DEFAULT 50 NULL
);