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
			gameLibrary.Setup(x => x.GetGameWithSize(5)).Returns(new Game(new char[0], 5, new List<Word>())).Verifiable();
			gameScore.Setup(x => x.SetDifficulty(5, 60)).Verifiable();
			GameManager gameManager = new GameManager(mainWindowController.Object,
				scoreManager.Object, gameLibrary.Object, gameScore.Object, gameTimer.Object);

			gameManager.LoadNewGame(5, 60);

			gameLibrary.Verify();
			gameScore.Verify();
		}
	}
}
