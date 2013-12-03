CREATE TABLE [dbo].[BadgeType] (
    [BadgeTypeId]     INT          IDENTITY (1, 1) NOT NULL CONSTRAINT [pk_BadgeType] PRIMARY KEY CLUSTERED ([BadgeTypeId] ASC),
    [BadgeTypeName]   VARCHAR (50) NOT NULL,
    [PayrollEligible] BIT          CONSTRAINT [df_BadgeType_PayrollEligble] DEFAULT (0) NOT NULL
);