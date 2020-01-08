# substitution_decipherer
A simple WPF program for manual substitution cipher decryption.
It is a fair starting point for GUIs using MVVM design pattern with custom controls, template data bindings, etc.

## Requirements

- [Visual Studio 2019](https://visualstudio.microsoft.com/downloads/)
- .NET Framework 4.7.2

## Installation

1. Clone this project to somewhere. `git clone git@github.com:8ToThePowerOfMol/substitution_decipherer.git`
2. Open the `substitution_decipherer.sln` with Visual Studio
3. Build project and run, or execute from `substitution_decipherer\substitution_decipherer\bin\Debug`.
You can build release, but debug version is working enough.
- (Optinal for behavior debugging) **Expression.Blend.Sdk.1.0.2** - once you build the project, install as NuGet package:
  - Project > Manage NuGet Packages ...
  - Search for `Expression.Blend.Sdk` and install

## Demo

![alt text](https://raw.githubusercontent.com/8ToThePowerOfMol/substitution_decipherer/master/images/demo.gif)

## Issues

- [ ] Complete *About* section. Write *Help* and *About*
- [ ] Fix cursor escaping when custom undo/redo is used in the cipher textbox
- [ ] Fix tab and their branches indexing
