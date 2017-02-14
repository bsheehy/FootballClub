-- ==============================================================================================
--				File automatically created by SqlTools DoStaticInserts method.
--				Command Line used: "C:\development\SQLTools\SqlTools\bin\Debug\SqlTools.vshost.exe" -m "staticInserts" -c "Data Source=.\SQL2012;Initial Catalog=ClubFrRocks;User Id=sa;Password=Mallon2012;" -d "ClubFrRocks" -t "club_membership_type;club_person_title;club_club_details" -o "C:\Temp\Ouput_SQL.sql"
-- ==============================================================================================
BEGIN TRANSACTION;
BEGIN TRY
-- ==============================================================================================
--					 [club_membership_type] Static Data 
-- ==============================================================================================

IF NOT EXISTS(SELECT * FROM [club_membership_type] WHERE [Oid] = N'1ba9d177-34f1-4dea-b4ac-07d91717a0f5')
BEGIN
	INSERT [dbo].[club_membership_type] ([oid], [orev], [time_type], [sex], [key], [active], [name], [year], [start_date], [end_date], [cost]) VALUES (N'1ba9d177-34f1-4dea-b4ac-07d91717a0f5', 0, N'Y', N'M', 0, 1, N'2017 Child', 2017, NULL, NULL, 10)
END

IF NOT EXISTS(SELECT * FROM [club_membership_type] WHERE [Oid] = N'438149e1-21c3-4f03-8be7-49dafdd22512')
BEGIN
	INSERT [dbo].[club_membership_type] ([oid], [orev], [time_type], [sex], [key], [active], [name], [year], [start_date], [end_date], [cost]) VALUES (N'438149e1-21c3-4f03-8be7-49dafdd22512', 0, N'Y', N'M', 0, 1, N'2017 Student', 2017, NULL, NULL, 15)
END

IF NOT EXISTS(SELECT * FROM [club_membership_type] WHERE [Oid] = N'6e1620df-ec7e-413b-b960-e4b41d221d58')
BEGIN
	INSERT [dbo].[club_membership_type] ([oid], [orev], [time_type], [sex], [key], [active], [name], [year], [start_date], [end_date], [cost]) VALUES (N'6e1620df-ec7e-413b-b960-e4b41d221d58', 0, N'Y', N'M', 0, 1, N'2017 Adult', 2017, NULL, NULL, 35)
END


-- ==============================================================================================
--					 [club_person_title] Static Data 
-- ==============================================================================================

IF NOT EXISTS(SELECT * FROM [club_person_title] WHERE [Oid] = N'ee38c53a-8a27-4684-bb9e-792d63bc41d0')
BEGIN
	INSERT [dbo].[club_person_title] ([oid], [orev], [description], [active]) VALUES (N'ee38c53a-8a27-4684-bb9e-792d63bc41d0', 1, N'Mr', 1)
END

IF NOT EXISTS(SELECT * FROM [club_person_title] WHERE [Oid] = N'ee38c53a-8a27-4684-bb9e-792d63bc41d1')
BEGIN
	INSERT [dbo].[club_person_title] ([oid], [orev], [description], [active]) VALUES (N'ee38c53a-8a27-4684-bb9e-792d63bc41d1', 1, N'Mrs', 1)
END

IF NOT EXISTS(SELECT * FROM [club_person_title] WHERE [Oid] = N'ee38c53a-8a27-4684-bb9e-792d63bc41d2')
BEGIN
	INSERT [dbo].[club_person_title] ([oid], [orev], [description], [active]) VALUES (N'ee38c53a-8a27-4684-bb9e-792d63bc41d2', 1, N'Master', 1)
END

IF NOT EXISTS(SELECT * FROM [club_person_title] WHERE [Oid] = N'ee38c53a-8a27-4684-bb9e-792d63bc41d3')
BEGIN
	INSERT [dbo].[club_person_title] ([oid], [orev], [description], [active]) VALUES (N'ee38c53a-8a27-4684-bb9e-792d63bc41d3', 1, N'Doctor', 1)
END

IF NOT EXISTS(SELECT * FROM [club_person_title] WHERE [Oid] = N'ee38c53a-8a27-4684-bb9e-792d63bc41d4')
BEGIN
	INSERT [dbo].[club_person_title] ([oid], [orev], [description], [active]) VALUES (N'ee38c53a-8a27-4684-bb9e-792d63bc41d4', 1, N'Miss', 1)
END


-- ==============================================================================================
--					 [club_club_details] Static Data 
-- ==============================================================================================

IF NOT EXISTS(SELECT * FROM [club_club_details] WHERE [Oid] = N'15ac9f29-85ab-4ece-a5fb-dad23d8d946e')
BEGIN
	INSERT [dbo].[club_club_details] ([oid], [orev], [name], [description], [address_number], [address_street], [address_town], [address_county], [address_country], [address_post_code], [address_xlng_coord], [address_ylat_coord]) VALUES (N'15ac9f29-85ab-4ece-a5fb-dad23d8d946e', 0, N'Fr Rocks', NULL, N'1', N'Convent Rd', N'Cookstown', N'Tyrone', NULL, N'BT80 8QA', NULL, NULL)
