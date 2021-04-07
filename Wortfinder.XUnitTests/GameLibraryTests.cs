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

			Assert.Empty(gamelibrary.LoadedGames);
		}
		[Fact]
		public void LoadGames()
		{
			int size1 = 5;
			int size2 = 7;
			Mock<IGameGenerator> gameGenerator = new Mock<IGameGenerator>();
			Mock<IGameDataController> gameDataController = new Mock<IGameDataController>();
			var dict = new Dictionary<int, List<Game>>
			{
				{ size1, new List<Game>() },
				{ size2, new List<Game>() }
			};
			gameDataController.Setup(x => x.LoadGames()).Returns(dict);

			GameLibrary gamelibrary = new GameLibrary(gameGenerator.Object, gameDataController.Object);
			gamelibrary.LoadGames();

			Assert.True(gamelibrary.LoadedGames.ContainsKey(size1));
			Assert.True(gamelibrary.LoadedGames.ContainsKey(size2));
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

			Assert.Single(gameLibrary.LoadedGames);
			Assert.Contains(size, gameLibrary.LoadedGames.Keys);
			Assert.Equal(minAmount, gameLibrary.LoadedGames[size].Count);
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

			Assert.Single(gameLibrary.LoadedGames);
			Assert.Contains(size, gameLibrary.LoadedGames.Keys);
			Assert.Equal(minAmount, gameLibrary.LoadedGames[size].Count);
		}
	}
}
