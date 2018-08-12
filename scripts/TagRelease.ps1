Write-Host "Last 5 tags";
git tag -l | Select -Last 5;

$tag = Read-Host -Prompt "New version tag?";

$releaseNotesDirPath = [System.IO.Path]::Combine((pwd), "ReleaseNotes");
if(![System.IO.Directory]::Exists($releaseNotesDirPath))
{
    New-Item -ItemType Directory -Path $releaseNotesDirPath| Out-Null;
}

$tagNotesPath = ([System.IO.Path]::Combine($releaseNotesDirPath, "${tag}.txt"));
Write-Host $tagNotesPath;

if(![System.IO.File]::Exists($tagNotesPath))
{
    New-Item -ItemType File -Path $tagNotesPath | Out-Null;
}

Write-Host "Launching editor for patch notes";
notepad.exe $tagNotesPath | Out-Null;

Write-Host "Tagging release";
git tag -a $tag -m "${tag}";