===========================================================
Build the Database
===========================================================

cd C:\development\FootballClub\db\

-- Database structure only
make boot all_NewDatabaseTest 


MY NEW CHANGE TEST...



To Build the release do:

NAnt -buildfile:"C:\development\FootballClub\Build\Build.build" createLiveRelease -D:setupVer.default=1.1.0.0 -D:build.type=Release -D:org.default=trunk -logfile:BuildOutput.txt
