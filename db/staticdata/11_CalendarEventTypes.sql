-- ==============================================================================================
--				File automatically created by SqlTools DoStaticInserts method.
--				Command Line used: "C:\development\SQLTools\SqlTools\bin\Debug\SqlTools.vshost.exe" -m "staticInserts" -c "Data Source=.\SQL2012;Initial Catalog=ClubFrRocks;User Id=sa;Password=Mallon2012;" -d "ClubFrRocks" -t "club_club_calendar_event_type" -o "C:\Temp\Ouput_SQL.sql"
-- ==============================================================================================
BEGIN TRANSACTION;
BEGIN TRY
-- ==============================================================================================
--					 [club_club_calendar_event_type] Static Data 
-- ==============================================================================================

IF NOT EXISTS(SELECT * FROM [club_club_calendar_event_type] WHERE [Oid] = N'd591be14-40b8-4447-9606-78d82af7ff01')
BEGIN
	INSERT [dbo].[club_club_calendar_event_type] ([oid], [orev], [active], [default], [name], [description], [color_hex]) VALUES (N'd591be14-40b8-4447-9606-78d82af7ff01', 0, 1, 1, N'Team Training', N'A recurring booking for the Time and Day a Team trains.', N'#ffbf00')
END

IF NOT EXISTS(SELECT * FROM [club_club_calendar_event_type] WHERE [Oid] = N'd591be14-40b8-4447-9606-78d82af7ff02')
BEGIN
	INSERT [dbo].[club_club_calendar_event_type] ([oid], [orev], [active], [default], [name], [description], [color_hex]) VALUES (N'd591be14-40b8-4447-9606-78d82af7ff02', 0, 1, 0, N'Social Event', N'A one off Social Event.', N'#00ffff')
END

IF NOT EXISTS(SELECT * FROM [club_club_calendar_event_type] WHERE [Oid] = N'd591be14-40b8-4447-9606-78d82af7ff03')
BEGIN
	INSERT [dbo].[club_club_calendar_event_type] ([oid], [orev], [active], [default], [name], [description], [color_hex]) VALUES (N'd591be14-40b8-4447-9606-78d82af7ff03', 0, 1, 0, N'Fund Raiser', N'A one off Fund Raising event.', N'#ff0080')
END

IF NOT EXISTS(SELECT * FROM [club_club_calendar_event_type] WHERE [Oid] = N'd591be14-40b8-4447-9606-78d82af7ff04')
BEGIN
	INSERT [dbo].[club_club_calendar_event_type] ([oid], [orev], [active], [default], [name], [description], [color_hex]) VALUES (N'd591be14-40b8-4447-9606-78d82af7ff04', 0, 1, 0, N'Committee Meeting', N'A recurring or single Committee Meeting.', N'#ff0000')
END

IF NOT EXISTS(SELECT * FROM [club_club_calendar_event_type] WHERE [Oid] = N'd591be14-40b8-4447-9606-78d82af7ff05')
BEGIN
	INSERT [dbo].[club_club_calendar_event_type] ([oid], [orev], [active], [default], [name], [description], [color_hex]) VALUES (N'd591be14-40b8-4447-9606-78d82af7ff05', 0, 1, 0, N'Match', N'A one off Match.', N'#ffff00')
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
