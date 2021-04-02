using Xunit;
using Moq;
using System.Collections.Generic;

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
	}
}
