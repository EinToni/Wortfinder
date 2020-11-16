using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Wortfinder
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private readonly GameScore gameScore;
		private readonly GameGrid gameGrid;
		private readonly GameTimer gameTimer;
		private readonly FindableWords findableWords;

		public MainWindow()
		{
			InitializeComponent();
			DataController dataController = new DataController();
			dataController.LoadGerman();
			gameTimer = new GameTimer(remainingTimeLabel, StopGame);
			gameScore = new GameScore(OutputScore);
			findableWords = new FindableWords(this, allWords);
			gameGrid = new GameGrid(dataController, gameScore, findableWords, LetterGrid);
			
		}

		private void NewGame(object sender, RoutedEventArgs e) {
			List<RadioButton> radioButtons = controllGrid.Children.OfType<RadioButton>().ToList();
			RadioButton fieldSizeButton = radioButtons.Where(r => r.GroupName == "FieldSize" && r.IsChecked == true).Single();
			int fieldSize = int.Parse(fieldSizeButton.Tag.ToString());
			RadioButton gameTimeButton  = radioButtons.Where(r => r.GroupName == "GameTime"  && r.IsChecked == true).Single();
			int gameTime = int.Parse(gameTimeButton.Tag.ToString());

			findableWords.ClearAllWords();
			gameScore.ResetScore();
			gameGrid.NewGrid(fieldSize);
			gameTimer.StartTimerInMinutes(gameTime);
		}

		public bool StopGame()
		{
			gameGrid.DeactivateAllLetter();
			findableWords.ShowAllWords();
			return true;
		}

		private void WordMissing(object sender, RoutedEventArgs e)
		{
			WordMissingWindow wmw = new WordMissingWindow();
			wmw.Show();
		}

		public void AddAllWordsToList(List<Word> words)
		{
			Dispatcher.Invoke(new Action(() => { AddWordsInvoke(words); }));
		}

		public void AddWordToList(Word word)
		{
			Dispatcher.Invoke(new Action(() => { allWords.Children.Add(new WordDisplay(gameGrid, word)); }));
		}

		private void AddWordsInvoke(List<Word> words)
		{
			List<WordDisplay> wordDisplays = new List<WordDisplay>();
			for(int i = 0; i < words.Count; i++)
			{
				wordDisplays.Add(new WordDisplay(gameGrid, words[i]));
				foreach (WordDisplay wordAlreadyInList in allWords.Children)
				{
					if(words[i].Name.Equals(wordAlreadyInList.Word.Name))
					{
						wordDisplays[i] = wordAlreadyInList;
					}
				}
			}
			allWords.Children.Clear();
			foreach(WordDisplay word in wordDisplays)
			{
				allWords.Children.Add(word);
			}
		}
	}
}