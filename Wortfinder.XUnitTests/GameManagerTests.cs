using Xunit;
using Moq;
using Wortfinder.Interfaces;
using System.Collections.Generic;

namespace Wortfinder.XUnitTests
{
	public class GameManagerTests
	{
		[Fact]
		public void StopGame_NullGame()
		{
			// Arrange
			Mock<IMainWindowController> mainWindowController	= new Mock<IMainWindowController>();
			Mock<IScoreManager> scoreManager					= new Mock<IScoreManager>();
			Mock<IGameLibrary> gameLibrary						= new Mock<IGameLibrary>();
			Mock<IGameScore> gameScore							= new Mock<IGameScore>();
			Mock<IGameTimer> gameTimer							= new Mock<IGameTimer>();
			GameManager gameManager = new GameManager(mainWindowController.Object, 
				scoreManager.Object, gameLibrary.Object, gameScore.Object, gameTimer.Object);

			// Act
			gameManager.StopGame();

			// Assert
			Assert.False(gameManager.GameRunning);
		}

		[Fact]
		public void LoadNewGame()
		{
			Mock<IMainWindowController> mainWindowController = new Mock<IMainWindowController>();
			Mock<IScoreManager> scoreManager = new Mock<IScoreManager>();
			Mock<IGameLibrary> gameLibrary = new Mock<IGameLibrary>();
			Mock<IGameScore> gameScore = new Mock<IGameScore>();
			Mock<IGameTimer> gameTimer = new Mock<IGameTimer>();
			int size = 5;
			int time = 60;
			gameLibrary.Setup(x => x.GetGameWithSize(size)).Returns(new Game(new char[0], size, new List<Word>())).Verifiable();
			gameScore.Setup(x => x.SetDifficulty(size, time)).Verifiable();
			GameManager gameManager = new GameManager(mainWindowController.Object,
				scoreManager.Object, gameLibrary.Object, gameScore.Object, gameTimer.Object);

			gameManager.LoadNewGame(size, time);

			gameLibrary.Verify();
			gameScore.Verify();
		}
	}
}
