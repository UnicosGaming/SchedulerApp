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

$file = $args[0]
$placeholder = $args[1]
$key_value = $args[2]

Replace-Text $file $placeholder $key_value