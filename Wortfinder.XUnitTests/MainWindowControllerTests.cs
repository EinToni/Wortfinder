using Xunit;
using Moq;
using System.Collections.Generic;
using Wortfinder.Interfaces;

namespace Wortfinder.XUnitTests
{
	public class MainWindowControllerTests
	{
		[Fact]
		public void CurrentScore()
		{
			Mock<IMainWindow> mainWindow = new Mock<IMainWindow>();
			Mock<IWordMissingController> wordMissingController = new Mock<IWordMissingController>();
			Mock<IWordBuilder> wordBuilder = new Mock<IWordBuilder>();
			mainWindow.Setup(x => x.SetCurrentScore("10")).Verifiable();
			MainWindowController mainWindowController = new MainWindowController(mainWindow.Object, wordMissingController.Object, wordBuilder.Object);

			mainWindowController.SetCurrentScore(10);

			mainWindow.Verify();
		}

		[Fact]
		public void MaxWordsFindable()
		{
			Mock<IMainWindow> mainWindow = new Mock<IMainWindow>();
			Mock<IWordMissingController> wordMissingController = new Mock<IWordMissingController>();
			Mock<IWordBuilder> wordBuilder = new Mock<IWordBuilder>();
			mainWindow.Setup(x => x.SetFindableWordsAmount("10")).Verifiable();
			MainWindowController mainWindowController = new MainWindowController(mainWindow.Object, wordMissingController.Object, wordBuilder.Object);

			mainWindowController.SetMaxWordsFindable(10);

			mainWindow.Verify();
		}

		[Fact]
		public void FoundWordsAmount()
		{
			Mock<IMainWindow> mainWindow = new Mock<IMainWindow>();
			Mock<IWordMissingController> wordMissingController = new Mock<IWordMissingController>();
			Mock<IWordBuilder> wordBuilder = new Mock<IWordBuilder>();
			mainWindow.Setup(x => x.SetFoundWordsAmount("10")).Verifiable();
			MainWindowController mainWindowController = new MainWindowController(mainWindow.Object, wordMissingController.Object, wordBuilder.Object);

			mainWindowController.SetFoundWordsAmount(10);

			mainWindow.Verify();
		}

		[Fact]
		public void LettersInactive()
		{
			Mock<IMainWindow> mainWindow = new Mock<IMainWindow>();
			Mock<IWordMissingController> wordMissingController = new Mock<IWordMissingController>();
			Mock<IWordBuilder> wordBuilder = new Mock<IWordBuilder>();
			mainWindow.Setup(x => x.LettersInactive()).Verifiable();
			MainWindowController mainWindowController = new MainWindowController(mainWindow.Object, wordMissingController.Object, wordBuilder.Object);

			mainWindowController.LettersInactive();

			mainWindow.Verify();
		}

		[Fact]
		public void LettersActive()
		{
			Mock<IMainWindow> mainWindow = new Mock<IMainWindow>();
			Mock<IWordMissingController> wordMissingController = new Mock<IWordMissingController>();
			Mock<IWordBuilder> wordBuilder = new Mock<IWordBuilder>();
			mainWindow.Setup(x => x.LettersActive()).Verifiable();
			MainWindowController mainWindowController = new MainWindowController(mainWindow.Object, wordMissingController.Object, wordBuilder.Object);

			mainWindowController.LettersActive();

			mainWindow.Verify();
		}

		[Fact]
		public void ClearWordsToShow()
		{
			Mock<IMainWindow> mainWindow = new Mock<IMainWindow>();
			Mock<IWordMissingController> wordMissingController = new Mock<IWordMissingController>();
			Mock<IWordBuilder> wordBuilder = new Mock<IWordBuilder>();
			mainWindow.Setup(x => x.ClearWords()).Verifiable();
			MainWindowController mainWindowController = new MainWindowController(mainWindow.Object, wordMissingController.Object, wordBuilder.Object);

			mainWindowController.ClearWordsToShow();

			mainWindow.Verify();
		}

		[Fact]
		public void SetBestScores()
		{
			Mock<IMainWindow> mainWindow = new Mock<IMainWindow>();
			Mock<IWordMissingController> wordMissingController = new Mock<IWordMissingController>();
			Mock<IWordBuilder> wordBuilder = new Mock<IWordBuilder>();
			List<Score> scores = new List<Score>();
			mainWindow.Setup(x => x.SetBestScores(scores)).Verifiable();
			MainWindowController mainWindowController = new MainWindowController(mainWindow.Object, wordMissingController.Object, wordBuilder.Object);

			mainWindowController.SetBestScores(scores);

			mainWindow.Verify();
		}

