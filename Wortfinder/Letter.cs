using System;
using System.Collections.Generic;
using System.Text;

namespace Wortfinder
{
	public class Letter
	{
		public readonly char Name;
		public readonly decimal Probability;
		public Letter(char name, decimal probability )
		{
			Name = name;
			if (probability > 1)
			{
				throw new Exception("Probability of letter is larger then 1.");
			}
			Probability = probability;
		}
	}
}
