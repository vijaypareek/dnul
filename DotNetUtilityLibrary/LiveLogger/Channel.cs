using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNetUtilityLibrary.LiveLogger
{
	internal class Channel
	{
		#region Properties

		public readonly string Title;

		#endregion Properties

		#region Member Variables

		private ChannelForm mChannelForm;
		private string mContent;

		#endregion Member Variables

		#region Constants

		private const string MESSAGE_FORMAT = @"{0}: {1}";
		private const string TIME_FORMAT = @"hh:mm.ss.fff";

		#endregion Constants

		#region Constructor

		public Channel(string title)
		{
			Title = title;
			mChannelForm = new ChannelForm(this, title);
			mContent = string.Empty;
		}

		#endregion Constructor

		#region Exposed Methods

		public void Trace(string text)
		{
			string message = string.Format(MESSAGE_FORMAT,
				DateTime.Now.ToString(TIME_FORMAT), text) + Environment.NewLine;

			if (mChannelForm.IsDisposed)
			{
				mChannelForm = new ChannelForm(this, Title, mContent);
			}

			if (mChannelForm.Visible == false) { mChannelForm.Show(); }

			mContent += message;
			mChannelForm.Write(message);
		}

		public void ClearContent()
		{
			mContent = string.Empty;
			mChannelForm.ClearContent();
		}

		#endregion Exposed Methods
	}
}
