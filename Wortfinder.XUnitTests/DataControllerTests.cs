using System.Collections.Generic;
using System.Windows.Documents;
using Xunit;

namespace Wortfinder.XUnitTests
{
	public class DataControllerTests
	{/*
		[Fact]
		public void CheckBeginningTest()
		{
			DataController dataController = new DataController();
			var result = dataController.CheckWord("AALGLATT", 0);
			Assert.True(result);
		}

		[Fact]
		public void CheckBeginnTest()
		{
			DataController dataController = new DataController();
			var result = dataController.CheckBeginning("ABAENDERUNGSVEREINBARUM", 0);
			Assert.True(result);
		}
		*/

		[Fact]
		public void InsertWord_Empty()
		{
			DataController dataController = new DataController();
			List<string> data = new List<string>() {"Hallo", "Welt"};
			List<string> expected = new List<string>() { "Hallo", "Welt"};

			dataController.InsertWord("", data);

			Assert.Equal(data, expected);
		}

		[Fact]
		public void InsertWord_Word()
		{
			DataController dataController = new DataController();
			List<string> data = new List<string>() { "Hallo", "Welt" };
			List<string> expected = new List<string>() { "Hallo", "Welt", "!" };

			dataController.InsertWord("!", data);

			Assert.Equal(data, expected);
		}

		[Fact]
		public void NormaliseList_Empty()
		{
			DataController dataController = new DataController();
			List<string> data = new List<string>();

			var result = dataController.NormaliseList(data);

			Assert.Equal(data, result);
		}

		[Fact]
		public void NormaliseList_umlaut()
		{
			DataController dataController = new DataController();
			List<string> data = new List<string>(){ "Äpfel", "Öfen", "Über", "Ä", "Großfräsmaschinenöffnungstür" };
			List<string> expected = new List<string>() { "AEPFEL", "OEFEN", "UEBER", "AE", "GROßFRAESMASCHINENOEFFNUNGSTUER" };

			var result = dataController.NormaliseList(data);

			Assert.Equal(expected, result);
		}

		[Fact]
		public void NormaliseVowels_Empty()
		{
			DataController dataController = new DataController();

			Assert.Equal("", dataController.NormaliseVowels(""));
		}

		[Fact]
		public void NormaliseVowels_BigVowels()
		{
			DataController dataController = new DataController();
			Assert.Equal("Ae", dataController.NormaliseVowels("Ä"));
			Assert.Equal("Oe", dataController.NormaliseVowels("Ö"));
			Assert.Equal("Ue", dataController.NormaliseVowels("Ü"));
		}
		[Fact]
		public void NormaliseVowels_SmallVowel()
		{
			DataController dataController = new DataController();
			Assert.Equal("ae", dataController.NormaliseVowels("ä"));
			Assert.Equal("oe", dataController.NormaliseVowels("ö"));
			Assert.Equal("ue", dataController.NormaliseVowels("ü"));
		}

		[Fact]
		public void NormaliseVowels_Words()
		{
			DataController dataController = new DataController();
			Assert.Equal("Aepfel", dataController.NormaliseVowels("Äpfel"));
			Assert.Equal("Oefen", dataController.NormaliseVowels("Öfen"));
			Assert.Equal("Ueber", dataController.NormaliseVowels("Über"));
		}
	}
}
