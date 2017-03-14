


    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK933E62B7260EFD8]') AND parent_object_id = OBJECT_ID('[core_application_session]'))

alter table [core_application_session]  drop constraint FK933E62B7260EFD8



go





    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK5003B28BD20F26B2]') AND parent_object_id = OBJECT_ID('[core_category_access]'))

alter table [core_category_access]  drop constraint FK5003B28BD20F26B2



go





    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK56E0A009D20F26B2]') AND parent_object_id = OBJECT_ID('[core_class_access]'))

alter table [core_class_access]  drop constraint FK56E0A009D20F26B2



go





    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKE5F90C45D20F26B2]') AND parent_object_id = OBJECT_ID('[core_field_access_roles]'))

alter table [core_field_access_roles]  drop constraint FKE5F90C45D20F26B2



go





    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKE5F90C45AE8B319F]') AND parent_object_id = OBJECT_ID('[core_field_access_roles]'))

alter table [core_field_access_roles]  drop constraint FKE5F90C45AE8B319F



go





    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK5D5A586C260EFD8]') AND parent_object_id = OBJECT_ID('[core_log_entry_viewed_by]'))

alter table [core_log_entry_viewed_by]  drop constraint FK5D5A586C260EFD8



go





    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK5D5A586CB9613EA7]') AND parent_object_id = OBJECT_ID('[core_log_entry_viewed_by]'))

alter table [core_log_entry_viewed_by]  drop constraint FK5D5A586CB9613EA7



go





    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK7060868A5C85D2E]') AND parent_object_id = OBJECT_ID('[core_role_start_page_commands]'))

alter table [core_role_start_page_commands]  drop constraint FK7060868A5C85D2E



go





    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK7060868D20F26B2]') AND parent_object_id = OBJECT_ID('[core_role_start_page_commands]'))

alter table [core_role_start_page_commands]  drop constraint FK7060868D20F26B2



go





    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK9E6B43DEB3375D98]') AND parent_object_id = OBJECT_ID('[core_role_function_access]'))

alter table [core_role_function_access]  drop constraint FK9E6B43DEB3375D98



go





    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK9E6B43DED20F26B2]') AND parent_object_id = OBJECT_ID('[core_role_function_access]'))

alter table [core_role_function_access]  drop constraint FK9E6B43DED20F26B2



go





    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKCB69BAD9D20F26B2]') AND parent_object_id = OBJECT_ID('[core_user_roles]'))

alter table [core_user_roles]  drop constraint FKCB69BAD9D20F26B2



go





    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKCB69BAD9260EFD8]') AND parent_object_id = OBJECT_ID('[core_user_roles]'))

alter table [core_user_roles]  drop constraint FKCB69BAD9260EFD8



go





    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKA878687F260EFD8]') AND parent_object_id = OBJECT_ID('[core_profile]'))

alter table [core_profile]  drop constraint FKA878687F260EFD8



go





    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK18499AC4454AE32D]') AND parent_object_id = OBJECT_ID('[core_profile_items]'))

alter table [core_profile_items]  drop constraint FK18499AC4454AE32D



go





    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK180940532160F210]') AND parent_object_id = OBJECT_ID('[core_criterion]'))

alter table [core_criterion]  drop constraint FK180940532160F210



go





    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK5CF4B9F151153D4A]') AND parent_object_id = OBJECT_ID('[documents_attached_document]'))

alter table [documents_attached_document]  drop constraint FK5CF4B9F151153D4A



go





    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK4403E2D51B5C1577]') AND parent_object_id = OBJECT_ID('[documents_stored_file]'))

alter table [documents_stored_file]  drop constraint FK4403E2D51B5C1577



go





    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKF8C85DD28C0C498]') AND parent_object_id = OBJECT_ID('[club_club_calendar]'))

alter table [club_club_calendar]  drop constraint FKF8C85DD28C0C498



go





    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKF8C85DD297D4C465]') AND parent_object_id = OBJECT_ID('[club_club_calendar]'))

alter table [club_club_calendar]  drop constraint FKF8C85DD297D4C465



go





    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK64E97048260EFD8]') AND parent_object_id = OBJECT_ID('[club_committee_admin]'))

alter table [club_committee_admin]  drop constraint FK64E97048260EFD8



go





    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK64E970481010492]') AND parent_object_id = OBJECT_ID('[club_committee_admin]'))

alter table [club_committee_admin]  drop constraint FK64E970481010492



go





    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK37EFB5AD6A96A258]') AND parent_object_id = OBJECT_ID('[club_committee_member]'))

alter table [club_committee_member]  drop constraint FK37EFB5AD6A96A258



go





    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK37EFB5AD1010492]') AND parent_object_id = OBJECT_ID('[club_committee_member]'))

alter table [club_committee_member]  drop constraint FK37EFB5AD1010492



go





    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK37EFB5AD1BF806FC]') AND parent_object_id = OBJECT_ID('[club_committee_member]'))

alter table [club_committee_member]  drop constraint FK37EFB5AD1BF806FC



go





    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK7206BC6B1010492]') AND parent_object_id = OBJECT_ID('[club_committee_minute]'))

alter table [club_committee_minute]  drop constraint FK7206BC6B1010492



go





    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKC600C4439B3B4AD1]') AND parent_object_id = OBJECT_ID('[club_lotto]'))

alter table [club_lotto]  drop constraint FKC600C4439B3B4AD1



go





    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK73954481696D5F52]') AND parent_object_id = OBJECT_ID('[club_lotto_result]'))

alter table [club_lotto_result]  drop constraint FK73954481696D5F52



go





    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK22AAC0979B3B4AD1]') AND parent_object_id = OBJECT_ID('[club_lotto_result_winner]'))

alter table [club_lotto_result_winner]  drop constraint FK22AAC0979B3B4AD1



go





    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK22AAC0976A96A258]') AND parent_object_id = OBJECT_ID('[club_lotto_result_winner]'))

alter table [club_lotto_result_winner]  drop constraint FK22AAC0976A96A258



