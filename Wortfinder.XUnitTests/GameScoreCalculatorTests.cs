using Xunit;


namespace Wortfinder.XUnitTests
{
	public class GameScoreCalculatorTests
	{
		[Fact]
		public void SetDifficulty()
		{
			int fieldSize = 10;
			int gameTimeSeconds = 61;
			GameScoreCalculator gameScoreCalculator = new GameScoreCalculator();

			gameScoreCalculator.SetDifficulty(fieldSize, gameTimeSeconds);

			Assert.Equal(fieldSize, gameScoreCalculator.gameFieldSize);
			Assert.Equal(gameTimeSeconds, gameScoreCalculator.gameTime);
		}
	}
}
