using System;
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
        [Fact]
        public void GetWord_Valid()
        {
            String wordName = "test";
            int CoordinateR = 1;
            int CoordinateC = 2;
            Coordinate coordinate = new Coordinate(CoordinateR, CoordinateC);
            Word ExpectedWord = new Word(wordName, new List<Coordinate>() { coordinate });
            Game game = new Game(new char[0], 0, new List<Word>() { ExpectedWord });
            Word word = game.GetWord(wordName);
            Assert.Equal(wordName, word.Name);
            Assert.Equal(CoordinateR, word.Coordinates[0].Row);
            Assert.Equal(CoordinateC, word.Coordinates[0].Column);
        }
        [Fact]
        public void GetWord_NotValid()
		{
            Game game = new Game(new char[0], 0, new List<Word>());
            Assert.Throws<Exception>(() => game.GetWord("test"));
        }
        [Fact]
        public void WordValid_EmptyList()
		{
            Game game = new Game(new char[0], 0, new List<Word>());
            bool result = game.WordValid("test");
            Assert.False(result);
        }
        [Fact]
        public void WordValid_False()
        {
            Word ExpectedWord = new Word("testtest", new List<Coordinate>());
            Game game = new Game(new char[0], 0, new List<Word>() { ExpectedWord });
            bool result = game.WordValid("test");
            Assert.False(result);
        }
        [Fact]
        public void WordValid_True()
        {
            Word ExpectedWord = new Word("test", new List<Coordinate>());
            Game game = new Game(new char[0], 0, new List<Word>() { ExpectedWord });
            bool result = game.WordValid("test");
            Assert.True(result);
        }
    }
}
