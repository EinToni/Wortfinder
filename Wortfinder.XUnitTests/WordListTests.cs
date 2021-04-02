using System;
using System.Collections.Generic;
using System.Windows.Documents;
using Xunit;

namespace Wortfinder.XUnitTests
{
	public class WordListTests
	{/*
		[Fact]
		public void CheckBeginningTest()
		{
			wordList wordList = new wordList();
			var result = wordList.CheckWord("AALGLATT", 0);
			Assert.True(result);
		}

		[Fact]
		public void CheckBeginnTest()
		{
			wordList wordList = new wordList();
			var result = wordList.CheckBeginning("ABAENDERUNGSVEREINBARUM", 0);
			Assert.True(result);
		}
		*/

		[Fact]
		public void InsertWord_Empty()
		{
			WordList wordList = new WordList();
			List<string> data = new List<string>() {"Hallo", "Welt"};
			List<string> expected = new List<string>() { "Hallo", "Welt"};

			wordList.InsertWord("", data);

			Assert.Equal(data, expected);
		}

		[Fact]
		public void InsertWord_Word()
		{
			WordList wordList = new WordList();
			List<string> data = new List<string>() { "Hallo", "Welt" };
			List<string> expected = new List<string>() { "Hallo", "Welt", "!" };

			wordList.InsertWord("!", data);

			Assert.Equal(data, expected);
		}

		[Fact]
		public void NormaliseList_Empty()
		{
			WordList wordList = new WordList();
			List<string> data = new List<string>();

			var result = wordList.NormaliseList(data);

			Assert.Equal(data, result);
		}

		[Fact]
		public void NormaliseList_umlaut()
		{
			WordList wordList = new WordList();
			List<string> data = new List<string>(){ "Äpfel", "Öfen", "Über", "Ä", "Großfräsmaschinenöffnungstür" };
			List<string> expected = new List<string>() { "AEPFEL", "OEFEN", "UEBER", "AE", "GROßFRAESMASCHINENOEFFNUNGSTUER" };

			var result = wordList.NormaliseList(data);

			Assert.Equal(expected, result);
		}

		[Fact]
		public void NormaliseVowels_Empty()
		{
			WordList wordList = new WordList();

			Assert.Equal("", wordList.NormaliseVowels(""));
		}

		[Fact]
		public void NormaliseVowels_BigVowels()
		{
			WordList wordList = new WordList();
			Assert.Equal("Ae", wordList.NormaliseVowels("Ä"));
			Assert.Equal("Oe", wordList.NormaliseVowels("Ö"));
			Assert.Equal("Ue", wordList.NormaliseVowels("Ü"));
		}
		[Fact]
		public void NormaliseVowels_SmallVowel()
		{
			WordList wordList = new WordList();
			Assert.Equal("ae", wordList.NormaliseVowels("ä"));
			Assert.Equal("oe", wordList.NormaliseVowels("ö"));
			Assert.Equal("ue", wordList.NormaliseVowels("ü"));
		}

		[Fact]
		public void NormaliseVowels_Words()
		{
			WordList wordList = new WordList();
			Assert.Equal("Aepfel", wordList.NormaliseVowels("Äpfel"));
			Assert.Equal("Oefen", wordList.NormaliseVowels("Öfen"));
			Assert.Equal("Ueber", wordList.NormaliseVowels("Über"));
		}

		[Fact] 
		public void Loaded_Initial()
		{
			WordList wordList = new WordList();

			bool loaded = wordList.Loaded();

			Assert.False(loaded);
		}

	}
}
