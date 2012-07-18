using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNetUtilityLibrary
{
	public static class ConvertHelper
	{
		public static byte[] StringToBytes(string str)
		{
			byte[] bytes = new byte[str.Length * sizeof(char)];
			System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
			return bytes;
		}

		public static string BytesToString(byte[] bytes)
		{
			char[] chars = new char[bytes.Length / sizeof(char)];
			System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
			return new string(chars);
		}

		public static byte[] HexStringToBytes(string str)
		{
			int NumberChars = str.Length;
			byte[] bytes = new byte[NumberChars / 2];
			for (int i = 0; i < NumberChars; i += 2)
				bytes[i / 2] = Convert.ToByte(str.Substring(i, 2), 16);
			return bytes;
		}

		public static string BytesToHexString(byte[] bytes)
		{
			string str = BitConverter.ToString(bytes);
			return str.Replace("-", "");
		}
	}
}
