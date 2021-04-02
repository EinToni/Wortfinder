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
    }
}
