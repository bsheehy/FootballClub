IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_EnableAllTriggers]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_EnableAllTriggers]
GO

/* ***** Object:  StoredProcedure [dbo].[sp_EnableAllTriggers]    Script Date: 06/30/2015 09:04:22 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Brendan Sheehy
-- Create date: 30/June/2015
-- Description:	Used to enable all Triggers on a database.
--				Use this at the end of the data import from existing Diamond Fire database
--				- at the start all Triggers were disabled
-- =============================================
CREATE PROCEDURE [dbo].[sp_EnableAllTriggers]
AS
	DECLARE @string VARCHAR(8000)
	DECLARE @tableName NVARCHAR(500)
	DECLARE cur CURSOR
	FOR SELECT name AS tbname FROM sysobjects WHERE id IN(SELECT parent_obj FROM sysobjects WHERE xtype='tr')
	OPEN cur
		FETCH next FROM cur INTO @tableName
		WHILE @@fetch_status = 0
		BEGIN
			SET @string ='Alter table '+ @tableName + ' Enable trigger all'
			EXEC (@string)
			FETCH next FROM cur INTO @tableName
		END
		CLOSE cur
	DEALLOCATE cur

GO


