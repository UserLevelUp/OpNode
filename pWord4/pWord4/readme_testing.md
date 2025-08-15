# Testing Documentation

This document outlines the setup for testing the pWord solution and the issues encountered during the process.

## Test Solutions

Two new solutions were created to isolate testing for different parts of the application:

- `TestAlpha.sln`: Intended for testing the `pWordLib` library.
- `TestFormAlpha.sln`: Intended for testing the `pWord4` Windows Forms application.

## Issues Encountered

During the setup of the testing solutions, several issues were encountered that currently prevent the tests from being run.

### .NET Framework vs. .NET Core Incompatibility

The core issue is an incompatibility between the target frameworks of the projects under test and the modern .NET testing tools.

- `pWordLib` and `pWord4` are .NET Framework 4.8 projects.
- The standard `dotnet new mstest` command creates a .NET (Core) test project (e.g., targeting .NET 9.0).

A .NET (Core) test project cannot directly reference a .NET Framework project.

### Build Tool Issues

To resolve the framework incompatibility, I attempted to create a .NET Framework 4.8 test project. However, I was unable to build this project due to the following reasons:

- The `dotnet build` and `dotnet msbuild` commands, which are part of the .NET SDK, are not fully compatible with the old-style `.csproj` format used by .NET Framework projects, especially for testing. They fail to resolve the testing framework dependencies correctly.
- The correct tool to build these projects is `MSBuild.exe`, which is included with Visual Studio. I was unable to locate `MSBuild.exe` on the system in the standard installation directories.

## Recommendations

To proceed with creating a runnable test suite, I recommend one of the following approaches:

1.  **Provide the path to `MSBuild.exe`**: If you have Visual Studio installed, please provide the full path to `MSBuild.exe`. This will allow me to build the .NET Framework test projects.
2.  **Upgrade Projects to .NET**: A more long-term solution would be to upgrade the `pWordLib` and `pWord4` projects to a more modern version of .NET (e.g., .NET 6 or later). This would allow for the use of the modern .NET SDK and tooling, which would simplify the testing process and provide access to new features and performance improvements.

Once the build issues are resolved, I can proceed with migrating the existing tests from `OpNodeTest2` and creating new tests.
