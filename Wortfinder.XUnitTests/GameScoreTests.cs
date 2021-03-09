using Xunit;

namespace Wortfinder.XUnitTests
{
    public class GameScoreTests
    {
        [Fact]
        public void ResetScore()
        {
            GameScore gameScore = new GameScore();

            gameScore.ResetScore();

            Assert.Equal(0, gameScore.Score);
        }

        [Fact]
        public void AddPoints()
        {
            int points = 10;
            GameScore gameScore = new GameScore();

            gameScore.AddPoints(points);

            Assert.Equal(points, gameScore.Score);
        }
    }
}
