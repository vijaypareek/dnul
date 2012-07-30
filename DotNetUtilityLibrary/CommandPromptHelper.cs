using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.IO;

namespace DotNetUtilityLibrary
{
	/// <summary>
	/// This class contains useful methods for running command-line commands.
	/// </summary>
	public static class CommandPromptHelper
	{
		#region Static Members
		
		private static readonly ProcessStartInfo DEFAULT_PROCESS_INFO = new ProcessStartInfo("cmd")
		{
			RedirectStandardError = true,
			RedirectStandardInput = true,
			RedirectStandardOutput = true,
			UseShellExecute = false,
			CreateNoWindow = true
		};

		#endregion Static Members 

		#region Helper Methods

		/// <summary>
		/// Starts up a command prompt, hooks up event handlers, runs specified
		/// commands, and closes the command prompt.
		/// </summary>
		public static void RunCommands(string currentWorkingDirectory,
			IEnumerable<string> commands,
			DataReceivedEventHandler outputDataEventHandler,
			DataReceivedEventHandler errorDataEventHandler)
		{
			Process process = new Process();
			process.StartInfo = DEFAULT_PROCESS_INFO;
			process.Start();

			if (outputDataEventHandler != null)
			{
				process.OutputDataReceived += outputDataEventHandler;
				process.BeginOutputReadLine();
			}
			if (outputDataEventHandler != null)
			{
				process.ErrorDataReceived += errorDataEventHandler;
				process.BeginErrorReadLine();
			}

			commands = InsertCommands(currentWorkingDirectory, commands);

			foreach (string command in commands)
			{
				process.StandardInput.WriteLine(command);
			}
			process.StandardInput.WriteLine("exit" + Environment.NewLine);
			process.WaitForExit();
		}

		public static void RunCommands(IEnumerable<string> commands)
		{
			RunCommands(Directory.GetCurrentDirectory(), commands, null, null);
		}

		public static void RunCommands(IEnumerable<string> commands,
			out string output)
		{
			CommandPromptListener listener = new CommandPromptListener();

			RunCommands(Directory.GetCurrentDirectory(),
				commands,
				listener.DataReceived_EventHandler,
				listener.DataReceived_EventHandler);

			output = listener.Data;
		}

		public static void RunCommands(IEnumerable<string> commands,
			DataReceivedEventHandler dataEventHandler)
		{
			RunCommands(Directory.GetCurrentDirectory(),
				commands,
				dataEventHandler,
				dataEventHandler);
		}

		public static void RunCommands(string currentWorkingDirectory,
			IEnumerable<string> commands)
		{
			RunCommands(currentWorkingDirectory, commands, null, null);
		}


		public static void RunCommands(string currentWorkingDirectory,
			IEnumerable<string> commands,
			out string output)
		{
			CommandPromptListener listener = new CommandPromptListener();

			RunCommands(currentWorkingDirectory,
				commands,
				listener.DataReceived_EventHandler,
				listener.DataReceived_EventHandler);

			output = listener.Data;
		}

		public static void RunCommands(string currentWorkingDirectory,
			IEnumerable<string> commands,
			DataReceivedEventHandler dataEventHandler)
		{
			RunCommands(currentWorkingDirectory,
				commands,
				dataEventHandler,
				dataEventHandler);
		}

		#endregion Helper Methods

		#region Private Methods

		private static List<string> InsertCommands(string currentWorkingDirectory,
			IEnumerable<string> commands)
		{
			string currentRoot = Path.GetPathRoot(currentWorkingDirectory);
			List<string> newCommands = new List<string>();
			foreach (string command in commands)
			{
				newCommands.Add(command); 
				if (command.StartsWith("cd "))
				{
					string path = command.Remove(0, 3);
					if (Path.IsPathRooted(path))
					{
						string newRoot = Path.GetPathRoot(path);
						if (newRoot.Equals(currentRoot) == false)
						{
							newCommands.Add(newRoot.Substring(0, 2));
							currentRoot = newRoot;
						}
					}
				}
			}
			return newCommands;
		}
		#endregion Private Methods
	}

	/// <summary>
	/// Reads from an output stream and aggregates the data received from it.
	/// </summary>
	public class CommandPromptListener
	{
		#region Properties

		public string Data = string.Empty;

		#endregion Properties

		#region Event Handlers
		
		public void DataReceived_EventHandler(object sender,
			DataReceivedEventArgs e)
		{
			if (e.Data == null)
				return;

			Data = Data + e.Data.Trim() + Environment.NewLine;
		}

		#endregion Event Handlers
	}
}
