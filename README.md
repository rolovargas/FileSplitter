# File Splitter

Splits files based on record count

All the behavior of the application is driven by its configuration:

```json
{
  "AppParameters": {
    "InputFolder": "C:\\temp\\FileSplitter\\input",
    "InputFileNamePattern": "*.csv",
    "OutputFolder": "C:\\temp\\FileSplitter\\output",
    "RecordsTreshold": 10,
    "ShowProgressEveryXRecords": 1000,
    "DeleteOriginalFile": true,
    "ArchivePath" :"C:\\temp\\FileSplitter\\archive",
    "ArchivePathGroupByDate": true
  }
}
```

## Description of properties

|Property|Requirement|Description|
|---|---|---|
|InputFolder|Required|Path where the input files are located|
|InputFileNamePattern|Optional|File name pattern to filter the files to be read|
|OutputFolder|Required|Path where the split files will be written to|
|RecordsThreshold|Required|Max amount of records allowed per output file|
|ShowProgressEveryXRecords|Optional|Defines how often is progress displayed. Defaults to 1000 records|
|DeleteOriginalFile|Required|Should the input file be removed from `InputFolder`|
|ArchivePath|Optional|If a copy of the original file should be made to an archive folder|
|ArchivePathGroupByDate|Optional|If the archive path should be grouped by date. Defaults to `true`|

## VS Code

If opened in VS Code, you can build the application to Windows, Linux or OSX.
In the Command Palette, type `Tasks: Run Task`, and you'll see the tasks to build the code to each platform.
The output is written into an `executable` folder.

---
App is written using [dot net core](http://dotnet.microsoft.com/).
