-- ==============================================================================================
--				File automatically created by SqlTools DoStaticInserts method.
--				Command Line used: "C:\development\SQLTools\SqlTools\bin\Debug\SqlTools.vshost.exe" -m "staticInserts" -c "Data Source=.\SQL2012;Initial Catalog=ClubFrRocks;User Id=sa;Password=Mallon2012;" -d "ClubFrRocks" -t "club_team_position" -o "C:\Temp\Ouput_SQL.sql"
-- ==============================================================================================
BEGIN TRANSACTION;
BEGIN TRY
-- ==============================================================================================
--					 [club_team_position] Static Data 
-- ==============================================================================================

IF NOT EXISTS(SELECT * FROM [club_team_position] WHERE [Oid] = N'3dbe81e4-5b1c-4f62-97f9-5e1b855d9b01')
BEGIN
	INSERT [dbo].[club_team_position] ([oid], [orev], [name], [number], [active]) VALUES (N'3dbe81e4-5b1c-4f62-97f9-5e1b855d9b01', 0, N'Goal Keeper', 1, 1)
END

IF NOT EXISTS(SELECT * FROM [club_team_position] WHERE [Oid] = N'3dbe81e4-5b1c-4f62-97f9-5e1b855d9b02')
BEGIN
	INSERT [dbo].[club_team_position] ([oid], [orev], [name], [number], [active]) VALUES (N'3dbe81e4-5b1c-4f62-97f9-5e1b855d9b02', 0, N'Right Full Back', 2, 1)
END

IF NOT EXISTS(SELECT * FROM [club_team_position] WHERE [Oid] = N'3dbe81e4-5b1c-4f62-97f9-5e1b855d9b03')
BEGIN
	INSERT [dbo].[club_team_position] ([oid], [orev], [name], [number], [active]) VALUES (N'3dbe81e4-5b1c-4f62-97f9-5e1b855d9b03', 0, N'Centre Full Back', 3, 1)
END

IF NOT EXISTS(SELECT * FROM [club_team_position] WHERE [Oid] = N'3dbe81e4-5b1c-4f62-97f9-5e1b855d9b04')
BEGIN
	INSERT [dbo].[club_team_position] ([oid], [orev], [name], [number], [active]) VALUES (N'3dbe81e4-5b1c-4f62-97f9-5e1b855d9b04', 0, N'Left Full Back', 4, 1)
END

IF NOT EXISTS(SELECT * FROM [club_team_position] WHERE [Oid] = N'3dbe81e4-5b1c-4f62-97f9-5e1b855d9b05')
BEGIN
	INSERT [dbo].[club_team_position] ([oid], [orev], [name], [number], [active]) VALUES (N'3dbe81e4-5b1c-4f62-97f9-5e1b855d9b05', 0, N'Right Half Back', 5, 1)
END

IF NOT EXISTS(SELECT * FROM [club_team_position] WHERE [Oid] = N'3dbe81e4-5b1c-4f62-97f9-5e1b855d9b06')
BEGIN
	INSERT [dbo].[club_team_position] ([oid], [orev], [name], [number], [active]) VALUES (N'3dbe81e4-5b1c-4f62-97f9-5e1b855d9b06', 0, N'Centre Half Back', 6, 1)
END

IF NOT EXISTS(SELECT * FROM [club_team_position] WHERE [Oid] = N'3dbe81e4-5b1c-4f62-97f9-5e1b855d9b07')
BEGIN
	INSERT [dbo].[club_team_position] ([oid], [orev], [name], [number], [active]) VALUES (N'3dbe81e4-5b1c-4f62-97f9-5e1b855d9b07', 0, N'Left Half Back', 7, 1)
END

IF NOT EXISTS(SELECT * FROM [club_team_position] WHERE [Oid] = N'3dbe81e4-5b1c-4f62-97f9-5e1b855d9b08')
BEGIN
	INSERT [dbo].[club_team_position] ([oid], [orev], [name], [number], [active]) VALUES (N'3dbe81e4-5b1c-4f62-97f9-5e1b855d9b08', 0, N'Midfield', 8, 1)
END

IF NOT EXISTS(SELECT * FROM [club_team_position] WHERE [Oid] = N'3dbe81e4-5b1c-4f62-97f9-5e1b855d9b09')
BEGIN
	INSERT [dbo].[club_team_position] ([oid], [orev], [name], [number], [active]) VALUES (N'3dbe81e4-5b1c-4f62-97f9-5e1b855d9b09', 0, N'Midfield', 9, 1)
END

IF NOT EXISTS(SELECT * FROM [club_team_position] WHERE [Oid] = N'3dbe81e4-5b1c-4f62-97f9-5e1b855d9b10')
BEGIN
	INSERT [dbo].[club_team_position] ([oid], [orev], [name], [number], [active]) VALUES (N'3dbe81e4-5b1c-4f62-97f9-5e1b855d9b10', 0, N'Right Half Forward', 10, 1)
END

IF NOT EXISTS(SELECT * FROM [club_team_position] WHERE [Oid] = N'3dbe81e4-5b1c-4f62-97f9-5e1b855d9b11')
BEGIN
	INSERT [dbo].[club_team_position] ([oid], [orev], [name], [number], [active]) VALUES (N'3dbe81e4-5b1c-4f62-97f9-5e1b855d9b11', 0, N'Centre Half Forward', 11, 1)
END

IF NOT EXISTS(SELECT * FROM [club_team_position] WHERE [Oid] = N'3dbe81e4-5b1c-4f62-97f9-5e1b855d9b12')
BEGIN
	INSERT [dbo].[club_team_position] ([oid], [orev], [name], [number], [active]) VALUES (N'3dbe81e4-5b1c-4f62-97f9-5e1b855d9b12', 0, N'Left Half Forward', 12, 1)
