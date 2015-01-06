/*
 * Author: Jöran Malek
 */

using System;
using System.Collections.Generic;

namespace TextAdventure
{
	public sealed class UpperCaseStringEqualityComparer : IEqualityComparer<string>
	{
		private static UpperCaseStringEqualityComparer instance;

		public static UpperCaseStringEqualityComparer Instance
		{
			get
			{
				return instance ?? (instance = new UpperCaseStringEqualityComparer());
			}
		}

		public bool Equals(string x, string y)
		{
			return string.Equals(x, y, StringComparison.InvariantCultureIgnoreCase);
		}

		public int GetHashCode(string obj)
		{
			return obj.ToUpperInvariant().GetHashCode();
		}
	}
}
