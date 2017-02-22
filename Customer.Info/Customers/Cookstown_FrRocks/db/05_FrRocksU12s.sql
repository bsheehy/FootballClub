-- ==============================================================================================
--				File automatically created by SqlTools DoStaticInserts method.
--				Command Line used: "C:\development\SQLTools\SqlTools\bin\Debug\SqlTools.vshost.exe" -m "staticInserts" -c "Data Source=.\SQL2012;Initial Catalog=ClubFrRocks;User Id=sa;Password=Mallon2012;" -d "ClubFrRocks" -t "club_person;club_person_guardian;club_team_member" -o "C:\Temp\Ouput_SQL.sql"
-- ==============================================================================================
BEGIN TRANSACTION;
BEGIN TRY
-- ==============================================================================================
--					 [club_person] Static Data 
-- ==============================================================================================
SET IDENTITY_INSERT [dbo].[club_person] ON 


IF NOT EXISTS(SELECT * FROM [club_person] WHERE [Oid] = N'9f0f590d-3cbd-4d30-9eea-04e5912cd553')
BEGIN
	INSERT [dbo].[club_person] ([oid], [orev], [key], [sex], [active], [forename], [surname], [dob], [phone], [mobile_no], [email], [alergies], [alergies_details], [comments], [player_profile], [address_number], [address_street], [address_town], [address_county], [address_country], [address_post_code], [address_xlng_coord], [address_ylat_coord], [title]) VALUES (N'9f0f590d-3cbd-4d30-9eea-04e5912cd553', 0, 505, N'0', 1, N'Michael', N'Leek', NULL, N'07808131089', NULL, NULL, 1, NULL, NULL, NULL, N'33', N'Greenvale Drive', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
END

IF NOT EXISTS(SELECT * FROM [club_person] WHERE [Oid] = N'd596d4ea-d6b0-4ae9-92b5-0808e800960f')
BEGIN
	INSERT [dbo].[club_person] ([oid], [orev], [key], [sex], [active], [forename], [surname], [dob], [phone], [mobile_no], [email], [alergies], [alergies_details], [comments], [player_profile], [address_number], [address_street], [address_town], [address_county], [address_country], [address_post_code], [address_xlng_coord], [address_ylat_coord], [title]) VALUES (N'd596d4ea-d6b0-4ae9-92b5-0808e800960f', 0, 493, N'M', 1, N'Conall', N'Sheehy', CAST(0x0000976F00000000 AS DateTime), N'07715206598
07743311667', NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
END

IF NOT EXISTS(SELECT * FROM [club_person] WHERE [Oid] = N'63c3b555-fae3-414f-b0e5-0c4b56e61108')
BEGIN
	INSERT [dbo].[club_person] ([oid], [orev], [key], [sex], [active], [forename], [surname], [dob], [phone], [mobile_no], [email], [alergies], [alergies_details], [comments], [player_profile], [address_number], [address_street], [address_town], [address_county], [address_country], [address_post_code], [address_xlng_coord], [address_ylat_coord], [title]) VALUES (N'63c3b555-fae3-414f-b0e5-0c4b56e61108', 0, 487, N'M', 1, N'Daragh', N'Mullin', CAST(0x0000959200000000 AS DateTime), N'86758801
07789790685
07802510172', NULL, NULL, 0, NULL, NULL, NULL, N'10', N'Mallon Way', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
END

IF NOT EXISTS(SELECT * FROM [club_person] WHERE [Oid] = N'76247131-8ec0-4918-acc0-0d94679ad822')
BEGIN
	INSERT [dbo].[club_person] ([oid], [orev], [key], [sex], [active], [forename], [surname], [dob], [phone], [mobile_no], [email], [alergies], [alergies_details], [comments], [player_profile], [address_number], [address_street], [address_town], [address_county], [address_country], [address_post_code], [address_xlng_coord], [address_ylat_coord], [title]) VALUES (N'76247131-8ec0-4918-acc0-0d94679ad822', 0, 515, N'0', 1, N'Geraldine and Raymond', N'Quinn', NULL, N'07903975092', NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
END

IF NOT EXISTS(SELECT * FROM [club_person] WHERE [Oid] = N'78edd860-6cea-4281-afbd-0e9cd914dd3a')
BEGIN
	INSERT [dbo].[club_person] ([oid], [orev], [key], [sex], [active], [forename], [surname], [dob], [phone], [mobile_no], [email], [alergies], [alergies_details], [comments], [player_profile], [address_number], [address_street], [address_town], [address_county], [address_country], [address_post_code], [address_xlng_coord], [address_ylat_coord], [title]) VALUES (N'78edd860-6cea-4281-afbd-0e9cd914dd3a', 0, 472, N'M', 1, N'Ronan', N'Devlin', CAST(0x0000955400000000 AS DateTime), N'07742792649', NULL, NULL, 0, NULL, NULL, NULL, N'65', N'Greevale Drive', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
END

IF NOT EXISTS(SELECT * FROM [club_person] WHERE [Oid] = N'2c65d642-aabf-468f-af80-17fe03126fac')
BEGIN
	INSERT [dbo].[club_person] ([oid], [orev], [key], [sex], [active], [forename], [surname], [dob], [phone], [mobile_no], [email], [alergies], [alergies_details], [comments], [player_profile], [address_number], [address_street], [address_town], [address_county], [address_country], [address_post_code], [address_xlng_coord], [address_ylat_coord], [title]) VALUES (N'2c65d642-aabf-468f-af80-17fe03126fac', 0, 509, N'0', 1, N'Declan and Claire Mc Hugh', N'Mc Hugh', NULL, N'07885857725
07967819356', NULL, NULL, 1, NULL, NULL, NULL, N'2', N'Westland Court', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
END

IF NOT EXISTS(SELECT * FROM [club_person] WHERE [Oid] = N'0092e3d9-0dbb-4393-a286-2786c895b5cc')
BEGIN
	INSERT [dbo].[club_person] ([oid], [orev], [key], [sex], [active], [forename], [surname], [dob], [phone], [mobile_no], [email], [alergies], [alergies_details], [comments], [player_profile], [address_number], [address_street], [address_town], [address_county], [address_country], [address_post_code], [address_xlng_coord], [address_ylat_coord], [title]) VALUES (N'0092e3d9-0dbb-4393-a286-2786c895b5cc', 0, 475, N'M', 1, N'Keighan', N'Hampshey', CAST(0x0000965A00000000 AS DateTime), N'07990893275', NULL, NULL, 0, NULL, NULL, NULL, N'1A', N'Westbury', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
END

IF NOT EXISTS(SELECT * FROM [club_person] WHERE [Oid] = N'18d396ed-49a9-40c2-868b-2e6bcc4a831a')
BEGIN
	INSERT [dbo].[club_person] ([oid], [orev], [key], [sex], [active], [forename], [surname], [dob], [phone], [mobile_no], [email], [alergies], [alergies_details], [comments], [player_profile], [address_number], [address_street], [address_town], [address_county], [address_country], [address_post_code], [address_xlng_coord], [address_ylat_coord], [title]) VALUES (N'18d396ed-49a9-40c2-868b-2e6bcc4a831a', 0, 519, N'0', 1, N'Nial ', N'Falls', NULL, N'07779 042 578
07779042576', NULL, NULL, 1, NULL, NULL, NULL, NULL, N'Muff Road', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
END

IF NOT EXISTS(SELECT * FROM [club_person] WHERE [Oid] = N'1c6d64b9-42de-48cd-aaeb-3c60a463ff42')
BEGIN
	INSERT [dbo].[club_person] ([oid], [orev], [key], [sex], [active], [forename], [surname], [dob], [phone], [mobile_no], [email], [alergies], [alergies_details], [comments], [player_profile], [address_number], [address_street], [address_town], [address_county], [address_country], [address_post_code], [address_xlng_coord], [address_ylat_coord], [title]) VALUES (N'1c6d64b9-42de-48cd-aaeb-3c60a463ff42', 0, 482, N'M', 1, N'Thomas', N'Mc Caffery', CAST(0x0000973C00000000 AS DateTime), N'07773774559', NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
END

IF NOT EXISTS(SELECT * FROM [club_person] WHERE [Oid] = N'e55260d8-9ff7-4f5f-b4f3-3c96d8599940')
BEGIN
	INSERT [dbo].[club_person] ([oid], [orev], [key], [sex], [active], [forename], [surname], [dob], [phone], [mobile_no], [email], [alergies], [alergies_details], [comments], [player_profile], [address_number], [address_street], [address_town], [address_county], [address_country], [address_post_code], [address_xlng_coord], [address_ylat_coord], [title]) VALUES (N'e55260d8-9ff7-4f5f-b4f3-3c96d8599940', 0, 501, N'0', 1, N'Aidan & Tracey ', N'Devlin', NULL, N'07909982033', NULL, NULL, 1, NULL, NULL, NULL, N'14', N'Mallon Manor', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
END

IF NOT EXISTS(SELECT * FROM [club_person] WHERE [Oid] = N'0a64d537-d334-45ac-b9a0-43d3634b7a4a')
BEGIN
	INSERT [dbo].[club_person] ([oid], [orev], [key], [sex], [active], [forename], [surname], [dob], [phone], [mobile_no], [email], [alergies], [alergies_details], [comments], [player_profile], [address_number], [address_street], [address_town], [address_county], [address_country], [address_post_code], [address_xlng_coord], [address_ylat_coord], [title]) VALUES (N'0a64d537-d334-45ac-b9a0-43d3634b7a4a', 0, 513, N'0', 1, N'Kate', N'Oneill', NULL, N'07880752021', NULL, NULL, 1, NULL, NULL, NULL, N'24', N'Westbury Drive', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
END

IF NOT EXISTS(SELECT * FROM [club_person] WHERE [Oid] = N'c1ebdecc-88da-42ac-ad0d-46302c13fca3')
BEGIN
	INSERT [dbo].[club_person] ([oid], [orev], [key], [sex], [active], [forename], [surname], [dob], [phone], [mobile_no], [email], [alergies], [alergies_details], [comments], [player_profile], [address_number], [address_street], [address_town], [address_county], [address_country], [address_post_code], [address_xlng_coord], [address_ylat_coord], [title]) VALUES (N'c1ebdecc-88da-42ac-ad0d-46302c13fca3', 0, 473, N'M', 1, N'Matthew', N'Donaghy', CAST(0x0000954400000000 AS DateTime), N'86764541
07876105854', NULL, NULL, 0, NULL, NULL, NULL, N'23', N'Dirnan Road', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
END

IF NOT EXISTS(SELECT * FROM [club_person] WHERE [Oid] = N'17b74593-5b56-4233-a0c2-49a80bf081be')
BEGIN
	INSERT [dbo].[club_person] ([oid], [orev], [key], [sex], [active], [forename], [surname], [dob], [phone], [mobile_no], [email], [alergies], [alergies_details], [comments], [player_profile], [address_number], [address_street], [address_town], [address_county], [address_country], [address_post_code], [address_xlng_coord], [address_ylat_coord], [title]) VALUES (N'17b74593-5b56-4233-a0c2-49a80bf081be', 0, 470, N'M', 1, N'Luke', N'Cullen', CAST(0x0000959C00000000 AS DateTime), N'07592279909', NULL, NULL, 0, NULL, NULL, NULL, N'63', N'Ratheen Ave', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
END

IF NOT EXISTS(SELECT * FROM [club_person] WHERE [Oid] = N'd4777c5f-3a69-499b-a5f2-555249e53df0')
BEGIN
	INSERT [dbo].[club_person] ([oid], [orev], [key], [sex], [active], [forename], [surname], [dob], [phone], [mobile_no], [email], [alergies], [alergies_details], [comments], [player_profile], [address_number], [address_street], [address_town], [address_county], [address_country], [address_post_code], [address_xlng_coord], [address_ylat_coord], [title]) VALUES (N'd4777c5f-3a69-499b-a5f2-555249e53df0', 0, 486, N'M', 1, N'Daragh', N'Mc Hugh', CAST(0x000098FE00000000 AS DateTime), N'07885857725
07967819356', NULL, NULL, 0, NULL, NULL, NULL, N'2', N'Westland Court', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
END

IF NOT EXISTS(SELECT * FROM [club_person] WHERE [Oid] = N'e448e999-3c6b-4e5f-b5b0-581b1f69dd60')
BEGIN
	INSERT [dbo].[club_person] ([oid], [orev], [key], [sex], [active], [forename], [surname], [dob], [phone], [mobile_no], [email], [alergies], [alergies_details], [comments], [player_profile], [address_number], [address_street], [address_town], [address_county], [address_country], [address_post_code], [address_xlng_coord], [address_ylat_coord], [title]) VALUES (N'e448e999-3c6b-4e5f-b5b0-581b1f69dd60', 0, 481, N'M', 1, N'Michael', N'Mc Aleer', CAST(0x000095A300000000 AS DateTime), N'07771900300
86758529', NULL, NULL, 0, NULL, NULL, NULL, N'130A', N'Drum Road', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
END

IF NOT EXISTS(SELECT * FROM [club_person] WHERE [Oid] = N'd06fc0a7-827d-448b-9204-5a9a367bfba6')
BEGIN
	INSERT [dbo].[club_person] ([oid], [orev], [key], [sex], [active], [forename], [surname], [dob], [phone], [mobile_no], [email], [alergies], [alergies_details], [comments], [player_profile], [address_number], [address_street], [address_town], [address_county], [address_country], [address_post_code], [address_xlng_coord], [address_ylat_coord], [title]) VALUES (N'd06fc0a7-827d-448b-9204-5a9a367bfba6', 0, 520, N'0', 1, N'Padraig', N'Hughes', NULL, N'07803228706
86761423', NULL, NULL, 1, NULL, NULL, NULL, N'9', N'Golf View', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
END

IF NOT EXISTS(SELECT * FROM [club_person] WHERE [Oid] = N'bf8c0fbe-a064-4837-a5e6-61ee4be79d68')
BEGIN
	INSERT [dbo].[club_person] ([oid], [orev], [key], [sex], [active], [forename], [surname], [dob], [phone], [mobile_no], [email], [alergies], [alergies_details], [comments], [player_profile], [address_number], [address_street], [address_town], [address_county], [address_country], [address_post_code], [address_xlng_coord], [address_ylat_coord], [title]) VALUES (N'bf8c0fbe-a064-4837-a5e6-61ee4be79d68', 0, 518, N'0', 1, N'Marty', N'Devlin', NULL, N'07754058450
07748575443', NULL, NULL, 1, NULL, NULL, NULL, N'23', N'Castlecourt', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
END

IF NOT EXISTS(SELECT * FROM [club_person] WHERE [Oid] = N'be5e1aa5-c6d6-467d-b4cf-6e38a4537a10')
BEGIN
	INSERT [dbo].[club_person] ([oid], [orev], [key], [sex], [active], [forename], [surname], [dob], [phone], [mobile_no], [email], [alergies], [alergies_details], [comments], [player_profile], [address_number], [address_street], [address_town], [address_county], [address_country], [address_post_code], [address_xlng_coord], [address_ylat_coord], [title]) VALUES (N'be5e1aa5-c6d6-467d-b4cf-6e38a4537a10', 0, 511, N'0', 1, N'Donal', N'Neeson', NULL, N'07557640021', NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
END

IF NOT EXISTS(SELECT * FROM [club_person] WHERE [Oid] = N'286bcb93-12ca-4316-82cf-6e547deb8bf1')
BEGIN
	INSERT [dbo].[club_person] ([oid], [orev], [key], [sex], [active], [forename], [surname], [dob], [phone], [mobile_no], [email], [alergies], [alergies_details], [comments], [player_profile], [address_number], [address_street], [address_town], [address_county], [address_country], [address_post_code], [address_xlng_coord], [address_ylat_coord], [title]) VALUES (N'286bcb93-12ca-4316-82cf-6e547deb8bf1', 0, 477, N'M', 1, N'Cuba', N'Kupilas', CAST(0x0000977600000000 AS DateTime), N'07845140441', NULL, NULL, 0, NULL, NULL, NULL, N'7', N'Rectory Park', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
END

IF NOT EXISTS(SELECT * FROM [club_person] WHERE [Oid] = N'b0a07f4e-e716-4e57-87f8-6e7434ac3a26')
BEGIN
	INSERT [dbo].[club_person] ([oid], [orev], [key], [sex], [active], [forename], [surname], [dob], [phone], [mobile_no], [email], [alergies], [alergies_details], [comments], [player_profile], [address_number], [address_street], [address_town], [address_county], [address_country], [address_post_code], [address_xlng_coord], [address_ylat_coord], [title]) VALUES (N'b0a07f4e-e716-4e57-87f8-6e7434ac3a26', 0, 500, N'0', 1, N'Lisa & Eammon ', N'Cullen', NULL, N'07919243521
07783761420', NULL, NULL, 1, NULL, NULL, NULL, N'49', N'Rathbeg ', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
END

IF NOT EXISTS(SELECT * FROM [club_person] WHERE [Oid] = N'2f581c10-bd48-4fff-ae10-749d45171fe5')
BEGIN
	INSERT [dbo].[club_person] ([oid], [orev], [key], [sex], [active], [forename], [surname], [dob], [phone], [mobile_no], [email], [alergies], [alergies_details], [comments], [player_profile], [address_number], [address_street], [address_town], [address_county], [address_country], [address_post_code], [address_xlng_coord], [address_ylat_coord], [title]) VALUES (N'2f581c10-bd48-4fff-ae10-749d45171fe5', 0, 516, N'0', 1, N'Caroline', N'Sheehy', NULL, N'07715206598
07743311667', NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
END

IF NOT EXISTS(SELECT * FROM [club_person] WHERE [Oid] = N'cc623322-46c1-424e-8bd3-8029d4af05d7')
BEGIN
	INSERT [dbo].[club_person] ([oid], [orev], [key], [sex], [active], [forename], [surname], [dob], [phone], [mobile_no], [email], [alergies], [alergies_details], [comments], [player_profile], [address_number], [address_street], [address_town], [address_county], [address_country], [address_post_code], [address_xlng_coord], [address_ylat_coord], [title]) VALUES (N'cc623322-46c1-424e-8bd3-8029d4af05d7', 0, 502, N'0', 1, N'Barry ', N'Eccles', NULL, N'07860119478', NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
END

IF NOT EXISTS(SELECT * FROM [club_person] WHERE [Oid] = N'512385d4-8239-4b97-8a13-863bacfaec71')
BEGIN
	INSERT [dbo].[club_person] ([oid], [orev], [key], [sex], [active], [forename], [surname], [dob], [phone], [mobile_no], [email], [alergies], [alergies_details], [comments], [player_profile], [address_number], [address_street], [address_town], [address_county], [address_country], [address_post_code], [address_xlng_coord], [address_ylat_coord], [title]) VALUES (N'512385d4-8239-4b97-8a13-863bacfaec71', 0, 471, N'M', 1, N'Jake', N'Devlin', CAST(0x0000973C00000000 AS DateTime), N'07909982033', NULL, NULL, 1, N'Alergic to penacillon', NULL, NULL, N'14', N'Mallon Manor', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
END

IF NOT EXISTS(SELECT * FROM [club_person] WHERE [Oid] = N'd67694f5-b56e-482a-9c4d-883132c0369f')
BEGIN
	INSERT [dbo].[club_person] ([oid], [orev], [key], [sex], [active], [forename], [surname], [dob], [phone], [mobile_no], [email], [alergies], [alergies_details], [comments], [player_profile], [address_number], [address_street], [address_town], [address_county], [address_country], [address_post_code], [address_xlng_coord], [address_ylat_coord], [title]) VALUES (N'd67694f5-b56e-482a-9c4d-883132c0369f', 0, 496, N'M', 1, N'Conor', N'Falls', CAST(0x0000946200000000 AS DateTime), N'07779 042 578
07779042576', NULL, NULL, 0, NULL, NULL, NULL, NULL, N'Muff Road', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
END

IF NOT EXISTS(SELECT * FROM [club_person] WHERE [Oid] = N'752e3337-b72d-4a88-a5e1-88d37011a123')
BEGIN
	INSERT [dbo].[club_person] ([oid], [orev], [key], [sex], [active], [forename], [surname], [dob], [phone], [mobile_no], [email], [alergies], [alergies_details], [comments], [player_profile], [address_number], [address_street], [address_town], [address_county], [address_country], [address_post_code], [address_xlng_coord], [address_ylat_coord], [title]) VALUES (N'752e3337-b72d-4a88-a5e1-88d37011a123', 0, 499, N'0', 1, N'Paul and Louise ', N'Byrne', NULL, N'07936923193', NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
END

IF NOT EXISTS(SELECT * FROM [club_person] WHERE [Oid] = N'b816ef7c-90fd-48ca-8c6b-8e4b3dad5188')
BEGIN
	INSERT [dbo].[club_person] ([oid], [orev], [key], [sex], [active], [forename], [surname], [dob], [phone], [mobile_no], [email], [alergies], [alergies_details], [comments], [player_profile], [address_number], [address_street], [address_town], [address_county], [address_country], [address_post_code], [address_xlng_coord], [address_ylat_coord], [title]) VALUES (N'b816ef7c-90fd-48ca-8c6b-8e4b3dad5188', 0, 490, N'M', 1, N'Connor', N'O Neill', CAST(0x000094BC00000000 AS DateTime), N'07880752021', NULL, NULL, 1, N'MILD ASTHMA                               ', NULL, NULL, N'24', N'Westbury Drive', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
END

IF NOT EXISTS(SELECT * FROM [club_person] WHERE [Oid] = N'47ddc8eb-3150-4ebf-8485-90a24a299b5c')
BEGIN
	INSERT [dbo].[club_person] ([oid], [orev], [key], [sex], [active], [forename], [surname], [dob], [phone], [mobile_no], [email], [alergies], [alergies_details], [comments], [player_profile], [address_number], [address_street], [address_town], [address_county], [address_country], [address_post_code], [address_xlng_coord], [address_ylat_coord], [title]) VALUES (N'47ddc8eb-3150-4ebf-8485-90a24a299b5c', 0, 492, N'M', 1, N'Jude', N'Quinn', CAST(0x0000973C00000000 AS DateTime), N'07903975092', NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
END

IF NOT EXISTS(SELECT * FROM [club_person] WHERE [Oid] = N'7f65f53a-07b9-463c-8e59-962ea1626ad0')
BEGIN
	INSERT [dbo].[club_person] ([oid], [orev], [key], [sex], [active], [forename], [surname], [dob], [phone], [mobile_no], [email], [alergies], [alergies_details], [comments], [player_profile], [address_number], [address_street], [address_town], [address_county], [address_country], [address_post_code], [address_xlng_coord], [address_ylat_coord], [title]) VALUES (N'7f65f53a-07b9-463c-8e59-962ea1626ad0', 0, 489, N'M', 1, N'Ryan', N'Nugent', CAST(0x000095EF00000000 AS DateTime), N'07709143714', NULL, NULL, 0, NULL, NULL, NULL, N'21', N'Garden Mews', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
END

IF NOT EXISTS(SELECT * FROM [club_person] WHERE [Oid] = N'fdeb2653-3bc2-44a2-b00b-9665025b5db6')
BEGIN
	INSERT [dbo].[club_person] ([oid], [orev], [key], [sex], [active], [forename], [surname], [dob], [phone], [mobile_no], [email], [alergies], [alergies_details], [comments], [player_profile], [address_number], [address_street], [address_town], [address_county], [address_country], [address_post_code], [address_xlng_coord], [address_ylat_coord], [title]) VALUES (N'fdeb2653-3bc2-44a2-b00b-9665025b5db6', 0, 479, N'M', 1, N'Ben', N'Leek', CAST(0x000095CF00000000 AS DateTime), N'07808131089', NULL, NULL, 1, N'Alergic to nuts', NULL, NULL, N'33', N'Greenvale Drive', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
END

IF NOT EXISTS(SELECT * FROM [club_person] WHERE [Oid] = N'afe66c48-5f26-40cf-8cd4-9b4766892a99')
BEGIN
	INSERT [dbo].[club_person] ([oid], [orev], [key], [sex], [active], [forename], [surname], [dob], [phone], [mobile_no], [email], [alergies], [alergies_details], [comments], [player_profile], [address_number], [address_street], [address_town], [address_county], [address_country], [address_post_code], [address_xlng_coord], [address_ylat_coord], [title]) VALUES (N'afe66c48-5f26-40cf-8cd4-9b4766892a99', 0, 512, N'0', 1, N'Lisa', N'Bell', NULL, N'07709143714', NULL, NULL, 1, NULL, NULL, NULL, N'21', N'Garden Mews', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
END

IF NOT EXISTS(SELECT * FROM [club_person] WHERE [Oid] = N'f928ba09-8a12-49af-87cd-a272b10b7e58')
BEGIN
	INSERT [dbo].[club_person] ([oid], [orev], [key], [sex], [active], [forename], [surname], [dob], [phone], [mobile_no], [email], [alergies], [alergies_details], [comments], [player_profile], [address_number], [address_street], [address_town], [address_county], [address_country], [address_post_code], [address_xlng_coord], [address_ylat_coord], [title]) VALUES (N'f928ba09-8a12-49af-87cd-a272b10b7e58', 0, 466, N'M', 1, N'Jamie', N'Barry', CAST(0x000098A900000000 AS DateTime), N'07874719128', NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
END

IF NOT EXISTS(SELECT * FROM [club_person] WHERE [Oid] = N'a4e4e0f3-cd92-4d89-885f-a60848df33b2')
BEGIN
	INSERT [dbo].[club_person] ([oid], [orev], [key], [sex], [active], [forename], [surname], [dob], [phone], [mobile_no], [email], [alergies], [alergies_details], [comments], [player_profile], [address_number], [address_street], [address_town], [address_county], [address_country], [address_post_code], [address_xlng_coord], [address_ylat_coord], [title]) VALUES (N'a4e4e0f3-cd92-4d89-885f-a60848df33b2', 0, 504, N'0', 1, N'Collie ', N'Larmour', NULL, N'07754615161', NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
END

IF NOT EXISTS(SELECT * FROM [club_person] WHERE [Oid] = N'3d5516f3-e0fe-450f-a667-a9eb35ff9304')
BEGIN
	INSERT [dbo].[club_person] ([oid], [orev], [key], [sex], [active], [forename], [surname], [dob], [phone], [mobile_no], [email], [alergies], [alergies_details], [comments], [player_profile], [address_number], [address_street], [address_town], [address_county], [address_country], [address_post_code], [address_xlng_coord], [address_ylat_coord], [title]) VALUES (N'3d5516f3-e0fe-450f-a667-a9eb35ff9304', 0, 476, N'M', 1, N'Luke', N'Haycock', CAST(0x0000973C00000000 AS DateTime), N'07967819356', NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
END

IF NOT EXISTS(SELECT * FROM [club_person] WHERE [Oid] = N'0d73b794-2faa-437b-a7a1-b2a0fbfe50a3')
BEGIN
	INSERT [dbo].[club_person] ([oid], [orev], [key], [sex], [active], [forename], [surname], [dob], [phone], [mobile_no], [email], [alergies], [alergies_details], [comments], [player_profile], [address_number], [address_street], [address_town], [address_county], [address_country], [address_post_code], [address_xlng_coord], [address_ylat_coord], [title]) VALUES (N'0d73b794-2faa-437b-a7a1-b2a0fbfe50a3', 0, 465, N'M', 1, N'Yasim', N'Baba', CAST(0x000098A900000000 AS DateTime), N'07729533082', NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
END

IF NOT EXISTS(SELECT * FROM [club_person] WHERE [Oid] = N'2dac0471-9bd4-417d-b3cc-b30e290ea833')
BEGIN
	INSERT [dbo].[club_person] ([oid], [orev], [key], [sex], [active], [forename], [surname], [dob], [phone], [mobile_no], [email], [alergies], [alergies_details], [comments], [player_profile], [address_number], [address_street], [address_town], [address_county], [address_country], [address_post_code], [address_xlng_coord], [address_ylat_coord], [title]) VALUES (N'2dac0471-9bd4-417d-b3cc-b30e290ea833', 0, 514, N'0', 1, N'Ray', N'Quinn', NULL, N'07813950026', NULL, NULL, 1, NULL, NULL, NULL, N'11', N'Fair Hill Road', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
END

IF NOT EXISTS(SELECT * FROM [club_person] WHERE [Oid] = N'e15948bd-0e0e-405e-a3c5-b4085fd06344')
BEGIN
	INSERT [dbo].[club_person] ([oid], [orev], [key], [sex], [active], [forename], [surname], [dob], [phone], [mobile_no], [email], [alergies], [alergies_details], [comments], [player_profile], [address_number], [address_street], [address_town], [address_county], [address_country], [address_post_code], [address_xlng_coord], [address_ylat_coord], [title]) VALUES (N'e15948bd-0e0e-405e-a3c5-b4085fd06344', 0, 469, N'M', 1, N'Canice', N'Cullen', CAST(0x0000982800000000 AS DateTime), N'07919243521
07783761420', NULL, NULL, 0, NULL, NULL, NULL, N'49', N'Rathbeg ', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
END

IF NOT EXISTS(SELECT * FROM [club_person] WHERE [Oid] = N'e46fc863-8b97-460d-99c5-b7787d5645ab')
BEGIN
	INSERT [dbo].[club_person] ([oid], [orev], [key], [sex], [active], [forename], [surname], [dob], [phone], [mobile_no], [email], [alergies], [alergies_details], [comments], [player_profile], [address_number], [address_street], [address_town], [address_county], [address_country], [address_post_code], [address_xlng_coord], [address_ylat_coord], [title]) VALUES (N'e46fc863-8b97-460d-99c5-b7787d5645ab', 0, 491, N'M', 1, N'Daragh', N'Quinn', CAST(0x0000973C00000000 AS DateTime), N'07813950026', NULL, NULL, 0, NULL, NULL, NULL, N'11', N'Fair Hill Road', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
END

IF NOT EXISTS(SELECT * FROM [club_person] WHERE [Oid] = N'b427dacd-04c0-4776-bf36-b8e6d8888888')
BEGIN
	INSERT [dbo].[club_person] ([oid], [orev], [key], [sex], [active], [forename], [surname], [dob], [phone], [mobile_no], [email], [alergies], [alergies_details], [comments], [player_profile], [address_number], [address_street], [address_town], [address_county], [address_country], [address_post_code], [address_xlng_coord], [address_ylat_coord], [title]) VALUES (N'b427dacd-04c0-4776-bf36-b8e6d8888888', 0, 483, N'M', 1, N'Michael', N'Mc Elhatton', CAST(0x0000973C00000000 AS DateTime), N'07759802921', NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
END

IF NOT EXISTS(SELECT * FROM [club_person] WHERE [Oid] = N'7e5174b6-9c67-45f9-9790-c1735311218a')
BEGIN
	INSERT [dbo].[club_person] ([oid], [orev], [key], [sex], [active], [forename], [surname], [dob], [phone], [mobile_no], [email], [alergies], [alergies_details], [comments], [player_profile], [address_number], [address_street], [address_town], [address_county], [address_country], [address_post_code], [address_xlng_coord], [address_ylat_coord], [title]) VALUES (N'7e5174b6-9c67-45f9-9790-c1735311218a', 0, 467, N'M', 1, N'Matthew', N'Byrne', CAST(0x0000973C00000000 AS DateTime), N'07936923193', NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
END

IF NOT EXISTS(SELECT * FROM [club_person] WHERE [Oid] = N'4820bf21-29d4-4784-a34b-cbc223a752c9')
BEGIN
	INSERT [dbo].[club_person] ([oid], [orev], [key], [sex], [active], [forename], [surname], [dob], [phone], [mobile_no], [email], [alergies], [alergies_details], [comments], [player_profile], [address_number], [address_street], [address_town], [address_county], [address_country], [address_post_code], [address_xlng_coord], [address_ylat_coord], [title]) VALUES (N'4820bf21-29d4-4784-a34b-cbc223a752c9', 0, 468, N'M', 1, N'Shea', N'Casey', CAST(0x000095CF00000000 AS DateTime), N'07516735200', NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
END

IF NOT EXISTS(SELECT * FROM [club_person] WHERE [Oid] = N'146e0ae1-0df3-451c-9e87-cc1011657588')
BEGIN
	INSERT [dbo].[club_person] ([oid], [orev], [key], [sex], [active], [forename], [surname], [dob], [phone], [mobile_no], [email], [alergies], [alergies_details], [comments], [player_profile], [address_number], [address_street], [address_town], [address_county], [address_country], [address_post_code], [address_xlng_coord], [address_ylat_coord], [title]) VALUES (N'146e0ae1-0df3-451c-9e87-cc1011657588', 0, 508, N'0', 1, N'Bernie & Jude', N'Mc Gurk', NULL, N'7753270695', NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
END

IF NOT EXISTS(SELECT * FROM [club_person] WHERE [Oid] = N'14dd7a24-9f41-409f-bb25-d1bfc01f4e63')
BEGIN
	INSERT [dbo].[club_person] ([oid], [orev], [key], [sex], [active], [forename], [surname], [dob], [phone], [mobile_no], [email], [alergies], [alergies_details], [comments], [player_profile], [address_number], [address_street], [address_town], [address_county], [address_country], [address_post_code], [address_xlng_coord], [address_ylat_coord], [title]) VALUES (N'14dd7a24-9f41-409f-bb25-d1bfc01f4e63', 0, 484, N'M', 1, N'Karol', N'Mc Guigan', CAST(0x0000965A00000000 AS DateTime), N'867 63681
07549514192', NULL, NULL, 1, N'ASTHMA', NULL, NULL, N'61', N'Castlecourt', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
END

IF NOT EXISTS(SELECT * FROM [club_person] WHERE [Oid] = N'15f0eccb-7034-4e89-816e-d93e847a8516')
BEGIN
	INSERT [dbo].[club_person] ([oid], [orev], [key], [sex], [active], [forename], [surname], [dob], [phone], [mobile_no], [email], [alergies], [alergies_details], [comments], [player_profile], [address_number], [address_street], [address_town], [address_county], [address_country], [address_post_code], [address_xlng_coord], [address_ylat_coord], [title]) VALUES (N'15f0eccb-7034-4e89-816e-d93e847a8516', 0, 517, N'0', 1, N'Eoin & Eileen', N'Ward', NULL, N'07709805851', NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
END

IF NOT EXISTS(SELECT * FROM [club_person] WHERE [Oid] = N'd69c35ed-3be1-479f-8f4e-dbd594f36e83')
BEGIN
	INSERT [dbo].[club_person] ([oid], [orev], [key], [sex], [active], [forename], [surname], [dob], [phone], [mobile_no], [email], [alergies], [alergies_details], [comments], [player_profile], [address_number], [address_street], [address_town], [address_county], [address_country], [address_post_code], [address_xlng_coord], [address_ylat_coord], [title]) VALUES (N'd69c35ed-3be1-479f-8f4e-dbd594f36e83', 0, 498, N'0', 1, N'Sarah & Manus ', N'Barry', NULL, N'07874719128', NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
END

IF NOT EXISTS(SELECT * FROM [club_person] WHERE [Oid] = N'3a541a4c-8c37-41d6-b4f9-de692ce6a2bc')
BEGIN
	INSERT [dbo].[club_person] ([oid], [orev], [key], [sex], [active], [forename], [surname], [dob], [phone], [mobile_no], [email], [alergies], [alergies_details], [comments], [player_profile], [address_number], [address_street], [address_town], [address_county], [address_country], [address_post_code], [address_xlng_coord], [address_ylat_coord], [title]) VALUES (N'3a541a4c-8c37-41d6-b4f9-de692ce6a2bc', 0, 488, N'M', 1, N'Luke', N'Neeson', CAST(0x000095CF00000000 AS DateTime), N'07557640021', NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
END

IF NOT EXISTS(SELECT * FROM [club_person] WHERE [Oid] = N'f7addaa5-a536-4c58-81da-dfb73e6e201b')
BEGIN
	INSERT [dbo].[club_person] ([oid], [orev], [key], [sex], [active], [forename], [surname], [dob], [phone], [mobile_no], [email], [alergies], [alergies_details], [comments], [player_profile], [address_number], [address_street], [address_town], [address_county], [address_country], [address_post_code], [address_xlng_coord], [address_ylat_coord], [title]) VALUES (N'f7addaa5-a536-4c58-81da-dfb73e6e201b', 0, 485, N'M', 1, N'Ruairi', N'Mc Gurk', CAST(0x0000973C00000000 AS DateTime), N'7753270695', NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
END

IF NOT EXISTS(SELECT * FROM [club_person] WHERE [Oid] = N'e1742649-bf66-45dc-a7fc-dff389daeabb')
BEGIN
	INSERT [dbo].[club_person] ([oid], [orev], [key], [sex], [active], [forename], [surname], [dob], [phone], [mobile_no], [email], [alergies], [alergies_details], [comments], [player_profile], [address_number], [address_street], [address_town], [address_county], [address_country], [address_post_code], [address_xlng_coord], [address_ylat_coord], [title]) VALUES (N'e1742649-bf66-45dc-a7fc-dff389daeabb', 0, 506, N'0', 1, N'Doreen', N'Mc Aleer', NULL, N'07771900300
86758529', NULL, NULL, 1, NULL, NULL, NULL, N'130A', N'Drum Road', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
END

IF NOT EXISTS(SELECT * FROM [club_person] WHERE [Oid] = N'69fcd808-2e2f-491d-973c-e20ea3ab536f')
BEGIN
	INSERT [dbo].[club_person] ([oid], [orev], [key], [sex], [active], [forename], [surname], [dob], [phone], [mobile_no], [email], [alergies], [alergies_details], [comments], [player_profile], [address_number], [address_street], [address_town], [address_county], [address_country], [address_post_code], [address_xlng_coord], [address_ylat_coord], [title]) VALUES (N'69fcd808-2e2f-491d-973c-e20ea3ab536f', 0, 474, N'M', 1, N'Ryan ', N'Eccles', CAST(0x0000973C00000000 AS DateTime), N'07860119478', NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
END

IF NOT EXISTS(SELECT * FROM [club_person] WHERE [Oid] = N'a27745cb-97a5-4f3d-87ad-e3924c17394b')
BEGIN
	INSERT [dbo].[club_person] ([oid], [orev], [key], [sex], [active], [forename], [surname], [dob], [phone], [mobile_no], [email], [alergies], [alergies_details], [comments], [player_profile], [address_number], [address_street], [address_town], [address_county], [address_country], [address_post_code], [address_xlng_coord], [address_ylat_coord], [title]) VALUES (N'a27745cb-97a5-4f3d-87ad-e3924c17394b', 0, 497, N'M', 1, N'Patrick', N'Hughes', CAST(0x0000949A00000000 AS DateTime), N'07803228706
86761423', NULL, NULL, 0, NULL, NULL, NULL, N'9', N'Golf View', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
END

IF NOT EXISTS(SELECT * FROM [club_person] WHERE [Oid] = N'1f59e5ce-0956-4225-9de3-e5aace531e81')
BEGIN
	INSERT [dbo].[club_person] ([oid], [orev], [key], [sex], [active], [forename], [surname], [dob], [phone], [mobile_no], [email], [alergies], [alergies_details], [comments], [player_profile], [address_number], [address_street], [address_town], [address_county], [address_country], [address_post_code], [address_xlng_coord], [address_ylat_coord], [title]) VALUES (N'1f59e5ce-0956-4225-9de3-e5aace531e81', 0, 495, N'M', 1, N'Callan', N'Devlin', CAST(0x000094BE00000000 AS DateTime), N'07754058450
07748575443', NULL, NULL, 0, NULL, NULL, NULL, N'23', N'Castlecourt', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
END

IF NOT EXISTS(SELECT * FROM [club_person] WHERE [Oid] = N'f0599bf3-2ac2-450a-9678-e7d0c25506b7')
BEGIN
	INSERT [dbo].[club_person] ([oid], [orev], [key], [sex], [active], [forename], [surname], [dob], [phone], [mobile_no], [email], [alergies], [alergies_details], [comments], [player_profile], [address_number], [address_street], [address_town], [address_county], [address_country], [address_post_code], [address_xlng_coord], [address_ylat_coord], [title]) VALUES (N'f0599bf3-2ac2-450a-9678-e7d0c25506b7', 0, 494, N'M', 1, N'Christopher', N'Ward', CAST(0x0000973C00000000 AS DateTime), N'07709805851', NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
END

IF NOT EXISTS(SELECT * FROM [club_person] WHERE [Oid] = N'039eb138-bad8-41fe-bf77-fc90b4baca82')
BEGIN
	INSERT [dbo].[club_person] ([oid], [orev], [key], [sex], [active], [forename], [surname], [dob], [phone], [mobile_no], [email], [alergies], [alergies_details], [comments], [player_profile], [address_number], [address_street], [address_town], [address_county], [address_country], [address_post_code], [address_xlng_coord], [address_ylat_coord], [title]) VALUES (N'039eb138-bad8-41fe-bf77-fc90b4baca82', 0, 478, N'M', 1, N'Matthew', N'Larmour', CAST(0x000095CF00000000 AS DateTime), N'07754615161', NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
END

IF NOT EXISTS(SELECT * FROM [club_person] WHERE [Oid] = N'f4241db1-6b31-401c-a2e3-fd28760a4dc9')
BEGIN
	INSERT [dbo].[club_person] ([oid], [orev], [key], [sex], [active], [forename], [surname], [dob], [phone], [mobile_no], [email], [alergies], [alergies_details], [comments], [player_profile], [address_number], [address_street], [address_town], [address_county], [address_country], [address_post_code], [address_xlng_coord], [address_ylat_coord], [title]) VALUES (N'f4241db1-6b31-401c-a2e3-fd28760a4dc9', 0, 510, N'0', 1, N'Denise', N'Mullin', NULL, N'86758801
07789790685
07802510172', NULL, NULL, 1, NULL, NULL, NULL, N'10', N'Mallon Way', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
END

IF NOT EXISTS(SELECT * FROM [club_person] WHERE [Oid] = N'259918e1-5b7f-4a5e-b064-fddbd2879074')
BEGIN
	INSERT [dbo].[club_person] ([oid], [orev], [key], [sex], [active], [forename], [surname], [dob], [phone], [mobile_no], [email], [alergies], [alergies_details], [comments], [player_profile], [address_number], [address_street], [address_town], [address_county], [address_country], [address_post_code], [address_xlng_coord], [address_ylat_coord], [title]) VALUES (N'259918e1-5b7f-4a5e-b064-fddbd2879074', 0, 480, N'M', 1, N'Daragh', N'Mc Aleer', CAST(0x000095CF00000000 AS DateTime), N'07562807549', NULL, NULL, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
END

IF NOT EXISTS(SELECT * FROM [club_person] WHERE [Oid] = N'9d93e441-d63d-4802-9cef-fed138ae680b')
BEGIN
	INSERT [dbo].[club_person] ([oid], [orev], [key], [sex], [active], [forename], [surname], [dob], [phone], [mobile_no], [email], [alergies], [alergies_details], [comments], [player_profile], [address_number], [address_street], [address_town], [address_county], [address_country], [address_post_code], [address_xlng_coord], [address_ylat_coord], [title]) VALUES (N'9d93e441-d63d-4802-9cef-fed138ae680b', 0, 503, N'0', 1, N'Marie & Michael ', N'Haycock', NULL, N'07967819356', NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
END

IF NOT EXISTS(SELECT * FROM [club_person] WHERE [Oid] = N'69de324c-c2fb-4ee0-8665-ff7793113d59')
BEGIN
	INSERT [dbo].[club_person] ([oid], [orev], [key], [sex], [active], [forename], [surname], [dob], [phone], [mobile_no], [email], [alergies], [alergies_details], [comments], [player_profile], [address_number], [address_street], [address_town], [address_county], [address_country], [address_post_code], [address_xlng_coord], [address_ylat_coord], [title]) VALUES (N'69de324c-c2fb-4ee0-8665-ff7793113d59', 0, 507, N'0', 1, N'Lisa', N'Mc Elhatton', NULL, N'07759802921', NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
END


-- ==============================================================================================
--					 [club_person_guardian] Static Data 
-- ==============================================================================================

IF NOT EXISTS(SELECT * FROM [club_person_guardian] WHERE [Oid] = N'8608605f-8ceb-407d-8c93-0fb697313fb1')
BEGIN
	INSERT [dbo].[club_person_guardian] ([oid], [orev], [person], [guardian]) VALUES (N'8608605f-8ceb-407d-8c93-0fb697313fb1', 0, N'd67694f5-b56e-482a-9c4d-883132c0369f', N'18d396ed-49a9-40c2-868b-2e6bcc4a831a')
END

IF NOT EXISTS(SELECT * FROM [club_person_guardian] WHERE [Oid] = N'e59993bc-7862-41a5-ad2d-1f87a0f63b2d')
BEGIN
	INSERT [dbo].[club_person_guardian] ([oid], [orev], [person], [guardian]) VALUES (N'e59993bc-7862-41a5-ad2d-1f87a0f63b2d', 0, N'512385d4-8239-4b97-8a13-863bacfaec71', N'e55260d8-9ff7-4f5f-b4f3-3c96d8599940')
END

IF NOT EXISTS(SELECT * FROM [club_person_guardian] WHERE [Oid] = N'9f45155b-c856-4cb0-b36b-2b589f3d5839')
BEGIN
	INSERT [dbo].[club_person_guardian] ([oid], [orev], [person], [guardian]) VALUES (N'9f45155b-c856-4cb0-b36b-2b589f3d5839', 0, N'b427dacd-04c0-4776-bf36-b8e6d8888888', N'69de324c-c2fb-4ee0-8665-ff7793113d59')
END

IF NOT EXISTS(SELECT * FROM [club_person_guardian] WHERE [Oid] = N'9b6f6cb8-2818-47b4-ad3b-2de0604e0488')
BEGIN
	INSERT [dbo].[club_person_guardian] ([oid], [orev], [person], [guardian]) VALUES (N'9b6f6cb8-2818-47b4-ad3b-2de0604e0488', 0, N'b816ef7c-90fd-48ca-8c6b-8e4b3dad5188', N'0a64d537-d334-45ac-b9a0-43d3634b7a4a')
END

IF NOT EXISTS(SELECT * FROM [club_person_guardian] WHERE [Oid] = N'54e06503-4ee9-441e-92b0-32c98dfae5b6')
BEGIN
	INSERT [dbo].[club_person_guardian] ([oid], [orev], [person], [guardian]) VALUES (N'54e06503-4ee9-441e-92b0-32c98dfae5b6', 0, N'f0599bf3-2ac2-450a-9678-e7d0c25506b7', N'15f0eccb-7034-4e89-816e-d93e847a8516')
END

IF NOT EXISTS(SELECT * FROM [club_person_guardian] WHERE [Oid] = N'1f3367f1-923c-401b-8d08-335fe4201157')
BEGIN
	INSERT [dbo].[club_person_guardian] ([oid], [orev], [person], [guardian]) VALUES (N'1f3367f1-923c-401b-8d08-335fe4201157', 0, N'e448e999-3c6b-4e5f-b5b0-581b1f69dd60', N'e1742649-bf66-45dc-a7fc-dff389daeabb')
END

IF NOT EXISTS(SELECT * FROM [club_person_guardian] WHERE [Oid] = N'081d1cda-6966-4a93-959a-38d4bf30aad1')
BEGIN
	INSERT [dbo].[club_person_guardian] ([oid], [orev], [person], [guardian]) VALUES (N'081d1cda-6966-4a93-959a-38d4bf30aad1', 0, N'd4777c5f-3a69-499b-a5f2-555249e53df0', N'2c65d642-aabf-468f-af80-17fe03126fac')
END

IF NOT EXISTS(SELECT * FROM [club_person_guardian] WHERE [Oid] = N'bd4ee0ad-9237-4f4b-bc1a-4becb93e71a0')
BEGIN
	INSERT [dbo].[club_person_guardian] ([oid], [orev], [person], [guardian]) VALUES (N'bd4ee0ad-9237-4f4b-bc1a-4becb93e71a0', 0, N'f928ba09-8a12-49af-87cd-a272b10b7e58', N'd69c35ed-3be1-479f-8f4e-dbd594f36e83')
END

IF NOT EXISTS(SELECT * FROM [club_person_guardian] WHERE [Oid] = N'7b11480e-8dc4-413d-90ac-50e8953b4d65')
BEGIN
	INSERT [dbo].[club_person_guardian] ([oid], [orev], [person], [guardian]) VALUES (N'7b11480e-8dc4-413d-90ac-50e8953b4d65', 0, N'e46fc863-8b97-460d-99c5-b7787d5645ab', N'2dac0471-9bd4-417d-b3cc-b30e290ea833')
END

IF NOT EXISTS(SELECT * FROM [club_person_guardian] WHERE [Oid] = N'506711d7-9676-4edb-a05e-613135ea440b')
BEGIN
	INSERT [dbo].[club_person_guardian] ([oid], [orev], [person], [guardian]) VALUES (N'506711d7-9676-4edb-a05e-613135ea440b', 0, N'fdeb2653-3bc2-44a2-b00b-9665025b5db6', N'9f0f590d-3cbd-4d30-9eea-04e5912cd553')
END

IF NOT EXISTS(SELECT * FROM [club_person_guardian] WHERE [Oid] = N'd000ec1b-3b2f-4f7b-a807-63d0138a21c5')
BEGIN
	INSERT [dbo].[club_person_guardian] ([oid], [orev], [person], [guardian]) VALUES (N'd000ec1b-3b2f-4f7b-a807-63d0138a21c5', 0, N'3d5516f3-e0fe-450f-a667-a9eb35ff9304', N'9d93e441-d63d-4802-9cef-fed138ae680b')
END

IF NOT EXISTS(SELECT * FROM [club_person_guardian] WHERE [Oid] = N'd0f2ba03-4e0a-4b31-be07-6607dc5ac4a4')
BEGIN
	INSERT [dbo].[club_person_guardian] ([oid], [orev], [person], [guardian]) VALUES (N'd0f2ba03-4e0a-4b31-be07-6607dc5ac4a4', 0, N'7e5174b6-9c67-45f9-9790-c1735311218a', N'752e3337-b72d-4a88-a5e1-88d37011a123')
END

IF NOT EXISTS(SELECT * FROM [club_person_guardian] WHERE [Oid] = N'faa7d685-ca81-4c53-adfc-7d569bf09f20')
BEGIN
	INSERT [dbo].[club_person_guardian] ([oid], [orev], [person], [guardian]) VALUES (N'faa7d685-ca81-4c53-adfc-7d569bf09f20', 0, N'63c3b555-fae3-414f-b0e5-0c4b56e61108', N'f4241db1-6b31-401c-a2e3-fd28760a4dc9')
END

IF NOT EXISTS(SELECT * FROM [club_person_guardian] WHERE [Oid] = N'7fc6d6ab-27c2-44d1-aae2-8132a13a7b40')
BEGIN
	INSERT [dbo].[club_person_guardian] ([oid], [orev], [person], [guardian]) VALUES (N'7fc6d6ab-27c2-44d1-aae2-8132a13a7b40', 0, N'3a541a4c-8c37-41d6-b4f9-de692ce6a2bc', N'be5e1aa5-c6d6-467d-b4cf-6e38a4537a10')
END

IF NOT EXISTS(SELECT * FROM [club_person_guardian] WHERE [Oid] = N'8c30adcc-0c5c-4535-ad46-859070c2e517')
BEGIN
	INSERT [dbo].[club_person_guardian] ([oid], [orev], [person], [guardian]) VALUES (N'8c30adcc-0c5c-4535-ad46-859070c2e517', 0, N'e15948bd-0e0e-405e-a3c5-b4085fd06344', N'b0a07f4e-e716-4e57-87f8-6e7434ac3a26')
END

IF NOT EXISTS(SELECT * FROM [club_person_guardian] WHERE [Oid] = N'b912eb23-4827-4a79-92bc-8c128c2f54db')
BEGIN
	INSERT [dbo].[club_person_guardian] ([oid], [orev], [person], [guardian]) VALUES (N'b912eb23-4827-4a79-92bc-8c128c2f54db', 0, N'1f59e5ce-0956-4225-9de3-e5aace531e81', N'bf8c0fbe-a064-4837-a5e6-61ee4be79d68')
END

IF NOT EXISTS(SELECT * FROM [club_person_guardian] WHERE [Oid] = N'61e93a10-6d71-4817-b128-8ff7064df3bb')
BEGIN
	INSERT [dbo].[club_person_guardian] ([oid], [orev], [person], [guardian]) VALUES (N'61e93a10-6d71-4817-b128-8ff7064df3bb', 0, N'47ddc8eb-3150-4ebf-8485-90a24a299b5c', N'76247131-8ec0-4918-acc0-0d94679ad822')
END

IF NOT EXISTS(SELECT * FROM [club_person_guardian] WHERE [Oid] = N'08e14800-1c02-4cf2-b944-965c2d0d170d')
BEGIN
	INSERT [dbo].[club_person_guardian] ([oid], [orev], [person], [guardian]) VALUES (N'08e14800-1c02-4cf2-b944-965c2d0d170d', 0, N'039eb138-bad8-41fe-bf77-fc90b4baca82', N'a4e4e0f3-cd92-4d89-885f-a60848df33b2')
END

IF NOT EXISTS(SELECT * FROM [club_person_guardian] WHERE [Oid] = N'77175235-d2ae-4018-b26a-99ce3ae0649f')
BEGIN
	INSERT [dbo].[club_person_guardian] ([oid], [orev], [person], [guardian]) VALUES (N'77175235-d2ae-4018-b26a-99ce3ae0649f', 0, N'7f65f53a-07b9-463c-8e59-962ea1626ad0', N'afe66c48-5f26-40cf-8cd4-9b4766892a99')
END

IF NOT EXISTS(SELECT * FROM [club_person_guardian] WHERE [Oid] = N'8bf16181-1210-42aa-8d3d-a8b923f1d22e')
BEGIN
	INSERT [dbo].[club_person_guardian] ([oid], [orev], [person], [guardian]) VALUES (N'8bf16181-1210-42aa-8d3d-a8b923f1d22e', 0, N'a27745cb-97a5-4f3d-87ad-e3924c17394b', N'd06fc0a7-827d-448b-9204-5a9a367bfba6')
END

IF NOT EXISTS(SELECT * FROM [club_person_guardian] WHERE [Oid] = N'21fdedd0-3e0f-4bf5-9a74-c54be0e0aef8')
BEGIN
	INSERT [dbo].[club_person_guardian] ([oid], [orev], [person], [guardian]) VALUES (N'21fdedd0-3e0f-4bf5-9a74-c54be0e0aef8', 0, N'69fcd808-2e2f-491d-973c-e20ea3ab536f', N'cc623322-46c1-424e-8bd3-8029d4af05d7')
END

IF NOT EXISTS(SELECT * FROM [club_person_guardian] WHERE [Oid] = N'79382a52-198d-44db-8e7b-cb96bc73bd5a')
BEGIN
	INSERT [dbo].[club_person_guardian] ([oid], [orev], [person], [guardian]) VALUES (N'79382a52-198d-44db-8e7b-cb96bc73bd5a', 0, N'd596d4ea-d6b0-4ae9-92b5-0808e800960f', N'2f581c10-bd48-4fff-ae10-749d45171fe5')
END

IF NOT EXISTS(SELECT * FROM [club_person_guardian] WHERE [Oid] = N'ca0619d8-e2ed-4ea7-ac28-edeabb1e7501')
BEGIN
	INSERT [dbo].[club_person_guardian] ([oid], [orev], [person], [guardian]) VALUES (N'ca0619d8-e2ed-4ea7-ac28-edeabb1e7501', 0, N'f7addaa5-a536-4c58-81da-dfb73e6e201b', N'146e0ae1-0df3-451c-9e87-cc1011657588')
END


-- ==============================================================================================
--					 [club_team_member] Static Data 
-- ==============================================================================================

IF NOT EXISTS(SELECT * FROM [club_team_member] WHERE [Oid] = N'ed3d2f5c-041c-4942-a220-015eca5c8e4c')
BEGIN
	INSERT [dbo].[club_team_member] ([oid], [orev], [person], [team], [team_member_type]) VALUES (N'ed3d2f5c-041c-4942-a220-015eca5c8e4c', 0, N'17b74593-5b56-4233-a0c2-49a80bf081be', N'27caf1f2-1b51-4192-8440-0b0fd8dddbe2', N'2151c74b-be85-4152-9b43-c31fc7ae8730')
END

IF NOT EXISTS(SELECT * FROM [club_team_member] WHERE [Oid] = N'804671d0-119e-47cd-bedb-01ea2ddf4354')
BEGIN
	INSERT [dbo].[club_team_member] ([oid], [orev], [person], [team], [team_member_type]) VALUES (N'804671d0-119e-47cd-bedb-01ea2ddf4354', 0, N'0d73b794-2faa-437b-a7a1-b2a0fbfe50a3', N'27caf1f2-1b51-4192-8440-0b0fd8dddbe2', N'2151c74b-be85-4152-9b43-c31fc7ae8730')
END

IF NOT EXISTS(SELECT * FROM [club_team_member] WHERE [Oid] = N'182d6314-c509-4c18-84b5-082539678758')
BEGIN
	INSERT [dbo].[club_team_member] ([oid], [orev], [person], [team], [team_member_type]) VALUES (N'182d6314-c509-4c18-84b5-082539678758', 0, N'1c6d64b9-42de-48cd-aaeb-3c60a463ff42', N'27caf1f2-1b51-4192-8440-0b0fd8dddbe2', N'2151c74b-be85-4152-9b43-c31fc7ae8730')
END

IF NOT EXISTS(SELECT * FROM [club_team_member] WHERE [Oid] = N'fd9622f3-5890-4388-8273-089564a4375e')
BEGIN
	INSERT [dbo].[club_team_member] ([oid], [orev], [person], [team], [team_member_type]) VALUES (N'fd9622f3-5890-4388-8273-089564a4375e', 0, N'4820bf21-29d4-4784-a34b-cbc223a752c9', N'27caf1f2-1b51-4192-8440-0b0fd8dddbe2', N'2151c74b-be85-4152-9b43-c31fc7ae8730')
END

IF NOT EXISTS(SELECT * FROM [club_team_member] WHERE [Oid] = N'33b84b28-5d5b-4829-ba94-09ef867de602')
BEGIN
	INSERT [dbo].[club_team_member] ([oid], [orev], [person], [team], [team_member_type]) VALUES (N'33b84b28-5d5b-4829-ba94-09ef867de602', 0, N'b427dacd-04c0-4776-bf36-b8e6d8888888', N'27caf1f2-1b51-4192-8440-0b0fd8dddbe2', N'2151c74b-be85-4152-9b43-c31fc7ae8730')
END

IF NOT EXISTS(SELECT * FROM [club_team_member] WHERE [Oid] = N'5d58a0b1-ce48-4bac-8389-125c1fa4b7c9')
BEGIN
	INSERT [dbo].[club_team_member] ([oid], [orev], [person], [team], [team_member_type]) VALUES (N'5d58a0b1-ce48-4bac-8389-125c1fa4b7c9', 0, N'14dd7a24-9f41-409f-bb25-d1bfc01f4e63', N'27caf1f2-1b51-4192-8440-0b0fd8dddbe2', N'2151c74b-be85-4152-9b43-c31fc7ae8730')
END

IF NOT EXISTS(SELECT * FROM [club_team_member] WHERE [Oid] = N'36b0e356-9719-459d-b97b-2455a525f560')
BEGIN
	INSERT [dbo].[club_team_member] ([oid], [orev], [person], [team], [team_member_type]) VALUES (N'36b0e356-9719-459d-b97b-2455a525f560', 0, N'1f59e5ce-0956-4225-9de3-e5aace531e81', N'27caf1f2-1b51-4192-8440-0b0fd8dddbe2', N'2151c74b-be85-4152-9b43-c31fc7ae8730')
END

IF NOT EXISTS(SELECT * FROM [club_team_member] WHERE [Oid] = N'97972fc0-9ee8-49fb-8dbe-3de370da8730')
BEGIN
	INSERT [dbo].[club_team_member] ([oid], [orev], [person], [team], [team_member_type]) VALUES (N'97972fc0-9ee8-49fb-8dbe-3de370da8730', 0, N'd67694f5-b56e-482a-9c4d-883132c0369f', N'27caf1f2-1b51-4192-8440-0b0fd8dddbe2', N'2151c74b-be85-4152-9b43-c31fc7ae8730')
END

IF NOT EXISTS(SELECT * FROM [club_team_member] WHERE [Oid] = N'e547bb66-cb2a-427c-8852-3eab7181a8b8')
BEGIN
	INSERT [dbo].[club_team_member] ([oid], [orev], [person], [team], [team_member_type]) VALUES (N'e547bb66-cb2a-427c-8852-3eab7181a8b8', 0, N'47ddc8eb-3150-4ebf-8485-90a24a299b5c', N'27caf1f2-1b51-4192-8440-0b0fd8dddbe2', N'2151c74b-be85-4152-9b43-c31fc7ae8730')
END

IF NOT EXISTS(SELECT * FROM [club_team_member] WHERE [Oid] = N'b474ddec-3def-437a-bb68-40948dc222c6')
BEGIN
	INSERT [dbo].[club_team_member] ([oid], [orev], [person], [team], [team_member_type]) VALUES (N'b474ddec-3def-437a-bb68-40948dc222c6', 0, N'f928ba09-8a12-49af-87cd-a272b10b7e58', N'27caf1f2-1b51-4192-8440-0b0fd8dddbe2', N'2151c74b-be85-4152-9b43-c31fc7ae8730')
END

IF NOT EXISTS(SELECT * FROM [club_team_member] WHERE [Oid] = N'93ef24e7-3349-454c-884d-4d53d8ca9306')
BEGIN
	INSERT [dbo].[club_team_member] ([oid], [orev], [person], [team], [team_member_type]) VALUES (N'93ef24e7-3349-454c-884d-4d53d8ca9306', 0, N'78edd860-6cea-4281-afbd-0e9cd914dd3a', N'27caf1f2-1b51-4192-8440-0b0fd8dddbe2', N'2151c74b-be85-4152-9b43-c31fc7ae8730')
END

IF NOT EXISTS(SELECT * FROM [club_team_member] WHERE [Oid] = N'47edb3ae-a469-457e-8965-604a9e4133e2')
BEGIN
	INSERT [dbo].[club_team_member] ([oid], [orev], [person], [team], [team_member_type]) VALUES (N'47edb3ae-a469-457e-8965-604a9e4133e2', 0, N'fdeb2653-3bc2-44a2-b00b-9665025b5db6', N'27caf1f2-1b51-4192-8440-0b0fd8dddbe2', N'2151c74b-be85-4152-9b43-c31fc7ae8730')
END

IF NOT EXISTS(SELECT * FROM [club_team_member] WHERE [Oid] = N'd5731dfb-be5f-48d5-82d2-6492eb4a539f')
BEGIN
	INSERT [dbo].[club_team_member] ([oid], [orev], [person], [team], [team_member_type]) VALUES (N'd5731dfb-be5f-48d5-82d2-6492eb4a539f', 0, N'3d5516f3-e0fe-450f-a667-a9eb35ff9304', N'27caf1f2-1b51-4192-8440-0b0fd8dddbe2', N'2151c74b-be85-4152-9b43-c31fc7ae8730')
END

IF NOT EXISTS(SELECT * FROM [club_team_member] WHERE [Oid] = N'e7aa7cb8-8452-4e77-b2ae-6bef975b2f78')
BEGIN
	INSERT [dbo].[club_team_member] ([oid], [orev], [person], [team], [team_member_type]) VALUES (N'e7aa7cb8-8452-4e77-b2ae-6bef975b2f78', 0, N'512385d4-8239-4b97-8a13-863bacfaec71', N'27caf1f2-1b51-4192-8440-0b0fd8dddbe2', N'2151c74b-be85-4152-9b43-c31fc7ae8730')
END

IF NOT EXISTS(SELECT * FROM [club_team_member] WHERE [Oid] = N'6f5e61f1-0288-4fe2-ab45-7179e19dcac2')
BEGIN
	INSERT [dbo].[club_team_member] ([oid], [orev], [person], [team], [team_member_type]) VALUES (N'6f5e61f1-0288-4fe2-ab45-7179e19dcac2', 0, N'e15948bd-0e0e-405e-a3c5-b4085fd06344', N'27caf1f2-1b51-4192-8440-0b0fd8dddbe2', N'2151c74b-be85-4152-9b43-c31fc7ae8730')
END

IF NOT EXISTS(SELECT * FROM [club_team_member] WHERE [Oid] = N'a38d38ea-fe5f-4296-abec-719d5aa7dc0d')
BEGIN
	INSERT [dbo].[club_team_member] ([oid], [orev], [person], [team], [team_member_type]) VALUES (N'a38d38ea-fe5f-4296-abec-719d5aa7dc0d', 0, N'f7addaa5-a536-4c58-81da-dfb73e6e201b', N'27caf1f2-1b51-4192-8440-0b0fd8dddbe2', N'2151c74b-be85-4152-9b43-c31fc7ae8730')
END

IF NOT EXISTS(SELECT * FROM [club_team_member] WHERE [Oid] = N'44e3f748-a9e9-46fe-87f9-72c2da3d6f38')
BEGIN
	INSERT [dbo].[club_team_member] ([oid], [orev], [person], [team], [team_member_type]) VALUES (N'44e3f748-a9e9-46fe-87f9-72c2da3d6f38', 0, N'e448e999-3c6b-4e5f-b5b0-581b1f69dd60', N'27caf1f2-1b51-4192-8440-0b0fd8dddbe2', N'2151c74b-be85-4152-9b43-c31fc7ae8730')
END

IF NOT EXISTS(SELECT * FROM [club_team_member] WHERE [Oid] = N'2e7ab36a-6135-4935-a90a-79c82baca7f1')
BEGIN
	INSERT [dbo].[club_team_member] ([oid], [orev], [person], [team], [team_member_type]) VALUES (N'2e7ab36a-6135-4935-a90a-79c82baca7f1', 0, N'69fcd808-2e2f-491d-973c-e20ea3ab536f', N'27caf1f2-1b51-4192-8440-0b0fd8dddbe2', N'2151c74b-be85-4152-9b43-c31fc7ae8730')
END

IF NOT EXISTS(SELECT * FROM [club_team_member] WHERE [Oid] = N'89003b20-4cb9-44ad-9068-82b80a247e0f')
BEGIN
	INSERT [dbo].[club_team_member] ([oid], [orev], [person], [team], [team_member_type]) VALUES (N'89003b20-4cb9-44ad-9068-82b80a247e0f', 0, N'286bcb93-12ca-4316-82cf-6e547deb8bf1', N'27caf1f2-1b51-4192-8440-0b0fd8dddbe2', N'2151c74b-be85-4152-9b43-c31fc7ae8730')
END

IF NOT EXISTS(SELECT * FROM [club_team_member] WHERE [Oid] = N'666b6610-c09d-4202-a50c-855a77f28a62')
BEGIN
	INSERT [dbo].[club_team_member] ([oid], [orev], [person], [team], [team_member_type]) VALUES (N'666b6610-c09d-4202-a50c-855a77f28a62', 0, N'7f65f53a-07b9-463c-8e59-962ea1626ad0', N'27caf1f2-1b51-4192-8440-0b0fd8dddbe2', N'2151c74b-be85-4152-9b43-c31fc7ae8730')
END

IF NOT EXISTS(SELECT * FROM [club_team_member] WHERE [Oid] = N'f746ad28-f421-411d-82e6-8b50a82e423b')
BEGIN
	INSERT [dbo].[club_team_member] ([oid], [orev], [person], [team], [team_member_type]) VALUES (N'f746ad28-f421-411d-82e6-8b50a82e423b', 0, N'c1ebdecc-88da-42ac-ad0d-46302c13fca3', N'27caf1f2-1b51-4192-8440-0b0fd8dddbe2', N'2151c74b-be85-4152-9b43-c31fc7ae8730')
END

IF NOT EXISTS(SELECT * FROM [club_team_member] WHERE [Oid] = N'3e350302-951b-4b79-b7e3-99c137fe640f')
BEGIN
	INSERT [dbo].[club_team_member] ([oid], [orev], [person], [team], [team_member_type]) VALUES (N'3e350302-951b-4b79-b7e3-99c137fe640f', 0, N'3a541a4c-8c37-41d6-b4f9-de692ce6a2bc', N'27caf1f2-1b51-4192-8440-0b0fd8dddbe2', N'2151c74b-be85-4152-9b43-c31fc7ae8730')
END

IF NOT EXISTS(SELECT * FROM [club_team_member] WHERE [Oid] = N'667cb555-cfc1-4c39-8490-9e6209b52690')
BEGIN
	INSERT [dbo].[club_team_member] ([oid], [orev], [person], [team], [team_member_type]) VALUES (N'667cb555-cfc1-4c39-8490-9e6209b52690', 0, N'a27745cb-97a5-4f3d-87ad-e3924c17394b', N'27caf1f2-1b51-4192-8440-0b0fd8dddbe2', N'2151c74b-be85-4152-9b43-c31fc7ae8730')
END

IF NOT EXISTS(SELECT * FROM [club_team_member] WHERE [Oid] = N'ae8faeae-6a20-4575-b924-a120ba8db135')
BEGIN
	INSERT [dbo].[club_team_member] ([oid], [orev], [person], [team], [team_member_type]) VALUES (N'ae8faeae-6a20-4575-b924-a120ba8db135', 0, N'259918e1-5b7f-4a5e-b064-fddbd2879074', N'27caf1f2-1b51-4192-8440-0b0fd8dddbe2', N'2151c74b-be85-4152-9b43-c31fc7ae8730')
END

IF NOT EXISTS(SELECT * FROM [club_team_member] WHERE [Oid] = N'2e7a962c-4f9c-41e3-8406-a47a7a470055')
BEGIN
	INSERT [dbo].[club_team_member] ([oid], [orev], [person], [team], [team_member_type]) VALUES (N'2e7a962c-4f9c-41e3-8406-a47a7a470055', 0, N'0092e3d9-0dbb-4393-a286-2786c895b5cc', N'27caf1f2-1b51-4192-8440-0b0fd8dddbe2', N'2151c74b-be85-4152-9b43-c31fc7ae8730')
END

IF NOT EXISTS(SELECT * FROM [club_team_member] WHERE [Oid] = N'd7fda021-07e9-4db0-8f56-bd708f8e5846')
BEGIN
	INSERT [dbo].[club_team_member] ([oid], [orev], [person], [team], [team_member_type]) VALUES (N'd7fda021-07e9-4db0-8f56-bd708f8e5846', 0, N'd4777c5f-3a69-499b-a5f2-555249e53df0', N'27caf1f2-1b51-4192-8440-0b0fd8dddbe2', N'2151c74b-be85-4152-9b43-c31fc7ae8730')
END

IF NOT EXISTS(SELECT * FROM [club_team_member] WHERE [Oid] = N'60a8e963-95b8-41dc-8a25-c0fd9a0d9bd9')
BEGIN
	INSERT [dbo].[club_team_member] ([oid], [orev], [person], [team], [team_member_type]) VALUES (N'60a8e963-95b8-41dc-8a25-c0fd9a0d9bd9', 0, N'b816ef7c-90fd-48ca-8c6b-8e4b3dad5188', N'27caf1f2-1b51-4192-8440-0b0fd8dddbe2', N'2151c74b-be85-4152-9b43-c31fc7ae8730')
END

IF NOT EXISTS(SELECT * FROM [club_team_member] WHERE [Oid] = N'65330a7f-ed15-43e4-9459-d055ae4bffab')
BEGIN
	INSERT [dbo].[club_team_member] ([oid], [orev], [person], [team], [team_member_type]) VALUES (N'65330a7f-ed15-43e4-9459-d055ae4bffab', 0, N'e46fc863-8b97-460d-99c5-b7787d5645ab', N'27caf1f2-1b51-4192-8440-0b0fd8dddbe2', N'2151c74b-be85-4152-9b43-c31fc7ae8730')
END

IF NOT EXISTS(SELECT * FROM [club_team_member] WHERE [Oid] = N'8b60a3cc-ba3e-4d59-9700-d279277de207')
BEGIN
	INSERT [dbo].[club_team_member] ([oid], [orev], [person], [team], [team_member_type]) VALUES (N'8b60a3cc-ba3e-4d59-9700-d279277de207', 0, N'7e5174b6-9c67-45f9-9790-c1735311218a', N'27caf1f2-1b51-4192-8440-0b0fd8dddbe2', N'2151c74b-be85-4152-9b43-c31fc7ae8730')
END

IF NOT EXISTS(SELECT * FROM [club_team_member] WHERE [Oid] = N'6012d03b-c219-4169-9bc1-dd0da3949f2c')
BEGIN
	INSERT [dbo].[club_team_member] ([oid], [orev], [person], [team], [team_member_type]) VALUES (N'6012d03b-c219-4169-9bc1-dd0da3949f2c', 0, N'63c3b555-fae3-414f-b0e5-0c4b56e61108', N'27caf1f2-1b51-4192-8440-0b0fd8dddbe2', N'2151c74b-be85-4152-9b43-c31fc7ae8730')
END

IF NOT EXISTS(SELECT * FROM [club_team_member] WHERE [Oid] = N'a7cbd672-f424-4eb1-ac87-df18d8571ba4')
BEGIN
	INSERT [dbo].[club_team_member] ([oid], [orev], [person], [team], [team_member_type]) VALUES (N'a7cbd672-f424-4eb1-ac87-df18d8571ba4', 0, N'd596d4ea-d6b0-4ae9-92b5-0808e800960f', N'27caf1f2-1b51-4192-8440-0b0fd8dddbe2', N'2151c74b-be85-4152-9b43-c31fc7ae8730')
END

IF NOT EXISTS(SELECT * FROM [club_team_member] WHERE [Oid] = N'4ff43daf-2b12-4a7c-ad98-e0661e3b52ce')
BEGIN
	INSERT [dbo].[club_team_member] ([oid], [orev], [person], [team], [team_member_type]) VALUES (N'4ff43daf-2b12-4a7c-ad98-e0661e3b52ce', 0, N'039eb138-bad8-41fe-bf77-fc90b4baca82', N'27caf1f2-1b51-4192-8440-0b0fd8dddbe2', N'2151c74b-be85-4152-9b43-c31fc7ae8730')
END

IF NOT EXISTS(SELECT * FROM [club_team_member] WHERE [Oid] = N'955a9d4e-0551-4c47-89b5-feafba24aca7')
BEGIN
	INSERT [dbo].[club_team_member] ([oid], [orev], [person], [team], [team_member_type]) VALUES (N'955a9d4e-0551-4c47-89b5-feafba24aca7', 0, N'f0599bf3-2ac2-450a-9678-e7d0c25506b7', N'27caf1f2-1b51-4192-8440-0b0fd8dddbe2', N'2151c74b-be85-4152-9b43-c31fc7ae8730')
END

SET IDENTITY_INSERT [dbo].[club_person] OFF 

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