END

IF NOT EXISTS(SELECT * FROM [club_team_position] WHERE [Oid] = N'3dbe81e4-5b1c-4f62-97f9-5e1b855d9b13')
BEGIN
	INSERT [dbo].[club_team_position] ([oid], [orev], [name], [number], [active]) VALUES (N'3dbe81e4-5b1c-4f62-97f9-5e1b855d9b13', 0, N'Right Full Forward', 13, 1)
END

IF NOT EXISTS(SELECT * FROM [club_team_position] WHERE [Oid] = N'3dbe81e4-5b1c-4f62-97f9-5e1b855d9b14')
BEGIN
	INSERT [dbo].[club_team_position] ([oid], [orev], [name], [number], [active]) VALUES (N'3dbe81e4-5b1c-4f62-97f9-5e1b855d9b14', 0, N'Centre Full Forward', 14, 1)
END

IF NOT EXISTS(SELECT * FROM [club_team_position] WHERE [Oid] = N'3dbe81e4-5b1c-4f62-97f9-5e1b855d9b15')
BEGIN
	INSERT [dbo].[club_team_position] ([oid], [orev], [name], [number], [active]) VALUES (N'3dbe81e4-5b1c-4f62-97f9-5e1b855d9b15', 0, N'Left Full Forward', 15, 1)
END

IF NOT EXISTS(SELECT * FROM [club_team_position] WHERE [Oid] = N'3dbe81e4-5b1c-4f62-97f9-5e1b855d9b16')
BEGIN
	INSERT [dbo].[club_team_position] ([oid], [orev], [name], [number], [active]) VALUES (N'3dbe81e4-5b1c-4f62-97f9-5e1b855d9b16', 0, N'Sub 1', 16, 1)
END

IF NOT EXISTS(SELECT * FROM [club_team_position] WHERE [Oid] = N'3dbe81e4-5b1c-4f62-97f9-5e1b855d9b17')
BEGIN
	INSERT [dbo].[club_team_position] ([oid], [orev], [name], [number], [active]) VALUES (N'3dbe81e4-5b1c-4f62-97f9-5e1b855d9b17', 0, N'Sub 2', 17, 1)
END

IF NOT EXISTS(SELECT * FROM [club_team_position] WHERE [Oid] = N'3dbe81e4-5b1c-4f62-97f9-5e1b855d9b18')
BEGIN
	INSERT [dbo].[club_team_position] ([oid], [orev], [name], [number], [active]) VALUES (N'3dbe81e4-5b1c-4f62-97f9-5e1b855d9b18', 0, N'Sub 3', 18, 1)
END

IF NOT EXISTS(SELECT * FROM [club_team_position] WHERE [Oid] = N'3dbe81e4-5b1c-4f62-97f9-5e1b855d9b19')
BEGIN
	INSERT [dbo].[club_team_position] ([oid], [orev], [name], [number], [active]) VALUES (N'3dbe81e4-5b1c-4f62-97f9-5e1b855d9b19', 0, N'Sub 4', 19, 1)
END

IF NOT EXISTS(SELECT * FROM [club_team_position] WHERE [Oid] = N'3dbe81e4-5b1c-4f62-97f9-5e1b855d9b20')
BEGIN
	INSERT [dbo].[club_team_position] ([oid], [orev], [name], [number], [active]) VALUES (N'3dbe81e4-5b1c-4f62-97f9-5e1b855d9b20', 0, N'Sub 5', 20, 1)
END

IF NOT EXISTS(SELECT * FROM [club_team_position] WHERE [Oid] = N'3dbe81e4-5b1c-4f62-97f9-5e1b855d9b21')
BEGIN
	INSERT [dbo].[club_team_position] ([oid], [orev], [name], [number], [active]) VALUES (N'3dbe81e4-5b1c-4f62-97f9-5e1b855d9b21', 0, N'Sub 6', 21, 1)
END

IF NOT EXISTS(SELECT * FROM [club_team_position] WHERE [Oid] = N'3dbe81e4-5b1c-4f62-97f9-5e1b855d9b22')
BEGIN
	INSERT [dbo].[club_team_position] ([oid], [orev], [name], [number], [active]) VALUES (N'3dbe81e4-5b1c-4f62-97f9-5e1b855d9b22', 0, N'Sub 7', 22, 1)
END

IF NOT EXISTS(SELECT * FROM [club_team_position] WHERE [Oid] = N'3dbe81e4-5b1c-4f62-97f9-5e1b855d9b23')
BEGIN
	INSERT [dbo].[club_team_position] ([oid], [orev], [name], [number], [active]) VALUES (N'3dbe81e4-5b1c-4f62-97f9-5e1b855d9b23', 0, N'Sub 8', 23, 1)
END

IF NOT EXISTS(SELECT * FROM [club_team_position] WHERE [Oid] = N'3dbe81e4-5b1c-4f62-97f9-5e1b855d9b24')
BEGIN
	INSERT [dbo].[club_team_position] ([oid], [orev], [name], [number], [active]) VALUES (N'3dbe81e4-5b1c-4f62-97f9-5e1b855d9b24', 0, N'Sub 9', 24, 1)
END

IF NOT EXISTS(SELECT * FROM [club_team_position] WHERE [Oid] = N'3dbe81e4-5b1c-4f62-97f9-5e1b855d9b25')
BEGIN
	INSERT [dbo].[club_team_position] ([oid], [orev], [name], [number], [active]) VALUES (N'3dbe81e4-5b1c-4f62-97f9-5e1b855d9b25', 0, N'Sub 10', 25, 1)
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
