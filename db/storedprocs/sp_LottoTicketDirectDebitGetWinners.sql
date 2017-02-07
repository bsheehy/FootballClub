IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_LottoTicketDirectDebitGetWinners]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_LottoTicketDirectDebitGetWinners]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Brendan Sheehy
-- Create date: 24 December 2016
-- Description:	Returns data about tickets where there are 3 or more matches.
-- =============================================
CREATE PROCEDURE [dbo].[sp_LottoTicketDirectDebitGetWinners]
	@No1 INT , 
  @No2 INT , 
  @No3 INT , 
  @No4 INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

  ;WITH tblWinningTickets AS 
  (
	  SELECT 
		  tblA.[oid] ,tblA.[person], tblA.[no1], tblA.[no2], tblA.[no3], tblA.[no4],
		  tblPerson.forename, tblPerson.surname,
		  CASE  
			   WHEN tblA.[no1] IN (@No1,@No2,@No3,@No4) THEN 1 
			   ELSE 0 
		  END AS Match1,
		  CASE  
			   WHEN tblA.[no2] IN (@No1,@No2,@No3,@No4) THEN 1 
			   ELSE 0 
		  END AS Match2,
		  CASE  
			   WHEN tblA.[no3] IN (@No1,@No2,@No3,@No4) THEN 1 
			   ELSE 0 
		  END AS Match3,
		  CASE  
			   WHEN tblA.[no4] IN (@No1,@No2,@No3,@No4) THEN 1 
			   ELSE 0 
		  END AS Match4,
			  CASE WHEN tblA.[no1] IN (@No1,@No2,@No3,@No4) THEN 1 ELSE 0 END + 
			  CASE WHEN tblA.[no2] IN (@No1,@No2,@No3,@No4) THEN 1 ELSE 0 END + 
			  CASE WHEN tblA.[no3] IN (@No1,@No2,@No3,@No4) THEN 1 ELSE 0 END + 
			  CASE WHEN tblA.[no4] IN (@No1,@No2,@No3,@No4) THEN 1 ELSE 0 END
		  AS TotalMatches
	  FROM [dbo].[club_lotto_ticket_direct_debit] tblA
	  INNER JOIN [dbo].[club_person] tblPerson ON tblPerson.oid = tblA.person
  )
  SELECT * FROM tblWinningTickets
  WHERE TotalMatches>=3
  ORDER BY TotalMatches DESC
END
GO


