namespace levywatch
{
	partial class frmLevyWatch
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLevyWatch));
			this.btnStart = new System.Windows.Forms.Button();
			this.timerMain = new System.Windows.Forms.Timer(this.components);
			this.pnlLevies = new System.Windows.Forms.Panel();
			this.menuMain = new System.Windows.Forms.MenuStrip();
			this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.menuMain.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnStart
			// 
			this.btnStart.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.btnStart.Location = new System.Drawing.Point(0, 712);
			this.btnStart.Name = "btnStart";
			this.btnStart.Size = new System.Drawing.Size(234, 23);
			this.btnStart.TabIndex = 0;
			this.btnStart.Text = "Start Watching";
			this.btnStart.UseVisualStyleBackColor = true;
			this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
			// 
			// timerMain
			// 
			this.timerMain.Tick += new System.EventHandler(this.timerMain_Tick);
			// 
			// pnlLevies
			// 
			this.pnlLevies.AutoScroll = true;
			this.pnlLevies.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
			this.pnlLevies.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlLevies.Location = new System.Drawing.Point(0, 24);
			this.pnlLevies.Name = "pnlLevies";
			this.pnlLevies.Size = new System.Drawing.Size(234, 688);
			this.pnlLevies.TabIndex = 4;
			// 
			// menuMain
			// 
			this.menuMain.BackColor = System.Drawing.SystemColors.Control;
			this.menuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem,
            this.helpToolStripMenuItem});
			this.menuMain.Location = new System.Drawing.Point(0, 0);
			this.menuMain.Name = "menuMain";
			this.menuMain.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.menuMain.Size = new System.Drawing.Size(234, 24);
			this.menuMain.TabIndex = 5;
			this.menuMain.Text = "menuStrip1";
			// 
			// aboutToolStripMenuItem
			// 
			this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
			this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
			this.aboutToolStripMenuItem.Text = "About";
			this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
			// 
			// helpToolStripMenuItem
			// 
			this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
			this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
			this.helpToolStripMenuItem.Text = "Help";
			this.helpToolStripMenuItem.Click += new System.EventHandler(this.helpToolStripMenuItem_Click);
			// 
			// frmLevyWatch
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(234, 735);
			this.Controls.Add(this.pnlLevies);
			this.Controls.Add(this.btnStart);
			this.Controls.Add(this.menuMain);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.menuMain;
			this.MinimumSize = new System.Drawing.Size(250, 320);
			this.Name = "frmLevyWatch";
			this.Text = "LevyWatch";
			this.Load += new System.EventHandler(this.frmLevyWatch_Load);
			this.menuMain.ResumeLayout(false);
			this.menuMain.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnStart;
		private System.Windows.Forms.Timer timerMain;
		private System.Windows.Forms.Panel pnlLevies;
		private System.Windows.Forms.MenuStrip menuMain;
		private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
	}
}

