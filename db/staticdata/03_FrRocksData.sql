-- ==============================================================================================
--				File automatically created by SqlTools DoStaticInserts method.
--				Command Line used: "C:\development\SQLTools\SqlTools\bin\Debug\SqlTools.vshost.exe" -m "staticInserts" -c "Data Source=.\SQL2008R2;Initial Catalog=dfwDemo;User Id=userDiamondFire;Password=userDiamondFire01;" -d "dfwMeath" -t "documents_stored_file;reporting_letter_template;reporting_list_spec;reporting_report_template;reporting_report_parameter;reportingwf_letter_template_workflow_state_access" -o "C:\development\DiamondFireWeb\db\staticdata\03_StaticLetterAndReportTemplates.sql"
-- ==============================================================================================
BEGIN TRANSACTION;
BEGIN TRY

-- ==============================================================================================
--					 [club_team] Static Data 
-- ==============================================================================================

IF NOT EXISTS(SELECT * FROM [club_team] WHERE [Oid] = N'8c3b75f8-8ec5-4677-8de8-078c411906c9')
BEGIN
	INSERT [dbo].[club_team] ([oid], [orev], [active], [name], [time_type], [sex], [year], [start_date], [end_date]) VALUES (N'8c3b75f8-8ec5-4677-8de8-078c411906c9', 0, 1, N'U10s', N'Y', N'F', 2016, NULL, NULL)
END

IF NOT EXISTS(SELECT * FROM [club_team] WHERE [Oid] = N'27caf1f2-1b51-4192-8440-0b0fd8dddbe0')
BEGIN
	INSERT [dbo].[club_team] ([oid], [orev], [active], [name], [time_type], [sex], [year], [start_date], [end_date]) VALUES (N'27caf1f2-1b51-4192-8440-0b0fd8dddbe0', 0, 1, N'U8s', N'Y', N'F', 2017, NULL, NULL)
END

IF NOT EXISTS(SELECT * FROM [club_team] WHERE [Oid] = N'27caf1f2-1b51-4192-8440-0b0fd8dddbe1')
BEGIN
	INSERT [dbo].[club_team] ([oid], [orev], [active], [name], [time_type], [sex], [year], [start_date], [end_date]) VALUES (N'27caf1f2-1b51-4192-8440-0b0fd8dddbe1', 0, 1, N'U10s', N'Y', N'F', 2017, NULL, NULL)
END

IF NOT EXISTS(SELECT * FROM [club_team] WHERE [Oid] = N'27caf1f2-1b51-4192-8440-0b0fd8dddbe2')
BEGIN
	INSERT [dbo].[club_team] ([oid], [orev], [active], [name], [time_type], [sex], [year], [start_date], [end_date]) VALUES (N'27caf1f2-1b51-4192-8440-0b0fd8dddbe2', 0, 1, N'U12s', N'Y', N'F', 2017, NULL, NULL)
END

IF NOT EXISTS(SELECT * FROM [club_team] WHERE [Oid] = N'27caf1f2-1b51-4192-8440-0b0fd8dddbe3')
BEGIN
	INSERT [dbo].[club_team] ([oid], [orev], [active], [name], [time_type], [sex], [year], [start_date], [end_date]) VALUES (N'27caf1f2-1b51-4192-8440-0b0fd8dddbe3', 0, 1, N'U14s', N'Y', N'F', 2017, NULL, NULL)
END

IF NOT EXISTS(SELECT * FROM [club_team] WHERE [Oid] = N'27caf1f2-1b51-4192-8440-0b0fd8dddbe4')
BEGIN
	INSERT [dbo].[club_team] ([oid], [orev], [active], [name], [time_type], [sex], [year], [start_date], [end_date]) VALUES (N'27caf1f2-1b51-4192-8440-0b0fd8dddbe4', 0, 1, N'U16s', N'Y', N'F', 2017, NULL, NULL)
END

IF NOT EXISTS(SELECT * FROM [club_team] WHERE [Oid] = N'27caf1f2-1b51-4192-8440-0b0fd8dddbe5')
BEGIN
	INSERT [dbo].[club_team] ([oid], [orev], [active], [name], [time_type], [sex], [year], [start_date], [end_date]) VALUES (N'27caf1f2-1b51-4192-8440-0b0fd8dddbe5', 0, 1, N'Minors', N'Y', N'F', 2017, NULL, NULL)
END

