using System.Collections.Generic;
using Xunit;

namespace Wortfinder.XUnitTests
{
    public class WordTests
    {
        [Fact]
        public void Equals_SameString()
        {
            Word wordToTest = new Word("testWord", new List<Coordinate>());

            Assert.True(wordToTest.Equals("testWord"));
        }
        [Fact]
        public void Equals_OtherString()
        {
            Word wordToTest = new Word("testWord", new List<Coordinate>());

            Assert.False(wordToTest.Equals("notTestWord"));
        }
        [Fact]
        public void Equals_Empty()
        {
            Word wordToTest = new Word("testWord", new List<Coordinate>());

            Assert.False(wordToTest.Equals(""));
        }
        [Fact]
        public void GotFound_False()
        {
            Word wordToTest = new Word("testWord", new List<Coordinate>());

            Assert.False(wordToTest.Found);
        }
        [Fact]
        public void GotFound_True()
        {
            Word wordToTest = new Word("testWord", new List<Coordinate>());
            wordToTest.GotFound();
            Assert.True(wordToTest.Found);
        }

    }
}
