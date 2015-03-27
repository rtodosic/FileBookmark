using Microsoft.Win32;
using SharpShell.Attributes;
using SharpShell.SharpContextMenu;
using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

// This using sharpShell to create Shell Context Menus described in the following code project article:
// http://www.codeproject.com/Articles/512956/NET-Shell-Extensions-Shell-Context-Menus

namespace FileBookmark
{
	/// <summary>
	/// Class FileBookmarkExtension add the bookmark menu items into the file explorer's context menu.
	/// </summary>
	[SuppressMessage("Microsoft.Interoperability", "CA1405:ComVisibleTypeBaseTypesShouldBeComVisible"), ComVisible(true)]
	[COMServerAssociation(AssociationType.AllFiles)]
	[COMServerAssociation(AssociationType.Directory)]
	[COMServerAssociation(AssociationType.Drive)]
	[COMServerAssociation(AssociationType.UnknownFiles)]
	public class FileBookmarkExtension : SharpContextMenu, IDisposable
	{
		/// <summary>
		/// The menu items. This is used in the file explorer's context menu.
		/// </summary>
		private ContextMenuStrip menu;

		/// <summary>
		/// The submenu item that holds all of the bookmark items.
		/// </summary>
		ToolStripMenuItem menuBookmark;

		/// <summary>
		/// Determines whether this instance can a show the context menu.
		/// </summary>
		/// <returns><c>true</c> if this instance should show a shell context menu; otherwise, <c>false</c>.</returns>
		protected override bool CanShowMenu()
		{
			return SelectedItemPaths.Count() <= 1;
		}

