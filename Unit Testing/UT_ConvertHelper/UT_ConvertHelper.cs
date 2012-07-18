using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using DotNetUtilityLibrary;

namespace UT_ConvertHelper
{
	[TestFixture]
	public class UT_TypeHelper
	{
		[Test]
		public void test_string()
		{
			string testString = "Hello world. This is a testing string.";
			Assert.AreEqual(testString, ConvertHelper.BytesToHexString(ConvertHelper.HexStringToBytes(testString)));
			Assert.AreEqual(testString, ConvertHelper.BytesToString(ConvertHelper.StringToBytes(testString)));
		}
	}
}