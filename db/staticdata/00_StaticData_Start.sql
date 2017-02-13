/*
	########################################################################################################################
		This scripts should be called FIRST before any import scripts are applied.
	########################################################################################################################
    1. Disable all ApexSQL Triggers
	########################################################################################################################
  */

    --DISABLE ALL APEXSQL AUDIT TRIGGERS TO START
  Print 'Info: Disabling all ApexSQL Audit Triggers'
  IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_Audit_DisableAuditTriggers]') AND type in (N'P', N'PC'))
  EXEC [dbo].[sp_Audit_DisableAuditTriggers]
