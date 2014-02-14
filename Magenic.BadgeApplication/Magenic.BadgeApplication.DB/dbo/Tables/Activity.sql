CREATE TABLE [dbo].[Activity] (
    [ActivityId]          INT           IDENTITY (1, 1) NOT NULL CONSTRAINT [pk_Activity] PRIMARY KEY CLUSTERED ([ActivityId] ASC),
    [ActivityName]        VARCHAR (100) NOT NULL,
    [ActivityDescription] VARCHAR (MAX) NULL,
    [RequiresApproval]    BIT           CONSTRAINT [df_Activity_RequiresApproval] DEFAULT (0) NOT NULL, 
    [CreateEmployeeId] INT NOT NULL, 
    CONSTRAINT [FK_Activity_Employee] FOREIGN KEY ([CreateEmployeeId]) REFERENCES [Employee]([EmployeeId])
);