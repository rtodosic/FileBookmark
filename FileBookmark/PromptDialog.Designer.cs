namespace FileBookmark
{
	partial class PromptDialog
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PromptDialog));
			this.CloseButton = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// CloseButton
			// 
			this.CloseButton.BackColor = System.Drawing.SystemColors.Window;
			this.CloseButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.CloseButton.Location = new System.Drawing.Point(16, 179);
			this.CloseButton.Name = "CloseButton";
			this.CloseButton.Size = new System.Drawing.Size(607, 47);
			this.CloseButton.TabIndex = 2;
			this.CloseButton.Text = "Cancel";
			this.CloseButton.UseVisualStyleBackColor = false;
			// 
			// button2
			// 
			this.button2.BackColor = System.Drawing.SystemColors.Window;
			this.button2.DialogResult = System.Windows.Forms.DialogResult.No;
			this.button2.Image = global::FileBookmark.Resource.Folder_Closed;
			this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.button2.Location = new System.Drawing.Point(16, 24);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(607, 62);
			this.button2.TabIndex = 0;
			this.button2.Text = "Open: 77777777777777777777777777777777777777777777777777";
			this.button2.UseVisualStyleBackColor = false;
			// 
			// button1
			// 
			this.button1.BackColor = System.Drawing.SystemColors.Window;
			this.button1.DialogResult = System.Windows.Forms.DialogResult.Yes;
			this.button1.Image = global::FileBookmark.Resource.bookmark;
			this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.button1.Location = new System.Drawing.Point(16, 92);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(607, 62);
			this.button1.TabIndex = 1;
			this.button1.Text = "Bookmark: 77777777777777777777777777777777777777777777777777";
			this.button1.UseVisualStyleBackColor = false;
			// 
			// PromptDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Window;
			this.CancelButton = this.CloseButton;
			this.ClientSize = new System.Drawing.Size(635, 247);
			this.Controls.Add(this.CloseButton);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "PromptDialog";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "File Bookmark";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button CloseButton;
	}
}