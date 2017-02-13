'------------------------------------------------------------ 
'	HEADER   : Utils
'	PURPOSE  : Code generation utils and helper functions
'	VERSION  : 20.53
'	AUTHOR   : ApexSQL 
'	Date     : 2015/6/16
'	NOTES    :
'------------------------------------------------------------

DbCompatibilityLevel = Audit.Database.CompatibilityLevel

Public nTabLength
Public nTabDepth

Sub InitTab(nLength)

	nTabLength = nLength
	nTabDepth = 0

End Sub

Sub IncTabDepth

	nTabDepth = nTabDepth + 1

End Sub

Sub DecTabDepth

	If nTabDepth > 0 Then
		nTabDepth = nTabDepth - 1
	End If

End Sub

Function Tab()

	Dim intTabSpaceLength
	intTabSpaceLength = nTabDepth * nTabLength

	If intTabSpaceLength > 0 Then
		Tab = Space( intTabSpaceLength )
	Else
		Tab = ""
	End If

End Function

Function FieldNumericOrDecimal( objField )

	FieldNumericOrDecimal = (Left(objField.SQL_Datatype,7) = "numeric" Or Left(objField.SQL_Datatype,7) = "decimal")

End Function

Function GetCONVERTOption( strDatatype )

	' for datetime fields use ODBC cannonical format
	if ucase(strDatatype) = "DATETIME" then
		GetCONVERTOption = 121
		exit function
	end if
	
	' for datetime fields use ODBC cannonical format
	if ucase(strDatatype) = "SMALLDATETIME" then
		GetCONVERTOption = 120
		exit function
	end if
	
	if ucase(strDatatype) = "TIME" then
		GetCONVERTOption = 121
		exit function
	end if
	
	if ucase(strDatatype) = "DATETIME2" then
		GetCONVERTOption = 121
		exit function
	end if
	
	if ucase(strDatatype) = "DATETIMEOFFSET" then
		GetCONVERTOption = 121
		exit function
	end if
		
	if ucase(strDatatype) = "FLOAT" or ucase(strDatatype) = "REAL" then
		GetCONVERTOption = 0
		exit function
	end if

	' for money and smallmoney fields use the four digit format
	if ucase(strDatatype) = "MONEY" or ucase(strDatatype) = "SMALLMONEY" then
		GetCONVERTOption = 2
		exit function
	end if

	' for all other types use default formatting
	GetCONVERTOption = 0
	
End Function

Function GetBinaryConvertedDatatype(strName)
	if DbCompatibilityLevel < 80 then
		GetBinaryConvertedDatatype = "CONVERT(varchar(4000)," & strName & ")"
	else
		GetBinaryConvertedDatatype = "dbo.AUDIT_fn_HexToStr(" & strName & ")"
	end if
End Function


Function GetConvertedFieldWithCustomNameDatatype( strDatatype , strName, intString, notkey)
	if strDatatype="timestamp" then
		GetConvertedFieldWithCustomNameDatatype = "CONVERT(varchar(4000), CAST(" & strName & " as bigint))"
		exit function
	end if
	if strDatatype="text" then
		GetConvertedFieldWithCustomNameDatatype = "CONVERT(ntext, " & strName & ")"
		exit function
	end if
	if strDatatype="xml" or strDatatype="geometry" or strDatatype="geography" or strDatatype="hierarchyid" then
		GetConvertedFieldWithCustomNameDatatype = "CONVERT(nvarchar(max), CONVERT(nvarchar(max), " & strName & "))"
		exit function
	end if
	if notkey then
		if strDatatype="image" or strDatatype="ntext" then
			GetConvertedFieldWithCustomNameDatatype = strName
			exit function
		end if
	else
		if strDatatype="image" then
			strName = "CONVERT(varbinary(8000), " & strName & ")"
			GetConvertedFieldWithCustomNameDatatype = GetBinaryConvertedDatatype(strName)
			exit function
		end if
	end if
	if strDatatype="binary" or strDatatype="varbinary" then
		GetConvertedFieldWithCustomNameDatatype = GetBinaryConvertedDatatype(strName)
		exit function
	end if
	if strDatatype="sql_variant" then
		GetConvertedFieldWithCustomNameDatatype = "dbo.AUDIT_fn_SqlVariantToString(" & strName & ", " & intString & ")"
		exit function
	end if
	GetConvertedFieldWithCustomNameDatatype = "CONVERT(nvarchar(4000), " & strName & ", " & GetCONVERTOption(strDatatype) & ")"
End Function

Function GetConvertedFieldWithCustomName( ByRef objField, strName, intString)
	GetConvertedFieldWithCustomName = GetConvertedFieldWithCustomNameDatatype(objField.SQL_Datatype, strName, intString, False)
End Function

Function GetConvertedField( ByRef objField, strParTablePseudoName, intString)
			if Len(strParTablePseudoName) > 0 then
				strName = strParTablePseudoName & "." & "[" & objField.Name & "]"
			else
				strName = "[" & objField.Name & "]"
			end if
			
			GetConvertedField = GetConvertedFieldWithCustomName(objField, strName, intString)
End Function

Function KeyString( objTable, strTablePseudoName )
	KeyString = ""
	' get each field for row key
	KeyString = MakeKeyString( objTable.Fields, strTablePseudoName )
End Function

