
      /*
      ===============================================================================================================
      This file was auto generated by theClub Admin Nant build file target: 'sqlCreateScripts' for version: 1.1.0.0
      ===============================================================================================================
      */
      IF NOT EXISTS(SELECT * FROM [core_system_info] WHERE [Oid] = N'48c42625-eace-4728-aa6f-2ab11b6712ec')
      BEGIN
      INSERT [dbo].[core_system_info] ([oid], [orev], [application_name], [customer_name], [ver_major], [ver_minor], [ver_schema], [ver_build]) VALUES (N'48c42625-eace-4728-aa6f-2ab11b6712ec', 1, N'Diamond Fire', N'Cookstown', 1, 1, 0, 0)
      END
      ELSE
      BEGIN
      UPDATE [dbo].[core_system_info]
      SET [ver_major] = 1, [ver_minor] = 1, [ver_schema] = 0, [ver_build] = 0
      END

      IF NOT EXISTS(SELECT * FROM [core_version_info] WHERE [Oid] = N'c19df5be-79c7-4ffc-8699-c2cfac4be2bd')
      BEGIN
      INSERT [dbo].[core_version_info] ([oid], [orev], [database_version], [software_version]) VALUES (N'c19df5be-79c7-4ffc-8699-c2cfac4be2bd', 1, N'1.1.0.0', N'1.1.0.0')
      END
      ELSE
      BEGIN
      UPDATE [core_version_info] SET [database_version] = N'1.1.0.0', [software_version] = N'1.1.0.0'
      END

    