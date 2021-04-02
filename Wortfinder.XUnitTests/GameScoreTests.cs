using Xunit;
using Moq;

namespace Wortfinder.XUnitTests
{
    public class GameScoreTests
    {
        [Fact]
        public void ResetScore()
        {
            var scoreCalc = new Mock<GameScoreCalculator>();
            GameScore gameScore = new GameScore(scoreCalc.Object);

            gameScore.ResetScore();

            Assert.Equal(0, gameScore.Score);
        }

        [Fact]
        public void AddPoints()
        {
            int points = 10;
            var scoreCalc = new Mock<GameScoreCalculator>();
            GameScore gameScore = new GameScore(scoreCalc.Object);

            gameScore.AddPoints(points);

            Assert.Equal(points, gameScore.Score);
        }
        [Fact]
        public void SetDifficulty()
		{
            Mock<GameScoreCalculator> mock = new Mock<GameScoreCalculator>();

            GameScore gameScore = new GameScore(mock.Object);
            gameScore.SetDifficulty(1, 1);

            mock.Verify();
		}
        [Fact]
        public void WordFound()
        {
            int score = 77;
            Mock<IGameScoreCalculator> calc = new Mock<IGameScoreCalculator>();
            calc.Setup(x => x.GetPoints(It.IsAny<int>())).Returns(score);

            GameScore gameScore = new GameScore(calc.Object);
            gameScore.WordFound("1");

            Assert.Equal(score, gameScore.Score);
        }
    }
}
