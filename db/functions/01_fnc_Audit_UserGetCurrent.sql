/****** Object:  UserDefinedFunction [dbo].[fnc_Audit_UserGetCurrent]    Script Date: 02/18/2016 14:32:06 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fnc_Audit_UserGetCurrent]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[fnc_Audit_UserGetCurrent]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER OFF
GO

/*
-- =============================================
	Author:			Brendan Sheehy
	Create date:	18\Feb\2016
	Description:	Use to return the Current user name - copied from Franks [user_current_get] function
-- =============================================
*/
CREATE function [dbo].[fnc_Audit_UserGetCurrent]()
returns nvarchar(255) 
as
begin
  declare @result nvarchar(255)
  declare @sessionId nvarchar(56)
  select @sessionId = cast(CONTEXT_INFO() as nvarchar(56))
  
  select top 1 @result = u.[login]
  from core_user u
  join core_application_session s on s.[user] = u.oid
  where id = @sessionId
  order by [date] desc

  if (@result is null)
    select @result = system_user
    
  return @result
end

GO