END


-- ==============================================================================================
--					 [documents_type_path_configuration] Static Data 
-- ==============================================================================================

IF NOT EXISTS(SELECT * FROM [documents_type_path_configuration] WHERE [Oid] = N'3c418db0-dd89-4a79-af0e-697b890c8e7a')
BEGIN
	INSERT [dbo].[documents_type_path_configuration] ([oid], [type], [orev], [applies_to], [path_config]) VALUES (N'3c418db0-dd89-4a79-af0e-697b890c8e7a', N'T', 0, N'Club.Domain.Artifacts.ClubDetails', N'\ClubDetails\')
END

IF NOT EXISTS(SELECT * FROM [documents_type_path_configuration] WHERE [Oid] = N'e16c9a6e-7a20-4701-9ccf-21a3e2b113ee')
BEGIN
	INSERT [dbo].[documents_type_path_configuration] ([oid], [type], [orev], [applies_to], [path_config]) VALUES (N'e16c9a6e-7a20-4701-9ccf-21a3e2b113ee', N'T', 0, N'Club.Domain.Artifacts.Person', N'\Person\[[Oid]]')
END

IF NOT EXISTS(SELECT * FROM [documents_type_path_configuration] WHERE [Oid] = N'068e3ae0-b17a-443e-9620-8557c1115a16')
BEGIN
	INSERT [dbo].[documents_type_path_configuration] ([oid], [type], [orev], [applies_to], [path_config]) VALUES (N'068e3ae0-b17a-443e-9620-8557c1115a16', N'T', 0, N'Club.Domain.Artifacts.Qualification', N'\Qualification\[[Oid]]')
END

IF NOT EXISTS(SELECT * FROM [documents_type_path_configuration] WHERE [Oid] = N'7af97f7c-3166-4cdd-8e7f-bc98fe307822')
BEGIN
	INSERT [dbo].[documents_type_path_configuration] ([oid], [type], [orev], [applies_to], [path_config]) VALUES (N'7af97f7c-3166-4cdd-8e7f-bc98fe307822', N'T', 0, N'Club.Domain.Artifacts.Team', N'\Team\[[Oid]]')
END


-- ==============================================================================================
--					 [club_qualification] Static Data 
-- ==============================================================================================

IF NOT EXISTS(SELECT * FROM [club_qualification] WHERE [Oid] = N'c675ff1f-b8bf-4eda-9a35-2ae313f04a3b')
BEGIN
	INSERT [dbo].[club_qualification] ([oid], [orev], [name], [description], [cost]) VALUES (N'c675ff1f-b8bf-4eda-9a35-2ae313f04a3b', 0, N'GAA Coaching Foundation Award', N'The Foundation Award is the introductory award for coaches of Gaelic Games. The course is aimed at beginner coaches and will enable participants to assist a coach in the organisation of activities to develop hurling or football. The course is seven and a half hours in duration and covers four key modules, as well as introductory and conclusion modules.', NULL)
END


IF NOT EXISTS(SELECT * FROM [club_qualification] WHERE [Oid] = N'73e4d8f7-32c1-4a93-99e1-b8e1533b7f38')
BEGIN
	INSERT [dbo].[club_qualification] ([oid], [orev], [name], [description], [cost]) VALUES (N'73e4d8f7-32c1-4a93-99e1-b8e1533b7f38', 0, N'GAA Coaching Award 2', N'The Coach Education Programme focuses on continuing education, so that coaches can improve by means of a series of specifically designed courses, workshops and conferences incorporating internationally recognised principles of best practice. These opportunities will include a combination of theoretical and practical inputs and allow for the use of digital and e-learning techniques. The programme of Applied Lifelong Learning makes provision for coaches to continually develop their skills and to progress at a rate suited to their own development.', NULL)
END


IF NOT EXISTS(SELECT * FROM [club_qualification] WHERE [Oid] = N'e870774d-9e32-4a00-9a80-0b3329399f98')
BEGIN
	INSERT [dbo].[club_qualification] ([oid], [orev], [name], [description], [cost]) VALUES (N'e870774d-9e32-4a00-9a80-0b3329399f98', 0, N'GAA Coaching Award 1', N'The Award 1 Coach Education course is the second award on the coaching pathway of Gaelic Games. The course is aimed at coaches that have progressed through the Foundation Award and have experience as a coach. The Award 1 course has been designed to take account of the different playing capacities that exist between children (up to 12 years), youths (age 13 – 18) and Adults (age 19+) and the competencies that a coach is required to display when working with each of the playing populations. Therefore, coaches can specialise in coaching the particular players that they are involved with.', NULL)
END



-- ==============================================================================================
--					 [club_team_member_type] Static Data 
-- ==============================================================================================

IF NOT EXISTS(SELECT * FROM [club_team_member_type] WHERE [Oid] = N'2151c74b-be85-4152-9b43-c31fc7ae8730')
BEGIN
	INSERT [dbo].[club_team_member_type] ([oid], [orev], [active], [admin_member], [playing_member], [default], [name], [description]) VALUES (N'2151c74b-be85-4152-9b43-c31fc7ae8730', 0, 1, 0, 1, 1, N'Player', NULL)
END

IF NOT EXISTS(SELECT * FROM [club_team_member_type] WHERE [Oid] = N'2151c74b-be85-4152-9b43-c31fc7ae8731')
BEGIN
	INSERT [dbo].[club_team_member_type] ([oid], [orev], [active], [admin_member], [playing_member], [default], [name], [description]) VALUES (N'2151c74b-be85-4152-9b43-c31fc7ae8731', 0, 1, 1, 0, 0, N'Manager', NULL)
END

IF NOT EXISTS(SELECT * FROM [club_team_member_type] WHERE [Oid] = N'2151c74b-be85-4152-9b43-c31fc7ae8732')
BEGIN
	INSERT [dbo].[club_team_member_type] ([oid], [orev], [active], [admin_member], [playing_member], [default], [name], [description]) VALUES (N'2151c74b-be85-4152-9b43-c31fc7ae8732', 0, 1, 1, 0, 0, N'Coach', NULL)
END

IF NOT EXISTS(SELECT * FROM [club_team_member_type] WHERE [Oid] = N'2151c74b-be85-4152-9b43-c31fc7ae8733')
BEGIN
	INSERT [dbo].[club_team_member_type] ([oid], [orev], [active], [admin_member], [playing_member], [default], [name], [description]) VALUES (N'2151c74b-be85-4152-9b43-c31fc7ae8733', 0, 1, 1, 0, 0, N'Physio', NULL)
END

IF NOT EXISTS(SELECT * FROM [club_team_member_type] WHERE [Oid] = N'2151c74b-be85-4152-9b43-c31fc7ae8734')
BEGIN
	INSERT [dbo].[club_team_member_type] ([oid], [orev], [active], [admin_member], [playing_member], [default], [name], [description]) VALUES (N'2151c74b-be85-4152-9b43-c31fc7ae8734', 0, 1, 1, 0, 0, N'Back Room Staff', NULL)
END


-- ==============================================================================================
--					 [core_role] Static Data 
-- ==============================================================================================

IF NOT EXISTS(SELECT * FROM [core_role] WHERE [Oid] = N'67b00f36-f679-4f3d-82dd-5abd7fac03e1')
BEGIN
	INSERT [dbo].[core_role] ([oid], [orev], [name], [active], [description], [default_access]) VALUES (N'67b00f36-f679-4f3d-82dd-5abd7fac03e1', 1, N'Committe Role', 1, N'Committe User Access', 15)
END

IF NOT EXISTS(SELECT * FROM [core_role] WHERE [Oid] = N'67b00f36-f679-4f3d-82dd-5abd7fac03e2')
BEGIN
	INSERT [dbo].[core_role] ([oid], [orev], [name], [active], [description], [default_access]) VALUES (N'67b00f36-f679-4f3d-82dd-5abd7fac03e2', 1, N'Club Role', 1, N'Club Role Access', 15)
END

IF NOT EXISTS(SELECT * FROM [core_role] WHERE [Oid] = N'058ca795-d577-43aa-958c-769295280d58')
BEGIN
	INSERT [dbo].[core_role] ([oid], [orev], [name], [active], [description], [default_access]) VALUES (N'058ca795-d577-43aa-958c-769295280d58', 1, N'Super Administrator', 1, N'Super Administrator Access', 15)
END

IF NOT EXISTS(SELECT * FROM [core_role] WHERE [Oid] = N'058ca795-d577-43aa-958c-769295280d59')
BEGIN
	INSERT [dbo].[core_role] ([oid], [orev], [name], [active], [description], [default_access]) VALUES (N'058ca795-d577-43aa-958c-769295280d59', 1, N'Administrator', 1, N'Administrator Access', 15)
END




END TRY
BEGIN CATCH
	IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
	SELECT ERROR_NUMBER() AS ErrorNumber,ERROR_SEVERITY() AS ErrorSeverity,ERROR_STATE() AS ErrorState,ERROR_PROCEDURE() AS ErrorProcedure,ERROR_LINE() AS ErrorLine,ERROR_MESSAGE() AS ErrorMessage;
	DECLARE @ERR NVARCHAR(2000)
	SET @ERR = 'Error Number  :' + CAST(ERROR_NUMBER() AS VARCHAR) + ' Error Severity:' + CAST(ERROR_SEVERITY() AS VARCHAR)+ ' Error State   :' + CAST(ERROR_STATE() AS VARCHAR) + ' Error Line    :' + CAST(ERROR_LINE() AS VARCHAR) + ' Error Message :' + ERROR_MESSAGE();
	RAISERROR(@ERR, 20, -1) with log;
END CATCH;
IF @@TRANCOUNT > 0
	COMMIT TRANSACTION;
GO
