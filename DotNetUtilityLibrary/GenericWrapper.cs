using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNetUtilityLibrary
{
	public class GenericWrapper<T>
	{
		public string DisplayName
		{
			get
			{
				if (DisplayString != null)
				{
					return DisplayString;
				}
				else if (DisplayStringMethod != null)
				{
					return DisplayStringMethod(Item);
				}
				else
				{
					return Item.ToString();
				}
			}
		}

		public T Item { get; private set; }

		public Func<T, string> DisplayStringMethod;
		public string DisplayString;

		public GenericWrapper(T item)
		{
			Item = item;
		}

		public GenericWrapper(T item, Func<T, string> displayStringMethod)
			: this(item)
		{
			DisplayStringMethod = displayStringMethod;
		}

		public GenericWrapper(T item, string displayString)
			: this(item)
		{
			DisplayString = displayString;
		}

	}
}
