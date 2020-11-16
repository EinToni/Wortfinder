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
		private readonly GameController	gameController;
		private readonly GameScore gameScore;
		private readonly GameGridFutureNew gameGrid;

		public MainWindow()
		{
			InitializeComponent();
			gameScore = new GameScore(OutputScore);
			gameGrid = new GameGridFutureNew(LetterGrid);
			gameController = new GameController(this, LetterGrid, gameScore);
		}

		private void NewGame(object sender, RoutedEventArgs e) {
			List<RadioButton> radioButtons = controllGrid.Children.OfType<RadioButton>().ToList();
			RadioButton fieldSizeButton = radioButtons.Where(r => r.GroupName == "FieldSize" && r.IsChecked == true).Single();
			int fieldSize = int.Parse(fieldSizeButton.Tag.ToString());
			RadioButton gameTimeButton  = radioButtons.Where(r => r.GroupName == "GameTime"  && r.IsChecked == true).Single();
			int gameTime = int.Parse(gameTimeButton.Tag.ToString());

			allWords.Children.Clear();
			gameController.NewGame(fieldSize, gameTime);
			gameScore.ResetScore();
			//gameGrid.NewGrid(fieldSize);
		}

		public void SetPoints(int points)
		{
			OutputScore.Content = points.ToString();
		}

		private void WordMissing(object sender, RoutedEventArgs e)
		{
			WordMissingWindow wmw = new WordMissingWindow();
			wmw.Show();
		}

		public void AddAllWordsToList(List<Word> words)
		{
			Dispatcher.Invoke(new System.Action(() => { AddWordsInvoke(words); }));
		}

		public void AddWordToList(Word word)
		{
			allWords.Children.Add(new WordDisplay(word));
		}

		private void AddWordsInvoke(List<Word> words)
		{
			List<WordDisplay> wordDisplays = new List<WordDisplay>();
			for(int i = 0; i < words.Count; i++)
			{
				wordDisplays.Add(new WordDisplay(words[i]));
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

		public void ActivateAllLetters()
		{
			foreach (LetterBox letter in LetterGrid.Children)
			{
				letter.Activated = true;
			}
		}

		public void DeactivateAllLetters()
		{
			foreach(LetterBox letter in LetterGrid.Children)
			{
				letter.Activated = false;
			}
		}

		internal void ShowAllWords()
		{
			foreach(WordDisplay wordDisplay in allWords.Children)
			{
				wordDisplay.ShowWord();
			}
		}
	}
}