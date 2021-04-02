using Xunit;
using Moq;
using System;
using System.Collections.Generic;

namespace Wortfinder.XUnitTests
{
	public class LetterGeneratorTests
	{
		[Fact]
		public void GetLetters()
		{
			Mock<ILetterProbability> mock = new Mock<ILetterProbability>();
			mock.Setup(x => x.GetList()).Returns(new List<Letter>() { new Letter('A', (decimal)0.5), new Letter('B', (decimal)0.5) });
			LetterGenerator letterGenerator = new LetterGenerator(mock.Object);
			int amount = 5;
			var result = letterGenerator.GetNewLetters(amount);
			Assert.Equal(amount * amount, result.Length);
		}

		[Fact]
		public void GetLetters_NoPropability()
		{
			Mock<ILetterProbability> mock = new Mock<ILetterProbability>();
			mock.Setup(x => x.GetList()).Returns(new List<Letter>());
			LetterGenerator letterGenerator = new LetterGenerator(mock.Object);

			Assert.Throws<Exception>(() => letterGenerator.GetNewLetters(1));
		}
	}
}
