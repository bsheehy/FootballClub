# #########################################################################################################################
# target: dependencies
# [tab] system command
# make boot all_NewDatabaseAll
# #########################################################################################################################
# ##################################################  VARIABLES ###########################################################
DB=ClubFrRocks
RELEASE=""
#If RELEASE is set then name database using it.
ifneq ($(RELEASE),"")
override DB := $(DB)_$(RELEASE)
endif

$(info DB=$(DB))
DBSERVER=.\\SQL2012
OSQL=osql -E -S $(DBSERVER) -d $(DB) 
SQLCONNECTIONSTRING= "Server=$(DBSERVER);Database=$(DB);Trusted_Connection=True;"

Assemblies=CoreSpatial.dll\
  Core.dll\
  Documents.dll\
  Club.dll

SchemaGen=../../../Lancelot/tools/SchemaGen
# SchemaGen=../../../Lancelot/SchemaGen/bin/Debug/SchemaGen.exe
SCHEMASLICE=../../BuildTools/perl/schemaSlice.pl
REMOVEMARKED=../../../BuildTools/perl/removeMarked.pl
DBTOOLSDIR=../../BuildTools/DbTools

# Get the version numbers
VER_SCHEMA=$(shell cat ../build.version)
VER_MAJOR=$(shell date +%Y)
VER_MINOR=$(shell date +%m)
VER_BUILD=0
version := $(VER_MAJOR).$(VER_MINOR).$(VER_SCHEMA).$(VER_BUILD)

DBSETUP=../../DbTools/DbSetup/bin
CURDIR=$(shell pwd)


#ApexSQL Audit Trigger Command Line Interface 
#http://knowledgebase.apexsql.com/using-apexsql-audit-command-line-interface-cli-switches/
#NOTE: The ApexSQL CLI DOES NOT work with relative paths or non windows paths and it requires the FULL PATH to the project and template files.
ApexSQLTriggerDirWindows=C:\\development\\ApexSQLTrigger2015\\
ApexSQLTriggerExe=C:\\development\\ApexSQLTrigger2015\\ApexSQLTrigger.com
ApexSQLTriggerDir=../../ApexSQLTrigger2015/
ApexSQLTriggerTemplate=$(ApexSQLTriggerDirWindows)dfwTemplate_ApexSQL2053.audx
ApexSQLTriggerProjectFile=$(ApexSQLTriggerDirWindows)dfwProject_ApexSQL2053.axap


#http://knowledgebase.apexsql.com/using-apexsql-audit-command-line-interface-cli-switches/
#cp command copies the ApexSQL arch template and project file to ApexSQLTrigger2015 repos that contains alls the ApexSQL stuff
#First command line creates the Audit Tables, Stored Procs, Views and Functions
#Second command line creates the Audi Triggers on each table in the ApexSQL project file.
.PHONY: auditApexSQL
auditApexSQL:
	cp ../../DiamondFireWeb/db/audit/dfwTemplate_ApexSQL2053.audx $(ApexSQLTriggerDir) --force
	cp ../../DiamondFireWeb/db/audit/dfwProject_ApexSQL2053.axap $(ApexSQLTriggerDir) --force
	$(ApexSQLTriggerExe) /server:$(DBSERVER) /verbose /database:$(DB) /arch:$(ApexSQLTriggerTemplate) /project:$(ApexSQLTriggerProjectFile) /archinstall:a
	$(ApexSQLTriggerExe) /server:$(DBSERVER) /verbose /database:$(DB) /arch:$(ApexSQLTriggerTemplate) /project:$(ApexSQLTriggerProjectFile) /addtrigger


