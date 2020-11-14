using System;
using System.Collections.Generic;
using Xunit;

namespace Wortfinder.XUnitTests
{
	public class LetterProbabilitysTests
	{
		[Fact]
		public void GermanProbabilitySum()
		{
			LetterProbalilitys probalilitys = new LetterProbalilitys();
			decimal sum = 0;

			List<Letter> list = probalilitys.German();
			foreach(Letter item in list)
			{
				sum += item.Probability;
				Console.WriteLine(sum);
			}

			Assert.Equal(1, sum);
		}
	}
}
