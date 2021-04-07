using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Wortfinder.Interfaces;

namespace Wortfinder
{
    public class GameLibrary : IGameLibrary
    {
        internal Dictionary<int, List<Game>> loadedGames { get; private set; } = new Dictionary<int, List<Game>>();
        private readonly IGameGenerator gameGenerator;
        private readonly Dictionary<int, Thread> threads = new Dictionary<int, Thread>();
        private readonly IGameDataController gameDataController;
        public GameLibrary(IGameGenerator gameGenerator, IGameDataController gameDataController)
        {
            this.gameGenerator = gameGenerator;
            this.gameDataController = gameDataController;
        }

        public void LoadGeneratedGames()
        {
            List<int> gameSizes = new List<int>() { 4, 5, 6 };
            int amountOfGames = 5;
            LoadGames();
            //GenerateNewGames(gameSizes, amountOfGames);
            CheckGames(gameSizes, amountOfGames);
        }
        internal void LoadGames()
		{
            foreach (KeyValuePair<int, List<Game>> data in gameDataController.LoadGames())
            {
                loadedGames.Add(data.Key, data.Value);
            }
        }
        // Deprecated
        internal void GenerateNewGames(List<int> gameSizes, int amountOfGames)
		{
            Thread thread = new Thread(() => GenerateAndSave(gameSizes, amountOfGames));
            thread.Start();
        }
        internal void CheckGames(List<int> gameSizes, int amountOfGames)
		{
            foreach (int size in gameSizes)
            {
                CheckLoadedGames(size, amountOfGames);
            }
        }
        // Deprecated
        public void GenerateAndSave(List<int> sizes, int numberOfGames)
        {
            Dictionary<int, List<Game>> games = new Dictionary<int, List<Game>>();
            foreach(int size in sizes)
            {
                for (int i = 0; i < numberOfGames; i++)
                {
                    Game newGame = gameGenerator.NewGame(size);
                    if (games.ContainsKey(size))
                    {
                        games[size].Add(newGame);
                    }
                    else
                    {
                        games.Add(size, new List<Game>() { newGame });
                    }
                }
            }
            gameDataController.SaveGames(games);
        }

        public Game GetGameWithSize(int fieldSize)
        {
            if (!loadedGames.ContainsKey(fieldSize))
            {
                throw new KeyNotFoundException();
            }
            Game game = loadedGames[fieldSize][0];
            loadedGames[fieldSize].RemoveAt(0);
            gameDataController.SaveGames(loadedGames);
            return game;
        }

        public void CheckLoadedGames(int fieldSize, int minAmountLoaded)
        {
            if (!threads.ContainsKey(fieldSize))
            {
                Thread thread = new Thread(() => CheckLoadedGamesThread(fieldSize, minAmountLoaded));
                threads.Add(fieldSize, thread);
                thread.Start();
            }
        }

        internal void CheckLoadedGamesThread(int fieldSize, int minAmountLoaded)
        {
            if (!loadedGames.ContainsKey(fieldSize))
            {
                loadedGames.Add(fieldSize, new List<Game>());
            }
            for (int i = loadedGames[fieldSize].Count; i < minAmountLoaded; i++)
            {
                loadedGames[fieldSize].Add(gameGenerator.NewGame(fieldSize));
            }
            gameDataController.SaveGames(loadedGames);
            threads.Remove(fieldSize);
        }
    }
}
