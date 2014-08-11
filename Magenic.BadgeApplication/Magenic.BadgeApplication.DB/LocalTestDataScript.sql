USE [Magenic.BadgeApplication.DB]
GO

SET IDENTITY_INSERT [dbo].[Employee] ON 
GO
INSERT [dbo].[Employee] ([EmployeeId], [FirstName], [LastName], [Location], [Department], [ADName], [EmploymentStartDate], [EmploymentEndDate], [ApprovingManagerId1], [ApprovingManagerId2], [AwardPayoutThreshold]) values (1, 'Admin', 'Admin', 'Minneapolis', 'Department', 'Admin', '1/1/2013', null, null, null, 150)
GO
INSERT [dbo].[Employee] ([EmployeeId], [FirstName], [LastName], [Location], [Department], [ADName], [EmploymentStartDate], [EmploymentEndDate], [ApprovingManagerId1], [ApprovingManagerId2], [AwardPayoutThreshold]) values (3, 'Manager', 'Manager', 'Minneapolis', 'Department', 'Manager', '1/1/2013', null, null, null, 150)
GO
INSERT [dbo].[Employee] ([EmployeeId], [FirstName], [LastName], [Location], [Department], [ADName], [EmploymentStartDate], [EmploymentEndDate], [ApprovingManagerId1], [ApprovingManagerId2], [AwardPayoutThreshold]) values (2, 'User', 'User', 'Minneapolis', 'Department', 'User', '1/1/2013', null, 3, null, 150)
GO
SET IDENTITY_INSERT [dbo].[Employee] OFF
GO
DECLARE @EmployeePermission TABLE (
    [EmployeeId]           INT NOT NULL,
    [PermissionId]         INT NOT NULL
);
INSERT INTO @EmployeePermission
SELECT 1, 2 UNION ALL
SELECT 2, 1 UNION ALL
SELECT 3, 3
;
MERGE
	INTO [dbo].[EmployeePermission] AS T
	USING @EmployeePermission AS S
	ON T.[EmployeeId] = S.[EmployeeId]
	WHEN MATCHED THEN
		UPDATE
			SET [PermissionId] = S.[PermissionId]
	WHEN NOT MATCHED THEN
		INSERT ([EmployeeId], [PermissionId])
		VALUES (S.[EmployeeId], S.[PermissionId])
