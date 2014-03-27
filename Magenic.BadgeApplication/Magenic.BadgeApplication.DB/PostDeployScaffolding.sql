/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

SET IDENTITY_INSERT [dbo].[Permission]  ON
MERGE INTO [dbo].[Permission]  AS Target
USING (VALUES
    (1, 'User', 'A normal system user'),
    (2, 'Administrator', 'An administrative user with the ability to approve corporate badges'),
    (3, 'Manager', 'A user who can approve submitted activities for their employees.')
)
AS Source ([permission_id], [permission_name], [permission_desc]) 
ON Target.PermissionId = Source.[permission_id]
WHEN MATCHED THEN 
    UPDATE SET [PermissionName] = Source.[permission_name],
               [PermissionDesc] = Source.[permission_desc]
WHEN NOT MATCHED BY TARGET THEN 
    INSERT ([PermissionId], [PermissionName], [PermissionDesc])
    VALUES ([permission_id], [permission_name], [permission_desc]);

SET IDENTITY_INSERT [dbo].[Permission]  OFF

SET IDENTITY_INSERT [dbo].[ActivityEntryType]  ON

MERGE INTO [dbo].[ActivityEntryType]  AS Target
USING (VALUES
    (1, 'Any'),
    (2, 'Manager'),
    (3, 'Administrator')
)
AS Source ([activity_entry_type_id], [entry_type_name]) 
ON Target.ActivityEntryTypeId = Source.[activity_entry_type_id]
WHEN MATCHED THEN 
    UPDATE SET [EntryTypeName] = Source.[entry_type_name]
WHEN NOT MATCHED BY TARGET THEN 
    INSERT ([ActivityEntryTypeId], [EntryTypeName])
    VALUES ([activity_entry_type_id], [entry_type_name]);

SET IDENTITY_INSERT [dbo].[ActivityEntryType]  OFF

SET IDENTITY_INSERT [dbo].[BadgeType]  ON
MERGE INTO [dbo].[BadgeType]  AS Target
USING (VALUES
    (1, 'Corporate', 1),
    (2, 'Community', 0)
)
AS Source ([badge_type_id], [badge_type_name], [payroll_eligible]) 
ON Target.[BadgeTypeId] = Source.[badge_type_id]
WHEN MATCHED THEN 
    UPDATE SET [BadgeTypeName] = Source.[badge_type_name],
               [PayrollEligible] = Source.[payroll_eligible]
WHEN NOT MATCHED BY TARGET THEN 
    INSERT ([BadgeTypeId], [BadgeTypeName], [PayrollEligible])
    VALUES ([badge_type_id], [badge_type_name], [payroll_eligible]);

SET IDENTITY_INSERT [dbo].[BadgeType]  OFF

SET IDENTITY_INSERT [dbo].[ItemStatus]  ON
MERGE INTO [dbo].[ItemStatus]  AS Target
USING (VALUES
    (1, 'Awaiting Approval'),
    (2, 'Approved'),
    (3, 'Denied'),
	(4, 'Complete'),
    (5, 'Error')
)
AS Source ([item_status_id], [item_status_name]) 
ON Target.[ItemStatusId] = Source.[item_status_id]
WHEN MATCHED THEN 
    UPDATE SET [StatusName] = Source.[item_status_name]
WHEN NOT MATCHED BY TARGET THEN 
    INSERT ([ItemStatusId], [StatusName])
    VALUES ([item_status_id], [item_status_name]);

SET IDENTITY_INSERT [dbo].[ItemStatus]  OFF

SET IDENTITY_INSERT [dbo].[QueueEvent]  ON
MERGE INTO [dbo].[QueueEvent]  AS Target
USING (VALUES
    (1, 'Processed', 'The item successfully processed'),
    (2, 'Processing', 'The item is processing'),
    (3, 'Failed', 'The item failed to process')
)
AS Source ([QueueEventId], [QueueEventName], [QueueEventDescription]) 
ON Target.[QueueEventId] = Source.[QueueEventId]
WHEN MATCHED THEN 
    UPDATE SET [QueueEventName] = Source.[QueueEventName],
				[QueueEventDescription] = Source.[QueueEventDescription]
WHEN NOT MATCHED BY TARGET THEN 
    INSERT ([QueueEventId], [QueueEventName], [QueueEventDescription])
    VALUES ([QueueEventId], [QueueEventName], [QueueEventDescription]);

SET IDENTITY_INSERT [dbo].[QueueEvent]  OFF
