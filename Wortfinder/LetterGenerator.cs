using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wortfinder
{
	public class LetterGenerator
	{
		private readonly ILetterProbability probalilitys;

		public LetterGenerator(ILetterProbability probalilitys)
		{
			this.probalilitys = probalilitys;
		}

		public char[] GetNewLetters(int fieldSize)
		{
			int letterCount = fieldSize * fieldSize;
			char[] letters = new char[letterCount];
			for(int i = 0; i < letterCount; i++)
			{
				letters[i] = GetSingleLetter();
			}
			return letters;
		}

		private char GetSingleLetter()
		{
			Random random = new Random();
			var listOfLetterProbs = probalilitys.GetList();
			decimal randomNumber = (decimal)random.NextDouble();
			decimal probability = 0m;
			foreach (var item in listOfLetterProbs.OrderBy(p => p.Probability))
			{
				probability += item.Probability;
				if (probability > randomNumber)
				{
					return item.Name;
				}
			}
			throw new Exception("Error generating Letter. No match to probability");
		}
	}
}