go





    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKCDD729FF6A96A258]') AND parent_object_id = OBJECT_ID('[club_lotto_ticket_direct_debit]'))

alter table [club_lotto_ticket_direct_debit]  drop constraint FKCDD729FF6A96A258



go





    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK18C1DAF22D43972B]') AND parent_object_id = OBJECT_ID('[club_person]'))

alter table [club_person]  drop constraint FK18C1DAF22D43972B



go





    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKCFB810946A96A258]') AND parent_object_id = OBJECT_ID('[club_person_guardian]'))

alter table [club_person_guardian]  drop constraint FKCFB810946A96A258



go





    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKCFB810945F0B89DE]') AND parent_object_id = OBJECT_ID('[club_person_guardian]'))

alter table [club_person_guardian]  drop constraint FKCFB810945F0B89DE



go





    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKE18D18386A96A258]') AND parent_object_id = OBJECT_ID('[club_person_membership_type]'))

alter table [club_person_membership_type]  drop constraint FKE18D18386A96A258



go





    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKE18D1838396E7F79]') AND parent_object_id = OBJECT_ID('[club_person_membership_type]'))

alter table [club_person_membership_type]  drop constraint FKE18D1838396E7F79



go





    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK94B835F86A96A258]') AND parent_object_id = OBJECT_ID('[club_person_qualification]'))

alter table [club_person_qualification]  drop constraint FK94B835F86A96A258



go





    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK94B835F8BD91226A]') AND parent_object_id = OBJECT_ID('[club_person_qualification]'))

alter table [club_person_qualification]  drop constraint FK94B835F8BD91226A



go





    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKBF9DF5E2260EFD8]') AND parent_object_id = OBJECT_ID('[club_team_admin]'))

alter table [club_team_admin]  drop constraint FKBF9DF5E2260EFD8



go





    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKBF9DF5E28C0C498]') AND parent_object_id = OBJECT_ID('[club_team_admin]'))

alter table [club_team_admin]  drop constraint FKBF9DF5E28C0C498



go





    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKABB107856A96A258]') AND parent_object_id = OBJECT_ID('[club_team_member]'))

alter table [club_team_member]  drop constraint FKABB107856A96A258



go





    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKABB107858C0C498]') AND parent_object_id = OBJECT_ID('[club_team_member]'))

alter table [club_team_member]  drop constraint FKABB107858C0C498



go





    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FKABB107858B904C2E]') AND parent_object_id = OBJECT_ID('[club_team_member]'))

alter table [club_team_member]  drop constraint FKABB107858B904C2E



go





    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK6BE3E3628C0C498]') AND parent_object_id = OBJECT_ID('[club_team_sheet]'))

alter table [club_team_sheet]  drop constraint FK6BE3E3628C0C498



go





    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK29C9BD82B6D6ABC1]') AND parent_object_id = OBJECT_ID('[club_team_sheet_person]'))

alter table [club_team_sheet_person]  drop constraint FK29C9BD82B6D6ABC1



go





    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK29C9BD826A96A258]') AND parent_object_id = OBJECT_ID('[club_team_sheet_person]'))

alter table [club_team_sheet_person]  drop constraint FK29C9BD826A96A258



go





    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK29C9BD829CB697D]') AND parent_object_id = OBJECT_ID('[club_team_sheet_person]'))

alter table [club_team_sheet_person]  drop constraint FK29C9BD829CB697D



go





    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK96C055CFE8E90626]') AND parent_object_id = OBJECT_ID('[core_incrementing_property_id]'))

alter table [core_incrementing_property_id]  drop constraint FK96C055CFE8E90626



go





    if exists (select 1 from sys.objects where object_id = OBJECT_ID(N'[FK16F3770AE8E90626]') AND parent_object_id = OBJECT_ID('[core_incrementing_property_yearly_id]'))

alter table [core_incrementing_property_yearly_id]  drop constraint FK16F3770AE8E90626



