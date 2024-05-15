## Installation

### Installation with InnoSetup

In the InnoSetup the `ServerRegistrationManager.exe` is used to install/uninstall the extension.

```
[Run]
Filename: "{app}\ServerRegistrationManager.exe"; Flags: postinstall runascurrentuser; Parameters: "install ""{app}\RevitShell.dll"" -codebase"

[UninstallRun]
Filename: "{app}\ServerRegistrationManager.exe"; Parameters: "uninstall ""{app}\RevitShell.dll"""; RunOnceId: "UninstallService"
```

### Installation with ServerRegistrationManager

Download `ServerRegistrationManager.zip` to install the extension.

* [ServerRegistrationManager.zip](https://github.com/dwmkerr/sharpshell/releases/latest/)

Install:

```cmd
ServerRegistrationManager.exe install RevitShell.dll
```

Uninstall:

```cmd
ServerRegistrationManager.exe uninstall RevitShell.dll
```

### Installation with ServerManager

Download `ServerManager.zip` to install the extension.

* [ServerManager.zip](https://github.com/dwmkerr/sharpshell/releases/latest/)

Load `RevitShell.dll` using `ServerManager.exe` and install/unistall the server.

