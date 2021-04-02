using Xunit;
using Moq;

namespace Wortfinder.XUnitTests
{
	public class LetterGeneratorTests
	{
		[Fact]
		public void GetLetters()
		{
			Mock<LetterProbalilitys> mock = new Mock<LetterProbalilitys>();
			LetterGenerator letterGenerator = new LetterGenerator(mock.Object);
			int amount = 5;
			var result = letterGenerator.GetNewLetters(amount);
			Assert.Equal(amount * amount, result.Length);
		}
	}
}