.PHONY: auditApexSQL_UseSqlScriptsOnly
auditApexSQL_UseSqlScriptsOnly: $(patsubst %.sql,%.log,$(sort $(wildcard ../../AuditApexSql/db/ApexSchema/tables/*.sql ../../AuditApexSql/db/ApexSchema/views/*.sql ../../AuditApexSql/db/ApexSchema/sp/*.sql audit/triggers/*.sql)))



# #########################################################################################################################

#default: debug
#default: tables

debug: BuildType=Debug
debug: installers

release: BuildType=Release
release: installers


#INFO: Call to drop and recreate the database.
boot:
	sed -e "s/<database>/$(DB)/g" $(DBTOOLSDIR)/scripts/kill_db_users.sql | osql -S $(DBSERVER) -E
	osql -S $(DBSERVER) -E -Q "if exists(select name from sysdatabases where name = '$(DB)') drop database [$(DB)]"
	osql -S $(DBSERVER) -E -Q "create database [$(DB)]"


#Use this to print a specific Variable value (useful in testing)
#Example: make print-CUSTOMER_NAME  (this will print out the CUSTOMER_NAME value)
print-%  : ; @echo $* = $($*)

#INFO: Clean away log files - - IF LOG FILE EXISTS SQL SCRIPTS WILL NOT RUN.
clean:
	rm -f *.log
	rm -f schema.sql
	rm -f ./*/*.log
	rm -f ./elmah/*/*.log
	rm -f ../../AuditApexSql/db/ApexSchema/*/*.log

	
#DELETES ALL .Log files so SQL files can be run again
cleanSqlLogs:
	rm -f ./tables/*.log
	rm -f ./staticdata/*.log
	rm -f ./triggers/*.log
	rm -f ./storedprocs/*.log
	rm -f ./functions/*.log
	rm -f ./scripts/*.log
	rm -f ../../AuditApexSql/db/storedprocs/*.log 
	rm -f ../../AuditApexSql/db/ApexSchema/*/*.log

# Setup the configuration required for the INSTALLER.
installers: mkinstalldir installscripts dbsetupbin


#Setup Database table structures with constraints, static data and audit-trail (no Diamond Fire - triggers, view, functions or stored procedures added)
tables: clean cleanSqlLogs schema.sql tables.log add_constraints.log elmah sqlStoredprocs staticData


#.PHONY: add_constraints.log
add_constraints.log: schema.sql
	$(SCHEMASLICE) constraints schema.sql | $(OSQL) | sqlcheck
	@echo ok > $@

# INFO: Create directories if not created
mkinstalldir:
	mkdir -p setup
	mkdir -p setup/scripts
	mkdir -p setup/schemas
	mkdir -p setup/upgrades


#INFO: Used to build and configure the installer setup folders and scripts. 
#Note folder names and files names are important do not change - > Overwrite file  >> Concat to file
installscripts: schema.sql mkinstalldir
	$(SCHEMASLICE) tables schema.sql > setup/schemas/tables.$(VER_SCHEMA).sql
	cat ./elmah/tables/*.sql >> setup/schemas/tables.$(VER_SCHEMA).sql		
	$(SCHEMASLICE) constraints schema.sql > setup/scripts/constraints.sql
	cat ./storedprocs/*.sql > setup/scripts/storedprocedures.sql
	cat ./elmah/storedprocs/*.sql >> setup/scripts/storedprocedures.sql
	cat ../../AuditApexSql/db/storedprocs/*.sql >> setup/scripts/storedprocedures.sql
	cat ./triggers/*.sql >> setup/scripts/triggers.sql


#elmah:	$(patsubst %.sql,%.log,$(wildcard ../../AuditTrail/dbLanceV2/functions/*.sql ../../AuditTrail/dbLanceV2/tables/*.sql ../../AuditTrail/dbLanceV2/storedprocs/*.sql  ../../AuditTrail/dbLanceV2/triggers/*.sql))
elmah: $(patsubst %.sql,%.log,$(wildcard elmah/storedprocs/*.sql elmah/tables/*.sql))


# INFO: Database - Setup	
dbsetupbin: mkinstalldir
	cp -f $(DBSETUP)/DbSetup.exe ./setup/
	cp -f $(DBSETUP)/DbSetup-console.exe ./setup/
	sed -e 's~<add key="version"[^/]*/>~<add key="version" value="$(version)"/>~g'\
             DbSetup.exe.config > setup/DbSetup.exe.config
	cp -f ./setup/DbSetup.exe.config ./setup/DbSetup-console.exe.config


.PHONY: schema.sql
schema.sql:
	cd ../FrRocks/bin/ && $(SchemaGen) $(Assemblies) | $(REMOVEMARKED)  > "$(CURDIR)/schema.sql"

# Generates static data SQL script by looking through the artefact classes for 'static properties'
#NOTE: This is not used anymore insatead SQLTools is used to create the static data script
static_objects:
	cd ../FrRocks/bin/ && $(SchemaGen) --static $(Assemblies) > "$(CURDIR)/staticdata/schemaGen/01_StaticObjectsSchemaGen.sql"


# Add the static objects using the SQL generated from 'static_objects'
staticData: static_objects $(patsubst %.sql,%.log,$(sort $(wildcard staticdata/*.sql)))


#.PHONY: deleteExistingTemplates
deleteExistingTemplates:
	rm -f $(TemplatesOutputDir)


#.PHONY: tables.log
tables.log: schema.sql
	$(SCHEMASLICE) tables schema.sql | $(OSQL) | sqlcheck
	@echo ok > $@


#.PHONY: add_constraints.log
add_constraints.log: schema.sql
	$(SCHEMASLICE) constraints schema.sql | $(OSQL) | sqlcheck
	@echo ok > $@


#Setup the test database
testsetup:
	./setup/DbSetup-console.exe -s .\\sql2008r2 -br True -i False -d diamondfireoffaly

#.PHONY: sqlStoredprocs
sqlStoredprocs: $(patsubst %.sql,%.log,$(wildcard storedprocs/*.sql))


#.PHONY: sqlIndexes
sqlIndexes: $(patsubst %.sql,%.log,$(sort $(wildcard indexes/*.sql)))


#.PHONY: sqlViews
sqlViews: $(patsubst %.sql,%.log,$(wildcard views/*.sql))


#.PHONY: sqlFunctions
sqlFunctions: $(patsubst %.sql,%.log,$(wildcard functions/*.sql))


#.PHONY: triggers (includes Audit triggers)
sqlTriggers: $(patsubst %.sql,%.log,$(wildcard triggers/*.sql))


.PHONY: importExistingData
importExistingData: $(patsubst %.sql,%.log,$(sort $(wildcard import/*.sql)))


%.log:%.sql
	$(OSQL) -i $< | sqlcheck
	@echo ok > $@


  # Builds for testing various components
all_NewDatabaseTest: boot clean tables importExistingData
	

# Builds for testing various components
all_NewDatabase: boot clean tables importExistingData auditApexSQL