Function MakeKeyString( ByRef objCollection, strTablePseudoName )
	MakeKeyString = ""
	
	For k = 1 To objCollection.Count				
		Set objKey = objCollection.Item(k)
	
		if objKey.IsInRowKey then

			If Len(MakeKeyString) > 0 Then
				MakeKeyString = MakeKeyString & " + ' AND ' + "
			End If

			Datatype = objKey.SQL_Datatype
			convertedName = replace(objKey.Name, "'", "''") 
			if Datatype="timestamp" _
				or Datatype="sql_variant" _
				or Datatype="binary" _
				or Datatype="varbinary" _
				or Datatype="int" _
				or Datatype="tinyint" _
				or Datatype="smallint" _
				or Datatype="real" _
				or Datatype="float" _
				or Datatype="bit" _
				or Datatype="decimal" _
				or Datatype="numeric" _
				or Datatype="bigint" _
				then
				MakeKeyString = MakeKeyString & "IsNull(" & _
					"'[" & convertedName & "]='+" & GetConvertedField(objKey, strTablePseudoName, 1) & _
					", '[" & convertedName & "] Is Null')"
			else
				MakeKeyString = MakeKeyString & "IsNull(" & _
					"'[" & convertedName & "]=N'''+replace(" & GetConvertedField(objKey, strTablePseudoName, 1) & _
					", '''', '''''')+'''', '[" & convertedName & "] Is Null')"
			end if
		End If
	Next
End Function
 
Function KeyEqualFieldsStringWithNulls( ByRef objCollection, strTablePseudoName1, strTablePseudoName2 )
	KeyEqualFieldsStringWithNulls = ""

	For k = 1 To objCollection.Count				
		Set objKey = objCollection.Item(k)

		if not objKey.IsBLOB and objKey.SQL_Datatype<>"xml" and objKey.IsInRowKey then

			If Len(KeyEqualFieldsStringWithNulls) > 0 Then
				KeyEqualFieldsStringWithNulls = KeyEqualFieldsStringWithNulls & " AND "
			End If

			KeyEqualFieldsStringWithNulls = KeyEqualFieldsStringWithNulls & _
				"(" & GetConvertedField(objkey, strTablePseudoName1, 1) & "=" & GetConvertedField(objkey, strTablePseudoName2, 1) & _
				" or (" & strTablePseudoName1 & ".[" & objkey.Name & "] Is Null and " & strTablePseudoName2 & ".[" & objkey.Name & "] Is Null))"
	
		End If
	Next
End Function

Function KeyEqualFieldsWithNulls( objTable, strTablePseudoName1, strTablePseudoName2 )
	KeyEqualFieldsWithNulls = ""
	' get each field for row key
	KeyEqualFieldsWithNulls = KeyEqualFieldsStringWithNulls( objTable.Fields, strTablePseudoName1, strTablePseudoName2 )
End Function

Function MakeKeyStringWithNotNull( ByRef objCollection, strTablePseudoName1, strTablePseudoName2 )
	MakeKeyStringWithNotNull = ""
	
	For k = 1 To objCollection.Count				
		Set objKey = objCollection.Item(k)
		
		if objKey.IsInRowKey then

			If Len(MakeKeyStringWithNotNull) > 0 Then
				MakeKeyStringWithNotNull = MakeKeyStringWithNotNull &  "+' AND '+"
			End If
						
			if objKey.SQL_Datatype = "xml" or objKey.SQL_Datatype = "geography" or objKey.SQL_Datatype = "geometry" or objKey.SQL_Datatype = "hierarchyid" then
				strName = "IsNull(Convert(nvarchar(max), " & strTablePseudoName1 & ".[" & objKey.Name & "]), " & "Convert(nvarchar(max), " & strTablePseudoName2 & ".[" & objKey.Name & "])) "
			else
				strName = "IsNull(" & strTablePseudoName1 & ".[" & objKey.Name & "], " & strTablePseudoName2 & ".[" & objKey.Name & "])"		
			end if
			
			
		
			if objKey.SQL_Datatype="timestamp" _
				or objKey.SQL_Datatype="sql_variant" _
				or objKey.SQL_Datatype="binary" _
				or objKey.SQL_Datatype="varbinary" _
				or objKey.SQL_Datatype="int" _
				or objKey.SQL_Datatype="tinyint" _
				or objKey.SQL_Datatype="smallint" _
				or objKey.SQL_Datatype="real" _
				or objKey.SQL_Datatype="float" _
				or objKey.SQL_Datatype="bit" _
				or objKey.SQL_Datatype="decimal" _
				or objKey.SQL_Datatype="numeric" _
				or objKey.SQL_Datatype="bigint" _
				then
				MakeKeyStringWithNotNull = MakeKeyStringWithNotNull & "IsNull('[" & replace(objKey.Name, "'", "''") & "]='+" & _
					GetConvertedFieldWithCustomName( objKey, strName, 1) & ", '[" & replace(objKey.Name, "'", "''") & "] Is Null')"
			else
				MakeKeyStringWithNotNull = MakeKeyStringWithNotNull & "IsNull('[" & replace(objKey.Name, "'", "''") & "]=N'''+replace(" & _
					GetConvertedFieldWithCustomName( objKey, strName, 1) & ",'''' ,'''''')+'''', '[" & replace(objKey.Name, "'", "''") & "] Is Null')"
			end if
		
		end if
	Next
End Function

Function KeyStringWithNotNull( objTable, strTablePseudoName1, strTablePseudoName2 )
	KeyStringWithNotNull = ""
	' get field for row key
	KeyStringWithNotNull = MakeKeyStringWithNotNull( objTable.Fields, strTablePseudoName1, strTablePseudoName2 )
End Function