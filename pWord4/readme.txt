Open up pWord 4 directory and launch the Microsoft Visual Studio 2010 Professional Project from that directory.  You should downlaod all directories.

To use the script, save it as CheckFiles2.ps1 and run it with one of the following commands:

To show all DLLs: .\CheckFiles2.ps1
To show only 32-bit DLLs: .\CheckFiles2.ps1 -filter "32-bit"
To show only 64-bit DLLs: .\CheckFiles2.ps1 -filter "64-bit"