IF NOT EXISTS(SELECT * FROM [club_team] WHERE [Oid] = N'27caf1f2-1b51-4192-8440-0b0fd8dddbe6')
BEGIN
	INSERT [dbo].[club_team] ([oid], [orev], [active], [name], [time_type], [sex], [year], [start_date], [end_date]) VALUES (N'27caf1f2-1b51-4192-8440-0b0fd8dddbe6', 0, 1, N'Seniors', N'Y', N'F', 2017, NULL, NULL)
END

IF NOT EXISTS(SELECT * FROM [club_team] WHERE [Oid] = N'c66de3ce-d0e5-45ae-806f-2d8acc414824')
BEGIN
	INSERT [dbo].[club_team] ([oid], [orev], [active], [name], [time_type], [sex], [year], [start_date], [end_date]) VALUES (N'c66de3ce-d0e5-45ae-806f-2d8acc414824', 0, 1, N'Minors', N'Y', N'F', 2016, NULL, NULL)
END

IF NOT EXISTS(SELECT * FROM [club_team] WHERE [Oid] = N'ecdf4dc2-52f7-4e08-8359-49beb362155d')
BEGIN
	INSERT [dbo].[club_team] ([oid], [orev], [active], [name], [time_type], [sex], [year], [start_date], [end_date]) VALUES (N'ecdf4dc2-52f7-4e08-8359-49beb362155d', 0, 1, N'Seniors', N'Y', N'F', 2016, NULL, NULL)
END

IF NOT EXISTS(SELECT * FROM [club_team] WHERE [Oid] = N'fd2f1584-44d6-43da-bd54-4c8c653cc8cd')
BEGIN
	INSERT [dbo].[club_team] ([oid], [orev], [active], [name], [time_type], [sex], [year], [start_date], [end_date]) VALUES (N'fd2f1584-44d6-43da-bd54-4c8c653cc8cd', 0, 1, N'U6s', N'Y', N'T', 2016, NULL, NULL)
END

IF NOT EXISTS(SELECT * FROM [club_team] WHERE [Oid] = N'225a8675-2e11-4d5d-a17a-607db59bd9e3')
BEGIN
	INSERT [dbo].[club_team] ([oid], [orev], [active], [name], [time_type], [sex], [year], [start_date], [end_date]) VALUES (N'225a8675-2e11-4d5d-a17a-607db59bd9e3', 0, 1, N'Seniors', N'Y', N'M', 2016, NULL, NULL)
END

IF NOT EXISTS(SELECT * FROM [club_team] WHERE [Oid] = N'90f4018d-cf8c-4da6-bfee-63280f4b0829')
BEGIN
	INSERT [dbo].[club_team] ([oid], [orev], [active], [name], [time_type], [sex], [year], [start_date], [end_date]) VALUES (N'90f4018d-cf8c-4da6-bfee-63280f4b0829', 0, 1, N'U10s', N'Y', N'M', 2016, NULL, NULL)
END

IF NOT EXISTS(SELECT * FROM [club_team] WHERE [Oid] = N'0846ad69-8e95-442d-ad4e-687909366a7f')
BEGIN
	INSERT [dbo].[club_team] ([oid], [orev], [active], [name], [time_type], [sex], [year], [start_date], [end_date]) VALUES (N'0846ad69-8e95-442d-ad4e-687909366a7f', 0, 1, N'U12s', N'Y', N'M', 2016, NULL, NULL)
END

IF NOT EXISTS(SELECT * FROM [club_team] WHERE [Oid] = N'8a7c1f33-76c3-4c2b-9ae1-6932d1dc86b9')
BEGIN
	INSERT [dbo].[club_team] ([oid], [orev], [active], [name], [time_type], [sex], [year], [start_date], [end_date]) VALUES (N'8a7c1f33-76c3-4c2b-9ae1-6932d1dc86b9', 0, 1, N'U12s', N'Y', N'F', 2016, NULL, NULL)
END

IF NOT EXISTS(SELECT * FROM [club_team] WHERE [Oid] = N'61cf7e30-b2be-4bd0-b5c8-6ee1bdda80a0')
BEGIN
	INSERT [dbo].[club_team] ([oid], [orev], [active], [name], [time_type], [sex], [year], [start_date], [end_date]) VALUES (N'61cf7e30-b2be-4bd0-b5c8-6ee1bdda80a0', 0, 1, N'U16s', N'Y', N'F', 2016, NULL, NULL)
END

