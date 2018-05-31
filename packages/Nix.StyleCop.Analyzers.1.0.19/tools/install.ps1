param($installPath, $toolsPath, $package, $project)

# Create folder for settings in the solution root directory
$styleCopFolderPath = Join-Path( Split-Path -Path $project.DTE.Solution.FullName) "StyleCop"
if(-Not (Test-Path $styleCopFolderPath) )
{
	New-Item -ItemType directory -Path $styleCopFolderPath
}

# Copy json file with custom settings
$settingsSourcePath = Join-Path $installPath "settings\stylecop.json"
$settingsDestinationPath = Join-Path $styleCopFolderPath "stylecop.json"
Copy-Item $settingsSourcePath -Destination $settingsDestinationPath

# Copy ruleset file with rules behavior
$rulesetSourcePath = Join-Path $installPath "settings\Nix.StyleCop.Analyzers.ruleset"
$rulesetDestinationPath = Join-Path $styleCopFolderPath "Nix.StyleCop.Analyzers.ruleset"
Copy-Item $rulesetSourcePath -Destination $rulesetDestinationPath

# Copy json schema for rules configuration
$schemaSourcePath = Join-Path $installPath "settings\stylecop.schema.json"
$schemaDestinationPath = Join-Path $styleCopFolderPath "stylecop.schema.json"
Copy-Item $schemaSourcePath -Destination $schemaDestinationPath

$analyzersPaths = Join-Path (Join-Path (Split-Path -Path $toolsPath -Parent) "analyzers" ) * -Resolve

foreach($analyzersPath in $analyzersPaths)
{
    # Install the language agnostic analyzers.
    if (Test-Path $analyzersPath)
    {
        foreach ($analyzerFilePath in Get-ChildItem $analyzersPath -Filter *.dll)
        {
            if($project.Object.AnalyzerReferences)
            {
                $project.Object.AnalyzerReferences.Add($analyzerFilePath.FullName)
            }
        }
    }
}

# $project.Type gives the language name like (C# or VB.NET)
$languageFolder = ""
if($project.Type -eq "C#")
{
    $languageFolder = "cs"
}
if($project.Type -eq "VB.NET")
{
    $languageFolder = "vb"
}
if($languageFolder -eq "")
{
    return
}

foreach($analyzersPath in $analyzersPaths)
{
    # Install language specific analyzers.
    $languageAnalyzersPath = join-path $analyzersPath $languageFolder
    if (Test-Path $languageAnalyzersPath)
    {
        foreach ($analyzerFilePath in Get-ChildItem $languageAnalyzersPath -Filter *.dll)
        {
            if($project.Object.AnalyzerReferences)
            {
                $project.Object.AnalyzerReferences.Add($analyzerFilePath.FullName)
            }
        }
    }
}

# Add roslyn settings file as a link and as AdditionFile build mode
$item = $project.ProjectItems | where-object {$_.Name -eq "stylecop.json"}
if (!$item)
{
	$item = $project.ProjectItems.AddFromFile($settingsDestinationPath)
	$item.Properties.Item("ItemType").Value = "AdditionalFiles"
}

# Add ruleset file to project
foreach ($config in $project.ConfigurationManager)
{
	$config.Properties.Item("CodeAnalysisRuleSet").Value = "..\StyleCop\Nix.StyleCop.Analyzers.ruleset"
}