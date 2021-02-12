using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Wortfinder
{
    class GameLibrary
    {
        private readonly Dictionary<int, List<Game>> loadedGames = new Dictionary<int, List<Game>>();
        private readonly GameGenerator gameGenerator;
        private readonly Dictionary<int, Thread> threads = new Dictionary<int, Thread>();
        private readonly GameDataController gameDataController;
        public GameLibrary()
        {
            gameGenerator = new GameGenerator();
            gameDataController = new GameDataController();
        }

        public void LoadGeneratedGames()
        {
            List<int> gameSizes = new List<int>() { 4, 5, 6 };
            int amountOfGames = 5;
            foreach (KeyValuePair<int, List<Game>> data in gameDataController.LoadGames())
            {
                loadedGames.Add(data.Key, data.Value);
            }
            Thread thread = new Thread(() => GenerateAndSave(gameSizes, amountOfGames));
            thread.Start();
            foreach(int size in gameSizes)
            {
                CheckLoadedGames(size, amountOfGames);
            }
        }
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
            gameDataController.SaveLoadedGames(games);
        }

        public Game GetGameWithSize(int fieldSize)
        {
            if (!loadedGames.ContainsKey(fieldSize))
            {
                throw new KeyNotFoundException();
            }
            Game game = loadedGames[fieldSize][0];
            loadedGames[fieldSize].RemoveAt(0);
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
        private void CheckLoadedGamesThread(int fieldSize, int minAmountLoaded)
        {
            if (!loadedGames.ContainsKey(fieldSize))
            {
                Game game = gameGenerator.NewGame(fieldSize);
                if (!loadedGames.ContainsKey(fieldSize))
                {
                    loadedGames.Add(fieldSize, new List<Game>() { game });
                }
                loadedGames[fieldSize].Add(game);
            }
            for (int i = loadedGames[fieldSize].Count; i < minAmountLoaded; i++)
            {
                loadedGames[fieldSize].Add(gameGenerator.NewGame(fieldSize));
            }
            threads.Remove(fieldSize);
        }
    }
}