go





    if exists (select * from dbo.sysobjects where id = object_id(N'[core_application_session]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [core_application_session]

go





    if exists (select * from dbo.sysobjects where id = object_id(N'[core_category_access]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [core_category_access]

go





    if exists (select * from dbo.sysobjects where id = object_id(N'[core_class_access]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [core_class_access]

go





    if exists (select * from dbo.sysobjects where id = object_id(N'[core_class_command]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [core_class_command]

go





    if exists (select * from dbo.sysobjects where id = object_id(N'[core_command]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [core_command]

go





    if exists (select * from dbo.sysobjects where id = object_id(N'[core_display_format]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [core_display_format]

go





    if exists (select * from dbo.sysobjects where id = object_id(N'[core_field_access]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [core_field_access]

go





    if exists (select * from dbo.sysobjects where id = object_id(N'[core_field_access_roles]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [core_field_access_roles]

go





    if exists (select * from dbo.sysobjects where id = object_id(N'[core_field_view_map]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [core_field_view_map]

go





    if exists (select * from dbo.sysobjects where id = object_id(N'[core_function]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [core_function]

go





    if exists (select * from dbo.sysobjects where id = object_id(N'[core_incrementing_property_config]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [core_incrementing_property_config]

go





    if exists (select * from dbo.sysobjects where id = object_id(N'[core_log_entry]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [core_log_entry]

go





    if exists (select * from dbo.sysobjects where id = object_id(N'[core_log_entry_viewed_by]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [core_log_entry_viewed_by]

go





    if exists (select * from dbo.sysobjects where id = object_id(N'[core_property_description]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [core_property_description]

go





    if exists (select * from dbo.sysobjects where id = object_id(N'[core_role]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [core_role]

go





    if exists (select * from dbo.sysobjects where id = object_id(N'[core_role_start_page_commands]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [core_role_start_page_commands]

go





    if exists (select * from dbo.sysobjects where id = object_id(N'[core_role_function_access]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [core_role_function_access]

go





    if exists (select * from dbo.sysobjects where id = object_id(N'[core_system_info]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [core_system_info]

go





    if exists (select * from dbo.sysobjects where id = object_id(N'[core_user]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [core_user]

go





    if exists (select * from dbo.sysobjects where id = object_id(N'[core_user_roles]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [core_user_roles]

go





    if exists (select * from dbo.sysobjects where id = object_id(N'[core_version_info]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [core_version_info]

go





    if exists (select * from dbo.sysobjects where id = object_id(N'[core_profile]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [core_profile]

go





    if exists (select * from dbo.sysobjects where id = object_id(N'[core_profile_items]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [core_profile_items]

go





    if exists (select * from dbo.sysobjects where id = object_id(N'[core_criterion]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [core_criterion]

go





    if exists (select * from dbo.sysobjects where id = object_id(N'[core_search_spec]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [core_search_spec]

go





    if exists (select * from dbo.sysobjects where id = object_id(N'[documents_attached_document]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [documents_attached_document]

go





    if exists (select * from dbo.sysobjects where id = object_id(N'[documents_scanned_document]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [documents_scanned_document]

go





    if exists (select * from dbo.sysobjects where id = object_id(N'[documents_stored_file]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [documents_stored_file]

go





    if exists (select * from dbo.sysobjects where id = object_id(N'[documents_type_path_configuration]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [documents_type_path_configuration]

go





    if exists (select * from dbo.sysobjects where id = object_id(N'[club_club_calendar]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [club_club_calendar]

go





    if exists (select * from dbo.sysobjects where id = object_id(N'[club_club_calendar_event_type]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [club_club_calendar_event_type]

go





    if exists (select * from dbo.sysobjects where id = object_id(N'[club_club_details]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [club_club_details]

go





    if exists (select * from dbo.sysobjects where id = object_id(N'[club_committee]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [club_committee]

go





    if exists (select * from dbo.sysobjects where id = object_id(N'[club_committee_admin]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [club_committee_admin]

go





    if exists (select * from dbo.sysobjects where id = object_id(N'[club_committee_member]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [club_committee_member]

go





    if exists (select * from dbo.sysobjects where id = object_id(N'[club_committee_member_type]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [club_committee_member_type]

go





    if exists (select * from dbo.sysobjects where id = object_id(N'[club_committee_minute]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [club_committee_minute]

go





    if exists (select * from dbo.sysobjects where id = object_id(N'[club_lotto]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [club_lotto]

go





    if exists (select * from dbo.sysobjects where id = object_id(N'[club_lotto_result]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [club_lotto_result]

go





    if exists (select * from dbo.sysobjects where id = object_id(N'[club_lotto_result_winner]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [club_lotto_result_winner]

go





    if exists (select * from dbo.sysobjects where id = object_id(N'[club_lotto_ticket_direct_debit]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [club_lotto_ticket_direct_debit]

go





    if exists (select * from dbo.sysobjects where id = object_id(N'[club_membership_type]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [club_membership_type]

go





    if exists (select * from dbo.sysobjects where id = object_id(N'[club_person]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [club_person]

go





    if exists (select * from dbo.sysobjects where id = object_id(N'[club_person_guardian]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [club_person_guardian]

go





    if exists (select * from dbo.sysobjects where id = object_id(N'[club_person_membership_type]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [club_person_membership_type]

go





    if exists (select * from dbo.sysobjects where id = object_id(N'[club_person_qualification]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [club_person_qualification]

go





    if exists (select * from dbo.sysobjects where id = object_id(N'[club_person_title]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [club_person_title]

go





    if exists (select * from dbo.sysobjects where id = object_id(N'[club_qualification]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [club_qualification]

go





    if exists (select * from dbo.sysobjects where id = object_id(N'[club_team]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [club_team]

go





    if exists (select * from dbo.sysobjects where id = object_id(N'[club_team_admin]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [club_team_admin]

go





    if exists (select * from dbo.sysobjects where id = object_id(N'[club_team_member]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [club_team_member]

go





    if exists (select * from dbo.sysobjects where id = object_id(N'[club_team_member_type]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [club_team_member_type]

go





    if exists (select * from dbo.sysobjects where id = object_id(N'[club_team_position]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [club_team_position]

go





    if exists (select * from dbo.sysobjects where id = object_id(N'[club_team_sheet]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [club_team_sheet]

go





    if exists (select * from dbo.sysobjects where id = object_id(N'[club_team_sheet_person]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [club_team_sheet_person]

go





    if exists (select * from dbo.sysobjects where id = object_id(N'[core_incrementing_id]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [core_incrementing_id]

go





    if exists (select * from dbo.sysobjects where id = object_id(N'[core_incrementing_property_id]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [core_incrementing_property_id]

go





    if exists (select * from dbo.sysobjects where id = object_id(N'[core_incrementing_property_yearly_id]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [core_incrementing_property_yearly_id]

go





    if exists (select * from dbo.sysobjects where id = object_id(N'[core_incrementing_yearly_id]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) drop table [core_incrementing_yearly_id]

go





    create table [core_application_session] (

        [oid] UNIQUEIDENTIFIER not null,

       [orev] INT not null,

       [date] DATETIME default getdate()  not null,

       [session_id] INT default @@SPID  not null,

       [host] NVARCHAR(50) default HOST_NAME()  null,

       [id] NVARCHAR(255) null,

       [process_id] INT default 0  not null,

       [end_date] DATETIME null,

       [user] UNIQUEIDENTIFIER null,

       primary key ([oid])

    )

go





    create table [core_category_access] (

        [oid] UNIQUEIDENTIFIER not null,

       [orev] INT not null,

       [category] NVARCHAR(255) not null,

       [access] INT default 0  not null,

       [role] UNIQUEIDENTIFIER null,

       primary key ([oid])

    )

go





    create table [core_class_access] (

        [oid] UNIQUEIDENTIFIER not null,

       [orev] INT not null,

       [class_type] NVARCHAR(255) null,

       [property] NVARCHAR(255) null,

       [value] NVARCHAR(255) null,

       [access] INT default 0  not null,

       [role] UNIQUEIDENTIFIER null,

       primary key ([oid])

    )

go





    create table [core_class_command] (

        [oid] UNIQUEIDENTIFIER not null,

       [orev] INT not null,

       [class_type] NVARCHAR(255) null,

       [command_index] INT default 0  not null,

       [point] NCHAR(1) default 0  not null,

       [order] NCHAR(1) default 0  not null,

       [command_type] NVARCHAR(255) null,

       [init_data] NVARCHAR(MAX) null,

       [property] NVARCHAR(100) null,

       primary key ([oid])

    )

go





    create table [core_command] (

        [oid] UNIQUEIDENTIFIER not null,

       [orev] INT not null,

       [icon] VARBINARY(MAX) null,

       [description] NVARCHAR(255) null,

       [concrete_command] NVARCHAR(255) null,

       [init_data] NVARCHAR(MAX) null,

       primary key ([oid])

    )

go





    create table [core_display_format] (

        [oid] UNIQUEIDENTIFIER not null,

       [orev] INT not null,

       [class_type] NVARCHAR(255) not null unique,

       [display_name] NVARCHAR(50) null,

       [default_sort] NVARCHAR(255) null,

       [summary_format] NVARCHAR(255) null,

       [list_format] NVARCHAR(255) null,

       [tabs] NVARCHAR(MAX) null,

       primary key ([oid])

    )

go





    create table [core_field_access] (

        [oid] UNIQUEIDENTIFIER not null,

       [orev] INT not null,

       [access] INT default 0  not null,

       [field_name] NVARCHAR(50) not null,

       primary key ([oid])

    )

go





    create table [core_field_access_roles] (

        [field_access] UNIQUEIDENTIFIER not null,

       [role] UNIQUEIDENTIFIER not null

    )

go





    create table [core_field_view_map] (

        [oid] UNIQUEIDENTIFIER not null,

       [orev] INT not null,

       [display_name] NVARCHAR(255) null,

       [field_name] NVARCHAR(255) not null,

       [is_hidden] BIT default 0  not null,

       [view_class] NVARCHAR(255) null,

       [row] INT null,

       [col] INT null,

       [col_span] INT null,

       [row_span] INT null,

       [max_length] INT null,

       [min_length] INT null,

       [show_display_name] BIT default 0  not null,

       [prefix_label] NVARCHAR(255) null,

       [tab] NVARCHAR(255) null,

       primary key ([oid])

    )

go





    create table [core_function] (

        [oid] UNIQUEIDENTIFIER not null,

       [orev] INT not null,

       [name] NVARCHAR(255) null,

       [description] NVARCHAR(255) null,

       primary key ([oid])

    )

go





    create table [core_incrementing_property_config] (

        [oid] UNIQUEIDENTIFIER not null,

       [orev] INT not null,

       [applies_to] NVARCHAR(255) null,

       [property] NVARCHAR(255) not null,

       [format] NVARCHAR(255) not null,

       [id_length] INT default 6  not null,

       primary key ([oid])

    )

go





    create table [core_log_entry] (

        [oid] UNIQUEIDENTIFIER not null,

       [orev] INT not null,

       [level] INT default 0  not null,

       [sub_system] NVARCHAR(255) not null,

       [description] NVARCHAR(255) not null,

       [details] NVARCHAR(MAX) null,

       [dismissed] BIT default 0  not null,

       [date] DATETIME not null,

       primary key ([oid])

    )

go





    create table [core_log_entry_viewed_by] (

        [log_entry] UNIQUEIDENTIFIER not null,

       [user] UNIQUEIDENTIFIER not null

    )

go





    create table [core_property_description] (

        [oid] UNIQUEIDENTIFIER not null,

       [orev] INT not null,

       [class_type] NVARCHAR(255) not null,

       [property] NVARCHAR(255) not null,

       [description] NVARCHAR(255) not null,

       primary key ([oid])

    )

go





    create table [core_role] (

        [oid] UNIQUEIDENTIFIER not null,

       [orev] INT not null,

       [name] NVARCHAR(80) not null unique,

       [active] BIT default 0  not null,

       [description] NVARCHAR(255) null,

       [default_access] INT default 0  not null,

       primary key ([oid])

    )

go





    create table [core_role_start_page_commands] (

        [role] UNIQUEIDENTIFIER not null,

       [command] UNIQUEIDENTIFIER not null,

       [order] INT not null,

       primary key ([role], [order])

    )

go





    create table [core_role_function_access] (

        [role] UNIQUEIDENTIFIER not null,

       [function] UNIQUEIDENTIFIER not null

    )

go





    create table [core_system_info] (

        [oid] UNIQUEIDENTIFIER not null,

       [orev] INT not null,

       [application_name] NVARCHAR(255) null,

       [customer_name] NVARCHAR(255) null,

       [ver_major] INT default 0  not null,

       [ver_minor] INT default 0  not null,

       [ver_schema] INT default 0  not null,

       [ver_build] INT default 0  not null,

       primary key ([oid])

    )

go





    create table [core_user] (

        [oid] UNIQUEIDENTIFIER not null,

       [orev] INT not null,

       [login] NVARCHAR(100) not null unique,

       [password] NVARCHAR(30) null,

       [full_name] NVARCHAR(100) null,

       [email] NVARCHAR(100) null,

       [title] NVARCHAR(30) null,

       [no_of_failed_logins] INT default 0  not null,

       [account_locked] BIT default 0  not null,

       [is_active] BIT default 1  not null,

       primary key ([oid])

    )

go





    create table [core_user_roles] (

        [user] UNIQUEIDENTIFIER not null,

       [role] UNIQUEIDENTIFIER not null,

       [order] INT not null,

       primary key ([user], [order])

    )

go





    create table [core_version_info] (

        [oid] UNIQUEIDENTIFIER not null,

       [orev] INT not null,

       [database_version] NVARCHAR(255) null,

       [software_version] NVARCHAR(255) null,

       primary key ([oid])

    )

go





    create table [core_profile] (

        [oid] UNIQUEIDENTIFIER not null,

       [option] INT default 0  not null,

       [application] NVARCHAR(30) null,

       [user] UNIQUEIDENTIFIER null,

       primary key ([oid])

    )

go





    create table [core_profile_items] (

        [profile] UNIQUEIDENTIFIER not null,

       [value] NVARCHAR(MAX) null,

       [key] NVARCHAR(255) not null,

       primary key ([profile], [key])

    )

go





    create table [core_criterion] (

        [oid] UNIQUEIDENTIFIER not null,

       [type] NCHAR(1) not null,

       [orev] INT not null,

       [field_name] NVARCHAR(255) not null,

       [caption] NVARCHAR(255) not null,

       [field_type] NVARCHAR(255) null,

       [match_type] NCHAR(1) default 0  not null,

       [is_mandatory] BIT default 0  not null,

       [is_visible] BIT default 1  not null,

       [check_for_null] BIT default 0  not null,

       [lookup_index] NVARCHAR(255) null,

       [dictionary_name] NVARCHAR(255) null,

       [lookup_type] NVARCHAR(255) null,

       [display_format] NVARCHAR(255) null,

       [use_specified_value] BIT default 0  not null,

       [lookup_property_name] NVARCHAR(255) null,

       [replacement_type] NCHAR(20) default 0  not null,

       [ascending] BIT default 1  not null,

       [parent] UNIQUEIDENTIFIER null,

       [search_spec] UNIQUEIDENTIFIER null,

       primary key ([oid])

    )

go





    create table [core_search_spec] (

        [oid] UNIQUEIDENTIFIER not null,

       [orev] INT not null,

       [header_fields] NVARCHAR(255) null,

       [command_class] NVARCHAR(255) null,

       [description] NVARCHAR(30) null,

       [applies_to] NVARCHAR(255) null,

       [display_index] INT default 0  not null,

       primary key ([oid])

    )

go





    create table [documents_attached_document] (

        [oid] UNIQUEIDENTIFIER not null,

       [orev] INT not null,

       [applies_to] NVARCHAR(255) null,

       [applies_to_oid] UNIQUEIDENTIFIER default '00000000-0000-0000-0000-000000000000'  not null,

       [record_type_oid] UNIQUEIDENTIFIER null,

       [date_added] DATETIME null,

       [file] UNIQUEIDENTIFIER null,

       primary key ([oid])

    )

go





    create table [documents_scanned_document] (

        [oid] UNIQUEIDENTIFIER not null,

       [orev] INT not null,

       [applies_to] NVARCHAR(255) null,

       [applies_to_oid] UNIQUEIDENTIFIER default '00000000-0000-0000-0000-000000000000'  not null,

       [date_added] DATETIME null,

       [reference] NVARCHAR(255) null,

       [sub_path] NVARCHAR(255) null,

       [document_type] NVARCHAR(255) null,

       [module] INT default 0  not null,

       [module_description] NVARCHAR(255) null,

       primary key ([oid])

    )

go





    create table [documents_stored_file] (

        [oid] UNIQUEIDENTIFIER not null,

       [type] NCHAR(1) not null,

       [orev] INT not null,

       [mime_type] NVARCHAR(255) null,

       [name] NVARCHAR(255) null,

       [data] VARBINARY(MAX) null,

       [sub_path] NVARCHAR(255) null,

       [type_path_configuration] UNIQUEIDENTIFIER null,

       [key] NVARCHAR(255) null,

       primary key ([oid])

    )

go





    create table [documents_type_path_configuration] (

        [oid] UNIQUEIDENTIFIER not null,

       [type] NCHAR(1) not null,

       [orev] INT not null,

       [applies_to] NVARCHAR(255) null unique,

       [path_config] NVARCHAR(255) null,

       primary key ([oid])

    )

go





    create table [club_club_calendar] (

        [oid] UNIQUEIDENTIFIER not null,

       [orev] INT not null,

       [is_all_day] BIT default 0  not null,

       [is_reminder] BIT default 0  not null,

       [title] NVARCHAR(255) null,

       [description] NVARCHAR(500) null,

       [start] DATETIME not null,

       [end] DATETIME null,

       [recurrence_i_d] NVARCHAR(255) null,

       [recurrence_exception] NVARCHAR(255) null,

       [recurrence_rule] NVARCHAR(255) null,

       [url] NVARCHAR(255) null,

       [team] UNIQUEIDENTIFIER null,

       [club_calendar_event_type] UNIQUEIDENTIFIER not null,

       primary key ([oid])

    )

go





    create table [club_club_calendar_event_type] (

        [oid] UNIQUEIDENTIFIER not null,

       [orev] INT not null,

       [active] BIT default 1  not null,

       [default] BIT default 0  not null,

       [name] NVARCHAR(255) not null,

       [description] NVARCHAR(255) null,

       [color_hex] NVARCHAR(255) null,

       primary key ([oid])

    )

go





    create table [club_club_details] (

        [oid] UNIQUEIDENTIFIER not null,

       [orev] INT not null,

       [key] INT default 0  not null,

       [name] NVARCHAR(255) not null,

       [description] NVARCHAR(255) null,

       [history] NVARCHAR(MAX) null,

       [address_number] NVARCHAR(200) null,

       [address_street] NVARCHAR(200) null,

       [address_town] NVARCHAR(200) null,

       [address_county] NVARCHAR(200) null,

       [address_country] NVARCHAR(200) null,

       [address_post_code] NVARCHAR(255) null,

       [address_xlng_coord] FLOAT(53) null,

       [address_ylat_coord] FLOAT(53) null,

       primary key ([oid])

    )

go





    create table [club_committee] (

        [oid] UNIQUEIDENTIFIER not null,

       [orev] INT not null,

       [name] NVARCHAR(255) not null,

       [year] INT default 0  not null,

       [description] NVARCHAR(255) null,

       [start_date] DATETIME null,

       [end_date] DATETIME null,

       [active] BIT default 1  not null,

       [time_type] NCHAR(1) default 0  not null,

       primary key ([oid]),

      unique ([name], [year])

    )

go





    create table [club_committee_admin] (

        [oid] UNIQUEIDENTIFIER not null,

       [orev] INT not null,

       [user] UNIQUEIDENTIFIER not null,

       [committee] UNIQUEIDENTIFIER not null,

       primary key ([oid])

    )

go





    create table [club_committee_member] (

        [oid] UNIQUEIDENTIFIER not null,

       [orev] INT not null,

       [person] UNIQUEIDENTIFIER not null,

       [committee] UNIQUEIDENTIFIER not null,

       [committee_member_type] UNIQUEIDENTIFIER not null,

       primary key ([oid])

    )

go





    create table [club_committee_member_type] (

        [oid] UNIQUEIDENTIFIER not null,

       [orev] INT not null,

       [active] BIT default 1  not null,

       [default] BIT default 0  not null,

       [name] NVARCHAR(255) not null,

       [description] NVARCHAR(255) null,

       primary key ([oid])

    )

go





    create table [club_committee_minute] (

        [oid] UNIQUEIDENTIFIER not null,

       [orev] INT not null,

       [date] DATETIME default 'getdate()'  not null,

       [minutes_text] NVARCHAR(MAX) null,

       [active] BIT default 1  not null,

       [committee] UNIQUEIDENTIFIER not null,

       primary key ([oid])

    )

go





    create table [club_lotto] (

        [oid] UNIQUEIDENTIFIER not null,

       [orev] INT not null,

       [lotto_draw_date] DATETIME default 'getdate()'  not null,

       [message] NVARCHAR(255) null,

       [jackpot] DECIMAL(19,5) default 0  not null,

       [lotto_result] UNIQUEIDENTIFIER null,

       primary key ([oid])

    )

go





    create table [club_lotto_result] (

        [oid] UNIQUEIDENTIFIER not null,

       [orev] INT not null,

       [no1] INT default 0  not null,

       [no2] INT default 0  not null,

       [no3] INT default 0  not null,

       [no4] INT default 0  not null,

       [no5] INT default 0  not null,

       [lotto] UNIQUEIDENTIFIER not null,

       primary key ([oid])

    )

go





    create table [club_lotto_result_winner] (

        [oid] UNIQUEIDENTIFIER not null,

       [orev] INT not null,

       [message] NVARCHAR(255) null,

       [matches] INT default 0  not null,

       [no1] INT default 0  not null,

       [no2] INT default 0  not null,

       [no3] INT default 0  not null,

       [no4] INT default 0  not null,

       [lotto_result] UNIQUEIDENTIFIER not null,

       [person] UNIQUEIDENTIFIER not null,

       primary key ([oid])

    )

go





    create table [club_lotto_ticket_direct_debit] (

        [oid] UNIQUEIDENTIFIER not null,

       [orev] INT not null,

       [key] INT default 0  not null,

       [no1] INT default 0  not null,

       [no2] INT default 0  not null,

       [no3] INT default 0  not null,

       [no4] INT default 0  not null,

       [active] BIT default 1  not null,

       [start_date] DATETIME not null,

       [end_date] DATETIME null,

       [person] UNIQUEIDENTIFIER not null,

       primary key ([oid])

    )

go





    create table [club_membership_type] (

        [oid] UNIQUEIDENTIFIER not null,

       [orev] INT not null,

       [name] NVARCHAR(255) not null,

       [year] INT default 0  not null,

       [time_type] NCHAR(1) default 0  not null,

       [sex] NCHAR(1) default 0  not null,

       [key] INT default 0  not null,

       [active] BIT default 1  not null,

       [start_date] DATETIME null,

       [end_date] DATETIME null,

       [cost] FLOAT(53) null,

       primary key ([oid]),

      unique ([name], [year])

    )

go





    create table [club_person] (

        [oid] UNIQUEIDENTIFIER not null,

       [orev] INT not null,

       [key] INT IDENTITY(1,1) not null,

       [sex] NCHAR(1) default 0  not null,

       [active] BIT default 1  not null,

       [registration_number] NVARCHAR(255) null,

       [forename] NVARCHAR(255) not null,

       [surname] NVARCHAR(255) not null,

       [irish_name] NVARCHAR(255) null,

       [dob] DATETIME null,

       [phone] NVARCHAR(255) null,

       [mobile_no] NVARCHAR(255) null,

       [email] NVARCHAR(255) null,

       [alergies] BIT default 1  not null,

       [alergies_details] NVARCHAR(255) null,

       [comments] NVARCHAR(255) null,

       [player_profile] NVARCHAR(2000) null,

       [address_number] NVARCHAR(200) null,

       [address_street] NVARCHAR(200) null,

       [address_town] NVARCHAR(200) null,

       [address_county] NVARCHAR(200) null,

       [address_country] NVARCHAR(200) null,

       [address_post_code] NVARCHAR(255) null,

       [address_xlng_coord] FLOAT(53) null,

       [address_ylat_coord] FLOAT(53) null,

       [title] UNIQUEIDENTIFIER null,

       primary key ([oid])

    )

go





    create table [club_person_guardian] (

        [oid] UNIQUEIDENTIFIER not null,

       [orev] INT not null,

       [person] UNIQUEIDENTIFIER not null,

       [guardian] UNIQUEIDENTIFIER not null,

       primary key ([oid])

    )

go





    create table [club_person_membership_type] (

        [oid] UNIQUEIDENTIFIER not null,

       [orev] INT not null,

       [date] DATETIME default getdate()  not null,

       [active] BIT default 1  not null,

       [person] UNIQUEIDENTIFIER not null,

       [membership_type] UNIQUEIDENTIFIER not null,

       primary key ([oid])

    )

go





    create table [club_person_qualification] (

        [oid] UNIQUEIDENTIFIER not null,

       [orev] INT not null,

       [date_taken] DATETIME default getdate()  not null,

       [active] BIT default 1  not null,

       [result] NVARCHAR(255) null,

       [person] UNIQUEIDENTIFIER not null,

       [qualification] UNIQUEIDENTIFIER not null,

       primary key ([oid])

    )

go





    create table [club_person_title] (

        [oid] UNIQUEIDENTIFIER not null,

       [orev] INT not null,

       [description] NVARCHAR(10) null,

       [active] BIT default 1  not null,

       primary key ([oid])

    )

go





    create table [club_qualification] (

        [oid] UNIQUEIDENTIFIER not null,

       [orev] INT not null,

       [name] NVARCHAR(255) not null,

       [active] BIT default 1  not null,

       [description] NVARCHAR(1000) null,

       [cost] FLOAT(53) null,

       primary key ([oid])

    )

go





    create table [club_team] (

        [oid] UNIQUEIDENTIFIER not null,

       [orev] INT not null,

       [active] BIT default 1  not null,

       [name] NVARCHAR(255) not null,

       [time_type] NCHAR(1) default 0  not null,

       [sex] NCHAR(1) default 0  not null,

       [year] INT default 0  not null,

       [start_date] DATETIME null,

       [end_date] DATETIME null,

       primary key ([oid])

    )

go





    create table [club_team_admin] (

        [oid] UNIQUEIDENTIFIER not null,

       [orev] INT not null,

       [user] UNIQUEIDENTIFIER not null,

       [team] UNIQUEIDENTIFIER not null,

       primary key ([oid])

    )

go





    create table [club_team_member] (

        [oid] UNIQUEIDENTIFIER not null,

       [orev] INT not null,

       [person] UNIQUEIDENTIFIER not null,

       [team] UNIQUEIDENTIFIER not null,

       [team_member_type] UNIQUEIDENTIFIER not null,

       primary key ([oid])

    )

go





    create table [club_team_member_type] (

        [oid] UNIQUEIDENTIFIER not null,

       [orev] INT not null,

       [active] BIT default 1  not null,

       [admin_member] BIT default 0  not null,

       [playing_member] BIT default 0  not null,

       [default] BIT default 0  not null,

       [name] NVARCHAR(255) not null,

       [description] NVARCHAR(255) null,

       primary key ([oid])

    )

go





    create table [club_team_position] (

        [oid] UNIQUEIDENTIFIER not null,

       [orev] INT not null,

       [name] NVARCHAR(255) not null,

       [number] INT null,

       [active] BIT default 1  not null,

       primary key ([oid])

    )

go





    create table [club_team_sheet] (

        [oid] UNIQUEIDENTIFIER not null,

       [orev] INT not null,

       [match_date] DATETIME null,

       [notes] NVARCHAR(255) null,

       [opponent] NVARCHAR(255) null,

       [result] NVARCHAR(255) null,

       [team] UNIQUEIDENTIFIER not null,

       primary key ([oid])

    )

go





    create table [club_team_sheet_person] (

        [oid] UNIQUEIDENTIFIER not null,

       [orev] INT not null,

       [notes] NVARCHAR(255) null,

       [team_sheet] UNIQUEIDENTIFIER not null,

       [person] UNIQUEIDENTIFIER not null,

       [team_position] UNIQUEIDENTIFIER null,

       primary key ([oid])

    )

go





    create table [core_incrementing_id] (

        [oid] UNIQUEIDENTIFIER not null,

       [orev] INT not null,

       [applies_to] NVARCHAR(255) null,

       [current] INT default 0  not null,

       primary key ([oid])

    )

go





    create table [core_incrementing_property_id] (

        [oid] UNIQUEIDENTIFIER not null,

       [orev] INT not null,

       [applies_to] NVARCHAR(255) null,

       [current] INT default 0  not null,

       [property_config] UNIQUEIDENTIFIER not null unique,

       primary key ([oid])

    )

go





    create table [core_incrementing_property_yearly_id] (

        [oid] UNIQUEIDENTIFIER not null,

       [orev] INT not null,

       [year] INT default 0  not null,

       [short_year] TINYINT default 0  not null,

       [applies_to] NVARCHAR(255) null,

       [current] INT default 0  not null,

       [property_config] UNIQUEIDENTIFIER not null unique,

       primary key ([oid])

    )

go





    create table [core_incrementing_yearly_id] (

        [oid] UNIQUEIDENTIFIER not null,

       [orev] INT not null,

       [year] INT default 0  not null,

       [short_year] TINYINT default 0  not null,

       [applies_to] NVARCHAR(255) null,

       [current] INT default 0  not null,

       primary key ([oid])

    )

go





    alter table [core_application_session] 

        add constraint FK933E62B7260EFD8 

        foreign key ([user]) 

        references [core_user]

go





    alter table [core_category_access] 

        add constraint FK5003B28BD20F26B2 

        foreign key ([role]) 

        references [core_role]

go





    alter table [core_class_access] 

        add constraint FK56E0A009D20F26B2 

        foreign key ([role]) 

        references [core_role]

go





    alter table [core_field_access_roles] 

        add constraint FKE5F90C45D20F26B2 

        foreign key ([role]) 

        references [core_role]

go





    alter table [core_field_access_roles] 

        add constraint FKE5F90C45AE8B319F 

        foreign key ([field_access]) 

        references [core_field_access]

go





    alter table [core_log_entry_viewed_by] 

        add constraint FK5D5A586C260EFD8 

        foreign key ([user]) 

        references [core_user]

go





    alter table [core_log_entry_viewed_by] 

        add constraint FK5D5A586CB9613EA7 

        foreign key ([log_entry]) 

        references [core_log_entry]

go





    alter table [core_role_start_page_commands] 

        add constraint FK7060868A5C85D2E 

        foreign key ([command]) 

        references [core_command]

go





    alter table [core_role_start_page_commands] 

        add constraint FK7060868D20F26B2 

        foreign key ([role]) 

        references [core_role]

go





    alter table [core_role_function_access] 

        add constraint FK9E6B43DEB3375D98 

        foreign key ([function]) 

        references [core_function]

go





    alter table [core_role_function_access] 

        add constraint FK9E6B43DED20F26B2 

        foreign key ([role]) 

        references [core_role]

go





    alter table [core_user_roles] 

        add constraint FKCB69BAD9D20F26B2 

        foreign key ([role]) 

        references [core_role]

go





    alter table [core_user_roles] 

        add constraint FKCB69BAD9260EFD8 

        foreign key ([user]) 

        references [core_user]

go





    alter table [core_profile] 

        add constraint FKA878687F260EFD8 

        foreign key ([user]) 

        references [core_user]

go





    alter table [core_profile_items] 

        add constraint FK18499AC4454AE32D 

        foreign key ([profile]) 

        references [core_profile]

go





    alter table [core_criterion] 

        add constraint FK180940532160F210 

        foreign key ([search_spec]) 

        references [core_search_spec]

go





    alter table [documents_attached_document] 

        add constraint FK5CF4B9F151153D4A 

        foreign key ([file]) 

        references [documents_stored_file]

go





    alter table [documents_stored_file] 

        add constraint FK4403E2D51B5C1577 

        foreign key ([type_path_configuration]) 

        references [documents_type_path_configuration]

go





    alter table [club_club_calendar] 

        add constraint FKF8C85DD28C0C498 

        foreign key ([team]) 

        references [club_team]

go





    alter table [club_club_calendar] 

        add constraint FKF8C85DD297D4C465 

        foreign key ([club_calendar_event_type]) 

        references [club_club_calendar_event_type]

go





    alter table [club_committee_admin] 

        add constraint FK64E97048260EFD8 

        foreign key ([user]) 

        references [core_user]

go





    alter table [club_committee_admin] 

        add constraint FK64E970481010492 

        foreign key ([committee]) 

        references [club_committee]

go





    alter table [club_committee_member] 

        add constraint FK37EFB5AD6A96A258 

        foreign key ([person]) 

        references [club_person]

go





    alter table [club_committee_member] 

        add constraint FK37EFB5AD1010492 

        foreign key ([committee]) 

        references [club_committee]

go





    alter table [club_committee_member] 

        add constraint FK37EFB5AD1BF806FC 

        foreign key ([committee_member_type]) 

        references [club_committee_member_type]

go





    alter table [club_committee_minute] 

        add constraint FK7206BC6B1010492 

        foreign key ([committee]) 

        references [club_committee]

go





    alter table [club_lotto] 

        add constraint FKC600C4439B3B4AD1 

        foreign key ([lotto_result]) 

        references [club_lotto_result]

go





    alter table [club_lotto_result] 

        add constraint FK73954481696D5F52 

        foreign key ([lotto]) 

        references [club_lotto]

go





    alter table [club_lotto_result_winner] 

        add constraint FK22AAC0979B3B4AD1 

        foreign key ([lotto_result]) 

        references [club_lotto_result]

go





    alter table [club_lotto_result_winner] 

        add constraint FK22AAC0976A96A258 

        foreign key ([person]) 

        references [club_person]

go





    alter table [club_lotto_ticket_direct_debit] 

        add constraint FKCDD729FF6A96A258 

        foreign key ([person]) 

        references [club_person]

go





    alter table [club_person] 

        add constraint FK18C1DAF22D43972B 

        foreign key ([title]) 

        references [club_person_title]

go





    alter table [club_person_guardian] 

        add constraint FKCFB810946A96A258 

        foreign key ([person]) 

        references [club_person]

go





    alter table [club_person_guardian] 

        add constraint FKCFB810945F0B89DE 

        foreign key ([guardian]) 

        references [club_person]

go





    alter table [club_person_membership_type] 

        add constraint FKE18D18386A96A258 

        foreign key ([person]) 

        references [club_person]

go





    alter table [club_person_membership_type] 

        add constraint FKE18D1838396E7F79 

        foreign key ([membership_type]) 

        references [club_membership_type]

go





    alter table [club_person_qualification] 

        add constraint FK94B835F86A96A258 

        foreign key ([person]) 

        references [club_person]

go





    alter table [club_person_qualification] 

        add constraint FK94B835F8BD91226A 

        foreign key ([qualification]) 

        references [club_qualification]

go





    alter table [club_team_admin] 

        add constraint FKBF9DF5E2260EFD8 

        foreign key ([user]) 

        references [core_user]

go





    alter table [club_team_admin] 

        add constraint FKBF9DF5E28C0C498 

        foreign key ([team]) 

        references [club_team]

go





    alter table [club_team_member] 

        add constraint FKABB107856A96A258 

        foreign key ([person]) 

        references [club_person]

go





    alter table [club_team_member] 

        add constraint FKABB107858C0C498 

        foreign key ([team]) 

        references [club_team]

go





    alter table [club_team_member] 

        add constraint FKABB107858B904C2E 

        foreign key ([team_member_type]) 

        references [club_team_member_type]

go





    alter table [club_team_sheet] 

        add constraint FK6BE3E3628C0C498 

        foreign key ([team]) 

        references [club_team]

go





    alter table [club_team_sheet_person] 

        add constraint FK29C9BD82B6D6ABC1 

        foreign key ([team_sheet]) 

        references [club_team_sheet]

go





    alter table [club_team_sheet_person] 

        add constraint FK29C9BD826A96A258 

        foreign key ([person]) 

        references [club_person]

go





    alter table [club_team_sheet_person] 

        add constraint FK29C9BD829CB697D 

        foreign key ([team_position]) 

        references [club_team_position]

go





    alter table [core_incrementing_property_id] 

        add constraint FK96C055CFE8E90626 

        foreign key ([property_config]) 

        references [core_incrementing_property_config]

go





    alter table [core_incrementing_property_yearly_id] 

        add constraint FK16F3770AE8E90626 

        foreign key ([property_config]) 

        references [core_incrementing_property_config]

go
