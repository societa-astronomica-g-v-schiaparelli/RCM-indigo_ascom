;
; Script generated by the ASCOM Driver Installer Script Generator 6.0.0.0
; Generated by CloudMakers, s. r. o. on 02/03/2017 (UTC)
;
[Setup]
AppID={{6a724ed0-c274-4761-b39f-a7ddf264843b}
AppName=ASCOM INDIGO Camera Driver
AppVerName=ASCOM INDIGO Camera Driver 6.3.0.3
AppVersion=6.3.0.3
AppPublisher=CloudMakers, s. r. o. <info@cloudmakers.eu>
AppPublisherURL=mailto:info@cloudmakers.eu
AppSupportURL=http://www.indigo-astronomy.org/
AppUpdatesURL=http://www.indigo-astronomy.org/for-users.html
VersionInfoVersion=1.0.1
MinVersion=0,5.0.2195sp4
DefaultDirName="{cf}\ASCOM\Camera"
DisableDirPage=yes
DisableProgramGroupPage=yes
OutputDir="."
OutputBaseFilename="INDIGO Camera Setup"
Compression=lzma
SolidCompression=yes
; Put there by Platform if Driver Installer Support selected
WizardImageFile="..\WizardImage.bmp"
LicenseFile="..\LICENSE.md"
; {cf}\ASCOM\Uninstall\Camera folder created by Platform, always
UninstallFilesDir="{cf}\ASCOM\Uninstall\Camera\INDIGO Camera"

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Dirs]
Name: "{cf}\ASCOM\Uninstall\Camera\INDIGO Camera"
; TODO: Add subfolders below {app} as needed (e.g. Name: "{app}\MyFolder")

[Files]
Source: "bin\Release\ASCOM.INDIGO.Camera.dll"; DestDir: "{app}"
Source: "bin\Release\ASCOM.INDIGO.dll"; DestDir: "{app}"
Source: "bin\Release\INDIGO.dll"; DestDir: "{app}"


; Only if driver is .NET
[Run]
; Only for .NET assembly/in-proc drivers
Filename: "{dotnet4032}\regasm.exe"; Parameters: "/codebase ""{app}\ASCOM.INDIGO.Camera.dll"""; Flags: runhidden 32bit
Filename: "{dotnet4064}\regasm.exe"; Parameters: "/codebase ""{app}\ASCOM.INDIGO.Camera.dll"""; Flags: runhidden 64bit; Check: IsWin64




; Only if driver is .NET
[UninstallRun]
; Only for .NET assembly/in-proc drivers
Filename: "{dotnet4032}\regasm.exe"; Parameters: "-u ""{app}\ASCOM.INDIGO.Camera.dll"""; Flags: runhidden 32bit
; This helps to give a clean uninstall
Filename: "{dotnet4064}\regasm.exe"; Parameters: "/codebase ""{app}\ASCOM.INDIGO.Camera.dll"""; Flags: runhidden 64bit; Check: IsWin64
Filename: "{dotnet4064}\regasm.exe"; Parameters: "-u ""{app}\ASCOM.INDIGO.Camera.dll"""; Flags: runhidden 64bit; Check: IsWin64




[CODE]
//
// Before the installer UI appears, verify that the (prerequisite)
// ASCOM Platform 6.0 or greater is installed, including both Helper
// components. Utility is required for all types (COM and .NET)!
//
function InitializeSetup(): Boolean;
var
   U : Variant;
   H : Variant;
begin
   Result := FALSE;  // Assume failure
   try
      H := CreateOLEObject('DriverHelper.Util');
   except
      MsgBox('The ASCOM DriverHelper object has failed to load, this indicates a serious problem with the ASCOM installation', mbInformation, MB_OK);
   end;
   try
      U := CreateOLEObject('ASCOM.Utilities.Util');
   except
      MsgBox('The ASCOM Utilities object has failed to load, this indicates that the ASCOM Platform has not been installed correctly', mbInformation, MB_OK);
   end;
   try
      if (U.IsMinimumRequiredVersion(6,0)) then	// this will work in all locales
         Result := TRUE;
   except
   end;
   if(not Result) then
      MsgBox('The ASCOM Platform 6.0 or greater is required for this driver.', mbInformation, MB_OK);
end;

// Code to enable the installer to uninstall previous versions of itself when a new version is installed
procedure CurStepChanged(CurStep: TSetupStep);
var
  ResultCode: Integer;
  UninstallExe: String;
  UninstallRegistry: String;
begin
  if (CurStep = ssInstall) then // Install step has started
	begin
      // Create the correct registry location name, which is based on the AppId
      UninstallRegistry := ExpandConstant('Software\Microsoft\Windows\CurrentVersion\Uninstall\{#SetupSetting("AppId")}' + '_is1');
      // Check whether an extry exists
      if RegQueryStringValue(HKLM, UninstallRegistry, 'UninstallString', UninstallExe) then
        begin // Entry exists and previous version is installed so run its uninstaller quietly after informing the user
          MsgBox('Setup will now remove the previous version.', mbInformation, MB_OK);
          Exec(RemoveQuotes(UninstallExe), ' /SILENT', '', SW_SHOWNORMAL, ewWaitUntilTerminated, ResultCode);
          sleep(1000);    //Give enough time for the install screen to be repainted before continuing
        end
  end;
end;

