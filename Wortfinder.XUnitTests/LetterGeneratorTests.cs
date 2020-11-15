using Xunit;

namespace Wortfinder.Tests
{
	public class LetterGeneratorTests
	{
		[Fact]
		public void GetLetters()
		{
			LetterGenerator letterGenerator = new LetterGenerator();
			int amount = 5;
			var result = letterGenerator.GetNewLetters(amount);
			Assert.Equal(amount, result.Length);
		}
	}
}
