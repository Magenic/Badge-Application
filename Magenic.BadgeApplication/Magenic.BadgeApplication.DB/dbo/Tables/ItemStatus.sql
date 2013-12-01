CREATE TABLE [dbo].[ItemStatus] (
    [ItemStatusId] INT          IDENTITY (1, 1) NOT NULL,
    [StatusName]   VARCHAR (50) NOT NULL,
    CONSTRAINT [pk_ItemStatus] PRIMARY KEY CLUSTERED ([ItemStatusId] ASC)
);