;
SET IDENTITY_INSERT [dbo].[Activity] ON 
GO
INSERT [dbo].[Activity] ([ActivityId], [ActivityName], [ActivityDescription], [RequiresApproval], [CreateEmployeeId], [EntryTypeId]) VALUES (29, N'Referred Business', N'Employee referred business.', 1, 1, 2)
GO
INSERT [dbo].[Activity] ([ActivityId], [ActivityName], [ActivityDescription], [RequiresApproval], [CreateEmployeeId], [EntryTypeId]) VALUES (30, N'Closed Deal', N'A deal for business the employee referred was closed.', 0, 1, 2)
GO
INSERT [dbo].[Activity] ([ActivityId], [ActivityName], [ActivityDescription], [RequiresApproval], [CreateEmployeeId], [EntryTypeId]) VALUES (31, N'Acted as a mentor', N'Participated in the mentor program as a mentor.', 1, 1, 1)
GO
INSERT [dbo].[Activity] ([ActivityId], [ActivityName], [ActivityDescription], [RequiresApproval], [CreateEmployeeId], [EntryTypeId]) VALUES (32, N'Acted as a mentee', N'Participated in the mentor program as a mentee.', 1, 1, 1)
GO
INSERT [dbo].[Activity] ([ActivityId], [ActivityName], [ActivityDescription], [RequiresApproval], [CreateEmployeeId], [EntryTypeId]) VALUES (33, N'Consultant of the quarter', N'Named consultant of the quarter.', 0, 1, 2)
GO
INSERT [dbo].[Activity] ([ActivityId], [ActivityName], [ActivityDescription], [RequiresApproval], [CreateEmployeeId], [EntryTypeId]) VALUES (34, N'Consultant of the year', N'Named consultant of the year', 0, 1, 2)
GO
INSERT [dbo].[Activity] ([ActivityId], [ActivityName], [ActivityDescription], [RequiresApproval], [CreateEmployeeId], [EntryTypeId]) VALUES (35, N'Referred Employee', N'Employee referred another employee who was hired.', 1, 1, 1)
GO
INSERT [dbo].[Activity] ([ActivityId], [ActivityName], [ActivityDescription], [RequiresApproval], [CreateEmployeeId], [EntryTypeId]) VALUES (36, N'Magenic MVP', N'Named a Magenic MVP', 0, 1, 3)
GO
INSERT [dbo].[Activity] ([ActivityId], [ActivityName], [ActivityDescription], [RequiresApproval], [CreateEmployeeId], [EntryTypeId]) VALUES (37, N'Microsoft MVP', N'Named a Microsoft MVP.', 1, 1, 1)
GO
INSERT [dbo].[Activity] ([ActivityId], [ActivityName], [ActivityDescription], [RequiresApproval], [CreateEmployeeId], [EntryTypeId]) VALUES (38, N'QA Certification', N'Earned a QA certification.', 1, 1, 1)
GO
INSERT [dbo].[Activity] ([ActivityId], [ActivityName], [ActivityDescription], [RequiresApproval], [CreateEmployeeId], [EntryTypeId]) VALUES (39, N'Spoke at an event', N'Spoke at an event in the technology space.', 1, 1, 1)
GO
INSERT [dbo].[Activity] ([ActivityId], [ActivityName], [ActivityDescription], [RequiresApproval], [CreateEmployeeId], [EntryTypeId]) VALUES (40, N'Kudos', N'When a manager wants to financially reward an employee.', 0, 1, 2)
GO
INSERT [dbo].[Activity] ([ActivityId], [ActivityName], [ActivityDescription], [RequiresApproval], [CreateEmployeeId], [EntryTypeId]) VALUES (41, N'One year of service ', N'Given each time an employee has a single year of service.', 0, 1, 3)
GO
INSERT [dbo].[Activity] ([ActivityId], [ActivityName], [ActivityDescription], [RequiresApproval], [CreateEmployeeId], [EntryTypeId]) VALUES (45, N'Attended Bacon Club Meeting', N'Bacon is great!', 0, 1, 1)
GO
SET IDENTITY_INSERT [dbo].[Activity] OFF
GO

