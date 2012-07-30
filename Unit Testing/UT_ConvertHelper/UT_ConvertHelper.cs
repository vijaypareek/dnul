using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using DotNetUtilityLibrary;

namespace UT_ConvertHelper
{
	[TestFixture]
	public class UT_ConvertHelper
	{
		[Test]
		public void test_string()
		{
			string testString = "Hello world. This is a testing string.";
			Assert.AreEqual(testString, ConvertHelper.BytesToString(ConvertHelper.StringToBytes(testString)));

			string hexTestString = "0f011fa2";
			Assert.AreEqual(hexTestString, ConvertHelper.BytesToString(ConvertHelper.StringToBytes(hexTestString)));
			
			string hexTestString2 = "0F011FA2";
			Assert.True(string.Equals(hexTestString2, ConvertHelper.BytesToString(ConvertHelper.StringToBytes(hexTestString)), StringComparison.CurrentCultureIgnoreCase));

			byte[] testBytes = { 0, 1, 2, 3, 1, 2, 3, 1, 2, 1 };
			Assert.AreEqual(testBytes, ConvertHelper.HexStringToBytes(ConvertHelper.BytesToHexString(testBytes)));
			Assert.AreEqual(testBytes, ConvertHelper.StringToBytes(ConvertHelper.BytesToString(testBytes)));
		}
	}
}