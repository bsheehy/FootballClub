===========================================================
        ApexSQL Audit
===========================================================
If you are opening the dfwProject_ApexSQL2053.axap in ApexSQL Audit application then the Utils2053.inc (C:\Program Files\ApexSQL\ApexSQLTrigger2015) is required to be in the same folder as the dfwProject_ApexSQL2053.axap and the dfwTemplate_ApexSQL2053.audx files or else it will fail.



===========================================================
        The following tables SHOULD NOT BE AUDITED:
===========================================================
1. All Tables using the formsData schema
2. domainfire_i_plan
3. domainfire_i_plan_temp
4. ELMAH_Error




