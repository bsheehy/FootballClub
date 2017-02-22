--/*
--  Adds the 'cdc_userid' clumn to every table
--*/
--BEGIN TRANSACTION;
--BEGIN TRY

--declare @table sysname
--declare @qry nvarchar(max)
--select @table = min(t.name)
--from sys.tables t, sys.schemas s
--where t.schema_id = s.schema_id
--and t.is_ms_shipped = 0
--and t.object_id in (select object_id from sys.indexes where type= 1)
--and s.name = 'dbo'


--while @table is not null
--begin
--  -- Add the user id column to each table 'cdc_userid'
--  set @qry = 'if not exists(
--    select * from sys.columns c, sys.objects o, sys.schemas s
--    where c.object_id = o.object_id
--    and o.name = ''' + @table + '''
--    and o.schema_id = s.schema_id
--    and s.name = ''dbo''
--    and c.name = ''cdc_userid'')
--  alter table [dbo].[' + @table +'] add cdc_userid nvarchar(255) default dbo.fnc_Audit_UserGetCurrent()'
--  execute sp_executesql @qry

	
--  select @table = min(t.name)
--  from sys.tables t, sys.schemas s
--  where t.schema_id = s.schema_id
--  and t.is_ms_shipped = 0
--  and t.object_id in (select object_id from sys.indexes where type= 1)
--  and s.name = 'dbo'
--  and t.name > @table
--end

--  END TRY
--BEGIN CATCH
--    IF @@TRANCOUNT > 0
--        ROLLBACK TRANSACTION;

--    SELECT 
--        ERROR_NUMBER() AS ErrorNumber
--        ,ERROR_SEVERITY() AS ErrorSeverity
--        ,ERROR_STATE() AS ErrorState
--        ,ERROR_PROCEDURE() AS ErrorProcedure
--        ,ERROR_LINE() AS ErrorLine
--        ,ERROR_MESSAGE() AS ErrorMessage;

--    DECLARE @ERR NVARCHAR(2000)
--    SET @ERR = 'Error Number  :' + CAST(ERROR_NUMBER() AS VARCHAR) 
--      + ' Error Severity:' + CAST(ERROR_SEVERITY() AS VARCHAR)
--      + ' Error State   :' + CAST(ERROR_STATE() AS VARCHAR)
--      + ' Error Line    :' + CAST(ERROR_LINE() AS VARCHAR)
--      + ' Error Message :' + ERROR_MESSAGE();
--    RAISERROR(@ERR, 20, -1) with log;
--END CATCH;

--IF @@TRANCOUNT > 0
--    COMMIT TRANSACTION;
--GO