SET IDENTITY_INSERT [dbo].[Badge] ON 
GO
INSERT [dbo].[Badge] ([BadgeId], [BadgeName], [BadgeTagLine], [BadgeDescription], [BadgeTypeId], [BadgePath], [BadgeCreated], [BadgeEffectiveStart], [BadgeEffectiveEnd], [BadgePriority], [MultipleAwardPossible], [DisplayOnce], [ManagementApprovalRequired], [ActivityPointsAmount], [BadgeAwardValueAmount], [BadgeApprovedById], [BadgeApprovedDate], [BadgeStatusId], [CreateEmployeeId]) VALUES (36, N'At Bat', N'At Bat for Magenic', N'Recognized employees who provide referrals for new business.', 1, N'https://magenicbadgeappprod.blob.core.windows.net/badgeimagesqa/badgeimage36', CAST(0x07B1BC1F668355380B AS DateTime2), NULL, NULL, 2147483647, 1, 1, 0, 1, 10, NULL, NULL, 2, 1)
GO
INSERT [dbo].[Badge] ([BadgeId], [BadgeName], [BadgeTagLine], [BadgeDescription], [BadgeTypeId], [BadgePath], [BadgeCreated], [BadgeEffectiveStart], [BadgeEffectiveEnd], [BadgePriority], [MultipleAwardPossible], [DisplayOnce], [ManagementApprovalRequired], [ActivityPointsAmount], [BadgeAwardValueAmount], [BadgeApprovedById], [BadgeApprovedDate], [BadgeStatusId], [CreateEmployeeId]) VALUES (37, N'Close Business', N'Closing for Magenic', N'Given to employees whose referral for new business through the At Bat award turns into an engagement.', 1, N'https://magenicbadgeappprod.blob.core.windows.net/badgeimagesqa/badgeimage37', CAST(0x07F9A9C2988355380B AS DateTime2), NULL, NULL, 2147483647, 1, 1, 0, 1, 500, NULL, NULL, 2, 1)
GO
INSERT [dbo].[Badge] ([BadgeId], [BadgeName], [BadgeTagLine], [BadgeDescription], [BadgeTypeId], [BadgePath], [BadgeCreated], [BadgeEffectiveStart], [BadgeEffectiveEnd], [BadgePriority], [MultipleAwardPossible], [DisplayOnce], [ManagementApprovalRequired], [ActivityPointsAmount], [BadgeAwardValueAmount], [BadgeApprovedById], [BadgeApprovedDate], [BadgeStatusId], [CreateEmployeeId]) VALUES (38, N'Consultant of the Quarter', N'Way to go', N'On a quarterly basis, each GM will identify and select one individual from their team that has consistently gone above and beyond expectations.', 1, N'https://magenicbadgeappprod.blob.core.windows.net/badgeimagesqa/badgeimage38', CAST(0x07FFF33AB38455380B AS DateTime2), NULL, NULL, 2, 1, 0, 0, 1, 500, NULL, NULL, 2, 1)
GO
INSERT [dbo].[Badge] ([BadgeId], [BadgeName], [BadgeTagLine], [BadgeDescription], [BadgeTypeId], [BadgePath], [BadgeCreated], [BadgeEffectiveStart], [BadgeEffectiveEnd], [BadgePriority], [MultipleAwardPossible], [DisplayOnce], [ManagementApprovalRequired], [ActivityPointsAmount], [BadgeAwardValueAmount], [BadgeApprovedById], [BadgeApprovedDate], [BadgeStatusId], [CreateEmployeeId]) VALUES (39, N'Consultant of the Year', N'Keep on doing that thing', N'Each GM will identify and select one individual from their team to be considered for the award. One overall winner will be selected', 1, N'https://magenicbadgeappprod.blob.core.windows.net/badgeimagesqa/badgeimage39', CAST(0x074A9082B48655380B AS DateTime2), NULL, NULL, 1, 1, 0, 0, 1, 2000, NULL, NULL, 2, 1)
GO
INSERT [dbo].[Badge] ([BadgeId], [BadgeName], [BadgeTagLine], [BadgeDescription], [BadgeTypeId], [BadgePath], [BadgeCreated], [BadgeEffectiveStart], [BadgeEffectiveEnd], [BadgePriority], [MultipleAwardPossible], [DisplayOnce], [ManagementApprovalRequired], [ActivityPointsAmount], [BadgeAwardValueAmount], [BadgeApprovedById], [BadgeApprovedDate], [BadgeStatusId], [CreateEmployeeId]) VALUES (40, N'Mentorship Program - Mentor', N'Passing it on', N'Given to employees who participate in Magenic''s Mentorship Program as a mentor.', 1, N'https://magenicbadgeappprod.blob.core.windows.net/badgeimagesqa/badgeimage40', CAST(0x07BDB068F98655380B AS DateTime2), NULL, NULL, 2147483647, 1, 1, 0, 1, 0, NULL, NULL, 2, 1)
GO
INSERT [dbo].[Badge] ([BadgeId], [BadgeName], [BadgeTagLine], [BadgeDescription], [BadgeTypeId], [BadgePath], [BadgeCreated], [BadgeEffectiveStart], [BadgeEffectiveEnd], [BadgePriority], [MultipleAwardPossible], [DisplayOnce], [ManagementApprovalRequired], [ActivityPointsAmount], [BadgeAwardValueAmount], [BadgeApprovedById], [BadgeApprovedDate], [BadgeStatusId], [CreateEmployeeId]) VALUES (41, N'Mentorship Program - Mentee', N'It''s good to grow', N'Given to employees who participate in Magenic''s Mentorship Program as a mentee.', 1, N'https://magenicbadgeappprod.blob.core.windows.net/badgeimagesqa/badgeimage41', CAST(0x07398F9D268755380B AS DateTime2), NULL, NULL, 2147483647, 1, 1, 0, 1, 0, NULL, NULL, 2, 1)
GO
INSERT [dbo].[Badge] ([BadgeId], [BadgeName], [BadgeTagLine], [BadgeDescription], [BadgeTypeId], [BadgePath], [BadgeCreated], [BadgeEffectiveStart], [BadgeEffectiveEnd], [BadgePriority], [MultipleAwardPossible], [DisplayOnce], [ManagementApprovalRequired], [ActivityPointsAmount], [BadgeAwardValueAmount], [BadgeApprovedById], [BadgeApprovedDate], [BadgeStatusId], [CreateEmployeeId]) VALUES (42, N'One Year of Service', N'Getting Started', N'Magenic recognizes employees at milestone service anniversaries.', 1, N'https://magenicbadgeappprod.blob.core.windows.net/badgeimagesqa/badgeimage42', CAST(0x07873F4E668A55380B AS DateTime2), NULL, NULL, 15, 0, 0, 0, 1, 150, NULL, NULL, 2, 1)
GO
INSERT [dbo].[Badge] ([BadgeId], [BadgeName], [BadgeTagLine], [BadgeDescription], [BadgeTypeId], [BadgePath], [BadgeCreated], [BadgeEffectiveStart], [BadgeEffectiveEnd], [BadgePriority], [MultipleAwardPossible], [DisplayOnce], [ManagementApprovalRequired], [ActivityPointsAmount], [BadgeAwardValueAmount], [BadgeApprovedById], [BadgeApprovedDate], [BadgeStatusId], [CreateEmployeeId]) VALUES (43, N'Three Years of Service', N'Getting to know you', N'Magenic recognizes employees at milestone service anniversaries.', 1, N'https://magenicbadgeappprod.blob.core.windows.net/badgeimagesqa/badgeimage43', CAST(0x079979B0938A55380B AS DateTime2), NULL, NULL, 14, 0, 0, 0, 3, 250, NULL, NULL, 2, 1)
GO
INSERT [dbo].[Badge] ([BadgeId], [BadgeName], [BadgeTagLine], [BadgeDescription], [BadgeTypeId], [BadgePath], [BadgeCreated], [BadgeEffectiveStart], [BadgeEffectiveEnd], [BadgePriority], [MultipleAwardPossible], [DisplayOnce], [ManagementApprovalRequired], [ActivityPointsAmount], [BadgeAwardValueAmount], [BadgeApprovedById], [BadgeApprovedDate], [BadgeStatusId], [CreateEmployeeId]) VALUES (47, N'Bacon Club Attendee', N'You like bacon', N'Went to the Bacon club', 2, N'https://magenicbadgeappprod.blob.core.windows.net/badgeimages/badgeimage47', CAST(0x07308357AFBB80380B AS DateTime2), NULL, NULL, 2147483647, 0, 0, 0, 1, 0, NULL, NULL, 1, 1)
GO
INSERT [dbo].[Badge] ([BadgeId], [BadgeName], [BadgeTagLine], [BadgeDescription], [BadgeTypeId], [BadgePath], [BadgeCreated], [BadgeEffectiveStart], [BadgeEffectiveEnd], [BadgePriority], [MultipleAwardPossible], [DisplayOnce], [ManagementApprovalRequired], [ActivityPointsAmount], [BadgeAwardValueAmount], [BadgeApprovedById], [BadgeApprovedDate], [BadgeStatusId], [CreateEmployeeId]) VALUES (48, N'Bacon Club President', N'You like bacon', N'Bacon bacon bacon', 2, N'https://magenicbadgeappprod.blob.core.windows.net/badgeimages/badgeimage48', CAST(0x0764186C63BC80380B AS DateTime2), NULL, NULL, 2147483647, 0, 0, 0, 1, 0, NULL, NULL, 1, 1)
GO
INSERT [dbo].[Badge] ([BadgeId], [BadgeName], [BadgeTagLine], [BadgeDescription], [BadgeTypeId], [BadgePath], [BadgeCreated], [BadgeEffectiveStart], [BadgeEffectiveEnd], [BadgePriority], [MultipleAwardPossible], [DisplayOnce], [ManagementApprovalRequired], [ActivityPointsAmount], [BadgeAwardValueAmount], [BadgeApprovedById], [BadgeApprovedDate], [BadgeStatusId], [CreateEmployeeId]) VALUES (49, N'Bacon Eaters Anonymous', N'Bacon', N'Bacon bacon bacon', 2, N'https://magenicbadgeappprod.blob.core.windows.net/badgeimages/badgeimage49', CAST(0x072D3DB2EABC80380B AS DateTime2), CAST(0x07000000000079380B AS DateTime2), CAST(0x07000000000097380B AS DateTime2), 1, 0, 0, 0, 1, 0, NULL, NULL, 3, 1)
GO
INSERT [dbo].[Badge] ([BadgeId], [BadgeName], [BadgeTagLine], [BadgeDescription], [BadgeTypeId], [BadgePath], [BadgeCreated], [BadgeEffectiveStart], [BadgeEffectiveEnd], [BadgePriority], [MultipleAwardPossible], [DisplayOnce], [ManagementApprovalRequired], [ActivityPointsAmount], [BadgeAwardValueAmount], [BadgeApprovedById], [BadgeApprovedDate], [BadgeStatusId], [CreateEmployeeId]) VALUES (50, N'Funny Badge', N'Test', N'Test', 2, N'https://magenicbadgeappprod.blob.core.windows.net/badgeimages/badgeimage50', CAST(0x079677287ABD80380B AS DateTime2), CAST(0x07000000000080380B AS DateTime2), NULL, 2147483647, 0, 1, 0, 1, 0, NULL, NULL, 3, 1)
GO
INSERT [dbo].[Badge] ([BadgeId], [BadgeName], [BadgeTagLine], [BadgeDescription], [BadgeTypeId], [BadgePath], [BadgeCreated], [BadgeEffectiveStart], [BadgeEffectiveEnd], [BadgePriority], [MultipleAwardPossible], [DisplayOnce], [ManagementApprovalRequired], [ActivityPointsAmount], [BadgeAwardValueAmount], [BadgeApprovedById], [BadgeApprovedDate], [BadgeStatusId], [CreateEmployeeId]) VALUES (51, N'Funny Badge', N'Test', N'Test', 2, N'https://magenicbadgeappprod.blob.core.windows.net/badgeimages/badgeimage51', CAST(0x071626F986BD80380B AS DateTime2), NULL, NULL, 2147483647, 0, 1, 0, 1, 0, NULL, NULL, 3, 1)
GO
INSERT [dbo].[Badge] ([BadgeId], [BadgeName], [BadgeTagLine], [BadgeDescription], [BadgeTypeId], [BadgePath], [BadgeCreated], [BadgeEffectiveStart], [BadgeEffectiveEnd], [BadgePriority], [MultipleAwardPossible], [DisplayOnce], [ManagementApprovalRequired], [ActivityPointsAmount], [BadgeAwardValueAmount], [BadgeApprovedById], [BadgeApprovedDate], [BadgeStatusId], [CreateEmployeeId]) VALUES (52, N'Speaker', N'', N'Test Speaker Badge', 1, N'https://magenicbadgeappprod.blob.core.windows.net/badgeimagesqa/badgeimage52', CAST(0x079C7F8130668E380B AS DateTime2), NULL, NULL, 5, 1, 1, 0, 1, 50, NULL, NULL, 2, 1)
GO
INSERT [dbo].[Badge] ([BadgeId], [BadgeName], [BadgeTagLine], [BadgeDescription], [BadgeTypeId], [BadgePath], [BadgeCreated], [BadgeEffectiveStart], [BadgeEffectiveEnd], [BadgePriority], [MultipleAwardPossible], [DisplayOnce], [ManagementApprovalRequired], [ActivityPointsAmount], [BadgeAwardValueAmount], [BadgeApprovedById], [BadgeApprovedDate], [BadgeStatusId], [CreateEmployeeId]) VALUES (53, N'Speaker 3', N'More Speaking', N'', 1, N'https://magenicbadgeappprod.blob.core.windows.net/badgeimagesqa/badgeimage53', CAST(0x072ACBE330678E380B AS DateTime2), NULL, NULL, 6, 0, 0, 0, 3, 0, NULL, NULL, 2, 1)
GO
SET IDENTITY_INSERT [dbo].[Badge] OFF
GO
