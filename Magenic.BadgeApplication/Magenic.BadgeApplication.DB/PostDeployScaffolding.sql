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

SET IDENTITY_INSERT [dbo].[Employee]  ON
MERGE INTO [dbo].[Employee]  AS Target
USING (VALUES
    (1, 'Renee', 'Bourget', 'reneeb', '1/1/2010', 1, 50),
    (2, 'Scott', 'Diehl', 'scottd', '4/1/2010', 2, 100),
    (3, 'Rockford', 'Lhotka', 'rockyl', '1/1/1999', 3, 150),
    (4, 'Kevin', 'Ford', 'kevinf', '4/1/2011', 2, 100)
)
AS Source ([employee_id], [first_name], [last_name], [ad_name], [start_date], [approving_manager], [award_payout_threshold]) 
ON Target.EmployeeId = Source.[employee_id]
WHEN MATCHED THEN 
    UPDATE SET [FirstName] = Source.[first_name],
               [LastName] = Source.[last_name],
               [ADName] = Source.[ad_name],
               [EmploymentStartDate] = Source.[start_date],
               [ApprovingManagerId1] = Source.[approving_manager],
               [AwardPayoutThreshold] = Source.[award_payout_threshold]
WHEN NOT MATCHED BY TARGET THEN 
    INSERT ([EmployeeId], [FirstName], [LastName], [ADName], [EmploymentStartDate], [ApprovingManagerId1], [AwardPayoutThreshold])
    VALUES ([employee_id], [first_name], [last_name], [ad_name], [start_date], [approving_manager], [award_payout_threshold]);

SET IDENTITY_INSERT [dbo].[Employee]  OFF

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

SET IDENTITY_INSERT [dbo].[EmployeePermission]  ON
MERGE INTO [dbo].[EmployeePermission]  AS Target
USING (VALUES
    (1, 1, 1),
    (2, 1, 2),
    (3, 1, 3),
    (4, 2, 1),
    (5, 2, 3),
    (6, 3, 1),
    (7, 3, 2),
    (8, 4, 1)
)
AS Source ([employee_permission_id], [employee_id], [permission_id]) 
ON Target.EmployeePermissionId = Source.[employee_permission_id]
WHEN MATCHED THEN 
    UPDATE SET [EmployeeId] = Source.[employee_id],
               [PermissionId] = Source.[permission_id]
WHEN NOT MATCHED BY TARGET THEN 
    INSERT ([EmployeePermissionId], [EmployeeId], [PermissionId])
    VALUES ([employee_permission_id], [employee_id], [permission_id]);

SET IDENTITY_INSERT [dbo].[EmployeePermission]  OFF

SET IDENTITY_INSERT [dbo].[ActivityEntryType]  ON

