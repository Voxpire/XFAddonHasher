@SET CURPATH=%~dp0

@SET EXENAME=XFAddonHasher

@TITLE: %EXENAME%

::##########

dotnet publish -p:PublishProfile=FolderProfile

@ECHO:
@ECHO: Done!
@ECHO:

@PAUSE

