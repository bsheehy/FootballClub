/*
	########################################################################################################################
		This scripts should be called AFTER ALL import scripts are applied.
	########################################################################################################################
    1. Enable all ApexSQL Triggers
	########################################################################################################################
  */

  --ENABLE ALL APEXSQL AUDIT TRIGGERS TO START
  Print 'Info: Enabling all ApexSQL Audit Triggers'
  IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_Audit_EnableAuditTriggers]') AND type in (N'P', N'PC'))
  EXEC [dbo].[sp_Audit_EnableAuditTriggers]
