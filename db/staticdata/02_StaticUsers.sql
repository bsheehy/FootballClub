-- ==============================================================================================
--				File automatically created by SqlTools DoStaticInserts method.
--				Command Line used: "C:\development\SQLTools\SqlTools\bin\Debug\SqlTools.vshost.exe" -m "staticInserts" -c "Data Source=.\SQL2012;Initial Catalog=ClubFrRocks;User Id=sa;Password=Mallon2012;" -d "ClubFrRocks" -t "core_role;core_user;core_user_roles" -o "C:\Temp\Ouput_SQL.sql"
-- ==============================================================================================
BEGIN TRANSACTION;
BEGIN TRY


-- ==============================================================================================
--					 [core_user] Static Data 
-- ==============================================================================================

IF NOT EXISTS(SELECT * FROM [core_user] WHERE [Oid] = N'2adc084f-c38c-4f6e-adfa-00c5e87809e7')
BEGIN
	INSERT [dbo].[core_user] ([oid], [orev], [login], [password], [full_name], [email], [title], [no_of_failed_logins], [account_locked], [is_active]) VALUES (N'2adc084f-c38c-4f6e-adfa-00c5e87809e7', 2, N'bsheehy@hotmail.com', N'Passw0rd!', N'Brendan Sheehy (Super Admin)', N'bsheehy@hotmail.com', NULL, 0, 0, 1)
  INSERT [dbo].[core_user_roles] ([user], [role], [order]) VALUES (N'2adc084f-c38c-4f6e-adfa-00c5e87809e7', N'058ca795-d577-43aa-958c-769295280d58', 0)
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
