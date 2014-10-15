namespace levywatch
{
	partial class LevyPanel
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.lblName = new System.Windows.Forms.Label();
			this.lblPopularity = new System.Windows.Forms.Label();
			this.lblPercent = new System.Windows.Forms.Label();
			this.chboxEnabled = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// lblName
			// 
			this.lblName.AutoSize = true;
			this.lblName.Dock = System.Windows.Forms.DockStyle.Left;
			this.lblName.ForeColor = System.Drawing.Color.White;
			this.lblName.Location = new System.Drawing.Point(17, 2);
			this.lblName.Name = "lblName";
			this.lblName.Size = new System.Drawing.Size(25, 13);
			this.lblName.TabIndex = 0;
			this.lblName.Text = "???";
			this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblPopularity
			// 
			this.lblPopularity.Dock = System.Windows.Forms.DockStyle.Right;
			this.lblPopularity.ForeColor = System.Drawing.Color.White;
			this.lblPopularity.Location = new System.Drawing.Point(79, 2);
			this.lblPopularity.Name = "lblPopularity";
			this.lblPopularity.Size = new System.Drawing.Size(35, 14);
			this.lblPopularity.TabIndex = 1;
			this.lblPopularity.Text = "0";
			this.lblPopularity.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lblPercent
			// 
			this.lblPercent.Dock = System.Windows.Forms.DockStyle.Right;
			this.lblPercent.ForeColor = System.Drawing.Color.White;
			this.lblPercent.Location = new System.Drawing.Point(44, 2);
			this.lblPercent.Name = "lblPercent";
			this.lblPercent.Size = new System.Drawing.Size(35, 14);
			this.lblPercent.TabIndex = 2;
			this.lblPercent.Text = "0%";
			this.lblPercent.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// chboxEnabled
			// 
			this.chboxEnabled.AutoSize = true;
			this.chboxEnabled.Checked = true;
			this.chboxEnabled.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chboxEnabled.Dock = System.Windows.Forms.DockStyle.Left;
			this.chboxEnabled.Location = new System.Drawing.Point(2, 2);
			this.chboxEnabled.Name = "chboxEnabled";
			this.chboxEnabled.Size = new System.Drawing.Size(15, 14);
			this.chboxEnabled.TabIndex = 3;
			this.chboxEnabled.UseVisualStyleBackColor = true;
			this.chboxEnabled.CheckedChanged += new System.EventHandler(this.chboxEnabled_CheckedChanged);
			// 
			// LevyPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.BackColor = System.Drawing.Color.Transparent;
			this.Controls.Add(this.lblName);
			this.Controls.Add(this.chboxEnabled);
			this.Controls.Add(this.lblPercent);
			this.Controls.Add(this.lblPopularity);
			this.MaximumSize = new System.Drawing.Size(9999, 18);
			this.MinimumSize = new System.Drawing.Size(0, 18);
			this.Name = "LevyPanel";
			this.Padding = new System.Windows.Forms.Padding(2);
			this.Size = new System.Drawing.Size(116, 18);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label lblName;
		private System.Windows.Forms.Label lblPopularity;
		private System.Windows.Forms.Label lblPercent;
		private System.Windows.Forms.CheckBox chboxEnabled;
	}
}