		[Fact]
		public void SetWordsToShow()
		{
			Mock<IMainWindow> mainWindow = new Mock<IMainWindow>();
			Mock<IWordMissingController> wordMissingController = new Mock<IWordMissingController>();
			Mock<IWordBuilder> wordBuilder = new Mock<IWordBuilder>();
			List<Word> words = new List<Word>();
			mainWindow.Setup(x => x.SetWordsToShow(words)).Verifiable();
			MainWindowController mainWindowController = new MainWindowController(mainWindow.Object, wordMissingController.Object, wordBuilder.Object);

			mainWindowController.SetWordsToShow(words);

			mainWindow.Verify();
		}

		[Fact]
		public void AddWordToShow()
		{
			Mock<IMainWindow> mainWindow = new Mock<IMainWindow>();
			Mock<IWordMissingController> wordMissingController = new Mock<IWordMissingController>();
			Mock<IWordBuilder> wordBuilder = new Mock<IWordBuilder>();
			Word word = new Word("", new List<Coordinate>());
			mainWindow.Setup(x => x.AddWordToShow(word)).Verifiable();
			MainWindowController mainWindowController = new MainWindowController(mainWindow.Object, wordMissingController.Object, wordBuilder.Object);

			mainWindowController.AddWordToShow(word);

			mainWindow.Verify();
		}

		[Fact]
		public void SetGameField()
		{
			Mock<IMainWindow> mainWindow = new Mock<IMainWindow>();
			Mock<IWordMissingController> wordMissingController = new Mock<IWordMissingController>();
			Mock<IWordBuilder> wordBuilder = new Mock<IWordBuilder>();
			int size = 5;
			char[] letters = new char[0];
			mainWindow.Setup(x => x.SetGameField(size, letters)).Verifiable();
			MainWindowController mainWindowController = new MainWindowController(mainWindow.Object, wordMissingController.Object, wordBuilder.Object);

			mainWindowController.SetGameField(size, letters);

			mainWindow.Verify();
		}

		[Fact]
		public void SetTimer()
		{
			Mock<IMainWindow> mainWindow = new Mock<IMainWindow>();
			Mock<IWordMissingController> wordMissingController = new Mock<IWordMissingController>();
			Mock<IWordBuilder> wordBuilder = new Mock<IWordBuilder>();
			int time = 5;
			mainWindow.Setup(x => x.SetTime("5 s")).Verifiable();
			
			MainWindowController mainWindowController = new MainWindowController(mainWindow.Object, wordMissingController.Object, wordBuilder.Object);
			bool result = mainWindowController.SetTimer(time);

			mainWindow.Verify();
			Assert.True(result);
		}

		[Fact]
		public void ReleaseMouse()
		{
			Mock<IMainWindow> mainWindow = new Mock<IMainWindow>();
			Mock<IWordMissingController> wordMissingController = new Mock<IWordMissingController>();
			Mock<IWordBuilder> wordBuilder = new Mock<IWordBuilder>();
			Mock<IGameManager> gameManager = new Mock<IGameManager>();
			gameManager.Setup(x => x.TryWord(It.IsAny<string>())).Verifiable();
			mainWindow.Setup(x => x.DeselectAllLetters()).Verifiable();

			MainWindowController mainWindowController = new MainWindowController(mainWindow.Object, wordMissingController.Object, wordBuilder.Object);
			mainWindowController.SetGameManager(gameManager.Object);
			mainWindowController.ReleaseMouse();

			mainWindow.Verify();
			gameManager.Verify();
		}

		[Fact]
		public void HoverLetter_NotClickedYet()
		{
			Mock<IMainWindow> mainWindow = new Mock<IMainWindow>();
			Mock<IWordMissingController> wordMissingController = new Mock<IWordMissingController>();
			Mock<IWordBuilder> wordBuilder = new Mock<IWordBuilder>();
			Mock<IGameManager> gameManager = new Mock<IGameManager>();

			MainWindowController mainWindowController = new MainWindowController(mainWindow.Object, wordMissingController.Object, wordBuilder.Object);
			mainWindowController.SetGameManager(gameManager.Object);

			bool result = mainWindowController.HoverLetter("S", "5", "5");

			Assert.False(result);
		}
	}
}
