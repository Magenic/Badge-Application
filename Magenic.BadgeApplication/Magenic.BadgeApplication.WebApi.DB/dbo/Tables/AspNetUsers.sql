CREATE TABLE [dbo].[AspNetUsers] (
    [Id]            NVARCHAR (128) NOT NULL,
    [UserName]      NVARCHAR (MAX) NOT NULL,
    [PasswordHash]  NVARCHAR (MAX) NULL,
    [SecurityStamp] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED ([Id] ASC)
);

