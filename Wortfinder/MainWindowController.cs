using System;
using System.Collections.Generic;
using System.Text;

namespace Wortfinder
{
    class MainWindowController : IMainWindowController
    {
        private readonly MainWindow mainWindow;
        private readonly GameManager gameManager;
        private string wordBuild = "";
        private List<Coordinate> wordCoords = new List<Coordinate>();
        public MainWindowController(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            this.gameManager = new GameManager(this);
        }

        public void SetGameField(int size, char[] letters) => mainWindow.SetGameField(size, letters);
        public void NewGame(int fieldSize, int gameTimeInMinutes) => gameManager.NewGame(fieldSize, gameTimeInMinutes * 60);
        public bool SetTimer(int timeInSeconds)
        {
            mainWindow.Time = timeInSeconds.ToString() + " s";
            return true;
        }
        public void ShowWord(Word word) => mainWindow.AddFoundWord(word);
        public void ShowWords(List<Word> words) => mainWindow.ShowWords(words);
        internal void StopGame() => gameManager.StopGame();
        internal int GetFieldSize() => gameManager.GetFieldSize();
        public void LettersActive() => mainWindow.LettersActive();
        public void LettersInactive() => mainWindow.LettersInactive();
        public void SetCurrentScore(int score) => mainWindow.ActualScore = score.ToString();
        public void SetMaxWordsFindable(int amount) => mainWindow.FindableWordsAmount = amount.ToString();
        public void SetFoundWordsAmount(int amount) => mainWindow.FoundWordsAmount = amount.ToString();
        public void SetBestScores(List<Score> scores) => mainWindow.SetBestScores(scores);
        public void TryWord(string word)
        {
            gameManager.TryWord(word);
            wordBuild = "";
            wordCoords.Clear();
            mainWindow.DeselectAllLetters();
        }
        internal bool HoverLetter(string letter, string row, string column)
        {
            if (gameManager.GameRunning() && wordBuild != "")
            {
                Coordinate coordinate = new Coordinate(int.Parse(row), int.Parse(column));
                foreach(Coordinate clickedCoord in wordCoords)
                {
                    if (coordinate.Equals(clickedCoord))
                    {
                        TryWord(wordBuild);
                        return false;
                    }
                }
                if (wordCoords.Count > 0 && !coordinate.IsNeighbour(wordCoords[^1]))
                {
                    TryWord(wordBuild);
                    return false;
                }
                wordBuild += letter;
                wordCoords.Add(coordinate);
                return true;
            }
            return false;
        }

        internal bool ClickLetter(string letter, string row, string column)
        {
            if (gameManager.GameRunning())
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
    }
}
