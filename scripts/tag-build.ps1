param(
    [Parameter(Mandatory = $true)]
    [string]$Version,

    [string]$Message
)

if ([string]::IsNullOrWhiteSpace($Message)) {
    $Message = "Build $Version"
}

$gitStatus = git status --porcelain
if ($gitStatus) {
    Write-Host "Working tree is not clean. Commit your changes before tagging." -ForegroundColor Yellow
    exit 1
}

git tag -a $Version -m $Message
if ($LASTEXITCODE -ne 0) {
    exit $LASTEXITCODE
}

git push origin $Version
