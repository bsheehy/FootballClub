-- ==============================================================================================
--				File automatically created by SqlTools DoStaticInserts method.
--				Command Line used: "C:\development\SQLTools\SqlTools\bin\Debug\SqlTools.vshost.exe" -m "staticInserts" -c "Data Source=.\SQL2008R2;Initial Catalog=dfwDemo;User Id=userDiamondFire;Password=userDiamondFire01;" -d "dfwMeath" -t "documents_stored_file;reporting_letter_template;reporting_list_spec;reporting_report_template;reporting_report_parameter;reportingwf_letter_template_workflow_state_access" -o "C:\development\DiamondFireWeb\db\staticdata\03_StaticLetterAndReportTemplates.sql"
-- ==============================================================================================
BEGIN TRANSACTION;
BEGIN TRY

/*
DELETE FROM [club_lotto_ticket_direct_debit]
DELETE FROM [club_person]
*/


If(OBJECT_ID('tempdb..#TempPlayers') Is Not Null)
Begin
    Drop Table #TempPlayers
End

If(OBJECT_ID('tempdb..#TempNumbers') Is Not Null)
Begin
    Drop Table #TempNumbers
End

CREATE TABLE #TempPlayers
(
    ID int, 
    Oid UNIQUEIDENTIFIER, 
	[Forename] [nvarchar](255),
	[Surname] [nvarchar](255),
	[Title] [nvarchar](255),
	[Contact] [nvarchar](255),
	[House] [nvarchar](255),
	[Street] [nvarchar](255),
	[Town] [nvarchar](255),
	[County] [nvarchar](255),
	[Postcode] [nvarchar](255)
)

CREATE TABLE #TempNumbers
(
    ID int, 
	Player int,
    Oid UNIQUEIDENTIFIER, 
	PlayerOid UNIQUEIDENTIFIER,
	[No1] INT,
	[No2] INT,
	[No3] INT,
	[No4] INT
)

Insert Into #TempPlayers
SELECT 
	ID,
	NEWID(),
	[Forename] ,
	[Surname] ,
	[Title] ,
	[Contact] ,
	[House] ,
	[Street] ,
	[Town] ,
	[County] ,
	[Postcode]
FROM [FrRocks].[dbo].[TblPlayers]

Insert Into #TempNumbers
SELECT 
	tblNums.ID,
	tblNums.Player, 
	NEWID(),
	tblPlayers.Oid ,
	[No1] ,
	[No2] ,
	[No3] ,
	[No4]
FROM [FrRocks].[dbo].[TblNumbers] tblNums
INNER JOIN #TempPlayers tblPlayers ON tblNums.Player = tblPlayers.ID

/*
=================================================
INSERT CLUB PERSON
=================================================
*/
INSERT INTO [dbo].[club_person]
	([oid]
	,[orev]
	,[forename]
	,[surname]
	,[comments]
	,[address_number]
	,[address_street]
	,[address_town]
	,[address_county]
	,[address_post_code]
	,[title]
  ,[sex])

SELECT 
    Oid, 
	0,
	[Forename] ,
	[Surname] ,
	[Contact] ,
	[House],
	[Street],
	[Town],
	[County],
	[Postcode],
	(SELECT Oid from [dbo].[club_person_title] WHERE [description] = [title]),
  'M'
FROM #TempPlayers


/*
=================================================
INSERT CLUB LOTTO TICKET
=================================================
*/
INSERT INTO [dbo].[club_lotto_ticket_direct_debit]
	([oid]
	,[orev]
	,[no1]
	,[no2]
	,[no3]
	,[no4]
	,[start_date]
	,[person])
SELECT 
    Oid,
	0,
	[No1],
	[No2],
	[No3],
	[No4],
	'01/01/2016',
	PlayerOid 
FROM #TempNumbers


If(OBJECT_ID('tempdb..#TempPlayers') Is Not Null)
Begin
    Drop Table #TempPlayers
End

If(OBJECT_ID('tempdb..#TempNumbers') Is Not Null)
Begin
    Drop Table #TempNumbers
End



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