IF NOT EXISTS(SELECT * FROM [club_team] WHERE [Oid] = N'380892fb-59dc-409c-a822-719399c116af')
BEGIN
	INSERT [dbo].[club_team] ([oid], [orev], [active], [name], [time_type], [sex], [year], [start_date], [end_date]) VALUES (N'380892fb-59dc-409c-a822-719399c116af', 0, 1, N'U8s', N'Y', N'F', 2016, NULL, NULL)
END

IF NOT EXISTS(SELECT * FROM [club_team] WHERE [Oid] = N'ee9816e0-a3e7-41f9-9fab-725093a01cb0')
BEGIN
	INSERT [dbo].[club_team] ([oid], [orev], [active], [name], [time_type], [sex], [year], [start_date], [end_date]) VALUES (N'ee9816e0-a3e7-41f9-9fab-725093a01cb0', 0, 1, N'U14s', N'Y', N'M', 2016, NULL, NULL)
END

IF NOT EXISTS(SELECT * FROM [club_team] WHERE [Oid] = N'ea66e20c-4a5b-4215-9fd7-7ab460d97859')
BEGIN
	INSERT [dbo].[club_team] ([oid], [orev], [active], [name], [time_type], [sex], [year], [start_date], [end_date]) VALUES (N'ea66e20c-4a5b-4215-9fd7-7ab460d97859', 0, 1, N'U16s', N'Y', N'M', 2016, NULL, NULL)
END

IF NOT EXISTS(SELECT * FROM [club_team] WHERE [Oid] = N'04e08298-4671-4404-9d95-8c1997bfd992')
BEGIN
	INSERT [dbo].[club_team] ([oid], [orev], [active], [name], [time_type], [sex], [year], [start_date], [end_date]) VALUES (N'04e08298-4671-4404-9d95-8c1997bfd992', 0, 1, N'Minors', N'Y', N'M', 2016, NULL, NULL)
END

IF NOT EXISTS(SELECT * FROM [club_team] WHERE [Oid] = N'76095d5d-1ef9-4619-b8a3-9097dccd67ea')
BEGIN
	INSERT [dbo].[club_team] ([oid], [orev], [active], [name], [time_type], [sex], [year], [start_date], [end_date]) VALUES (N'76095d5d-1ef9-4619-b8a3-9097dccd67ea', 0, 1, N'U8s', N'Y', N'M', 2016, NULL, NULL)
END

IF NOT EXISTS(SELECT * FROM [club_team] WHERE [Oid] = N'a576fe6a-711a-4db2-9588-c6b7f55a0350')
BEGIN
	INSERT [dbo].[club_team] ([oid], [orev], [active], [name], [time_type], [sex], [year], [start_date], [end_date]) VALUES (N'a576fe6a-711a-4db2-9588-c6b7f55a0350', 0, 1, N'U12s', N'Y', N'M', 2017, NULL, NULL)
END

IF NOT EXISTS(SELECT * FROM [club_team] WHERE [Oid] = N'a576fe6a-711a-4db2-9588-c6b7f55a0351')
BEGIN
	INSERT [dbo].[club_team] ([oid], [orev], [active], [name], [time_type], [sex], [year], [start_date], [end_date]) VALUES (N'a576fe6a-711a-4db2-9588-c6b7f55a0351', 0, 1, N'U14s', N'Y', N'M', 2017, NULL, NULL)
END

IF NOT EXISTS(SELECT * FROM [club_team] WHERE [Oid] = N'a576fe6a-711a-4db2-9588-c6b7f55a0352')
BEGIN
	INSERT [dbo].[club_team] ([oid], [orev], [active], [name], [time_type], [sex], [year], [start_date], [end_date]) VALUES (N'a576fe6a-711a-4db2-9588-c6b7f55a0352', 0, 1, N'U8s', N'Y', N'M', 2017, NULL, NULL)
END

IF NOT EXISTS(SELECT * FROM [club_team] WHERE [Oid] = N'a576fe6a-711a-4db2-9588-c6b7f55a0353')
BEGIN
	INSERT [dbo].[club_team] ([oid], [orev], [active], [name], [time_type], [sex], [year], [start_date], [end_date]) VALUES (N'a576fe6a-711a-4db2-9588-c6b7f55a0353', 0, 1, N'U10s', N'Y', N'M', 2017, NULL, NULL)
END

