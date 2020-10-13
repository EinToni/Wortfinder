using System.Collections.Generic;

namespace Wortfinder
{
	public class BeginningComparer : IComparer<string>
	{
		public int Compare(string x, string y)
		{
			if (x.StartsWith(y))
			{
				return 0;
			}
			return x.CompareTo(y);
		}
	}
}
