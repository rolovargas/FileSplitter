using System;

namespace FileSplitter {
	public class Utils {

		/// <summary>
		/// Prints an exception message
		/// </summary>
		public static void PrintErrorMessage(string message) {
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine($"[{DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")}] - {message}");
			Console.ResetColor();
		}

		/// <summary>
		/// Prints exception messages
		/// </summary>
		public static void PrintErrorMessage(string message, Exception ex) {
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine($"[{DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")}] - {message}");
			Console.WriteLine($"[{DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")}] - {ex.Message}");
			Exception innerException = ex.InnerException;
			while (innerException != null) {
				Console.WriteLine($"[{DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")}] - {innerException.Message}");
				innerException = innerException.InnerException;
			}
			Console.ResetColor();
		}

		/// <summary>
		/// Prints a message in regular color
		/// </summary>
		public static void PrintMessage(string message) {
			Console.WriteLine($"[{DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")}] - {message}");
		}

	}
}
