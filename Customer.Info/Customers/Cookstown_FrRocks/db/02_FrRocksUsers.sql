-- ==============================================================================================
--				File automatically created by SqlTools DoStaticInserts method.
--				Command Line used: "C:\development\SQLTools\SqlTools\bin\Debug\SqlTools.vshost.exe" -m "staticInserts" -c "Data Source=.\SQL2012;Initial Catalog=ClubFrRocks;User Id=sa;Password=Mallon2012;" -d "ClubFrRocks" -t "core_role;core_user;core_user_roles" -o "C:\Temp\Ouput_SQL.sql"
-- ==============================================================================================
BEGIN TRANSACTION;
BEGIN TRY


-- ==============================================================================================
--					 [core_user] Static Data 
-- ==============================================================================================


IF NOT EXISTS(SELECT * FROM [core_user] WHERE [Oid] = N'2adc084f-c38c-4f6e-adfa-00c5e87809e8')
BEGIN
	INSERT [dbo].[core_user] ([oid], [orev], [login], [password], [full_name], [email], [title], [no_of_failed_logins], [account_locked], [is_active]) VALUES (N'2adc084f-c38c-4f6e-adfa-00c5e87809e8', 1, N'sconway57@yahoo.co.uk', N'Conway678!', N'Stephen Conway', N'sconway57@yahoo.co.uk', NULL, 0, 0, 1)
END

IF NOT EXISTS(SELECT * FROM [core_user] WHERE [Oid] = N'05e34e77-ee11-43f3-a84a-9b9d6f490702')
BEGIN
	INSERT [dbo].[core_user] ([oid], [orev], [login], [password], [full_name], [email], [title], [no_of_failed_logins], [account_locked], [is_active]) VALUES (N'05e34e77-ee11-43f3-a84a-9b9d6f490702', 1, N'gilly@hotmail.com', N'Passw0rd!', N'Gilly Committe User', N'gilly@hotmail.com', NULL, 0, 0, 1)
END

IF NOT EXISTS(SELECT * FROM [core_user] WHERE [Oid] = N'965f0d9f-00c8-4afa-929a-a70f00e20df3')
BEGIN
	INSERT [dbo].[core_user] ([oid], [orev], [login], [password], [full_name], [email], [title], [no_of_failed_logins], [account_locked], [is_active]) VALUES (N'965f0d9f-00c8-4afa-929a-a70f00e20df3', 1, N'colliequinn@hotmail.com', N'Cquinn5544!', N'Column Quinn', N'colliequinn@hotmail.com', NULL, 0, 0, 1)
END

IF NOT EXISTS(SELECT * FROM [core_user] WHERE [Oid] = N'9cb95b88-d35d-451d-a59f-a70f00e250b5')
BEGIN
	INSERT [dbo].[core_user] ([oid], [orev], [login], [password], [full_name], [email], [title], [no_of_failed_logins], [account_locked], [is_active]) VALUES (N'9cb95b88-d35d-451d-a59f-a70f00e250b5', 2, N'pickering-r@email.ulster.ac.uk', N'Ryan1234$', N'Ryan Pickering', N'pickering-r@email.ulster.ac.uk', NULL, 0, 0, 1)
END

IF NOT EXISTS(SELECT * FROM [core_user] WHERE [Oid] = N'f338788a-8e95-4799-98c4-a70f00e2dd00')
BEGIN
	INSERT [dbo].[core_user] ([oid], [orev], [login], [password], [full_name], [email], [title], [no_of_failed_logins], [account_locked], [is_active]) VALUES (N'f338788a-8e95-4799-98c4-a70f00e2dd00', 1, N'stephen_mulligan@hotmail.com', N'Stephen123!', N'Stephen Mulligan', N'stephen_mulligan@hotmail.com', NULL, 0, 0, 1)
END

IF NOT EXISTS(SELECT * FROM [core_user] WHERE [Oid] = N'3d08a81a-f856-46a7-bbff-a70f00e307f6')
BEGIN
	INSERT [dbo].[core_user] ([oid], [orev], [login], [password], [full_name], [email], [title], [no_of_failed_logins], [account_locked], [is_active]) VALUES (N'3d08a81a-f856-46a7-bbff-a70f00e307f6', 1, N'eoindevlin@hotmail.com', N'Eoin789!', N'Eoin Devlin', N'eoindevlin@hotmail.com', NULL, 0, 0, 1)
END

IF NOT EXISTS(SELECT * FROM [core_user] WHERE [Oid] = N'2198874b-47c8-4969-bc69-f1654ef071b3')
BEGIN
	INSERT [dbo].[core_user] ([oid], [orev], [login], [password], [full_name], [email], [title], [no_of_failed_logins], [account_locked], [is_active]) VALUES (N'2198874b-47c8-4969-bc69-f1654ef071b3', 1, N'neesond@staffordlynch.ie', N'Dneeson999!', N'Donald Neeson', N'neesond@staffordlynch.ie', NULL, 0, 0, 1)
