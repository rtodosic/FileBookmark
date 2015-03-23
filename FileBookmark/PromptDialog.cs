using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileBookmark
{
	public partial class PromptDialog : Form
	{
		public static DialogResult Show(string selectedFileItem, string bookmarkedFileItem)
		{
			var prompt = new PromptDialog();
			prompt.SetSelectedFileItem(selectedFileItem);
			prompt.SetBookmarkedFileItem(bookmarkedFileItem);
			return prompt.ShowDialog();
		}

		public PromptDialog()
		{
			InitializeComponent();
		}

		public void SetBookmarkedFileItem(string fileItemName)
		{
			button2.Text = "OPEN" + Environment.NewLine + fileItemName;
		}

		public void SetSelectedFileItem(string fileItemName)
		{
			button1.Text = "BOOKMARK" + Environment.NewLine + fileItemName;
		}

	}
}
