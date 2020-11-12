using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace FileSplitter {
	class Program {
		public static int Main(string[] args) {
			Utils.PrintMessage("Application started");
			try {

				// load config file
				var builder = new ConfigurationBuilder()
					.SetBasePath(Directory.GetCurrentDirectory())
					.AddJsonFile("appSettings.json", optional : true, reloadOnChange : true);

				// build config object
				var inputFolder = builder.Build().GetSection("AppParameters").GetSection("InputFolder").Value;
				var inputFileNamePattern = builder.Build().GetSection("AppParameters").GetSection("InputFileNamePattern").Value;
				var outputFolder = builder.Build().GetSection("AppParameters").GetSection("OutputFolder").Value;
				var recordsTresholdStr = builder.Build().GetSection("AppParameters").GetSection("RecordsTreshold").Value;

				var deleteOriginalFileStr = builder.Build().GetSection("AppParameters").GetSection("DeleteOriginalFile").Value;
				var archivePath = builder.Build().GetSection("AppParameters").GetSection("ArchivePath").Value;
				var archivePathGroupByDateStr = builder.Build().GetSection("AppParameters").GetSection("ArchivePathGroupByDate").Value;

				// validate parameters
				if (!int.TryParse(recordsTresholdStr, out var recordsTreshold)) {
					Utils.PrintErrorMessage("The value in 'RecordsTreshold' is not a valid positive integer");
					return 1;
				}

				if (recordsTreshold <= 0) {
					Utils.PrintErrorMessage("The value in 'RecordsTreshold' is not a valid positive integer");
					return 1;
				}

				if (!Directory.Exists(inputFolder)) {
					Utils.PrintErrorMessage($"Input folder does not exist: '{inputFolder}'");
					return 2;
				}

				if (!Directory.Exists(outputFolder)) {
					Utils.PrintErrorMessage($"Output folder does not exist: '{outputFolder}'");
					return 3;
				}

				if (!bool.TryParse(deleteOriginalFileStr, out var deleteOriginalFile)) {
					Utils.PrintErrorMessage("The value in 'DeleteOriginalFile' is not valid");
					return 4;
				}

				if (!bool.TryParse(archivePathGroupByDateStr, out var archivePathGroupByDate)) {
					Utils.PrintErrorMessage("The value in 'ArchivePathGroupByDate' is not valid");
					return 5;
				}

				if (!string.IsNullOrWhiteSpace(archivePath)) {
					if (!Directory.Exists(archivePath)) {
						Utils.PrintErrorMessage($"Archive folder does not exist: '{archivePath}'");
						return 2;
					}
				}

				FileSplitter.Configuration config = new Configuration {
					InputFolder = inputFolder,
						InputFileNamePattern = inputFileNamePattern,
						OutputFolder = outputFolder,
						RecordsTreshold = recordsTreshold,
						DeleteOriginalFile = deleteOriginalFile,
						ArchivePath = archivePath,
						ArchivePathGroupByDate = archivePathGroupByDate
				};

				// go split the files
				Splitter splitter = new Splitter(config);
				splitter.SplitFiles();

				return 0;
			} catch (Exception ex) {
				Utils.PrintErrorMessage("Error in application", ex);
				return -1;
			} finally {
				Utils.PrintMessage("Application ended");
			}
		}
	}

}
