using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinFileSelector
{
	/// <summary>
	/// Class Program for the WinFileSelector program.
	/// This is executed by the FileBookmark menu items. This can be run as a standalone app by
	/// executing the following:
	///		WinFileSelector.exe 3
	/// Which would open the file explorer for bookmark 3.
	/// </summary>
	static class Program
	{
		/// <summary>
		/// The application title
		/// </summary>
		private const string AppTitle = "Windows File Selector";

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			// The Windows explorer.exe can be run with a select flag as follows:
			// explorer.exe /select,"c:\Windows"

			// Get the arguments
			var args = Environment.GetCommandLineArgs();

			// Save the current path
			SaveExePath();

			// Test if we have args
			if (args.Count() < 2)
			{
				DisplayUsage();
				return;
			}

			int idx;
			string selectedFile = args[args.Count() - 1];

			// Try to convert to int and get the registry value.
			if (int.TryParse(selectedFile, out idx))
				selectedFile = GetRegSelectedFile(idx);
			else
			{
				DisplayUsage();
				return;
			}

			// if empty then just return and don't show an error.
			if (string.IsNullOrWhiteSpace(selectedFile))
				return;

			// Test that the file or directory exists.
			if (!File.Exists(selectedFile) && !Directory.Exists(selectedFile))
			{
				MessageBox.Show(string.Format("File or directory not found.\n\r \"{0}\"", selectedFile), AppTitle, MessageBoxButtons.OK, MessageBoxIcon.None);
				return;
			}


			try
			{
				// Use ProcessStartInfo class and Process Start() to execute the Windows explorer
				using (Process exeProcess = Process.Start(new ProcessStartInfo
					{
						UseShellExecute = false,
						FileName = "explorer.exe",
						Arguments = string.Format("/select,\"{0}\"", selectedFile)
					}))
				{
					exeProcess.WaitForExit();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("ERROR: " + ex.Message, AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		/// <summary>
		/// Saves the current path of this running WinFileSelector.exe. This is needed to make sure the FileBookmark menu command can find this application. This isn't needed under most situation but can help when the WinFileSelector.exe isn't on the search path.
		/// </summary>
		private static void SaveExePath()
		{
			try
			{
				RegistryKey rk = Registry.CurrentUser;
				RegistryKey sk1 = rk.CreateSubKey("Software\\FileBookmark");
				string exePath = Environment.GetCommandLineArgs()[0];
				if (sk1 != null)
					sk1.SetValue("WinFileSelector", exePath);
			}
			catch (Exception)
			{
				// Ignore errors
			}
		}

		/// <summary>
		/// Displays the usage when the arguments are invalid or not supplied.
		/// </summary>
		private static void DisplayUsage()
		{
			var s = new StringBuilder();
			s.AppendLine("Usage: WinFileSelector.exe Index");
			s.AppendLine();
			s.AppendLine("    Index   Specified bookmark index number.");
			s.AppendLine("            If unassigned index, application exits without error.");
			s.AppendLine("            Stored at: HKCU\\Software\\FileBookmark");
			s.AppendLine();
			s.AppendLine("For more information see: http:\\\\www.FileBookmark.com");
			MessageBox.Show(s.ToString(), AppTitle, MessageBoxButtons.OK);
		}

		/// <summary>
		/// Gets the selected file from the registry for a given index. So, of you click on bookmark 5, the
		/// regIdx is 5 and we lookup the actual selected file for regIdx 5.
		/// </summary>
		/// <param name="regIdx">Index of the selected file.</param>
		/// <returns>System.String.</returns>
		private static string GetRegSelectedFile(int regIdx)
		{
			RegistryKey rk = Registry.CurrentUser;
			RegistryKey sk1 = rk.OpenSubKey("Software\\FileBookmark");
			if (sk1 == null)
				return null;

			try
			{
				return (sk1.GetValue(regIdx.ToString(CultureInfo.InvariantCulture)) as string);
			}
			catch (Exception ex)
			{
				MessageBox.Show("ERROR: " + ex.Message, AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return null;
			}
		}

	}
}
