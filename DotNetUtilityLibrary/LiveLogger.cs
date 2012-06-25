using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

using DotNetUtilityLibrary;

namespace DotNetUtilityLibrary.LiveLogger
{
	/// <summary>
	/// Summary:
	///		This static class provides a means by which a winform application can
	///	display debug logging messages without interrupting the program flow in a
	///	constantly refreshing window.
	/// </summary>
	public static class LiveLogger
	{
		#region Static Members
		
		internal static Dictionary<string, Channel> mChannels;

		#endregion Static Members

		#region Initialization

		static LiveLogger()
		{
			mChannels = new Dictionary<string, Channel>();
			mChannels.Add(string.Empty, new Channel(string.Empty));
		}

		#endregion Initialization

		#region Exposed Methods

		public static void Trace(string channelName, string message)
		{
			if (mChannels.ContainsKey(channelName) == false)
			{
				mChannels.Add(channelName, new Channel(channelName));
			}

			mChannels[channelName].Trace(message);
		}

		public static void Trace(string message)
		{
			Trace(string.Empty, message);
		}
		
		public static void Trace()
		{
			Trace(string.Empty, string.Empty);
		}

		#endregion Exposed Methods
	}
}