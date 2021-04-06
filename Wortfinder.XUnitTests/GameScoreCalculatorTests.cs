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

			Assert.Equal(fieldSize, gameScoreCalculator.GameFieldSize);
			Assert.Equal(gameTimeSeconds, gameScoreCalculator.GameTime);
		}
		[Fact]
		public void WordHasMinimumLength_True()
		{
			GameScoreCalculator gameScoreCalculator = new GameScoreCalculator();
			bool result = gameScoreCalculator.WordHasMinimumLength(10);
			Assert.True(result);
		}
		[Fact]
		public void WordHasMinimumLength_False()
		{
			GameScoreCalculator gameScoreCalculator = new GameScoreCalculator();
			bool result = gameScoreCalculator.WordHasMinimumLength(1);
			Assert.False(result);
		}
		[Fact]
		public void WordHasMinimumLength_Exact()
		{
			GameScoreCalculator gameScoreCalculator = new GameScoreCalculator();
			bool result = gameScoreCalculator.WordHasMinimumLength(3);
			Assert.True(result);
		}
		[Fact]
		public void GetPoints_TooShort()
		{
			GameScoreCalculator gameScoreCalculator = new GameScoreCalculator();
			int result = gameScoreCalculator.GetPoints(1);
			Assert.Equal(0, result);
		}
	}
}
