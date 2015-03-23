using IWshRuntimeLibrary;
using System;
using System.IO;
using System.Linq;

namespace ShortcutCreator
{
	/// <summary>
	/// Class Program for the ShortcutCreator application. The ShortcutCreator is tool that helps to 
	/// create Windows shortcut files.
	/// </summary>
	class Program
	{
		/// <summary>
		/// Defines the entry point of the application.
		/// </summary>
		/// <param name="args">The arguments.</param>
		static void Main(string[] args)
		{
			// Validate arguments
			if (args.Count() < 2)
			{
				PrintUsage();
				Environment.Exit(1);
			}

			// Load shortcut from arguments
			var shortcutDefinition = new ShortcutDefinition
				{
					FileLocation = ConvertToLink(args[0]),
					TargetPath = args[1]
				};

			for (int i = 2; i < args.Count(); i++)
			{
				if ((args[i].ToUpper() == "-A" || args[i].ToUpper() == "/A") && args.Count() > i + 1)
					shortcutDefinition.Arguments = args[i + 1];

				if ((args[i].ToUpper() == "-D" || args[i].ToUpper() == "/D") && args.Count() > i + 1)
					shortcutDefinition.Description = args[i + 1];

				if ((args[i].ToUpper() == "-H" || args[i].ToUpper() == "/H") && args.Count() > i + 1)
					shortcutDefinition.HotKey = args[i + 1];

				if ((args[i].ToUpper() == "-I" || args[i].ToUpper() == "/I") && args.Count() > i + 1)
					shortcutDefinition.IconLocation = args[i + 1];

				if ((args[i].ToUpper() == "-R" || args[i].ToUpper() == "/R") && args.Count() > i + 1)
					shortcutDefinition.RelativePath = args[i + 1];

				if ((args[i].ToUpper() == "-S" || args[i].ToUpper() == "/S") && args.Count() > i + 1)shortcutDefinition.WindowStyle = ConvertToWindowStyle(args[i + 1]);

				if ((args[i].ToUpper() == "-W" || args[i].ToUpper() == "/W") && args.Count() > i + 1)
					shortcutDefinition.WorkingDirectory = args[i + 1];
			}

			if (shortcutDefinition.TargetPath.ToLower() == "-remove" ||
			    shortcutDefinition.TargetPath.ToLower() == "-delete" ||
			    shortcutDefinition.TargetPath.ToLower() == "/remove" ||
			    shortcutDefinition.TargetPath.ToLower() == "/delete")
			{
				if (System.IO.File.Exists(shortcutDefinition.FileLocation))
				{
					System.IO.File.Delete(shortcutDefinition.FileLocation);
					PrintSuccess("Successfully removed shortcut.");
				}
				Environment.Exit(0);
			}

			// Expand target path
			for (int i = 0; i < 5; i++)
				shortcutDefinition.TargetPath = Environment.ExpandEnvironmentVariables(shortcutDefinition.TargetPath);

			// Expand working directory
			if (!string.IsNullOrWhiteSpace(shortcutDefinition.WorkingDirectory))
				for (int i = 0; i < 5; i++)
					shortcutDefinition.WorkingDirectory = Environment.ExpandEnvironmentVariables(shortcutDefinition.WorkingDirectory);

			// Print
			PrintShortcutDefinition(shortcutDefinition);

			// Validate arguments
			if (!System.IO.File.Exists(shortcutDefinition.TargetPath))
				PrintError("Target not found.");

			if (!IsHotKey(shortcutDefinition.HotKey))
				PrintError("HotKey is invalid.");

			if (!string.IsNullOrWhiteSpace(shortcutDefinition.RelativePath) && !Directory.Exists(shortcutDefinition.RelativePath))
				PrintError("Relative path not found.");

			if (!string.IsNullOrWhiteSpace(shortcutDefinition.WorkingDirectory) && !Directory.Exists(shortcutDefinition.WorkingDirectory))
				PrintError("Working directory not found.");
			
			try
			{
				CreateShortcut(shortcutDefinition);
				PrintSuccess("Successfully created shortcut.");
			}
			catch (Exception ex)
			{
				PrintError(ex.Message);
			}
		}

		/// <summary>
		/// Converts to link.
		/// </summary>
		/// <param name="fileName">Name of the file.</param>
		/// <returns>System.String.</returns>
		private static string ConvertToLink(string fileName)
		{
			if (fileName.Contains("/") ||
				fileName.Contains("*") ||
				fileName.Contains("?") ||
				fileName.Contains("\"") ||
				fileName.Contains("<") ||
				fileName.Contains(">") ||
				fileName.Contains("|"))
			{
				PrintError("Link file name can't contain any of the following characters: \\/:*?\"<>|");
			}

			for (int i = 0; i < 5; i++)
				fileName = Environment.ExpandEnvironmentVariables(fileName);

			if (!Path.HasExtension(fileName))
				return fileName + ".lnk";

			if (Path.HasExtension(fileName) && Path.GetExtension(fileName).ToLower()!= ".lnk")
				return fileName + ".lnk";

			return fileName;
		}

