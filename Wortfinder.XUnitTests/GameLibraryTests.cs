using Xunit;
using Moq;
using System.Collections.Generic;
using System;

namespace Wortfinder.XUnitTests
{
	public class GameLibraryTests
	{
		[Fact]
		public void LoadGames_EmptyList()
		{
			Mock<IGameGenerator> gameGenerator = new Mock<IGameGenerator>();
			Mock<IGameDataController> gameDataController = new Mock<IGameDataController>();
			gameDataController.Setup(x => x.LoadGames()).Returns(new Dictionary<int, List<Game>>());

			GameLibrary gamelibrary = new GameLibrary(gameGenerator.Object, gameDataController.Object);
			gamelibrary.LoadGames();

			Assert.Empty(gamelibrary.loadedGames);
		}
		[Fact]
		public void LoadGames()
		{
			Mock<IGameGenerator> gameGenerator = new Mock<IGameGenerator>();
			Mock<IGameDataController> gameDataController = new Mock<IGameDataController>();
			var dict = new Dictionary<int, List<Game>>
			{
				{ 5, new List<Game>() },
				{ 7, new List<Game>() }
			};
			gameDataController.Setup(x => x.LoadGames()).Returns(dict);

			GameLibrary gamelibrary = new GameLibrary(gameGenerator.Object, gameDataController.Object);
			gamelibrary.LoadGames();

			Assert.True(gamelibrary.loadedGames.ContainsKey(5));
			Assert.True(gamelibrary.loadedGames.ContainsKey(7));
		}

		[Fact]
		public void GenerateAndSaveTest()
		{
			Mock<IGameDataController> gameDataController	= new Mock<IGameDataController>();
			Mock<IGameGenerator> gameGenerator				= new Mock<IGameGenerator>();
			Game game										= new Game(new char[0], 5, new List<Word>());
			gameGenerator.Setup(x => x.NewGame(5)).Returns(game).Verifiable();
			gameDataController.Setup(x => x.SaveGames(It.IsAny<Dictionary<int, List<Game>>>())).Verifiable();


			List<int> sizes = new List<int>() { 5 };
			int numberOfGames = 5;
			GameLibrary gameLibrary = new GameLibrary(gameGenerator.Object, gameDataController.Object);

			gameLibrary.GenerateAndSave(sizes, numberOfGames);
			gameGenerator.Verify();
			gameDataController.Verify();
		}
		[Fact]
		public void GetGameWithSize_NoGameAvailable()
		{
			Mock<IGameDataController> gameDataController = new Mock<IGameDataController>();
			Mock<IGameGenerator> gameGenerator = new Mock<IGameGenerator>();
			int size = 5;
			GameLibrary gameLibrary = new GameLibrary(gameGenerator.Object, gameDataController.Object);

			Assert.Throws<KeyNotFoundException>(() => gameLibrary.GetGameWithSize(size));
		}

		[Fact]
		public void CheckLoadedGamesThread_Single()
		{
			Mock<IGameDataController> gameDataController = new Mock<IGameDataController>();
			Mock<IGameGenerator> gameGenerator = new Mock<IGameGenerator>();
			int size = 5;
			int minAmount = 1;
			gameGenerator.Setup(x => x.NewGame(size)).Returns(new Game(new char[0], size, new List<Word>()));
			GameLibrary gameLibrary = new GameLibrary(gameGenerator.Object, gameDataController.Object);
			
			gameLibrary.CheckLoadedGamesThread(size, minAmount);

			Assert.Single(gameLibrary.loadedGames);
			Assert.Contains(size, gameLibrary.loadedGames.Keys);
			Assert.Equal(minAmount, gameLibrary.loadedGames[size].Count);
		}

		[Fact]
		public void CheckLoadedGamesThread_Multiple()
		{
			Mock<IGameDataController> gameDataController = new Mock<IGameDataController>();
			Mock<IGameGenerator> gameGenerator = new Mock<IGameGenerator>();
			int size = 5;
			int minAmount = 5;
			gameGenerator.Setup(x => x.NewGame(size)).Returns(new Game(new char[0], size, new List<Word>()));
			GameLibrary gameLibrary = new GameLibrary(gameGenerator.Object, gameDataController.Object);

			gameLibrary.CheckLoadedGamesThread(size, minAmount);

			Assert.Single(gameLibrary.loadedGames);
			Assert.Contains(size, gameLibrary.loadedGames.Keys);
			Assert.Equal(minAmount, gameLibrary.loadedGames[size].Count);
		}
	}
}
