using System.Collections.Generic;
using Xunit;

namespace Wortfinder.XUnitTests
{
    public class GameTests
    {
        [Fact]
        public void SetTime_Valid()
        {
            int testTime = 10;
            Game game = new Game(new char[0], 0, new List<Word>());
            game.SetTime(testTime);

            Assert.Equal(testTime, game.GameTimeInSeconds);
        }
        [Fact]
        public void SetTime_TwoTimes()
        {
            int validTime = 10;
            int invalidTime = 15;
            Game game = new Game(new char[0], 0, new List<Word>());
            game.SetTime(validTime);
            game.SetTime(invalidTime);

            Assert.Equal(validTime, game.GameTimeInSeconds);
            Assert.NotEqual(invalidTime, game.GameTimeInSeconds);
        }
    }
}
