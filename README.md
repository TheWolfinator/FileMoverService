# FileMoverService Installer

This project contains a **Windows Service** called `FileMoverService`
targeting **.NET Framework 4.8** and a **WiX Toolset setup project** to
build an installer (`.msi`).

## Prerequisites

-   [.NET Framework 4.8 Developer
    Pack](https://dotnet.microsoft.com/download/dotnet-framework/net48)
-   [WiX Toolset 3.11](https://wixtoolset.org/releases/v3-11-2/)
-   (Optional, for Visual Studio integration) [WiX Toolset Visual Studio
    Extension](https://marketplace.visualstudio.com/items?itemName=WixToolset.WixToolsetVisualStudio2019)

## Setup

1.  Clone the repository:

    ``` bash
    git clone https://github.com/yourusername/FileMoverServiceInstaller.git
    cd FileMoverServiceInstaller
    ```

2.  Restore NuGet packages (if needed):

    ``` bash
    nuget restore FileMoverServiceInstaller.sln
    ```

3.  Open the solution in **Visual Studio**.

4.  Build the **FileMoverService** project (generates the Windows
    Service EXE).

5.  Build the **WiX setup project** (generates the `.msi` installer).

    The installer will be located in the `bin/Debug` or `bin/Release`
    folder of the WiX project.

## How It Works

-   The **FileMoverService** is a Windows Service that runs in the
    background.
-   The **WiX installer** creates an `.msi` package which installs and
    registers the service with Windows.


