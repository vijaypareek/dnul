using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace DotNetUtilityLibrary
{
	public static class StreamHelper
	{
		public const int BUFFER_SIZE = 8192;

		public static void CopyStream(Stream input, Stream output)
		{
			byte[] buffer = new byte[BUFFER_SIZE];
			int read;
			while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
			{
				output.Write(buffer, 0, read);
			}
			output.Flush();
			if (input.CanSeek)
				input.Seek(0, SeekOrigin.Begin);
			if (output.CanSeek)
				output.Seek(0, SeekOrigin.Begin);
		}
	}
}