MERGE INTO [dbo].[ActivityEntryType]  AS Target
USING (VALUES
    (1, 'Any'),
    (2, 'Manage'),
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

SET IDENTITY_INSERT [dbo].[Activity]  ON
MERGE INTO [dbo].[Activity]  AS Target
USING (VALUES
    (1, 'Business Referral', 'Provided a referral for new business.', 1, 1, 2),
    (2, 'Speaking Engagement', 'Spoke at a user group, webinar, conference, educational event or had work published in a magazine or journal.', 1, 1, 1),
    (3, 'Closed Business Referral', 'A new business referral was closed upon.', 1, 1, 2),
    (4, 'Achieved Spot Recognition', 'Given a spot recognition by your manager', 1, 1, 2),
    (5, 'Passed a Microsoft Certification', 'Achieved one of Microsoft''s certifications.', 0, 1, 1),
    (6, 'Passed a QA Certification', 'Successfully completed a QA assurance certification from ISTQB, QAI or IIST.', 0, 1, 1),
    (7, 'Named a MS MVP', 'Become a named Microsoft MVP.', 1, 1, 1),
    (8, 'Named a Magenic MVP', 'Become a named Magenic MVP.', 1, 1, 2),
    (9, 'Named Consultant of the Quarter', 'Named consultant of the quarter.', 1, 1, 2),
    (10, 'Named Consultant of the Year', 'Named consultant of the year.', 1, 1, 3),
    (11, 'Single Year of Service', 'Achieve another year of service.', 1, 1, 3),
    (12, 'Refer an Employee', 'Referral of an outside candidate that results in full-time employment.', 1, 1, 2),
    (13, 'Conduct a Code Dojo Session', 'Conduct or be the facilitator at a code dojo session.', 0, 1, 1),
    (14, 'Attend a Code Dojo Session', 'Attend a code dojo session.', 0, 1, 1)
)
AS Source ([activity_id], [activity_name], [activity_description], [requires_approval], [create_employee_id], [entry_type_id]) 
ON Target.[ActivityId] = Source.[activity_id]
WHEN MATCHED THEN 
    UPDATE SET [ActivityName] = Source.[activity_name],
               [ActivityDescription] = Source.[activity_description],
               [RequiresApproval] = Source.[requires_approval],
			   [CreateEmployeeId] = Source.[create_employee_id],
			   [EntryTypeId] = Source.[entry_type_id]
WHEN NOT MATCHED BY TARGET THEN 
    INSERT ([ActivityId], [ActivityName], [ActivityDescription], [RequiresApproval], [CreateEmployeeId], [EntryTypeId])
    VALUES ([activity_id], [activity_name], [activity_description], [requires_approval], [create_employee_id], [entry_type_id]);

SET IDENTITY_INSERT [dbo].[Activity]  OFF

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

SET IDENTITY_INSERT [dbo].[Badge]  ON
MERGE INTO [dbo].[Badge]  AS Target
USING (VALUES
    (1, 'AT BAT', 'At bat for Magenic', 'Recognized employees who provide referrals for new business.', 1, '9/1/2013', '10/1/2013', null, 1, 1, 1, 1, 1, 20, 1, '9/1/2013', 2, 1),
    (2, 'SPEAKER', 'And now for the vocal stylings of...', 'Awarded for employees who speak at user groups, webinars, conferences, educational events or has their work published in a magazine or journal.', 1, '9/1/2013', '10/1/2013', null, 2, 1, 1, 1, 1, 50, 1, '9/1/2013', 2, 1),
    (3, 'SILVER SPEAKER', 'You silver tongued devil...', 'Awarded for employees who speak five times at user groups, webinars, conferences, educational events or has their work published in a magazine or journal.', 1, '9/1/2013', '10/1/2013', null, 3, 1, 1, 1, 5, 0, 1, '9/1/2013', 2, 1),
    (4, 'ATTENDED CODE DOJO', 'The zen of code...', 'Awarded for attending Code Dojo sessions.', 2, '1/1/2013', '1/1/2013', null, 4, 0, 1, 0, 1, 0, 3, '1/1/2013', 2, 1)
)
AS Source ([badge_id], [badge_name], [badge_tag_line], [badge_description], [badge_type_id], [badge_created], [badge_effective_start], [badge_effective_end], [badge_priority], [multiple_awards_possible], [display_once], [management_approval_required], [activity_points_amount], [badge_award_value_amount], [badge_approved_by_id], [badge_approved_date], [badge_status_id], [create_employee_id]) 
ON Target.[BadgeId] = Source.[badge_id]
WHEN MATCHED THEN 
    UPDATE SET [BadgeName] = Source.[badge_name],
               [BadgeTagLine] = Source.[badge_tag_line],
               [BadgeDescription] = Source.[badge_description],
               [BadgeTypeId] = Source.[badge_type_id],
               [BadgeCreated] = Source.[badge_created],
               [BadgeEffectiveStart] = Source.[badge_effective_start],
               [BadgeEffectiveEnd] = Source.[badge_effective_end],
               [BadgePriority] = Source.[badge_priority],
               [MultipleAwardPossible] = Source.[multiple_awards_possible],
               [DisplayOnce] = Source.[display_once],
               [ManagementApprovalRequired] = Source.[management_approval_required],
               [ActivityPointsAmount] = Source.[activity_points_amount],
               [BadgeAwardValueAmount] = Source.[badge_award_value_amount],
               [BadgeApprovedById] = Source.[badge_approved_by_id],
               [BadgeApprovedDate] = Source.[badge_approved_date],
			   [BadgeStatusId] = Source.[badge_status_id],
			   [CreateEmployeeId] = Source.[create_employee_id]
WHEN NOT MATCHED BY TARGET THEN 
    INSERT ([BadgeId], [BadgeName], [BadgeTagLine], [BadgeDescription], [BadgeTypeId], [BadgeCreated], [BadgeEffectiveStart], [BadgeEffectiveEnd], [BadgePriority], [MultipleAwardPossible], [DisplayOnce], [ManagementApprovalRequired], [ActivityPointsAmount], [BadgeAwardValueAmount], [BadgeApprovedById], [BadgeApprovedDate], [BadgeStatusId], [CreateEmployeeId])
    VALUES ([badge_id], [badge_name], [badge_tag_line], [badge_description], [badge_type_id], [badge_created], [badge_effective_start], [badge_effective_end], [badge_priority], [multiple_awards_possible], [display_once], [management_approval_required], [activity_points_amount], [badge_award_value_amount], [badge_approved_by_id], [badge_approved_date], [badge_status_id], [create_employee_id]);

SET IDENTITY_INSERT [dbo].[Badge]  OFF

SET IDENTITY_INSERT [dbo].[BadgeActivity]  ON
MERGE INTO [dbo].[BadgeActivity]  AS Target
USING (VALUES
    (1, 1, 1, 20),
    (2, 2, 2, 50),
    (3, 3, 2, 0),
	(4, 4, 14, 0)
)
AS Source ([badge_activity_id], [badge_id], [activity_id], [points_awarded]) 
ON Target.[BadgeActivityId] = Source.[badge_activity_id]
WHEN MATCHED THEN 
    UPDATE SET [BadgeId] = Source.[badge_id],
               [ActivityId] = Source.[activity_id],
               [PointsAwarded] = Source.[points_awarded]
WHEN NOT MATCHED BY TARGET THEN 
    INSERT ([BadgeActivityId], [BadgeId], [ActivityId], [PointsAwarded])
    VALUES ([badge_activity_id], [badge_id], [activity_id], [points_awarded]);

SET IDENTITY_INSERT [dbo].[BadgeActivity]  OFF

SET IDENTITY_INSERT [dbo].[ActivitySubmission]  ON
MERGE INTO [dbo].[ActivitySubmission]  AS Target
USING (VALUES
    (1, 2, 4, 'Spoke at the Boston Code Mastery Event.', 2, '9/26/2013', 2),
    (2, 4, 4, 'Scott thinks I''m great!', 2, '8/8/2013', 3),
    (3, 14, 4, 'Attended the code dojo on 8/5/2013.', null, '8/15/2013', 2),
    (4, 5, 4, 'Became an MCSD', 2, '9/3/2013', 1)
)
AS Source ([activity_submission_id], [activity_id], [employee_id], [submission_description], [submission_approved_by_id], [submission_date], [submission_status_id]) 
ON Target.[ActivitySubmissionId] = Source.[activity_submission_id]
WHEN MATCHED THEN 
    UPDATE SET [ActivityId] = Source.[activity_id],
               [EmployeeId] = Source.[employee_id],
               [SubmissionDescription] = Source.[submission_description],
               [SubmissionApprovedById] = Source.[submission_approved_by_id],
               [SubmissionDate] = Source.[submission_date],
               [SubmissionStatusId] = Source.[submission_status_id]
WHEN NOT MATCHED BY TARGET THEN 
    INSERT ([ActivitySubmissionId], [ActivityId], [EmployeeId], [SubmissionDescription], [SubmissionApprovedById], [SubmissionDate], [SubmissionStatusId])
    VALUES ([activity_submission_id], [activity_id], [employee_id], [submission_description], [submission_approved_by_id], [submission_date], [submission_status_id]);

SET IDENTITY_INSERT [dbo].[ActivitySubmission]  OFF

SET IDENTITY_INSERT [dbo].[BadgeAward]  ON
MERGE INTO [dbo].[BadgeAward]  AS Target
USING (VALUES
    (1, 2, 4, '9/26/2013', 50, 0, null, null, null),
    (2, 4, 4, '8/8/2013', 0, 0, null, null, null)
)
AS Source ([badge_award_id], [badge_id], [employee_id], [award_date], [award_amount], [paid_out], [paid_date], [paid_completed_by_id], [Published]) 
ON Target.[BadgeAwardId] = Source.[badge_award_id]
WHEN MATCHED THEN 
    UPDATE SET [BadgeId] = Source.[badge_id],
               [EmployeeId] = Source.[employee_id],
               [AwardDate] = Source.[award_date],
               [AwardAmount] = Source.[award_amount],
               [PaidOut] = Source.[paid_out],
               [PaidDate] = Source.[paid_date],
               [PaidCompletedById] = Source.[paid_completed_by_id],
               [Published] = Source.[Published]
WHEN NOT MATCHED BY TARGET THEN 
    INSERT ([BadgeAwardId], [BadgeId], [EmployeeId], [AwardDate], [AwardAmount], [PaidOut], [PaidDate], [PaidCompletedByID], [Published])
    VALUES ([badge_award_id], [badge_id], [employee_id], [award_date], [award_amount], [paid_out], [paid_date], [paid_completed_by_id], [Published]);

SET IDENTITY_INSERT [dbo].[BadgeAward]  OFF

SET IDENTITY_INSERT [dbo].[BadgePrerequisite]  ON

MERGE INTO [dbo].[BadgePrerequisite]  AS Target
USING (VALUES
    (1, 3, 2)
)
AS Source ([prerequisite_id],[badge_id],[required_badge_id]) 
ON Target.[PrerequisiteID] = Source.[prerequisite_id]
WHEN MATCHED THEN 
    UPDATE SET [BadgeId] = Source.[badge_id],
               [RequiredBadgeId] = Source.[required_badge_id]
WHEN NOT MATCHED BY TARGET THEN 
    INSERT ([PrerequisiteID], [BadgeId], [RequiredBadgeId])
    VALUES ([prerequisite_id], [badge_id], [required_badge_id]);

SET IDENTITY_INSERT [dbo].[BadgePrerequisite]  OFF

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
