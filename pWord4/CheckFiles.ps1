# Define the function to check the bitness of a DLL
function CheckDllBitness($dllPath) {
    $data = New-Object byte[] 4096
    $stream = [System.IO.File]::OpenRead($dllPath)
    $stream.Read($data, 0, 4096) | Out-Null
    $stream.Close()

    if ($data[0] -eq 0x4D -and $data[1] -eq 0x5A) {
        $peHeaderOffset = [BitConverter]::ToUInt32($data[60..63], 0)
        $machineType = [BitConverter]::ToUInt16($data[($peHeaderOffset + 4)..($peHeaderOffset + 5)], 0)
        switch ($machineType) {
            0x014c { return "32-bit" }
            0x8664 { return "64-bit" }
            default { return "Other" }
        }
    } else {
        return "Not a valid DLL"
    }
}

# Define the root folder path
$rootFolderPath = "."

# Check if the folder exists
if (Test-Path $rootFolderPath) {
    Write-Host "Checking DLLs in folder and subfolders: $rootFolderPath"

    # Get all DLL files in the folder and its subfolders
    $dllFiles = Get-ChildItem -Path $rootFolderPath -Filter *.dll -Recurse

    # Loop through each DLL file and check its bitness
    foreach ($dll in $dllFiles) {
        $bitness = CheckDllBitness($dll.FullName)
        Write-Host "Checking: $($dll.FullName)"
        Write-Host "$($dll.FullName) is $bitness."
    }
} else {
    Write-Host "The folder $rootFolderPath does not exist."
}
