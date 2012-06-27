using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNetUtilityLibrary
{
	public class GenericWrapper<T>
	{
		public string DisplayName { get { return Item.ToString(); } }
		public T Item { get; private set; }

		public GenericWrapper(T item)
		{
			Item = item;
		}

	}
}