END

IF NOT EXISTS(SELECT * FROM [core_user] WHERE [Oid] = N'd8a75dd0-b864-4153-bf29-790d16f63745')
BEGIN
	INSERT [dbo].[core_user] ([oid], [orev], [login], [password], [full_name], [email], [title], [no_of_failed_logins], [account_locked], [is_active]) VALUES (N'd8a75dd0-b864-4153-bf29-790d16f63745', 1, N'Caroline.Sheehy@midulstercouncil.org', N'Caroline432$', N'Caroline Sheehy', N'Caroline.Sheehy@midulstercouncil.org', NULL, 0, 0, 1)
END

IF NOT EXISTS(SELECT * FROM [core_user] WHERE [Oid] = N'986b55af-b092-4142-94b0-1e52c18224dc')
BEGIN
	INSERT [dbo].[core_user] ([oid], [orev], [login], [password], [full_name], [email], [title], [no_of_failed_logins], [account_locked], [is_active]) VALUES (N'986b55af-b092-4142-94b0-1e52c18224dc', 1, N'conormcfadden25@btinternet.com', N'Connor555!', N'Connor Mc Fadden', N'conormcfadden25@btinternet.com', NULL, 0, 0, 1)
END

IF NOT EXISTS(SELECT * FROM [core_user] WHERE [Oid] = N'e845cfe8-4a4a-4201-b030-a90986469e81')
BEGIN
	INSERT [dbo].[core_user] ([oid], [orev], [login], [password], [full_name], [email], [title], [no_of_failed_logins], [account_locked], [is_active]) VALUES (N'e845cfe8-4a4a-4201-b030-a90986469e81', 1, N'fitzpatrickgj@hotmail.com', N'Fitzpatrick856!', N'Gary Fitzpatrick', N'fitzpatrickgj@hotmail.com', NULL, 0, 0, 1)
END

IF NOT EXISTS(SELECT * FROM [core_user] WHERE [Oid] = N'561f455c-843a-4f06-b2af-df5456d3b5b5')
BEGIN
	INSERT [dbo].[core_user] ([oid], [orev], [login], [password], [full_name], [email], [title], [no_of_failed_logins], [account_locked], [is_active]) VALUES (N'561f455c-843a-4f06-b2af-df5456d3b5b5', 1, N'colinhampsey@aol.com', N'cHampshey889!', N'Collin Hampshey', N'colinhampsey@aol.com', NULL, 0, 0, 1)
END

IF NOT EXISTS(SELECT * FROM [core_user] WHERE [Oid] = N'afc2dd49-aee4-48e8-a4f0-748fa0fa7827')
BEGIN
	INSERT [dbo].[core_user] ([oid], [orev], [login], [password], [full_name], [email], [title], [no_of_failed_logins], [account_locked], [is_active]) VALUES (N'afc2dd49-aee4-48e8-a4f0-748fa0fa7827', 1, N'markmcmeal@hotmail.com', N'Mark887!', N'Mark Mc Meal', N'markmcmeal@hotmail.com', NULL, 0, 0, 1)
END

IF NOT EXISTS(SELECT * FROM [core_user] WHERE [Oid] = N'c582bf3a-8be2-4e09-954c-a202c163461b')
BEGIN
	INSERT [dbo].[core_user] ([oid], [orev], [login], [password], [full_name], [email], [title], [no_of_failed_logins], [account_locked], [is_active]) VALUES (N'c582bf3a-8be2-4e09-954c-a202c163461b', 1, N'ClodaghMcCracken@hotmail.com', N'Clodagh775!', N'Clodagh Mc Cracken', N'ClodaghMcCracken@hotmail.com', NULL, 0, 0, 1)
END

IF NOT EXISTS(SELECT * FROM [core_user] WHERE [Oid] = N'4dd48dc4-38c2-4118-bb69-0e8e848303b5')
BEGIN
	INSERT [dbo].[core_user] ([oid], [orev], [login], [password], [full_name], [email], [title], [no_of_failed_logins], [account_locked], [is_active]) VALUES (N'4dd48dc4-38c2-4118-bb69-0e8e848303b5', 1, N'ClaireBell@hotmail.com', N'Cbell487!', N'Claire Bell', N'ClaireBell@hotmail.com', NULL, 0, 0, 1)
END

