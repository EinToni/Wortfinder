using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Wortfinder.Interfaces;

namespace Wortfinder
{
    public class GameLibrary : IGameLibrary
    {
        internal Dictionary<int, List<Game>> LoadedGames { get; private set; } = new Dictionary<int, List<Game>>();
        private readonly IGameGenerator gameGenerator;
        private readonly Dictionary<int, Thread> threads = new Dictionary<int, Thread>();
        private readonly IGameDataController gameDataController;
        private readonly int minAmountOfGames = 5;

        public GameLibrary(IGameGenerator gameGenerator, IGameDataController gameDataController)
        {
            this.gameGenerator = gameGenerator;
            this.gameDataController = gameDataController;
        }

        public void LoadGeneratedGames()
        {
            List<int> gameSizes = new List<int>() { 4, 5, 6 };
            LoadGames();
            CheckGames(gameSizes, minAmountOfGames);
        }

        internal void LoadGames()
		{
            foreach (KeyValuePair<int, List<Game>> data in gameDataController.LoadGames())
            {
                LoadedGames.Add(data.Key, data.Value);
            }
        }

        internal void CheckGames(List<int> gameSizes, int amountOfGames)
		{
            foreach (int size in gameSizes)
            {
                CheckLoadedGames(size, amountOfGames);
            }
        }

        public Game GetGameWithSize(int fieldSize)
        {
            if (!LoadedGames.ContainsKey(fieldSize) || LoadedGames[fieldSize].Count == 0)
            {
                throw new KeyNotFoundException();
            }
            Game game = LoadedGames[fieldSize][0];
            LoadedGames[fieldSize].RemoveAt(0);
            gameDataController.SaveGames(LoadedGames);
            CheckLoadedGames(fieldSize, minAmountOfGames);
            return game;
        }

        public void CheckLoadedGames(int fieldSize, int minAmountLoaded)
        {
            if (!LoadedGames.ContainsKey(fieldSize))
            {
                LoadedGames.Add(fieldSize, new List<Game>());
            }
            if (!threads.ContainsKey(fieldSize))
            {
                Thread thread = new Thread(() => CheckLoadedGamesThread(fieldSize, minAmountLoaded));
                threads.Add(fieldSize, thread);
                thread.Start();
            }
        }

        internal void CheckLoadedGamesThread(int fieldSize, int minAmountLoaded)
        {
            for (int i = LoadedGames[fieldSize].Count; i < minAmountLoaded; i++)
            {
                LoadedGames[fieldSize].Add(gameGenerator.NewGame(fieldSize));
                gameDataController.SaveGames(LoadedGames);
            }
            threads.Remove(fieldSize);
        }
    }
}
