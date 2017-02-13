/* ***** Object:  StoredProcedure [dbo].[sp_DisableAllTriggers]    Script Date: 06/30/2015 09:03:37 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_DisableAllTriggers]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_DisableAllTriggers]
GO

/* ***** Object:  StoredProcedure [dbo].[sp_DisableAllTriggers]    Script Date: 06/30/2015 09:03:37 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Brendan Sheehy
-- Create date: 30/June/2015
-- Description:	Used to disabe all Triggers on a database.
--				Use this at the start of the data import from existing Diamond Fire database
--				then use sp_EnableAllTriggers at  end of import.
-- =============================================
CREATE PROCEDURE [dbo].[sp_DisableAllTriggers]
AS
	DECLARE @string VARCHAR(8000)
	DECLARE @tableName NVARCHAR(500)
	DECLARE cur CURSOR
	FOR SELECT name AS tbname FROM sysobjects WHERE id IN(SELECT parent_obj FROM sysobjects WHERE xtype='tr')
	OPEN cur
		FETCH next FROM cur INTO @tableName
		WHILE @@fetch_status = 0
		BEGIN
			SET @string ='Alter table '+ @tableName + ' Disable trigger all'
			EXEC (@string)
			FETCH next FROM cur INTO @tableName
		END
		CLOSE cur
	DEALLOCATE cur

GO

