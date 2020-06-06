function Get-Key {
    <#
    .SYNOPSIS
        Returns the $key value from $file
    .PARAMETER $file
        File path where the keys are saved. Mandatory
    .PAREMETER $key
        Name of the key. Mandatory
    .EXAMPLE
        Get-Key file_with_keys.txt key_name
    #>
    Param(
        [Parameter(Mandatory=$true)]
        [string]$file,
        [Parameter(Mandatory=$true)]
        [string]$key
    )
    Get-Content $file | % { if ($_ -match $key) {$key = $_}}

    return $key.split(":")[1]
}

function Replace-Text{
    <#
    .SYNOPSIS
        Replace the $placeholder value by $key value in $file
    .PARAMETER $file
        File to replace values. Mandatory
    .PARAMETER $placeholder
        Value to be replaced. Mandatory
    .PARAMETER $key
        The new value to replace the $placeholder. Mandatory
    .EXAMPLE
        Replace-Text file_path "#TEXT TO REPLACE#" "KEY VALUE"
    #>
    Param(
        [Parameter(Mandatory=$true)]
        [string]$file,
        [Parameter(Mandatory=$true)]
        [string]$placeholder,
        [Parameter(Mandatory=$true)]
        [string]$key
    )
    
    ((Get-Content -path $file -Raw) -replace $placeholder, $key) | Set-Content -Path $file
}

$key_file = $args[0]
$key_name = $args[1]
$file = $args[2]
$placeholder = $args[3]

$key = Get-Key $key_file $key_name

Replace-Text $file $placeholder $key