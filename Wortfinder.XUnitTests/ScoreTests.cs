using Xunit;

namespace Wortfinder.XUnitTests
{
    public class ScoreTests
    {
       
        [Fact]
        public void CompareTo_SmallerScore()
        {
            int largerIndicator = -1;
            Score scoreToTest   = new Score(10, 0, 0, "test", new System.DateTime());
            Score score2        = new Score(9, 0, 0, "test", new System.DateTime());

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
    }
}
