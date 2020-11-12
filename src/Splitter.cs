using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FileSplitter {
	public class Splitter {
		private Configuration Config { get; }
		public Splitter(Configuration config) {
			this.Config = config;
		}

		public void SplitFiles() {
			try {
				Utils.PrintMessage("Getting the files from input folder");
				// get the files from folder
				List<string> filesList = new List<string>();
				if (string.IsNullOrWhiteSpace(this.Config.InputFileNamePattern)) {
					filesList = Directory.GetFiles(this.Config.InputFolder).ToList();
				} else {
					filesList = Directory.GetFiles(this.Config.InputFolder, this.Config.InputFileNamePattern).ToList();
				}
				Utils.PrintMessage($"{filesList.Count} files found");

				// iterate on each file to split it
				foreach (var currentFilePath in filesList) {
					FileInfo file = new FileInfo(currentFilePath);
					var fileName = file.Name.Substring(0, file.Name.Length - file.Extension.Length);

					Utils.PrintMessage($"Reading file '{file.Name}'");

					using(StreamReader sr = new StreamReader(currentFilePath)) {
						string line;
						int splitCounter = 1;
						int recordsRead = 0;
						int currentSplitRecords = 0;
						var newFileName = Path.Combine(this.Config.OutputFolder, $"{fileName}_{splitCounter.ToString("00")}{file.Extension}");
						Utils.PrintMessage($"Writing into file '{newFileName}'");
						StreamWriter sw = new StreamWriter(newFileName, false);
						while ((line = sr.ReadLine()) != null) {
							recordsRead++;
							currentSplitRecords++;
							if (currentSplitRecords >= this.Config.RecordsTreshold) {
								sw.Close();
								currentSplitRecords = 1;
								splitCounter++;
								newFileName = Path.Combine(this.Config.OutputFolder, $"{fileName}_{splitCounter.ToString("00")}{file.Extension}");
								Utils.PrintMessage($"Writing into file '{newFileName}'");
								sw = new StreamWriter(newFileName, false);
							}
							sw.WriteLine(line);

							if (recordsRead % this.Config.ShowProgressEveryXRecords == 0) {
								Utils.PrintMessage($"{recordsRead} records read");
							}
						}
						if (sw != null) {
							sw.Close();
						}
						if (sr != null) {
							sr.Close();
						}
					}

					Utils.PrintMessage($"Finished reading file '{file.Name}'");

					// should it move it to archive folder?
					// TODO: update with proper file path
					Utils.PrintMessage($"Creating archive copy to folder '{file.Name}'");

					// should delete source file?
					if (this.Config.DeleteOriginalFile) {
						Utils.PrintMessage($"Deleting file '{file.Name}'");
						file.Delete();
						Utils.PrintMessage($"Deleted file '{file.Name}'");
					}

				}

			} catch (Exception ex) {
				Utils.PrintErrorMessage("Error splitting files", ex);
				throw;
			}
		}
	}
}
