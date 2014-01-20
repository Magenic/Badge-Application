CREATE TABLE [dbo].[ActivitySubmission] (
    [ActivitySubmissionId]  INT           IDENTITY (1, 1) NOT NULL,
    [ActivityId]            INT           NOT NULL,
    [EmployeeId]            INT           NOT NULL,
    [SubmissionDescription] VARCHAR (MAX) NULL,
    [SubmissionApprovedById]  INT           NULL,
    [SubmissionDate]        DATETIME2 (7) CONSTRAINT [df_ActivitySubmission_SubmissionDate] DEFAULT (getdate()) NOT NULL,
    [SubmissionStatusId]    INT           NOT NULL,
    CONSTRAINT [pk_ActivitySubmission] PRIMARY KEY CLUSTERED ([ActivitySubmissionId] ASC),
    CONSTRAINT [fk_ActivitySubmission_Activity] FOREIGN KEY ([ActivityId]) REFERENCES [dbo].[Activity] ([ActivityId]),
    CONSTRAINT [fk_ActivitySubmission_Employee] FOREIGN KEY ([EmployeeId]) REFERENCES [dbo].[Employee] ([EmployeeId]),
    CONSTRAINT [fk_ActivitySubmission_EmployeeApproval] FOREIGN KEY ([SubmissionApprovedById]) REFERENCES [dbo].[Employee] ([EmployeeId]),
	CONSTRAINT [fk_ActivitySubmission_ItemStatus] FOREIGN KEY ([SubmissionStatusId]) REFERENCES [dbo].[ItemStatus] ([ItemStatusId])
);

