using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNetUtilityLibrary
{
	public static class ConvertHelper
	{
		/// <summary>
		/// Converts a string to an array of bytes.
		/// This performs the inverse operation of ConvertHelper.BytesToString.
		/// See this link for implications of this approach:
		/// http://stackoverflow.com/a/10380166/391618
		/// </summary>
		public static byte[] StringToBytes(string str)
		{
			byte[] bytes = new byte[str.Length * sizeof(char)];
			System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
			return bytes;
		}

		/// <summary>
		/// Converts an array of bytes to a string, agnositic of the encoding.
		/// This performs the inverse operation of ConvertHelper.StringToBytes.
		/// See this link for implications of this approach:
		/// http://stackoverflow.com/a/10380166/391618
		/// </summary>
		public static string BytesToString(byte[] bytes)
		{
			char[] chars = new char[bytes.Length / sizeof(char)];
			System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
			return new string(chars);
		}

		/// <summary>
		/// Converts a hex string representation of bytes to a byte array.
		/// Taken from the implementation found here:
		/// http://stackoverflow.com/a/311179/391618
		/// </summary>
		public static byte[] HexStringToBytes(string str)
		{
			int hexStringLength = str.Length;
			byte[] b = new byte[hexStringLength / 2];
			for (int i = 0; i < hexStringLength; i += 2)
			{
				int topChar = (str[i] > 0x40 ? str[i] - 0x37 : str[i] - 0x30) << 4;
				int bottomChar = str[i + 1] > 0x40 ? str[i + 1] - 0x37 : str[i + 1] - 0x30;
				b[i / 2] = Convert.ToByte(topChar + bottomChar);
			}
			return b;
		}

		/// <summary>
		/// Converts a byte array to a hex string representation.
		/// Taken from the implementation found here:
		/// http://stackoverflow.com/a/632920/391618
		/// </summary>
		/// <param name="bytes"></param>
		/// <returns></returns>
		public static string BytesToHexString(byte[] bytes)
		{
			char[] c = new char[bytes.Length * 2];
			byte b;
			for (int i = 0; i < bytes.Length; ++i)
			{
				b = ((byte)(bytes[i] >> 4));
				c[i * 2] = (char)(b > 9 ? b + 0x37 : b + 0x30);
				b = ((byte)(bytes[i] & 0xF));
				c[i * 2 + 1] = (char)(b > 9 ? b + 0x37 : b + 0x30);
			}
			return new string(c);
		}
	}
}
