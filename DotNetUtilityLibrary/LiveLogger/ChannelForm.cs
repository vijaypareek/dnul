using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DotNetUtilityLibrary.LiveLogger
{
	internal partial class ChannelForm : Form
	{
		#region Overridden Properties

		protected override bool ShowWithoutActivation
		{
			get { return true; }
		}

		#endregion Overridden Properties

		#region Properties

		public readonly string Title;
		public readonly Channel Channel;

		#endregion Properties

		#region Constructors

		public ChannelForm(Channel channel, string title)
			: this(channel, title, string.Empty) { }

		public ChannelForm(Channel channel, string title, string content)
		{
			InitializeComponent();
			if (title.Length > 0) { this.Text += " - " + title; }
			Title = title;
			Channel = channel;
			txtLog.Text = content;
		}

		#endregion Constructors

		#region Exposed Methods

		public void Write(string text)
		{
			Invoke(new MethodInvoker(() =>
			{
				txtLog.AppendText(text);
			}));
		}

		public void ClearContent()
		{
			Invoke(new MethodInvoker(() =>
			{
				txtLog.Text = string.Empty;
			}));
		}

		#endregion Exposed Methods

		#region Click Handlers

		private void btnClearLog_Click(object sender, EventArgs e)
		{
			Channel.ClearContent();
		}

		#endregion Click Handlers
	}
}
