using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;
using DotNetUtilityLibrary;

namespace UT_TypeHelper
{
	[TestFixture]
	public class UT_TypeHelper
	{
		[Test]
		public void test_string()
		{
			Assert.AreEqual(TypeHelper.GetFriendlyName(typeof(string)), "String");
			Assert.AreEqual(TypeHelper.GetFriendlyName<string>(), "String");
		}
	}
}
