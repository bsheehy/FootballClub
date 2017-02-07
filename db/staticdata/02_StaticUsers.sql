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
	INSERT [dbo].[core_user] ([oid], [orev], [login], [password], [full_name], [email], [title], [no_of_failed_logins], [account_locked], [is_active]) VALUES (N'2adc084f-c38c-4f6e-adfa-00c5e87809e7', 2, N'bsheehy@hotmail.com', N'Passw0rd!', N'Super Admin User', N'bsheehy@hotmail.com', NULL, 0, 0, 1)
END

IF NOT EXISTS(SELECT * FROM [core_user] WHERE [Oid] = N'2adc084f-c38c-4f6e-adfa-00c5e87809e8')
BEGIN
	INSERT [dbo].[core_user] ([oid], [orev], [login], [password], [full_name], [email], [title], [no_of_failed_logins], [account_locked], [is_active]) VALUES (N'2adc084f-c38c-4f6e-adfa-00c5e87809e8', 1, N'scotch@hotmail.com', N'Passw0rd!', N'Admin User', N'scotch@hotmail.com', NULL, 0, 0, 1)
END

IF NOT EXISTS(SELECT * FROM [core_user] WHERE [Oid] = N'05e34e77-ee11-43f3-a84a-9b9d6f490702')
BEGIN
	INSERT [dbo].[core_user] ([oid], [orev], [login], [password], [full_name], [email], [title], [no_of_failed_logins], [account_locked], [is_active]) VALUES (N'05e34e77-ee11-43f3-a84a-9b9d6f490702', 1, N'gilly@hotmail.com', N'Passw0rd!', N'Gilly Committe User', N'gilly@hotmail.com', NULL, 0, 0, 1)
END

IF NOT EXISTS(SELECT * FROM [core_user] WHERE [Oid] = N'965f0d9f-00c8-4afa-929a-a70f00e20df3')
BEGIN
	INSERT [dbo].[core_user] ([oid], [orev], [login], [password], [full_name], [email], [title], [no_of_failed_logins], [account_locked], [is_active]) VALUES (N'965f0d9f-00c8-4afa-929a-a70f00e20df3', 1, N'colliequinn@hotmail.com', N'colliequinn1234', N'Column Quinn', N'colliequinn@hotmail.com', NULL, 0, 0, 1)
END

IF NOT EXISTS(SELECT * FROM [core_user] WHERE [Oid] = N'9cb95b88-d35d-451d-a59f-a70f00e250b5')
BEGIN
	INSERT [dbo].[core_user] ([oid], [orev], [login], [password], [full_name], [email], [title], [no_of_failed_logins], [account_locked], [is_active]) VALUES (N'9cb95b88-d35d-451d-a59f-a70f00e250b5', 2, N'ryanppickering@hotmail.com', N'ryan1234', N'Ryan Pickering', N'ryanppickering@hotmail.com', NULL, 0, 0, 1)
END

IF NOT EXISTS(SELECT * FROM [core_user] WHERE [Oid] = N'f338788a-8e95-4799-98c4-a70f00e2dd00')
BEGIN
	INSERT [dbo].[core_user] ([oid], [orev], [login], [password], [full_name], [email], [title], [no_of_failed_logins], [account_locked], [is_active]) VALUES (N'f338788a-8e95-4799-98c4-a70f00e2dd00', 1, N'stephenmulligan@hotmail.com', N'stephen123', N'Stephen Mulligan', N'stephenmulligan@hotmail.com', NULL, 0, 0, 1)
END

IF NOT EXISTS(SELECT * FROM [core_user] WHERE [Oid] = N'3d08a81a-f856-46a7-bbff-a70f00e307f6')
BEGIN
	INSERT [dbo].[core_user] ([oid], [orev], [login], [password], [full_name], [email], [title], [no_of_failed_logins], [account_locked], [is_active]) VALUES (N'3d08a81a-f856-46a7-bbff-a70f00e307f6', 1, N'eoindevlin@hotmail.com', N'eoin123', N'Eoin Devlin', N'Eoin Devlin', NULL, 0, 0, 1)
END


INSERT [dbo].[core_user_roles] ([user], [role], [order]) VALUES (N'2adc084f-c38c-4f6e-adfa-00c5e87809e7', N'058ca795-d577-43aa-958c-769295280d58', 0)

INSERT [dbo].[core_user_roles] ([user], [role], [order]) VALUES (N'2adc084f-c38c-4f6e-adfa-00c5e87809e8', N'058ca795-d577-43aa-958c-769295280d59', 1)

INSERT [dbo].[core_user_roles] ([user], [role], [order]) VALUES (N'05e34e77-ee11-43f3-a84a-9b9d6f490702', N'67b00f36-f679-4f3d-82dd-5abd7fac03e1', 1)

INSERT [dbo].[core_user_roles] ([user], [role], [order]) VALUES (N'965f0d9f-00c8-4afa-929a-a70f00e20df3', N'67b00f36-f679-4f3d-82dd-5abd7fac03e1', 0)

INSERT [dbo].[core_user_roles] ([user], [role], [order]) VALUES (N'9cb95b88-d35d-451d-a59f-a70f00e250b5', N'67b00f36-f679-4f3d-82dd-5abd7fac03e1', 0)

INSERT [dbo].[core_user_roles] ([user], [role], [order]) VALUES (N'f338788a-8e95-4799-98c4-a70f00e2dd00', N'67b00f36-f679-4f3d-82dd-5abd7fac03e1', 0)

INSERT [dbo].[core_user_roles] ([user], [role], [order]) VALUES (N'3d08a81a-f856-46a7-bbff-a70f00e307f6', N'058ca795-d577-43aa-958c-769295280d59', 0)


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
