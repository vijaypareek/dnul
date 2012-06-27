using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;
using DotNetUtilityLibrary;

namespace UT_TypeWrapper
{
	[TestFixture]
	public class UT_GenericWrapper
	{
		[Test]
		public void test1()
		{
			GenericWrapper<string> typeWrapper = new GenericWrapper<string>("hello");
			Assert.AreEqual(typeWrapper.Item, "hello");
			Assert.AreEqual(typeWrapper.DisplayName, "hello");
		}
	}
}
