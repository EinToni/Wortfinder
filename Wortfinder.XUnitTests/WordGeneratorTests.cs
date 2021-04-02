using System.Collections.Generic;
using Xunit;
using Moq;

namespace Wortfinder.XUnitTests
{
    public class WordGeneratorTests
    {
        /*
        [Fact]
       public void WordNotFound()
        {
            IFactory factory = new Factory();
            WordGenerator wordGenerator = new WordGenerator(factory);

            Word testWord = new Word("test", new List<Coordinate>());
            Word testWord2 = new Word("test2", new List<Coordinate>());
            List<Word> words = new List<Word> { new Word(testWord) };

            Assert.False(wordGenerator.WordNotFound(testWord, words));
            Assert.True(wordGenerator.WordNotFound(testWord2, words));
        }
        */
        [Fact]
        public void GetAllWords_EmptyList()
		{
            Mock<WordList> wordList = new Mock<WordList>();
            WordGenerator wordGenerator = new WordGenerator(wordList.Object);
            
            char[] letters = new char[4];
            int size = 2;

            List<Word> result = wordGenerator.GetAllWords(letters, size);

            Assert.Empty(result);
        }

    }
}
