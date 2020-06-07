function Get-Key {
    <#
    .SYNOPSIS
        Returns the $key value from Environment variables
    .PAREMETER $key_name
        Name of the key. Mandatory
    .EXAMPLE
        Get-Key key_name
    #>
    Param(
        [Parameter(Mandatory=$true)]
        [string]$key_name
    )

    return $([Environment]::GetEnvironmentVariable($key_name))
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

$key_name = $args[0]
$file = $args[1]
$placeholder = $args[2]

$key_value = Get-Key $key_name

Replace-Text $file $placeholder $key_value