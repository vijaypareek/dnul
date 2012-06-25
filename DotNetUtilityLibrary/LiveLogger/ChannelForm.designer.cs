namespace DotNetUtilityLibrary.LiveLogger
{
	partial class ChannelForm
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
			this.tlpRoot = new System.Windows.Forms.TableLayoutPanel();
			this.txtLog = new System.Windows.Forms.TextBox();
			this.btnClearLog = new System.Windows.Forms.Button();
			this.tlpRoot.SuspendLayout();
			this.SuspendLayout();
			// 
			// tlpRoot
			// 
			this.tlpRoot.ColumnCount = 1;
			this.tlpRoot.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tlpRoot.Controls.Add(this.txtLog, 0, 0);
			this.tlpRoot.Controls.Add(this.btnClearLog, 0, 1);
			this.tlpRoot.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tlpRoot.Location = new System.Drawing.Point(0, 0);
			this.tlpRoot.Name = "tlpRoot";
			this.tlpRoot.RowCount = 2;
			this.tlpRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tlpRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.tlpRoot.Size = new System.Drawing.Size(436, 388);
			this.tlpRoot.TabIndex = 0;
			// 
			// txtLog
			// 
			this.txtLog.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtLog.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtLog.Location = new System.Drawing.Point(3, 3);
			this.txtLog.Multiline = true;
			this.txtLog.Name = "txtLog";
			this.txtLog.Size = new System.Drawing.Size(430, 352);
			this.txtLog.TabIndex = 0;
			// 
			// btnClearLog
			// 
			this.btnClearLog.Dock = System.Windows.Forms.DockStyle.Right;
			this.btnClearLog.Location = new System.Drawing.Point(347, 361);
			this.btnClearLog.Name = "btnClearLog";
			this.btnClearLog.Size = new System.Drawing.Size(86, 24);
			this.btnClearLog.TabIndex = 1;
			this.btnClearLog.Text = "Clear Log";
			this.btnClearLog.UseVisualStyleBackColor = true;
			this.btnClearLog.Click += new System.EventHandler(this.btnClearLog_Click);
			// 
			// ChannelForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(436, 388);
			this.Controls.Add(this.tlpRoot);
			this.Name = "ChannelForm";
			this.Text = "Live Logger";
			this.tlpRoot.ResumeLayout(false);
			this.tlpRoot.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tlpRoot;
		private System.Windows.Forms.TextBox txtLog;
		private System.Windows.Forms.Button btnClearLog;
	}
}