IF NOT EXISTS(SELECT * FROM [club_team] WHERE [Oid] = N'a576fe6a-711a-4db2-9588-c6b7f55a0354')
BEGIN
	INSERT [dbo].[club_team] ([oid], [orev], [active], [name], [time_type], [sex], [year], [start_date], [end_date]) VALUES (N'a576fe6a-711a-4db2-9588-c6b7f55a0354', 0, 1, N'U6s', N'Y', N'T', 2017, NULL, NULL)
END

IF NOT EXISTS(SELECT * FROM [club_team] WHERE [Oid] = N'a576fe6a-711a-4db2-9588-c6b7f55a0355')
BEGIN
	INSERT [dbo].[club_team] ([oid], [orev], [active], [name], [time_type], [sex], [year], [start_date], [end_date]) VALUES (N'a576fe6a-711a-4db2-9588-c6b7f55a0355', 0, 1, N'U16s', N'Y', N'M', 2017, NULL, NULL)
END

IF NOT EXISTS(SELECT * FROM [club_team] WHERE [Oid] = N'a576fe6a-711a-4db2-9588-c6b7f55a0356')
BEGIN
	INSERT [dbo].[club_team] ([oid], [orev], [active], [name], [time_type], [sex], [year], [start_date], [end_date]) VALUES (N'a576fe6a-711a-4db2-9588-c6b7f55a0356', 0, 1, N'Minors', N'Y', N'M', 2017, NULL, NULL)
END

IF NOT EXISTS(SELECT * FROM [club_team] WHERE [Oid] = N'a576fe6a-711a-4db2-9588-c6b7f55a0357')
BEGIN
	INSERT [dbo].[club_team] ([oid], [orev], [active], [name], [time_type], [sex], [year], [start_date], [end_date]) VALUES (N'a576fe6a-711a-4db2-9588-c6b7f55a0357', 0, 1, N'Seniors', N'Y', N'M', 2017, NULL, NULL)
END

IF NOT EXISTS(SELECT * FROM [club_team] WHERE [Oid] = N'a7376ed3-96ce-42d5-ac08-d4db8695a124')
BEGIN
	INSERT [dbo].[club_team] ([oid], [orev], [active], [name], [time_type], [sex], [year], [start_date], [end_date]) VALUES (N'a7376ed3-96ce-42d5-ac08-d4db8695a124', 0, 1, N'U14s', N'Y', N'F', 2016, NULL, NULL)
END



-- ==============================================================================================
--					 [club_committee_member_type] Static Data 
-- ==============================================================================================

IF NOT EXISTS(SELECT * FROM [club_committee_member_type] WHERE [Oid] = N'66317e67-97d5-4076-9456-7a565f7fee30')
BEGIN
	INSERT [dbo].[club_committee_member_type] ([oid], [orev], [active], [default], [name], [description]) VALUES (N'66317e67-97d5-4076-9456-7a565f7fee30', 0, 1, 0, N'Public Relations Officer', N'Public Relations Officer (PRO)')
END

IF NOT EXISTS(SELECT * FROM [club_committee_member_type] WHERE [Oid] = N'66317e67-97d5-4076-9456-7a565f7fee31')
BEGIN
	INSERT [dbo].[club_committee_member_type] ([oid], [orev], [active], [default], [name], [description]) VALUES (N'66317e67-97d5-4076-9456-7a565f7fee31', 0, 1, 0, N'Chairman', N'Club Chairman')
END

IF NOT EXISTS(SELECT * FROM [club_committee_member_type] WHERE [Oid] = N'66317e67-97d5-4076-9456-7a565f7fee32')
BEGIN
	INSERT [dbo].[club_committee_member_type] ([oid], [orev], [active], [default], [name], [description]) VALUES (N'66317e67-97d5-4076-9456-7a565f7fee32', 0, 1, 0, N'Treasurer', N'Club Treasurer')
END

IF NOT EXISTS(SELECT * FROM [club_committee_member_type] WHERE [Oid] = N'66317e67-97d5-4076-9456-7a565f7fee33')
BEGIN
	INSERT [dbo].[club_committee_member_type] ([oid], [orev], [active], [default], [name], [description]) VALUES (N'66317e67-97d5-4076-9456-7a565f7fee33', 0, 1, 0, N'Secretary', N'Club Secretary')
END

IF NOT EXISTS(SELECT * FROM [club_committee_member_type] WHERE [Oid] = N'66317e67-97d5-4076-9456-7a565f7fee34')
BEGIN
	INSERT [dbo].[club_committee_member_type] ([oid], [orev], [active], [default], [name], [description]) VALUES (N'66317e67-97d5-4076-9456-7a565f7fee34', 0, 1, 1, N'Committee Member', N'Club Committee Member')