		/// <summary>
		/// Validates the hot key string.
		/// </summary>
		/// <param name="hotKey">The hot key.</param>
		/// <returns><c>true</c> if the hot key is valid; otherwise, <c>false</c>.</returns>
		private static bool IsHotKey(string hotKey)
		{
			if (string.IsNullOrWhiteSpace(hotKey))
				return true;

			var hkParts = hotKey.Split('+');
			if (hkParts.Count() < 2)
				return false;

			for (int i = 0; i < hkParts.Count() - 1; i++)
			{
				if (hkParts[i].Trim().ToLower() != "ctrl" &&
					hkParts[i].Trim().ToLower() != "shift" &&
					hkParts[i].Trim().ToLower() != "alt")
					return false;
			}

			return true;
		}

		/// <summary>
		/// Prints the shortcut definition.
		/// </summary>
		/// <param name="shortcutDefinition">The shortcut definition.</param>
		private static void PrintShortcutDefinition(ShortcutDefinition shortcutDefinition)
		{
			Console.WriteLine("------------------------------------");
			Console.WriteLine("FileLocation = " + shortcutDefinition.FileLocation);
			Console.WriteLine("TargetPath = " + shortcutDefinition.TargetPath);
			Console.WriteLine("Arguments = " + shortcutDefinition.Arguments);
			Console.WriteLine("Description = " + shortcutDefinition.Description);
			Console.WriteLine("HotKey = " + shortcutDefinition.HotKey);
			Console.WriteLine("IconLocation = " + shortcutDefinition.IconLocation);
			Console.WriteLine("RelativePath = " + shortcutDefinition.RelativePath);
			Console.WriteLine("WindowStyle = " + shortcutDefinition.WindowStyle);
			Console.WriteLine("WorkingDirectory = " + shortcutDefinition.WorkingDirectory);
			Console.WriteLine("------------------------------------");
		}

		/// <summary>
		/// Converts a string representation of a window style to an integer representation.
		/// </summary>
		/// <param name="s">The s.</param>
		/// <returns>System.Int32.</returns>
		private static int ConvertToWindowStyle(string s)
		{
			if (s.ToUpper() == "Minimized" || s.ToUpper() == "Min" || s.ToUpper() == "Minimize" || s.ToUpper() == "1")
				return 3;
			if (s.ToUpper() == "Maximized" || s.ToUpper() == "Maximize" || s.ToUpper() == "Maximize" || s.ToUpper() == "2")
				return 7;
			return 1;
		}

		/// <summary>
		/// Prints an error and exits the application.
		/// </summary>
		/// <param name="err">The error.</param>
		private static void PrintError(string err)
		{
			var defaultColor = Console.ForegroundColor;
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine("ERROR: " + err);
			Console.ForegroundColor = defaultColor;

			Environment.Exit(-1);
		}

		/// <summary>
		/// Prints the success message.
		/// </summary>
		/// <param name="message">The message.</param>
		private static void PrintSuccess(string message)
		{
			var defaultColor = Console.ForegroundColor;
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine(message);
			Console.ForegroundColor = defaultColor;
		}


		/// <summary>
		/// Prints the usage.
		/// </summary>
		private static void PrintUsage()
		{
			var defaultColor = Console.ForegroundColor;
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine("Usage: ShortcutCreator LinkFile Target [-A Arg] [-D Desc] [-H HotKey] [-I IconPath] [-R RelativePath] [-S WindowStyle] [-W WorkingDirectory]");
			Console.ForegroundColor = defaultColor;
		}

		/// <summary>
		/// Creates the shortcut.
		/// </summary>
		/// <param name="shortcutDefinition">The shortcut definition.</param>
		private static void CreateShortcut(ShortcutDefinition shortcutDefinition)
		{
			var shell = new WshShell();
			var shortcut = (IWshShortcut)shell.CreateShortcut(shortcutDefinition.FileLocation);
			if (!string.IsNullOrEmpty(shortcutDefinition.Arguments))
				shortcut.Arguments = shortcutDefinition.Arguments;
			if (!string.IsNullOrEmpty(shortcutDefinition.Description))
				shortcut.Description = shortcutDefinition.Description;
			if (!string.IsNullOrEmpty(shortcutDefinition.HotKey))
				shortcut.Hotkey = shortcutDefinition.HotKey;
			if (!string.IsNullOrEmpty(shortcutDefinition.IconLocation))
				shortcut.IconLocation = shortcutDefinition.IconLocation;
			if (!string.IsNullOrEmpty(shortcutDefinition.RelativePath))
				shortcut.RelativePath = shortcutDefinition.RelativePath;
			if (!string.IsNullOrEmpty(shortcutDefinition.TargetPath))
				shortcut.TargetPath = shortcutDefinition.TargetPath;
			if (!string.IsNullOrEmpty(shortcutDefinition.WorkingDirectory))
				shortcut.WorkingDirectory = shortcutDefinition.WorkingDirectory;
			shortcut.WindowStyle = shortcutDefinition.WindowStyle;
			shortcut.Save();
		}
	}
}