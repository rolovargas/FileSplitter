namespace FileSplitter {
	public class Configuration {
		public string InputFolder { get; set; }
		public string InputFileNamePattern { get; set; }
		public string OutputFolder { get; set; }
		public int RecordsTreshold { get; set; }
		public bool DeleteOriginalFile { get; set; }
		public string ArchivePath { get; set; }
		public bool ArchivePathGroupByDate { get; set; }
		public int ShowProgressEveryXRecords { get; set; }

	}
}
