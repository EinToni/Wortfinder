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
		[Fact]
		public void Points()
		{
			int minWordLength = 3;
			GameScoreCalculator gameScoreCalculator = new GameScoreCalculator();

			var result = gameScoreCalculator.Points(10);

			Assert.Equal(10 - (minWordLength - 1), result);
		}
		[Fact]
		public void BonusForTime_MuchTime()
		{
			GameScoreCalculator gameScoreCalculator = new GameScoreCalculator();
			int result = gameScoreCalculator.BonusForTime(500);
			Assert.Equal(0, result);
		}
		[Fact]
		public void BonusForTime_OneMinute()
		{
			GameScoreCalculator gameScoreCalculator = new GameScoreCalculator();
			int result = gameScoreCalculator.BonusForTime(60);
			Assert.Equal(2, result);
		}
		[Fact]
		public void BonusForSize_Large()
		{
			GameScoreCalculator gameScoreCalculator = new GameScoreCalculator();
			int result = gameScoreCalculator.BonusForSize(10);
			Assert.Equal(0, result);
		}
		[Fact]
		public void BonusForSize_SizeTHree()
		{
			GameScoreCalculator gameScoreCalculator = new GameScoreCalculator();
			int result = gameScoreCalculator.BonusForSize(3);
			Assert.Equal(3, result);
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
		[Fact]
		public void Bonus_NotInitialized()
		{
			GameScoreCalculator gameScoreCalculator = new GameScoreCalculator();
			int result = gameScoreCalculator.Bonus();
			Assert.Equal(0, result);
		}
	}
}
