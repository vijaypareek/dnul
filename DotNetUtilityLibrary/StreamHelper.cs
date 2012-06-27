using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace DotNetUtilityLibrary
{
	public static class StreamHelper
	{
		public const int DEFAULT_BUFFER_SIZE = 8192;

		/// <summary>
		/// Writes the contents of the input stream into the output stream.
		/// Note: The position of both streams will be set to the end of the
		/// stream.
		/// </summary>
		/// <param name="input">stream containing the contents to copy</param>
		/// <param name="output">stream the contents will be written to</param>
		/// <param name="bufferSize">the size of swapping buffer space</param>
		public static void CopyStream(Stream input, Stream output, int bufferSize)
		{
			byte[] buffer = new byte[bufferSize];
			int read;
			while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
			{
				output.Write(buffer, 0, read);
			}
			output.Flush();
		}

		/// <summary>
		/// Writes the contents of the input stream into the output stream.
		/// Note: The position of both streams will be set to the end of the
		/// stream.
		/// </summary>
		/// <param name="input">stream containing the contents to copy</param>
		/// <param name="output">stream the contents will be written to</param>
		public static void CopyStream(Stream input, Stream output)
		{
			CopyStream(input, output, DEFAULT_BUFFER_SIZE);
		}
	}
}