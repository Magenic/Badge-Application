
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 05/18/2018 17:37:38
-- Generated from EDMX file: D:\Dev\Badge-Application\Magenic.BadgeApplication\Magenic.BadgeApplication.DataAccess.EF\Badge.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Magenic.BadgeApplication.DB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Activity_ActivityEntryType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Activity] DROP CONSTRAINT [FK_Activity_ActivityEntryType];
GO
IF OBJECT_ID(N'[dbo].[FK_Activity_Employee]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Activity] DROP CONSTRAINT [FK_Activity_Employee];
GO
IF OBJECT_ID(N'[dbo].[fk_ActivitySubmission_Activity]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ActivitySubmission] DROP CONSTRAINT [fk_ActivitySubmission_Activity];
GO
IF OBJECT_ID(N'[dbo].[fk_ActivitySubmission_Employee]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ActivitySubmission] DROP CONSTRAINT [fk_ActivitySubmission_Employee];
GO
IF OBJECT_ID(N'[dbo].[fk_ActivitySubmission_EmployeeApproval]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ActivitySubmission] DROP CONSTRAINT [fk_ActivitySubmission_EmployeeApproval];
GO
IF OBJECT_ID(N'[dbo].[fk_ActivitySubmission_ItemStatus]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ActivitySubmission] DROP CONSTRAINT [fk_ActivitySubmission_ItemStatus];
GO
IF OBJECT_ID(N'[dbo].[fk_Badge_BadgeType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Badge] DROP CONSTRAINT [fk_Badge_BadgeType];
GO
IF OBJECT_ID(N'[dbo].[fk_Badge_Employee]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Badge] DROP CONSTRAINT [fk_Badge_Employee];
GO
IF OBJECT_ID(N'[dbo].[FK_Badge_ItemStatus]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Badge] DROP CONSTRAINT [FK_Badge_ItemStatus];
GO
IF OBJECT_ID(N'[dbo].[FK_Badge_ToCreateEmployee]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Badge] DROP CONSTRAINT [FK_Badge_ToCreateEmployee];
GO
IF OBJECT_ID(N'[dbo].[fk_BadgeActivity_Activity]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BadgeActivity] DROP CONSTRAINT [fk_BadgeActivity_Activity];
GO
IF OBJECT_ID(N'[dbo].[fk_BadgeActivity_Badge]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BadgeActivity] DROP CONSTRAINT [fk_BadgeActivity_Badge];
GO
IF OBJECT_ID(N'[dbo].[fk_BadgeAward_Badge]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BadgeAward] DROP CONSTRAINT [fk_BadgeAward_Badge];
GO
IF OBJECT_ID(N'[dbo].[fk_BadgeAward_Employee]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BadgeAward] DROP CONSTRAINT [fk_BadgeAward_Employee];
GO
IF OBJECT_ID(N'[dbo].[fk_BadgeAward_PaidCompletedBy]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BadgeAward] DROP CONSTRAINT [fk_BadgeAward_PaidCompletedBy];
GO
IF OBJECT_ID(N'[dbo].[FK_BadgePrerequisite_Badge]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BadgePrerequisite] DROP CONSTRAINT [FK_BadgePrerequisite_Badge];
GO
IF OBJECT_ID(N'[dbo].[FK_BadgePrerequisite_Badge1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BadgePrerequisite] DROP CONSTRAINT [FK_BadgePrerequisite_Badge1];
GO
IF OBJECT_ID(N'[dbo].[fk_Employee_ApprovingManager1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Employee] DROP CONSTRAINT [fk_Employee_ApprovingManager1];
GO
IF OBJECT_ID(N'[dbo].[fk_Employee_ApprovingManager2]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Employee] DROP CONSTRAINT [fk_Employee_ApprovingManager2];
GO
IF OBJECT_ID(N'[dbo].[fk_EmployeePermission_Employee]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EmployeePermission] DROP CONSTRAINT [fk_EmployeePermission_Employee];
GO
IF OBJECT_ID(N'[dbo].[fk_EmployeePermission_Permission]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EmployeePermission] DROP CONSTRAINT [fk_EmployeePermission_Permission];
GO
IF OBJECT_ID(N'[dbo].[fk_QueueEventLog_BadgeAward]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[QueueEventLog] DROP CONSTRAINT [fk_QueueEventLog_BadgeAward];
GO
IF OBJECT_ID(N'[dbo].[fk_QueueEventLog_QueueEvent]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[QueueEventLog] DROP CONSTRAINT [fk_QueueEventLog_QueueEvent];
GO
IF OBJECT_ID(N'[dbo].[fk_QueueItem_BadgeAward]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[QueueItem] DROP CONSTRAINT [fk_QueueItem_BadgeAward];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Activity]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Activity];
GO
IF OBJECT_ID(N'[dbo].[ActivityEntryType]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ActivityEntryType];
GO
IF OBJECT_ID(N'[dbo].[ActivitySubmission]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ActivitySubmission];
GO
IF OBJECT_ID(N'[dbo].[Badge]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Badge];
GO
IF OBJECT_ID(N'[dbo].[BadgeActivity]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BadgeActivity];
GO
IF OBJECT_ID(N'[dbo].[BadgeAward]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BadgeAward];
GO
IF OBJECT_ID(N'[dbo].[BadgePrerequisite]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BadgePrerequisite];
GO
IF OBJECT_ID(N'[dbo].[BadgeType]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BadgeType];
GO
IF OBJECT_ID(N'[dbo].[Employee]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Employee];
GO
IF OBJECT_ID(N'[dbo].[EmployeePermission]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EmployeePermission];
GO
IF OBJECT_ID(N'[dbo].[ItemStatus]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ItemStatus];
GO
IF OBJECT_ID(N'[dbo].[Permission]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Permission];
GO
IF OBJECT_ID(N'[dbo].[QueueEvent]', 'U') IS NOT NULL
    DROP TABLE [dbo].[QueueEvent];
GO
IF OBJECT_ID(N'[dbo].[QueueEventLog]', 'U') IS NOT NULL
    DROP TABLE [dbo].[QueueEventLog];
GO
IF OBJECT_ID(N'[dbo].[QueueItem]', 'U') IS NOT NULL
    DROP TABLE [dbo].[QueueItem];
GO
IF OBJECT_ID(N'[MagenicBadgeApplicationDBModelStoreContainer].[CurrentActiveBadges]', 'U') IS NOT NULL
    DROP TABLE [MagenicBadgeApplicationDBModelStoreContainer].[CurrentActiveBadges];
GO
IF OBJECT_ID(N'[MagenicBadgeApplicationDBModelStoreContainer].[EarnedBadges]', 'U') IS NOT NULL
    DROP TABLE [MagenicBadgeApplicationDBModelStoreContainer].[EarnedBadges];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Activities'
CREATE TABLE [dbo].[Activity] (
    [ActivityId] int IDENTITY(1,1) NOT NULL,
    [ActivityName] varchar(100)  NOT NULL,
    [ActivityDescription] varchar(max)  NULL,
    [RequiresApproval] bit  NOT NULL,
    [CreateEmployeeId] int  NOT NULL,
    [EntryTypeId] int  NOT NULL
);
GO

-- Creating table 'BadgeActivities'
CREATE TABLE [dbo].[BadgeActivity] (
    [BadgeActivityId] int IDENTITY(1,1) NOT NULL,
    [BadgeId] int  NOT NULL,
    [ActivityId] int  NOT NULL,
    [PointsAwarded] int  NOT NULL
);
GO

-- Creating table 'BadgeTypes'
CREATE TABLE [dbo].[BadgeType] (
    [BadgeTypeId] int IDENTITY(1,1) NOT NULL,
    [BadgeTypeName] varchar(50)  NOT NULL,
    [PayrollEligible] bit  NOT NULL
);
GO

-- Creating table 'EmployeePermissions'
CREATE TABLE [dbo].[EmployeePermission] (
    [EmployeePermissionId] int IDENTITY(1,1) NOT NULL,
    [EmployeeId] int  NOT NULL,
    [PermissionId] int  NOT NULL
);
GO

-- Creating table 'Permissions'
CREATE TABLE [dbo].[Permission] (
    [PermissionId] int IDENTITY(1,1) NOT NULL,
    [PermissionName] varchar(100)  NOT NULL,
    [PermissionDesc] varchar(max)  NULL
);
GO

-- Creating table 'BadgePrerequisites'
CREATE TABLE [dbo].[BadgePrerequisite] (
    [PrerequisiteID] int IDENTITY(1,1) NOT NULL,
    [BadgeId] int  NULL,
    [RequiredBadgeId] int  NULL
);
GO

-- Creating table 'ActivitySubmissions'
CREATE TABLE [dbo].[ActivitySubmission] (
    [ActivitySubmissionId] int IDENTITY(1,1) NOT NULL,
    [ActivityId] int  NOT NULL,
    [EmployeeId] int  NOT NULL,
    [SubmissionDescription] varchar(max)  NULL,
    [SubmissionApprovedById] int  NULL,
    [SubmissionDate] datetime  NOT NULL,
    [SubmissionStatusId] int  NOT NULL,
    [AwardValue] int  NULL
);
GO

-- Creating table 'BadgeAwards'
CREATE TABLE [dbo].[BadgeAward] (
    [BadgeAwardId] int IDENTITY(1,1) NOT NULL,
    [BadgeId] int  NOT NULL,
    [EmployeeId] int  NOT NULL,
    [AwardDate] datetime  NOT NULL,
    [AwardAmount] int  NOT NULL,
    [PaidOut] bit  NOT NULL,
    [PaidDate] datetime  NULL,
    [PaidCompletedById] int  NULL,
    [Published] bit  NULL
);
GO

-- Creating table 'CurrentActiveBadges'
CREATE TABLE [dbo].[CurrentActiveBadges] (
    [BadgeTypeName] varchar(50)  NOT NULL,
    [PayrollEligible] bit  NOT NULL,
    [BadgeId] int  NOT NULL,
    [BadgeName] varchar(200)  NOT NULL,
    [BadgeTagLine] varchar(200)  NULL,
    [BadgeDescription] varchar(max)  NULL,
    [BadgeTypeId] int  NOT NULL,
    [BadgePath] varchar(512)  NULL,
    [BadgeCreated] datetime  NOT NULL,
    [BadgeEffectiveStart] datetime  NULL,
    [BadgeEffectiveEnd] datetime  NULL,
    [BadgePriority] int  NOT NULL,
    [MultipleAwardPossible] bit  NOT NULL,
    [DisplayOnce] bit  NOT NULL,
    [ManagementApprovalRequired] bit  NOT NULL,
    [ActivityPointsAmount] int  NOT NULL,
    [BadgeAwardValueAmount] int  NOT NULL,
    [BadgeApprovedById] int  NULL,
    [BadgeApprovedDate] datetime  NULL,
    [BadgeAwardValueAmountMax] int  NULL
);
GO

-- Creating table 'EarnedBadges'
CREATE TABLE [dbo].[EarnedBadges] (
    [EmployeeId] int  NOT NULL,
    [FirstName] nvarchar(100)  NOT NULL,
    [LastName] nvarchar(100)  NOT NULL,
    [AwardPayoutThreshold] int  NULL,
    [ADName] nvarchar(100)  NOT NULL,
    [BadgeId] int  NOT NULL,
    [BadgeName] varchar(200)  NOT NULL,
    [BadgeTagLine] varchar(200)  NULL,
    [BadgeDescription] varchar(max)  NULL,
    [BadgeTypeId] int  NOT NULL,
    [BadgePath] varchar(512)  NULL,
    [BadgeCreated] datetime  NOT NULL,
    [BadgeEffectiveStart] datetime  NULL,
    [BadgeEffectiveEnd] datetime  NULL,
    [BadgePriority] int  NOT NULL,
    [MultipleAwardPossible] bit  NOT NULL,
    [DisplayOnce] bit  NOT NULL,
    [ManagementApprovalRequired] bit  NOT NULL,
    [ActivityPointsAmount] int  NOT NULL,
    [BadgeAwardValueAmount] int  NOT NULL,
    [BadgeApprovedById] int  NULL,
    [BadgeApprovedDate] datetime  NULL,
    [BadgeTypeName] varchar(50)  NOT NULL,
    [PayrollEligible] bit  NOT NULL,
    [AwardDate] datetime  NOT NULL,
    [AwardAmount] int  NOT NULL,
    [PaidOut] bit  NOT NULL,
    [BadgeAwardId] int  NOT NULL
);
GO

-- Creating table 'QueueEvents'
CREATE TABLE [dbo].[QueueEvent] (
    [QueueEventId] int  NOT NULL,
    [QueueEventName] nvarchar(30)  NOT NULL,
    [QueueEventDescription] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'QueueItems'
CREATE TABLE [dbo].[QueueItem] (
    [QueueItemId] int IDENTITY(1,1) NOT NULL,
    [BadgeAwardId] int  NOT NULL,
    [QueueItemCreated] datetime  NOT NULL
);
GO

-- Creating table 'ItemStatus'
CREATE TABLE [dbo].[ItemStatus] (
    [ItemStatusId] int IDENTITY(1,1) NOT NULL,
    [StatusName] varchar(50)  NOT NULL
);
GO

-- Creating table 'QueueEventLogs'
CREATE TABLE [dbo].[QueueEventLog] (
    [QueueEventLogId] int IDENTITY(1,1) NOT NULL,
    [QueueEventId] int  NOT NULL,
    [BadgeAwardId] int  NOT NULL,
    [QueueEventCreated] datetime  NOT NULL,
    [Message] nvarchar(200)  NULL
);
GO

-- Creating table 'Badges'
CREATE TABLE [dbo].[Badge] (
    [BadgeId] int IDENTITY(1,1) NOT NULL,
    [BadgeName] varchar(200)  NOT NULL,
    [BadgeTagLine] varchar(200)  NULL,
    [BadgeDescription] varchar(max)  NULL,
    [BadgeTypeId] int  NOT NULL,
    [BadgePath] varchar(512)  NULL,
    [BadgeCreated] datetime  NOT NULL,
    [BadgeEffectiveStart] datetime  NULL,
    [BadgeEffectiveEnd] datetime  NULL,
    [BadgePriority] int  NOT NULL,
    [MultipleAwardPossible] bit  NOT NULL,
    [DisplayOnce] bit  NOT NULL,
    [ManagementApprovalRequired] bit  NOT NULL,
    [ActivityPointsAmount] int  NOT NULL,
    [BadgeAwardValueAmount] int  NOT NULL,
    [BadgeApprovedById] int  NULL,
    [BadgeApprovedDate] datetime  NULL,
    [BadgeStatusId] int  NOT NULL,
    [CreateEmployeeId] int  NOT NULL,
    [BadgeAwardValueAmountMax] int  NULL
);
GO

-- Creating table 'Employees'
CREATE TABLE [dbo].[Employee] (
    [EmployeeId] int IDENTITY(1,1) NOT NULL,
    [FirstName] nvarchar(100)  NOT NULL,
    [LastName] nvarchar(100)  NOT NULL,
    [ADName] nvarchar(100)  NOT NULL,
    [EmploymentStartDate] datetime  NULL,
    [EmploymentEndDate] datetime  NULL,
    [ApprovingManagerId1] int  NULL,
    [ApprovingManagerId2] int  NULL,
    [AwardPayoutThreshold] int  NULL,
    [Location] varchar(100)  NULL,
    [Department] varchar(100)  NULL,
    [EmailAddress] nvarchar(255)  NULL
);
GO

-- Creating table 'ActivityEntryTypes'
CREATE TABLE [dbo].[ActivityEntryType] (
    [ActivityEntryTypeId] int IDENTITY(1,1) NOT NULL,
    [EntryTypeName] varchar(50)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ActivityId] in table 'Activities'
ALTER TABLE [dbo].[Activity]
ADD CONSTRAINT [PK_Activity]
    PRIMARY KEY CLUSTERED ([ActivityId] ASC);
GO

-- Creating primary key on [BadgeActivityId] in table 'BadgeActivities'
ALTER TABLE [dbo].[BadgeActivity]
ADD CONSTRAINT [PK_BadgeActivity]
    PRIMARY KEY CLUSTERED ([BadgeActivityId] ASC);
GO

-- Creating primary key on [BadgeTypeId] in table 'BadgeTypes'
ALTER TABLE [dbo].[BadgeType]
ADD CONSTRAINT [PK_BadgeType]
    PRIMARY KEY CLUSTERED ([BadgeTypeId] ASC);
GO

-- Creating primary key on [EmployeePermissionId] in table 'EmployeePermissions'
ALTER TABLE [dbo].[EmployeePermission]
ADD CONSTRAINT [PK_EmployeePermission]
    PRIMARY KEY CLUSTERED ([EmployeePermissionId] ASC);
GO

-- Creating primary key on [PermissionId] in table 'Permissions'
ALTER TABLE [dbo].[Permission]
ADD CONSTRAINT [PK_Permission]
    PRIMARY KEY CLUSTERED ([PermissionId] ASC);
GO

-- Creating primary key on [PrerequisiteID] in table 'BadgePrerequisites'
ALTER TABLE [dbo].[BadgePrerequisite]
ADD CONSTRAINT [PK_BadgePrerequisite]
    PRIMARY KEY CLUSTERED ([PrerequisiteID] ASC);
GO

-- Creating primary key on [ActivitySubmissionId] in table 'ActivitySubmissions'
ALTER TABLE [dbo].[ActivitySubmission]
ADD CONSTRAINT [PK_ActivitySubmission]
    PRIMARY KEY CLUSTERED ([ActivitySubmissionId] ASC);
GO

-- Creating primary key on [BadgeAwardId] in table 'BadgeAwards'
ALTER TABLE [dbo].[BadgeAward]
ADD CONSTRAINT [PK_BadgeAward]
    PRIMARY KEY CLUSTERED ([BadgeAwardId] ASC);
GO

-- Creating primary key on [BadgeTypeName], [PayrollEligible], [BadgeId], [BadgeName], [BadgeTypeId], [BadgeCreated], [BadgePriority], [MultipleAwardPossible], [DisplayOnce], [ManagementApprovalRequired], [ActivityPointsAmount], [BadgeAwardValueAmount] in table 'CurrentActiveBadges'
ALTER TABLE [dbo].[CurrentActiveBadges]
ADD CONSTRAINT [PK_CurrentActiveBadges]
    PRIMARY KEY CLUSTERED ([BadgeTypeName], [PayrollEligible], [BadgeId], [BadgeName], [BadgeTypeId], [BadgeCreated], [BadgePriority], [MultipleAwardPossible], [DisplayOnce], [ManagementApprovalRequired], [ActivityPointsAmount], [BadgeAwardValueAmount] ASC);
GO

-- Creating primary key on [EmployeeId], [FirstName], [LastName], [ADName], [BadgeId], [BadgeName], [BadgeTypeId], [BadgeCreated], [BadgePriority], [MultipleAwardPossible], [DisplayOnce], [ManagementApprovalRequired], [ActivityPointsAmount], [BadgeAwardValueAmount], [BadgeTypeName], [PayrollEligible], [AwardDate], [AwardAmount], [PaidOut], [BadgeAwardId] in table 'EarnedBadges'
ALTER TABLE [dbo].[EarnedBadges]
ADD CONSTRAINT [PK_EarnedBadges]
    PRIMARY KEY CLUSTERED ([EmployeeId], [FirstName], [LastName], [ADName], [BadgeId], [BadgeName], [BadgeTypeId], [BadgeCreated], [BadgePriority], [MultipleAwardPossible], [DisplayOnce], [ManagementApprovalRequired], [ActivityPointsAmount], [BadgeAwardValueAmount], [BadgeTypeName], [PayrollEligible], [AwardDate], [AwardAmount], [PaidOut], [BadgeAwardId] ASC);
GO

-- Creating primary key on [QueueEventId] in table 'QueueEvents'
ALTER TABLE [dbo].[QueueEvent]
ADD CONSTRAINT [PK_QueueEvent]
    PRIMARY KEY CLUSTERED ([QueueEventId] ASC);
GO

-- Creating primary key on [QueueItemId] in table 'QueueItems'
ALTER TABLE [dbo].[QueueItem]
ADD CONSTRAINT [PK_QueueItem]
    PRIMARY KEY CLUSTERED ([QueueItemId] ASC);
GO

-- Creating primary key on [ItemStatusId] in table 'ItemStatus'
ALTER TABLE [dbo].[ItemStatus]
ADD CONSTRAINT [PK_ItemStatus]
    PRIMARY KEY CLUSTERED ([ItemStatusId] ASC);
GO

-- Creating primary key on [QueueEventLogId] in table 'QueueEventLogs'
ALTER TABLE [dbo].[QueueEventLog]
ADD CONSTRAINT [PK_QueueEventLog]
    PRIMARY KEY CLUSTERED ([QueueEventLogId] ASC);
GO

-- Creating primary key on [BadgeId] in table 'Badges'
ALTER TABLE [dbo].[Badge]
ADD CONSTRAINT [PK_Badge]
    PRIMARY KEY CLUSTERED ([BadgeId] ASC);
GO

-- Creating primary key on [EmployeeId] in table 'Employees'
ALTER TABLE [dbo].[Employee]
ADD CONSTRAINT [PK_Employee]
    PRIMARY KEY CLUSTERED ([EmployeeId] ASC);
GO

-- Creating primary key on [ActivityEntryTypeId] in table 'ActivityEntryTypes'
ALTER TABLE [dbo].[ActivityEntryType]
ADD CONSTRAINT [PK_ActivityEntryType]
    PRIMARY KEY CLUSTERED ([ActivityEntryTypeId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [ActivityId] in table 'BadgeActivities'
ALTER TABLE [dbo].[BadgeActivity]
ADD CONSTRAINT [fk_BadgeActivity_Activity]
    FOREIGN KEY ([ActivityId])
    REFERENCES [dbo].[Activity]
        ([ActivityId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'fk_BadgeActivity_Activity'
CREATE INDEX [IX_fk_BadgeActivity_Activity]
ON [dbo].[BadgeActivity]
    ([ActivityId]);
GO

-- Creating foreign key on [PermissionId] in table 'EmployeePermissions'
ALTER TABLE [dbo].[EmployeePermission]
ADD CONSTRAINT [fk_EmployeePermission_Permission]
    FOREIGN KEY ([PermissionId])
    REFERENCES [dbo].[Permission]
        ([PermissionId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'fk_EmployeePermission_Permission'
CREATE INDEX [IX_fk_EmployeePermission_Permission]
ON [dbo].[EmployeePermission]
    ([PermissionId]);
GO

-- Creating foreign key on [ActivityId] in table 'ActivitySubmissions'
ALTER TABLE [dbo].[ActivitySubmission]
ADD CONSTRAINT [fk_ActivitySubmission_Activity]
    FOREIGN KEY ([ActivityId])
    REFERENCES [dbo].[Activity]
        ([ActivityId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'fk_ActivitySubmission_Activity'
CREATE INDEX [IX_fk_ActivitySubmission_Activity]
ON [dbo].[ActivitySubmission]
    ([ActivityId]);
GO

-- Creating foreign key on [BadgeAwardId] in table 'QueueItems'
ALTER TABLE [dbo].[QueueItem]
ADD CONSTRAINT [fk_QueueItem_BadgeAward]
    FOREIGN KEY ([BadgeAwardId])
    REFERENCES [dbo].[BadgeAward]
        ([BadgeAwardId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'fk_QueueItem_BadgeAward'
CREATE INDEX [IX_fk_QueueItem_BadgeAward]
ON [dbo].[QueueItem]
    ([BadgeAwardId]);
GO

-- Creating foreign key on [SubmissionStatusId] in table 'ActivitySubmissions'
ALTER TABLE [dbo].[ActivitySubmission]
ADD CONSTRAINT [fk_ActivitySubmission_ItemStatus]
    FOREIGN KEY ([SubmissionStatusId])
    REFERENCES [dbo].[ItemStatus]
        ([ItemStatusId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'fk_ActivitySubmission_ItemStatus'
CREATE INDEX [IX_fk_ActivitySubmission_ItemStatus]
ON [dbo].[ActivitySubmission]
    ([SubmissionStatusId]);
GO

-- Creating foreign key on [BadgeAwardId] in table 'QueueEventLogs'
ALTER TABLE [dbo].[QueueEventLog]
ADD CONSTRAINT [fk_QueueEventLog_BadgeAward]
    FOREIGN KEY ([BadgeAwardId])
    REFERENCES [dbo].[BadgeAward]
        ([BadgeAwardId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'fk_QueueEventLog_BadgeAward'
CREATE INDEX [IX_fk_QueueEventLog_BadgeAward]
ON [dbo].[QueueEventLog]
    ([BadgeAwardId]);
GO

-- Creating foreign key on [QueueEventId] in table 'QueueEventLogs'
ALTER TABLE [dbo].[QueueEventLog]
ADD CONSTRAINT [fk_QueueEventLog_QueueEvent]
    FOREIGN KEY ([QueueEventId])
    REFERENCES [dbo].[QueueEvent]
        ([QueueEventId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'fk_QueueEventLog_QueueEvent'
CREATE INDEX [IX_fk_QueueEventLog_QueueEvent]
ON [dbo].[QueueEventLog]
    ([QueueEventId]);
GO

-- Creating foreign key on [BadgeTypeId] in table 'Badges'
ALTER TABLE [dbo].[Badge]
ADD CONSTRAINT [fk_Badge_BadgeType]
    FOREIGN KEY ([BadgeTypeId])
    REFERENCES [dbo].[BadgeType]
        ([BadgeTypeId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'fk_Badge_BadgeType'
CREATE INDEX [IX_fk_Badge_BadgeType]
ON [dbo].[Badge]
    ([BadgeTypeId]);
GO

-- Creating foreign key on [BadgeStatusId] in table 'Badges'
ALTER TABLE [dbo].[Badge]
ADD CONSTRAINT [FK_Badge_ItemStatus]
    FOREIGN KEY ([BadgeStatusId])
    REFERENCES [dbo].[ItemStatus]
        ([ItemStatusId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Badge_ItemStatus'
CREATE INDEX [IX_FK_Badge_ItemStatus]
ON [dbo].[Badge]
    ([BadgeStatusId]);
GO

-- Creating foreign key on [BadgeId] in table 'BadgeActivities'
ALTER TABLE [dbo].[BadgeActivity]
ADD CONSTRAINT [fk_BadgeActivity_Badge]
    FOREIGN KEY ([BadgeId])
    REFERENCES [dbo].[Badge]
        ([BadgeId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'fk_BadgeActivity_Badge'
CREATE INDEX [IX_fk_BadgeActivity_Badge]
ON [dbo].[BadgeActivity]
    ([BadgeId]);
GO

-- Creating foreign key on [BadgeId] in table 'BadgeAwards'
ALTER TABLE [dbo].[BadgeAward]
ADD CONSTRAINT [fk_BadgeAward_Badge]
    FOREIGN KEY ([BadgeId])
    REFERENCES [dbo].[Badge]
        ([BadgeId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'fk_BadgeAward_Badge'
CREATE INDEX [IX_fk_BadgeAward_Badge]
ON [dbo].[BadgeAward]
    ([BadgeId]);
GO

-- Creating foreign key on [BadgeId] in table 'BadgePrerequisites'
ALTER TABLE [dbo].[BadgePrerequisite]
ADD CONSTRAINT [FK_BadgePrerequisite_Badge]
    FOREIGN KEY ([BadgeId])
    REFERENCES [dbo].[Badge]
        ([BadgeId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_BadgePrerequisite_Badge'
CREATE INDEX [IX_FK_BadgePrerequisite_Badge]
ON [dbo].[BadgePrerequisite]
    ([BadgeId]);
GO

-- Creating foreign key on [RequiredBadgeId] in table 'BadgePrerequisites'
ALTER TABLE [dbo].[BadgePrerequisite]
ADD CONSTRAINT [FK_BadgePrerequisite_Badge1]
    FOREIGN KEY ([RequiredBadgeId])
    REFERENCES [dbo].[Badge]
        ([BadgeId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_BadgePrerequisite_Badge1'
CREATE INDEX [IX_FK_BadgePrerequisite_Badge1]
ON [dbo].[BadgePrerequisite]
    ([RequiredBadgeId]);
GO

-- Creating foreign key on [EmployeeId] in table 'ActivitySubmissions'
ALTER TABLE [dbo].[ActivitySubmission]
ADD CONSTRAINT [fk_ActivitySubmission_Employee]
    FOREIGN KEY ([EmployeeId])
    REFERENCES [dbo].[Employee]
        ([EmployeeId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'fk_ActivitySubmission_Employee'
CREATE INDEX [IX_fk_ActivitySubmission_Employee]
ON [dbo].[ActivitySubmission]
    ([EmployeeId]);
GO

-- Creating foreign key on [SubmissionApprovedById] in table 'ActivitySubmissions'
ALTER TABLE [dbo].[ActivitySubmission]
ADD CONSTRAINT [fk_ActivitySubmission_EmployeeApproval]
    FOREIGN KEY ([SubmissionApprovedById])
    REFERENCES [dbo].[Employee]
        ([EmployeeId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'fk_ActivitySubmission_EmployeeApproval'
CREATE INDEX [IX_fk_ActivitySubmission_EmployeeApproval]
ON [dbo].[ActivitySubmission]
    ([SubmissionApprovedById]);
GO

-- Creating foreign key on [BadgeApprovedById] in table 'Badges'
ALTER TABLE [dbo].[Badge]
ADD CONSTRAINT [fk_Badge_Employee]
    FOREIGN KEY ([BadgeApprovedById])
    REFERENCES [dbo].[Employee]
        ([EmployeeId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'fk_Badge_Employee'
CREATE INDEX [IX_fk_Badge_Employee]
ON [dbo].[Badge]
    ([BadgeApprovedById]);
GO

-- Creating foreign key on [EmployeeId] in table 'BadgeAwards'
ALTER TABLE [dbo].[BadgeAward]
ADD CONSTRAINT [fk_BadgeAward_Employee]
    FOREIGN KEY ([EmployeeId])
    REFERENCES [dbo].[Employee]
        ([EmployeeId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'fk_BadgeAward_Employee'
CREATE INDEX [IX_fk_BadgeAward_Employee]
ON [dbo].[BadgeAward]
    ([EmployeeId]);
GO

-- Creating foreign key on [PaidCompletedById] in table 'BadgeAwards'
ALTER TABLE [dbo].[BadgeAward]
ADD CONSTRAINT [fk_BadgeAward_PaidCompletedBy]
    FOREIGN KEY ([PaidCompletedById])
    REFERENCES [dbo].[Employee]
        ([EmployeeId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'fk_BadgeAward_PaidCompletedBy'
CREATE INDEX [IX_fk_BadgeAward_PaidCompletedBy]
ON [dbo].[BadgeAward]
    ([PaidCompletedById]);
GO

-- Creating foreign key on [ApprovingManagerId1] in table 'Employees'
ALTER TABLE [dbo].[Employee]
ADD CONSTRAINT [fk_Employee_ApprovingManager1]
    FOREIGN KEY ([ApprovingManagerId1])
    REFERENCES [dbo].[Employee]
        ([EmployeeId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'fk_Employee_ApprovingManager1'
CREATE INDEX [IX_fk_Employee_ApprovingManager1]
ON [dbo].[Employee]
    ([ApprovingManagerId1]);
GO

-- Creating foreign key on [ApprovingManagerId2] in table 'Employees'
ALTER TABLE [dbo].[Employee]
ADD CONSTRAINT [fk_Employee_ApprovingManager2]
    FOREIGN KEY ([ApprovingManagerId2])
    REFERENCES [dbo].[Employee]
        ([EmployeeId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'fk_Employee_ApprovingManager2'
CREATE INDEX [IX_fk_Employee_ApprovingManager2]
ON [dbo].[Employee]
    ([ApprovingManagerId2]);
GO

-- Creating foreign key on [EmployeeId] in table 'EmployeePermissions'
ALTER TABLE [dbo].[EmployeePermission]
ADD CONSTRAINT [fk_EmployeePermission_Employee]
    FOREIGN KEY ([EmployeeId])
    REFERENCES [dbo].[Employee]
        ([EmployeeId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'fk_EmployeePermission_Employee'
CREATE INDEX [IX_fk_EmployeePermission_Employee]
ON [dbo].[EmployeePermission]
    ([EmployeeId]);
GO

-- Creating foreign key on [CreateEmployeeId] in table 'Activities'
ALTER TABLE [dbo].[Activity]
ADD CONSTRAINT [FK_Activity_Employee]
    FOREIGN KEY ([CreateEmployeeId])
    REFERENCES [dbo].[Employee]
        ([EmployeeId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Activity_Employee'
CREATE INDEX [IX_FK_Activity_Employee]
ON [dbo].[Activity]
    ([CreateEmployeeId]);
GO

-- Creating foreign key on [CreateEmployeeId] in table 'Badges'
ALTER TABLE [dbo].[Badge]
ADD CONSTRAINT [FK_Badge_ToCreateEmployee]
    FOREIGN KEY ([CreateEmployeeId])
    REFERENCES [dbo].[Employee]
        ([EmployeeId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Badge_ToCreateEmployee'
CREATE INDEX [IX_FK_Badge_ToCreateEmployee]
ON [dbo].[Badge]
    ([CreateEmployeeId]);
GO

-- Creating foreign key on [EntryTypeId] in table 'Activities'
ALTER TABLE [dbo].[Activity]
ADD CONSTRAINT [FK_Activity_ActivityEntryType]
    FOREIGN KEY ([EntryTypeId])
    REFERENCES [dbo].[ActivityEntryType]
        ([ActivityEntryTypeId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Activity_ActivityEntryType'
CREATE INDEX [IX_FK_Activity_ActivityEntryType]
ON [dbo].[Activity]
    ([EntryTypeId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------