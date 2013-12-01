CREATE TABLE [dbo].[EmployeePermission] (
    [EmployeePermissionId] INT IDENTITY (1, 1) NOT NULL CONSTRAINT [pk_EmployeePermission] PRIMARY KEY CLUSTERED ([EmployeePermissionId] ASC),
    [EmployeeId]           INT NOT NULL CONSTRAINT [fk_EmployeePermission_Employee] FOREIGN KEY ([EmployeeId]) REFERENCES [dbo].[Employee] ([EmployeeId]),
    [PermissionId]         INT NOT NULL CONSTRAINT [fk_EmployeePermission_Permission] FOREIGN KEY ([PermissionId]) REFERENCES [dbo].[Permission] ([PermissionId])
);