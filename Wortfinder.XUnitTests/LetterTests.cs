using System;
using Xunit;

namespace Wortfinder.XUnitTests
{
	public class LetterTests
	{
		[Fact]
		public void TooLargeProbability()
		{
			Assert.Throws<Exception>(() => new Letter('S', (decimal)1.1));
		}
		[Fact]
		public void Valid()
		{
			Letter letter = new Letter('Z', (decimal)0.9);

			Assert.Equal('Z', letter.Name);
			Assert.Equal((decimal)0.9, letter.Probability);
		}
		[Fact]
		public void SmallLetter()
		{
			Letter letter = new Letter('c', (decimal)0.9);

			Assert.Equal('C', letter.Name);
		}
		[Fact]
		public void NoLetter()
		{
			Assert.Throws<Exception>(() => new Letter('-', (decimal)0.9));
		}
	}
}
