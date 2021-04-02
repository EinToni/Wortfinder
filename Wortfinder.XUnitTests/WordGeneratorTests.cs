using System.Collections.Generic;
using Xunit;
using Moq;

namespace Wortfinder.XUnitTests
{
    public class WordGeneratorTests
    {
        [Fact]
        public void AddWords_Empty()
		{
            Mock<WordList> wordList = new Mock<WordList>();
            WordGenerator wordGenerator = new WordGenerator(wordList.Object);

            List<Word> words1 = new List<Word>();
            List<Word> words2 = new List<Word>();

            wordGenerator.AddWords(words1, words2);
            Assert.Empty(words1);
            Assert.Empty(words2);
        }
        [Fact]
        public void AddWords()
        {
            Mock<IWordList> wordList = new Mock<IWordList>();
            WordGenerator wordGenerator = new WordGenerator(wordList.Object);

            Word word = new Word("123", new List<Coordinate>());

            List<Word> words1 = new List<Word>() { word };
            List<Word> words2 = new List<Word>();

            wordGenerator.AddWords(words1, words2);
            Assert.Contains(word, words2);
            Assert.Single(words2);
        }
        [Fact]
        public void WordNotFound()
        {
            
        }
        
        [Fact]
        public void GetAllWords_EmptyList()
		{
            Mock<IWordList> wordList = new Mock<IWordList>();
            wordList.Setup(x => x.Loaded()).Returns(true);
            WordGenerator wordGenerator = new WordGenerator(wordList.Object);
            
            char[] letters = new char[4];
            int size = 2;

            List<Word> result = wordGenerator.GetAllWords(letters, size);

            Assert.Empty(result);
        }
        [Fact]
        public void GetAllWords_NotLoaded()
        {
            Mock<IWordList> wordList = new Mock<IWordList>();
            wordList.Setup(x => x.Loaded()).Returns(false);
            WordGenerator wordGenerator = new WordGenerator(wordList.Object);

            char[] letters = new char[4];
            int size = 2;

            List<Word> result = wordGenerator.GetAllWords(letters, size);

            Assert.Empty(result);
        }

    }
}