		/// <summary>
		/// Creates the context menu.
		/// </summary>
		/// <returns>The context menu for the shell context menu.</returns>
		protected override ContextMenuStrip CreateMenu()
		{
			// Create menu
			menu = new ContextMenuStrip();
			
			// Create submenu item
			menuBookmark = new ToolStripMenuItem { Text = "Bookmark" };
			
			// Create the bookmark menu items
			menuBookmark.DropDownItems.Add(new ToolStripMenuItem() {Text = "1. Unassigned", Image = Resource.Bookmark1_Disabled});
			menuBookmark.DropDownItems.Add(new ToolStripMenuItem() {Text = "2. Unassigned", Image = Resource.Bookmark2_Disabled});
			menuBookmark.DropDownItems.Add(new ToolStripMenuItem() {Text = "3. Unassigned", Image = Resource.Bookmark3_Disabled});
			menuBookmark.DropDownItems.Add(new ToolStripMenuItem() {Text = "4. Unassigned", Image = Resource.Bookmark4_Disabled});
			menuBookmark.DropDownItems.Add(new ToolStripMenuItem() {Text = "5. Unassigned", Image = Resource.Bookmark5_Disabled});
			menuBookmark.DropDownItems.Add(new ToolStripMenuItem() {Text = "6. Unassigned", Image = Resource.Bookmark6_Disabled});
			menuBookmark.DropDownItems.Add(new ToolStripMenuItem() {Text = "7. Unassigned", Image = Resource.Bookmark7_Disabled});
			menuBookmark.DropDownItems.Add(new ToolStripMenuItem() {Text = "8. Unassigned", Image = Resource.Bookmark8_Disabled});
			menuBookmark.DropDownItems.Add(new ToolStripMenuItem() {Text = "9. Unassigned", Image = Resource.Bookmark9_Disabled});
			
			menuBookmark.DropDownItems.Add(new ToolStripSeparator());
			menuBookmark.DropDownItems.Add(new ToolStripMenuItem() {Text = "Clear &All"});
			
			menuBookmark.DropDownItems[0].Click += (sender, args) => OnBookmark(1);
			menuBookmark.DropDownItems[1].Click += (sender, args) => OnBookmark(2);
			menuBookmark.DropDownItems[2].Click += (sender, args) => OnBookmark(3);
			menuBookmark.DropDownItems[3].Click += (sender, args) => OnBookmark(4);
			menuBookmark.DropDownItems[4].Click += (sender, args) => OnBookmark(5);
			menuBookmark.DropDownItems[5].Click += (sender, args) => OnBookmark(6);
			menuBookmark.DropDownItems[6].Click += (sender, args) => OnBookmark(7);
			menuBookmark.DropDownItems[7].Click += (sender, args) => OnBookmark(8);
			menuBookmark.DropDownItems[8].Click += (sender, args) => OnBookmark(9);
			menuBookmark.DropDownItems[10].Click += (sender, args) => OnClearAll();
			menu.Items.Add(menuBookmark);

			// Set the menu items based on prior saved bookmarks.
			RegistryKey rk = Registry.CurrentUser;
			RegistryKey sk1 = rk.CreateSubKey("Software\\FileBookmark");
			if (sk1 != null)
			{
				var names = sk1.GetValueNames();
				
				if (names.Contains("1"))
				{
					menuBookmark.DropDownItems[0].Image = Resource.Bookmark1;
					menuBookmark.DropDownItems[0].Text = "1. " +  PathShortener(sk1.GetValue("1") as string);
				}
				else
				{
					menuBookmark.DropDownItems[0].Image = Resource.Bookmark1_Disabled;
					menuBookmark.DropDownItems[0].Text = "1. Unassigned";
				}

				if (names.Contains("2"))
				{
					menuBookmark.DropDownItems[1].Image = Resource.Bookmark2;
					menuBookmark.DropDownItems[1].Text = "2. " + PathShortener(sk1.GetValue("2") as string);
				}
				else
				{
					menuBookmark.DropDownItems[1].Image = Resource.Bookmark2_Disabled;
					menuBookmark.DropDownItems[1].Text = "2. Unassigned";
				}

				if (names.Contains("3"))
				{
					menuBookmark.DropDownItems[2].Image = Resource.Bookmark3;
					menuBookmark.DropDownItems[2].Text = "3. " + PathShortener(sk1.GetValue("3") as string);
				}
				else
				{
					menuBookmark.DropDownItems[2].Image = Resource.Bookmark3_Disabled;
					menuBookmark.DropDownItems[2].Text = "3. Unassigned";
				}

				if (names.Contains("4"))
				{
					menuBookmark.DropDownItems[3].Image = Resource.Bookmark4;
					menuBookmark.DropDownItems[3].Text = "4. " + PathShortener(sk1.GetValue("4") as string);
				}
				else
				{
					menuBookmark.DropDownItems[3].Image = Resource.Bookmark4_Disabled;
					menuBookmark.DropDownItems[3].Text = "4. Unassigned";
				}

				if (names.Contains("5"))
				{
					menuBookmark.DropDownItems[4].Image = Resource.Bookmark5;
					menuBookmark.DropDownItems[4].Text = "5. " + PathShortener(sk1.GetValue("5") as string);
				}
				else
				{
					menuBookmark.DropDownItems[4].Image = Resource.Bookmark5_Disabled;
					menuBookmark.DropDownItems[4].Text = "5. Unassigned";
				}

				if (names.Contains("6"))
				{
					menuBookmark.DropDownItems[5].Image = Resource.Bookmark6;
					menuBookmark.DropDownItems[5].Text = "6. " + PathShortener(sk1.GetValue("6") as string);
				}
				else
				{
					menuBookmark.DropDownItems[5].Image = Resource.Bookmark6_Disabled;
					menuBookmark.DropDownItems[5].Text = "6. Unassigned";
				}

				if (names.Contains("7"))
				{
					menuBookmark.DropDownItems[6].Image = Resource.Bookmark7;
					menuBookmark.DropDownItems[6].Text = "7. " + PathShortener(sk1.GetValue("7") as string);
				}
				else
				{
					menuBookmark.DropDownItems[6].Image = Resource.Bookmark7_Disabled;
					menuBookmark.DropDownItems[6].Text = "7. Unassigned";
				}

				if (names.Contains("8"))
				{
					menuBookmark.DropDownItems[7].Image = Resource.Bookmark8;
					menuBookmark.DropDownItems[7].Text = "8. " + PathShortener(sk1.GetValue("8") as string);
				}
				else
				{
					menuBookmark.DropDownItems[7].Image = Resource.Bookmark8_Disabled;
					menuBookmark.DropDownItems[7].Text = "8. Unassigned";
				}

				if (names.Contains("9"))
				{
					menuBookmark.DropDownItems[8].Image = Resource.Bookmark9;
					menuBookmark.DropDownItems[8].Text = "9. " + PathShortener(sk1.GetValue("9") as string);
				}
				else
				{
					menuBookmark.DropDownItems[8].Image = Resource.Bookmark9_Disabled;
					menuBookmark.DropDownItems[8].Text = "9. Unassigned";
				}
			}

			return menu;
		}

		/// <summary>
		/// Handler for the clear all menu item.
		/// </summary>
		private void OnClearAll()
		{
			RegistryKey rk = Registry.CurrentUser;
			RegistryKey sk1 = rk.CreateSubKey("Software\\FileBookmark");
			if (sk1 != null)
			{
				var names = sk1.GetValueNames();
				foreach (var name in names)
					if (name != "WinFileSelector")
						sk1.DeleteValue(name);
			}
		}

