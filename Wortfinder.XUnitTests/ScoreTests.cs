using Xunit;

namespace Wortfinder.XUnitTests
{
    public class ScoreTests
    {

        [Fact]
        public void CompareTo_SmallerScore()
        {
            int largerIndicator = -1;
            Score scoreToTest = new Score(10, 0, 0, "test", new System.DateTime());
            Score score2 = new Score(9, 0, 0, "test", new System.DateTime());

            Assert.Equal(largerIndicator, scoreToTest.CompareTo(score2));
        }
        [Fact]
        public void CompareTo_LargerScore()
        {
            int smallerIndicator = 1;
            Score scoreToTest = new Score(10, 0, 0, "test", new System.DateTime());
            Score score2 = new Score(11, 0, 0, "test", new System.DateTime());

            Assert.Equal(smallerIndicator, scoreToTest.CompareTo(score2));
        }
        [Fact]
        public void CompareTo_EqualScore()
        {
            int equalIndicator = 0;
            Score scoreToTest = new Score(10, 0, 0, "test", new System.DateTime());
            Score score2 = new Score(10, 0, 0, "test", new System.DateTime());

            Assert.Equal(equalIndicator, scoreToTest.CompareTo(score2));
        }
        [Fact]
        public void Date()
		{
            Score scoreToTest = new Score(10, 0, 0, "test", new System.DateTime(2021, 10, 25));

            string result = scoreToTest.Date;

            Assert.Equal("25.10.21", result);
        }
        [Fact]
        public void FieldSize()
        {
            int fieldSize = 4;
            Score scoreToTest = new Score(10, fieldSize, 0, "test", new System.DateTime(2021, 10, 25));

            int result = scoreToTest.FieldSize;

            Assert.Equal(fieldSize, result);
        }
        [Fact]
        public void GameInfo()
        {
            Score scoreToTest = new Score(10, 4, 17, "test", new System.DateTime(2021, 10, 25));

            string result = scoreToTest.GameInfo;

            Assert.Equal("4x4 in 17min", result);
        }
        [Fact]
        public void GameTimeInMinutes()
        {
            Score scoreToTest = new Score(10, 4, 17, "test", new System.DateTime(2021, 10, 25));

            int result = scoreToTest.GameTimeInMinutes;

            Assert.Equal(17, result);
        }
        [Fact]
        public void Playername()
        {
            Score scoreToTest = new Score(10, 4, 17, "test", new System.DateTime(2021, 10, 25));

            string result = scoreToTest.PlayerName;

            Assert.Equal("test", result);
        }
    }
}
