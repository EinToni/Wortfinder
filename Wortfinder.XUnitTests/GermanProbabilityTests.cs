using System;
using System.Collections.Generic;
using Xunit;

namespace Wortfinder.XUnitTests
{
	public class GermanProbabilityTests
	{
		[Fact]
		public void GermanProbabilitySum()
		{
			ILetterProbability probalilitys = new LettersGerman();
			decimal sum = 0;

			List<Letter> list = probalilitys.GetList();
			foreach(Letter item in list)
			{
				sum += item.Probability;
				Console.WriteLine(sum);
			}

			Assert.Equal(1, sum);
		}
		[Fact]
		public void GermanProbabilityCount()
		{
			int lettersInGerman = 26;
			ILetterProbability probalilitys = new LettersGerman();

			List<Letter> list = probalilitys.GetList();

			Assert.Equal(lettersInGerman, list.Count);
		}
	}
}
