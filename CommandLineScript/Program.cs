using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

using DotNetUtilityLibrary;
using DotNetUtilityLibrary.Cryptography;

namespace CommandLineScript
{
	class Program
	{	
		public const string dataString = "trironk kiatkungwanlgai";
		public const string keyString = "tkiatkungwanglai";
		public const string ivString = "katherin";
		static void Main(string[] args)
		{
			//MemoryStream data = new MemoryStream(
			//    ConvertHelper.StringToBytes(dataString));
			//MemoryStream encryptedData = new MemoryStream();
			//byte[] key = ConvertHelper.StringToBytes(keyString);
			//byte[] iv = ConvertHelper.StringToBytes(ivString);
			//byte[] buffer =
			//    new byte[ConvertHelper.StringToBytes(dataString).Length];
			//using (SymmetricStreamEncryptor<AesManaged> encryptor =
			//    new SymmetricStreamEncryptor<AesManaged>(data, key, iv))
			//{
			//    encryptor.Read(buffer, 0, buffer.Length);
			//}
			//Console.WriteLine(ConvertHelper.BytesToString(buffer));
		}
	}
}
