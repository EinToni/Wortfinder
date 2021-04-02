using System;
using System.Collections.Generic;
using System.Text;

namespace Wortfinder
{
	public class Letter
	{
		public readonly char Name;
		public readonly decimal Probability;
		public Letter(char name, decimal probability)
		{
			if (!LetterValid(ref name) || !ProbabilityValid(probability))
			{
				throw new Exception("Letter invalid.");
			}
			Name = name;
			Probability = probability;
		}
		private bool LetterValid(ref char letter)
		{
			if (letter == 'ß')
			{
				return true;
			}
			char largeA = 'A';
			char largeZ = 'Z';
			int smallToLargeChar = 32;
			if (letter > largeZ)
			{
				letter = (char)(letter - smallToLargeChar);
			}
			if (letter < largeA || letter > largeZ)
			{
				return false;
			}
			return true;
		}

		private bool ProbabilityValid(decimal probability)
		{
			if (probability > 1)
			{
				return false;
			}
			return true;
		}
	}
}
