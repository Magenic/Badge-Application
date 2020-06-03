CREATE TABLE [dbo].[BadgeRequest]
(
	[BadgeRequestId] INT IDENTITY (1, 1) NOT NULL CONSTRAINT [pk_BadgeRequest] PRIMARY KEY CLUSTERED ([BadgeRequestId] ASC), 
    [EmployeeId] INT NOT NULL CONSTRAINT [FK_BadgeRequest_Employee] FOREIGN KEY ([EmployeeId]) REFERENCES [dbo].[Employee]([EmployeeId]), 
    [BadgeName] VARCHAR(100) NOT NULL,
	[BadgeDescription] VARCHAR(255) NOT NULL,
	[CreatedDate] [datetime] NOT NULL DEFAULT getdate(),
	[NotifySentDate] [datetime] NULL,
)
