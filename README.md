# RevitShell

[![Visual Studio 2022](https://img.shields.io/badge/Visual%20Studio-2022-blue)](../..)
[![License MIT](https://img.shields.io/badge/License-MIT-blue.svg)](LICENSE)
[![.NET Framework 4.5](https://img.shields.io/badge/.NET%20Framework%204.5-blue.svg)](../..)
[![Nuke](https://img.shields.io/badge/Nuke-Build-blue)](https://nuke.build/)
[![Build](../../actions/workflows/Build.yml/badge.svg)](../../actions)

RevitShell uses [SharpShell](https://github.com/dwmkerr/sharpshell) to enable the creation of Windows Shell Extensions in .NET for Revit files.

## Features

Show Revit File Version Information / Open Revit File in the respect Revit version.

<img src=assets/RevitShell.Show.gif width=400><img src=assets/RevitShell.Open.2025.gif width=400>

## Revit Files Extension

Revit files have the following extensions:

* Project: `.rvt`
* Family: `.rfa`
* Project Template: `.rte`
* Family Template: `.rft`

## Dependencies

* [SharpShell](https://github.com/dwmkerr/sharpshell)
* [ServerRegistrationManager](https://www.nuget.org/packages/ServerRegistrationManager)
* [ricaun.Revit.FileInfo](https://github.com/ricaun-io/ricaun.Revit.FileInfo)
* [ricaun.Revit.Installation](https://github.com/ricaun-io/ricaun.Revit.Installation)

## Installation

### Installation with InnoSetup

In the InnoSetup the `ServerRegistrationManager.exe` is used to install/uninstall the extension.

```
[Run]
Filename: "{app}\ServerRegistrationManager.exe"; Flags: postinstall runascurrentuser; Parameters: "install ""{app}\RevitShell.dll"" -codebase"

[UninstallRun]
Filename: "{app}\ServerRegistrationManager.exe"; Parameters: "uninstall ""{app}\RevitShell.dll""";
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

## Release

* [Latest release](../../releases/latest)

## License

This project is [licensed](LICENSE) under the [MIT License](https://en.wikipedia.org/wiki/MIT_License).

---

Do you like this project? Please [star this project on GitHub](../../stargazers)!