IF NOT EXISTS(SELECT * FROM [core_user] WHERE [Oid] = N'c168ee5d-a9b0-46f2-ba8b-ebbd84f8845f')
BEGIN
	INSERT [dbo].[core_user] ([oid], [orev], [login], [password], [full_name], [email], [title], [no_of_failed_logins], [account_locked], [is_active]) VALUES (N'c168ee5d-a9b0-46f2-ba8b-ebbd84f8845f', 1, N'ChuckMullan@hotmail.com', N'Cmullan543!', N'Chuck Mullan', N'ChuckMullan@hotmail.com', NULL, 0, 0, 1)
END


IF NOT EXISTS(SELECT * FROM [core_user] WHERE [Oid] = N'e4147d9c-d685-44db-8510-36673abc77db')
BEGIN
	INSERT [dbo].[core_user] ([oid], [orev], [login], [password], [full_name], [email], [title], [no_of_failed_logins], [account_locked], [is_active]) VALUES (N'e4147d9c-d685-44db-8510-36673abc77db', 1, N'RoryMcGarrity@hotmail.com', N'McGarrity443!', N'Rory Mc Garrity', N'RoryMcGarrity@hotmail.com', NULL, 0, 0, 1)
END


INSERT [dbo].[core_user_roles] ([user], [role], [order]) VALUES (N'2adc084f-c38c-4f6e-adfa-00c5e87809e8', N'058ca795-d577-43aa-958c-769295280d59', 1)

INSERT [dbo].[core_user_roles] ([user], [role], [order]) VALUES (N'4dd48dc4-38c2-4118-bb69-0e8e848303b5', N'67b00f36-f679-4f3d-82dd-5abd7fac03e1', 0)

INSERT [dbo].[core_user_roles] ([user], [role], [order]) VALUES (N'986b55af-b092-4142-94b0-1e52c18224dc', N'67b00f36-f679-4f3d-82dd-5abd7fac03e1', 0)

INSERT [dbo].[core_user_roles] ([user], [role], [order]) VALUES (N'e4147d9c-d685-44db-8510-36673abc77db', N'67b00f36-f679-4f3d-82dd-5abd7fac03e1', 0)

INSERT [dbo].[core_user_roles] ([user], [role], [order]) VALUES (N'afc2dd49-aee4-48e8-a4f0-748fa0fa7827', N'67b00f36-f679-4f3d-82dd-5abd7fac03e1', 0)

INSERT [dbo].[core_user_roles] ([user], [role], [order]) VALUES (N'd8a75dd0-b864-4153-bf29-790d16f63745', N'058ca795-d577-43aa-958c-769295280d59', 0)

INSERT [dbo].[core_user_roles] ([user], [role], [order]) VALUES (N'05e34e77-ee11-43f3-a84a-9b9d6f490702', N'67b00f36-f679-4f3d-82dd-5abd7fac03e1', 1)

INSERT [dbo].[core_user_roles] ([user], [role], [order]) VALUES (N'c582bf3a-8be2-4e09-954c-a202c163461b', N'67b00f36-f679-4f3d-82dd-5abd7fac03e2', 0)

INSERT [dbo].[core_user_roles] ([user], [role], [order]) VALUES (N'965f0d9f-00c8-4afa-929a-a70f00e20df3', N'67b00f36-f679-4f3d-82dd-5abd7fac03e1', 0)

INSERT [dbo].[core_user_roles] ([user], [role], [order]) VALUES (N'9cb95b88-d35d-451d-a59f-a70f00e250b5', N'67b00f36-f679-4f3d-82dd-5abd7fac03e1', 0)

INSERT [dbo].[core_user_roles] ([user], [role], [order]) VALUES (N'f338788a-8e95-4799-98c4-a70f00e2dd00', N'67b00f36-f679-4f3d-82dd-5abd7fac03e1', 0)

INSERT [dbo].[core_user_roles] ([user], [role], [order]) VALUES (N'3d08a81a-f856-46a7-bbff-a70f00e307f6', N'058ca795-d577-43aa-958c-769295280d59', 0)

INSERT [dbo].[core_user_roles] ([user], [role], [order]) VALUES (N'e845cfe8-4a4a-4201-b030-a90986469e81', N'67b00f36-f679-4f3d-82dd-5abd7fac03e1', 0)

INSERT [dbo].[core_user_roles] ([user], [role], [order]) VALUES (N'561f455c-843a-4f06-b2af-df5456d3b5b5', N'058ca795-d577-43aa-958c-769295280d59', 0)

INSERT [dbo].[core_user_roles] ([user], [role], [order]) VALUES (N'c168ee5d-a9b0-46f2-ba8b-ebbd84f8845f', N'67b00f36-f679-4f3d-82dd-5abd7fac03e2', 0)

INSERT [dbo].[core_user_roles] ([user], [role], [order]) VALUES (N'2198874b-47c8-4969-bc69-f1654ef071b3', N'058ca795-d577-43aa-958c-769295280d59', 0)


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