		/// <summary>
		/// Handler for the bookmark menu items.
		/// </summary>
		/// <param name="idx">The index.</param>
		private void OnBookmark(int idx)
		{
			try
			{
				string selectedFileItem = "";
				foreach (var fileItemPath in SelectedItemPaths)
					selectedFileItem = fileItemPath;

				string bookmarkedItem = GetRegSelectedFileItem(idx);

				var result = DialogResult.Yes;
				if (!String.IsNullOrWhiteSpace(bookmarkedItem) && bookmarkedItem.ToUpper() != selectedFileItem.ToUpper())
				{
					result = PromptDialog.Show(PathShortener(selectedFileItem), PathShortener(bookmarkedItem));
				}

				if (result == DialogResult.Cancel)
					return;

				if (result == DialogResult.Yes)
				{
					SetRegSelectedFileItem(idx, selectedFileItem);

					var s = new StringBuilder();
					s.AppendLine("Use Ctrl+Alt+" + idx + " to navigate to the following:");
					s.AppendLine();
					s.AppendLine(PathShortener(selectedFileItem));
					MessageBox.Show(s.ToString(), "File Bookmark", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}

				if (result == DialogResult.No)
				{
					ExecSelectFileItem(idx);
					//MessageBox.Show("Jump to: " + PathShortener(bookmarkedItem), "File Bookmark", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("ERROR: " + ex.Message, "File Bookmark", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		/// <summary>
		/// Sets the reg selected file item.
		/// </summary>
		/// <param name="regIdx">Index of the reg.</param>
		/// <param name="selectedFileItem">The selected file item.</param>
		private static void SetRegSelectedFileItem(int regIdx, string selectedFileItem)
		{
			RegistryKey rk = Registry.CurrentUser;
			RegistryKey sk1 = rk.CreateSubKey("Software\\FileBookmark");
			if (sk1 != null)
				sk1.SetValue(regIdx.ToString(CultureInfo.InvariantCulture), selectedFileItem);
		}


		/// <summary>
		/// Gets the reg selected file item.
		/// </summary>
		/// <param name="regIdx">Index of the reg.</param>
		/// <returns>System.String.</returns>
		private static string GetRegSelectedFileItem(int regIdx)
		{
			RegistryKey rk = Registry.CurrentUser;
			RegistryKey sk1 = rk.CreateSubKey("Software\\FileBookmark");
			if (sk1 == null)
				return null;
			try
			{
				return (sk1.GetValue(regIdx.ToString(CultureInfo.InvariantCulture)) as string);
			}
			catch (Exception)
			{
				return null;
			}
		}

		/// <summary>
		/// Gets the file path for the WinFileSelector application.
		/// </summary>
		/// <returns>System.String.</returns>
		private static string GetWinFileSelectorPath()
		{
			RegistryKey rk = Registry.CurrentUser;
			RegistryKey sk1 = rk.CreateSubKey("Software\\FileBookmark");
			if (sk1 == null)
				return null;
			try
			{
				return sk1.GetValue("WinFileSelector") as string;
			}
			catch (Exception)
			{
				return null;
			}
		}

		/// <summary>
		/// Shortens a path string to a specified length.
		/// </summary>
		/// <param name="path">The path.</param>
		/// <param name="wantedLength">The wanted path length.</param>
		/// <returns>System.String.</returns>
		private static string PathShortener(string path, int wantedLength = 50)
		{
			StringBuilder sb = new StringBuilder(wantedLength + 1);
			NativeMethods.PathCompactPathEx(sb, path, wantedLength + 1, 0);
			return sb.ToString();
		}

		/// <summary>
		/// Executes the select file item.
		/// </summary>
		/// <param name="idx">The index.</param>
		private static void ExecSelectFileItem(int idx)
		{
			string exe = GetWinFileSelectorPath();
			if (string.IsNullOrWhiteSpace(exe))
				return;

			if (!System.IO.File.Exists(exe))
				return;

			var startInfo = new ProcessStartInfo
			{
				UseShellExecute = false,
				FileName = exe,
				Arguments = string.Format("\"{0}\"", idx)
			};

			try
			{
				using (Process exeProcess = Process.Start(startInfo))
				{
					exeProcess.WaitForExit();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("ERROR: " + ex.Message, "File Bookmark", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (menu != null)
				{
					menu.Dispose();
					menu = null;
				}
			}
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
	}

	internal class NativeMethods
	{
		/// <summary>
		/// Windows API method to create shortened paths from a path string.
		/// </summary>
		/// <param name="pszOut">The PSZ out.</param>
		/// <param name="szPath">The sz path.</param>
		/// <param name="cchMax">The CCH maximum.</param>
		/// <param name="dwFlags">The dw flags.</param>
		/// <returns><c>true</c> if successful, <c>false</c> otherwise.</returns>
		[DllImport("shlwapi.dll", CharSet = CharSet.Unicode)]
		internal static extern bool PathCompactPathEx([Out] StringBuilder pszOut, string szPath, int cchMax, int dwFlags);
	}
}
