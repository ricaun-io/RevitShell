; RevitShell.iss
; 
; created 22 05 2023
; by Luiz Henrique Cassettari 

#define AppId "{75C82D94-C2D8-4F8A-AB17-F98A8BC0E712}"
#define AppName "RevitShell"
#define AppVersion "1.0.0"
#define AppPublisher "ricaun"
#define AppComments "Windows Shell Extensions in .NET for Revit files. "
#define AppFolder "RevitShell"
#define AppURL "https://github.com/ricaun-io/RevitShell"
#define AppEmail ""

[Setup]

AppId={{#AppId}}
AppName={#AppName}
AppVersion={#AppVersion}
AppPublisher={#AppPublisher}
AppComments={#AppComments}

;VersionInfoVersion={#AppVersion}

AppPublisherURL={#AppURL}
AppSupportURL={#AppURL}
AppUpdatesURL={#AppURL}

DefaultDirName="{autopf}\{#AppFolder}"
DisableWelcomePage=no
DisableDirPage=no
DisableProgramGroupPage=yes
OutputBaseFilename="{#AppName} {#AppVersion}"
UninstallDisplayName="{#AppName}"

LicenseFile="..\LICENSE"

; ICON CONFIGURATION
SetupIconFile=icon.ico
UninstallDisplayIcon={app}\unins000.exe

; Size: 55x55
;WizardSmallImageFile=icon55.bmp   
; Size: 164x314
;WizardImageFile = icon164.bmp

; Languages
ShowLanguageDialog=no

[UninstallDelete]
Type: filesandordirs; Name: "{app}\*.*"

[Dirs]
Name: {app}; Permissions: users-full

[Languages]
Name: "en"; MessagesFile: "compiler:Default.isl";

[Files]
Source: "..\{#AppFolder}\bin\Release\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs signonce

[Run]
Filename: "{app}\ServerRegistrationManager.exe"; Parameters: "install ""{app}\RevitShell.dll"" -codebase"

[UninstallRun]
Filename: "{app}\ServerRegistrationManager.exe"; Parameters: "uninstall ""{app}\RevitShell.dll"""; RunOnceId: "UninstallService"