using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNetUtilityLibrary
{
	public static class TypeHelper
	{
		public static string GetFriendlyName(Type t)
		{
			return t.FullName.Substring(t.FullName.LastIndexOf('.') + 1);
		}
		public static string GetFriendlyName<T>()
		{
			return GetFriendlyName(typeof(T));
		}
	}
}
