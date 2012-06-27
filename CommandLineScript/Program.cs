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
			GenericWrapper<string> t = new GenericWrapper<string>("hello");
			Console.Out.WriteLine(t);
		}
	}
}
