using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortcutCreator
{
	/// <summary>
	/// Class ShortcutDefinition which holds a definition of a shortcut.
	/// </summary>
	public class ShortcutDefinition
	{
		/// <summary>
		/// Gets or sets the file location.
		/// </summary>
		/// <value>The file location.</value>
		public string FileLocation { get; set; }

		/// <summary>
		/// Gets or sets the arguments.
		/// </summary>
		/// <value>The arguments.</value>
		public string Arguments { get; set; }
		
		/// <summary>
		/// Gets or sets the description.
		/// </summary>
		/// <value>The description.</value>
		public string Description { get; set; }
		
		/// <summary>
		/// Gets or sets the hot key.
		/// </summary>
		/// <value>The hot key.</value>
		public string HotKey { get; set; }
		
		/// <summary>
		/// Gets or sets the icon location.
		/// </summary>
		/// <value>The icon location.</value>
		public string IconLocation { get; set; } // not tested
		
		/// <summary>
		/// Gets or sets the relative path.
		/// </summary>
		/// <value>The relative path.</value>
		public string RelativePath { get; set; } // not tested
		
		/// <summary>
		/// Gets or sets the target path.
		/// </summary>
		/// <value>The target path.</value>
		public string TargetPath { get; set; }
		
		/// <summary>
		/// Gets or sets the window style.
		/// </summary>
		/// <value>The window style.</value>
		public int WindowStyle { get; set; }
		
		/// <summary>
		/// Gets or sets the working directory.
		/// </summary>
		/// <value>The working directory.</value>
		public string WorkingDirectory { get; set; } // not tested
	}
}
