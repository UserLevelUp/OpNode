param (
    [string]$filter = "all"
)

function CheckDll {
    param (
        [string]$filePath
    )

    $data = Get-Command $filePath
    $is32bit = $data.FileVersionInfo.FileDescription -like "*32-bit*"

    if ($is32bit) {
        $bitness = "32-bit"
    } else {
        $bitness = "64-bit"
    }

    if ($filter -eq "all" -or $filter -eq $bitness) {
        Write-Host "Checking: $filePath"
        Write-Host "$filePath is $bitness."
    }
}

Write-Host "Checking DLLs in folder and subfolders: ."

Get-ChildItem -Path "." -Filter "*.dll" -Recurse | ForEach-Object {
    CheckDll -filePath $_.FullName
}