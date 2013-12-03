CREATE TABLE [dbo].[Permission] (
    [PermissionId]   INT           IDENTITY (1, 1) NOT NULL CONSTRAINT [pk_Permission] PRIMARY KEY CLUSTERED ([PermissionId] ASC),
    [PermissionName] VARCHAR (100) NOT NULL,
    [PermissionDesc] VARCHAR (MAX) NULL
);