END

IF NOT EXISTS(SELECT * FROM [club_committee_member_type] WHERE [Oid] = N'66317e67-97d5-4076-9456-7a565f7fee35')
BEGIN
	INSERT [dbo].[club_committee_member_type] ([oid], [orev], [active], [default], [name], [description]) VALUES (N'66317e67-97d5-4076-9456-7a565f7fee35', 0, 1, 0, N'Assistant Treasurer', N'Club Assistant Treasurer')
END

IF NOT EXISTS(SELECT * FROM [club_committee_member_type] WHERE [Oid] = N'66317e67-97d5-4076-9456-7a565f7fee36')
BEGIN
	INSERT [dbo].[club_committee_member_type] ([oid], [orev], [active], [default], [name], [description]) VALUES (N'66317e67-97d5-4076-9456-7a565f7fee36', 0, 1, 0, N'Vice Chairman', N'Club Vice Chairman')
END

IF NOT EXISTS(SELECT * FROM [club_committee_member_type] WHERE [Oid] = N'66317e67-97d5-4076-9456-7a565f7fee37')
BEGIN
	INSERT [dbo].[club_committee_member_type] ([oid], [orev], [active], [default], [name], [description]) VALUES (N'66317e67-97d5-4076-9456-7a565f7fee37', 0, 1, 0, N'Assistant Secretary', N'Club Assistant Secretary')
END



-- ==============================================================================================
--					 [club_committee] Static Data 
-- ==============================================================================================

IF NOT EXISTS(SELECT * FROM [club_committee] WHERE [Oid] = N'8a6e667b-07ac-476d-a164-4410639ae910')
BEGIN
	INSERT [dbo].[club_committee] ([oid], [orev], [name], [description], [year], [start_date], [end_date], [active], [time_type]) VALUES (N'8a6e667b-07ac-476d-a164-4410639ae910', 0, N'Club Committee', NULL, 2017, NULL, NULL, 1, N'Y')
END

IF NOT EXISTS(SELECT * FROM [club_committee] WHERE [Oid] = N'8a6e667b-07ac-476d-a164-4410639ae911')
BEGIN
	INSERT [dbo].[club_committee] ([oid], [orev], [name], [description], [year], [start_date], [end_date], [active], [time_type]) VALUES (N'8a6e667b-07ac-476d-a164-4410639ae911', 0, N'Youth Committee', NULL, 2017, NULL, NULL, 1, N'Y')
END

IF NOT EXISTS(SELECT * FROM [club_committee] WHERE [Oid] = N'8a6e667b-07ac-476d-a164-4410639ae912')
BEGIN
	INSERT [dbo].[club_committee] ([oid], [orev], [name], [description], [year], [start_date], [end_date], [active], [time_type]) VALUES (N'8a6e667b-07ac-476d-a164-4410639ae912', 0, N'Finance & Fundraising Committee', NULL, 2017, NULL, NULL, 1, N'Y')
END

IF NOT EXISTS(SELECT * FROM [club_committee] WHERE [Oid] = N'8a6e667b-07ac-476d-a164-4410639ae913')
BEGIN
	INSERT [dbo].[club_committee] ([oid], [orev], [name], [description], [year], [start_date], [end_date], [active], [time_type]) VALUES (N'8a6e667b-07ac-476d-a164-4410639ae913', 0, N'Field & Games Committee', NULL, 2017, NULL, NULL, 1, N'Y')
END

IF NOT EXISTS(SELECT * FROM [club_committee] WHERE [Oid] = N'8a6e667b-07ac-476d-a164-4410639ae914')
BEGIN
	INSERT [dbo].[club_committee] ([oid], [orev], [name], [description], [year], [start_date], [end_date], [active], [time_type]) VALUES (N'8a6e667b-07ac-476d-a164-4410639ae914', 0, N'Gym Committee', NULL, 2017, NULL, NULL, 1, N'Y')
END

IF NOT EXISTS(SELECT * FROM [club_committee] WHERE [Oid] = N'8a6e667b-07ac-476d-a164-4410639ae915')
BEGIN
	INSERT [dbo].[club_committee] ([oid], [orev], [name], [description], [year], [start_date], [end_date], [active], [time_type]) VALUES (N'8a6e667b-07ac-476d-a164-4410639ae915', 0, N'Culture Committee', NULL, 2017, NULL, NULL, 1, N'Y')
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
