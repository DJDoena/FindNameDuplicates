[Setup]
AppName=Find Name Duplicates
AppId=FindNameDuplicates
AppVerName=Find Name Duplicates 3.0.0.2
AppCopyright=Copyright � Doena Soft. 2009 - 2022
AppPublisher=Doena Soft.
AppPublisherURL=http://doena-journal.net/en/dvd-profiler-tools/
DefaultDirName={commonpf32}\Doena Soft.\Find Name Duplicates
; DefaultGroupName=Doena Soft.
DirExistsWarning=No
SourceDir=..\FindNameDuplicates\bin\x86\FindNameDuplicates
Compression=zip/9
AppMutex=InvelosDVDPro
OutputBaseFilename=FindNameDuplicatesSetup
OutputDir=..\..\..\..\FindNameDuplicatesSetup\Setup\FindNameDuplicates
MinVersion=0,6.1sp1
PrivilegesRequired=admin
WizardStyle=modern
DisableReadyPage=yes
ShowLanguageDialog=no
VersionInfoCompany=Doena Soft.
VersionInfoCopyright=2009 - 2022
VersionInfoDescription=Find Name Duplicates Setup
VersionInfoVersion=3.0.0.2
UninstallDisplayIcon={app}\djdsoft.ico

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Messages]
WinVersionTooLowError=This program requires Windows XP or above to be installed.%n%nWindows 9x, NT and 2000 are not supported.

[Types]
Name: "full"; Description: "Full installation"

[Files]
Source: "djdsoft.ico"; DestDir: "{app}"; Flags: ignoreversion
Source: "DoenaSoft.DVDProfilerHelper.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "DoenaSoft.DVDProfilerXML.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "DoenaSoft.FindNameDuplicates.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "DoenaSoft.FindNameDuplicates.pdb"; DestDir: "{app}"; Flags: ignoreversion
Source: "DoenaSoft.WindowsAPICodePack.Core.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "DoenaSoft.WindowsAPICodePack.Shell.dll"; DestDir: "{app}"; Flags: ignoreversion

Source: "de\DoenaSoft.DVDProfilerHelper.resources.dll"; DestDir: "{app}\de"; Flags: ignoreversion

Source: "Readme\readme.txt"; DestDir: "{app}\Readme"; Flags: ignoreversion
; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Run]
Filename: "{win}\Microsoft.NET\Framework\v4.0.30319\RegAsm.exe"; Parameters: "/codebase ""{app}\DoenaSoft.FindNameDuplicates.dll"""; Flags: runhidden

;[UninstallDelete]

[UninstallRun]
Filename: "{win}\Microsoft.NET\Framework\v4.0.30319\RegAsm.exe"; Parameters: "/u ""{app}\DoenaSoft.FindNameDuplicates.dll"""; Flags: runhidden

[Registry]
; Register - Cleanup ahead of time in case the user didn't uninstall the previous version.
Root: HKCR; Subkey: "CLSID\{{6E02321E-1226-417E-947B-11BC88C7676E}"; Flags: dontcreatekey deletekey
Root: HKCR; Subkey: "DoenaSoft.DVDProfiler.FindNameDuplicates.Plugin"; Flags: dontcreatekey deletekey
Root: HKCU; Subkey: "Software\Invelos Software\DVD Profiler\Plugins\Identified"; ValueType: none; ValueName: "{{6E02321E-1226-417E-947B-11BC88C7676E}"; ValueData: "0"; Flags: deletevalue
Root: HKLM; Subkey: "Software\Classes\CLSID\{{6E02321E-1226-417E-947B-11BC88C7676E}"; Flags: dontcreatekey deletekey
Root: HKLM; Subkey: "Software\Classes\DoenaSoft.DVDProfiler.FindNameDuplicates.Plugin"; Flags: dontcreatekey deletekey
; Unregister
Root: HKCR; Subkey: "CLSID\{{6E02321E-1226-417E-947B-11BC88C7676E}"; Flags: dontcreatekey uninsdeletekey
Root: HKCR; Subkey: "DoenaSoft.DVDProfiler.FindNameDuplicates.Plugin"; Flags: dontcreatekey uninsdeletekey
Root: HKCU; Subkey: "Software\Invelos Software\DVD Profiler\Plugins\Identified"; ValueType: none; ValueName: "{{6E02321E-1226-417E-947B-11BC88C7676E}"; ValueData: "0"; Flags: uninsdeletevalue
Root: HKLM; Subkey: "Software\Classes\CLSID\{{6E02321E-1226-417E-947B-11BC88C7676E}"; Flags: dontcreatekey uninsdeletekey
Root: HKLM; Subkey: "Software\Classes\DoenaSoft.DVDProfiler.FindNameDuplicates.Plugin"; Flags: dontcreatekey uninsdeletekey

[Code]
function IsDotNET4Detected(): boolean;
// Function to detect dotNet framework version 4
// Returns true if it is available, false it's not.
var
dotNetStatus: boolean;
begin
dotNetStatus := RegKeyExists(HKLM, 'SOFTWARE\Microsoft\NET Framework Setup\NDP\v4');
Result := dotNetStatus;
end;

function InitializeSetup(): Boolean;
// Called at the beginning of the setup package.
begin

if not IsDotNET4Detected then
begin
MsgBox( 'The Microsoft .NET Framework version 4 is not installed. Please install it and try again.', mbInformation, MB_OK );
Result := false;
end
else
Result := true;
end;