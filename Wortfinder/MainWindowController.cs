using System;
using System.Collections.Generic;
using System.Text;
using Wortfinder.Interfaces;

namespace Wortfinder
{
    public class MainWindowController : IMainWindowController
    {
        private readonly IMainWindow mainWindow;
        private readonly IWordMissingController wordMissingController;
        private IGameManager gameManager;
        private string wordBuild = "";
        private readonly List<Coordinate> wordCoords = new List<Coordinate>();

        public MainWindowController(IMainWindow mainWindow, IWordMissingController wordMissingController) 
        {
            this.mainWindow = mainWindow;
            this.wordMissingController = wordMissingController;
        }
		internal void SetGameManager(IGameManager gameManager)   => this.gameManager = gameManager;

		public bool SetTimer(int timeInSeconds)
        {
            mainWindow.SetTime(timeInSeconds.ToString() + " s");
            return true;
        }
        
        public void SetGameField(int size, char[] letters) => mainWindow.SetGameField(size, letters);
        public void AddWordToShow(Word word)            => mainWindow.AddWordToShow(word);
        public void SetWordsToShow(List<Word> words)    => mainWindow.SetWordsToShow(words);
        public void ClearWordsToShow()                  => mainWindow.ClearWords();
        public void LettersActive()                     => mainWindow.LettersActive();
        public void LettersInactive()                   => mainWindow.LettersInactive();
        public void SetCurrentScore(int score)          => mainWindow.SetCurrentScore(score.ToString());
        public void SetMaxWordsFindable(int amount)     => mainWindow.SetFindableWordsAmount(amount.ToString());
        public void SetFoundWordsAmount(int amount)     => mainWindow.SetFoundWordsAmount(amount.ToString());
        public void SetBestScores(List<Score> scores)   => mainWindow.SetBestScores(scores);

        public void NewGame(string fieldSize, string gameTimeInMinutes) => gameManager.NewGame(int.Parse(fieldSize), int.Parse(gameTimeInMinutes) * 60);
        internal void StopGame()                        => gameManager.StopGame();
        internal int GetFieldSize()                     => gameManager.GetFieldSize();

        public void TryWord(string word)
        {
            gameManager.TryWord(word);
            wordBuild = "";
            wordCoords.Clear();
            mainWindow.DeselectAllLetters();
        }

        internal bool HoverLetter(string letter, string row, string column)
        {
            if (gameManager.GameRunning && wordBuild != "")
            {
                Coordinate coordinate = new Coordinate(int.Parse(row), int.Parse(column));
                if (!AlreadyClicked(coordinate, wordCoords))
				{
                    if (wordCoords.Count > 0 && !coordinate.IsNeighbour(wordCoords[^1]))
                    {
                        TryWord(wordBuild);
                    } else { 
                        wordBuild += letter;
                        wordCoords.Add(coordinate);
                        return true;
                    }
                }
            }
            return false;
        }

        internal bool AlreadyClicked(Coordinate coordinate, List<Coordinate> coordinates)
		{
            foreach (Coordinate clickedCoord in coordinates)
            {
                if (coordinate.Equals(clickedCoord))
                {
                    return true;
                }
            }
            return false;
        }
        internal bool ClickLetter(string letter, string row, string column)
        {
            if (gameManager.GameRunning)
            {
                wordBuild = letter;
                wordCoords.Clear();
                wordCoords.Add(new Coordinate(int.Parse(row), int.Parse(column)));
                return true;
            }
            return false;
        }
        internal void ReleaseMouse()
        {
            TryWord(wordBuild);
        }

		internal void WordMissing()
		{
            wordMissingController.Open();
            
        }
	}
}
