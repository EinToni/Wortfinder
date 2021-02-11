using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

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
		private readonly ScoreManager scoreManager;
		private readonly MainWindowController mainWindowController;
		private string selectedWord = "";
		private List<int[]> selectedFields = new List<int[]>();

		public MainWindow()
		{
			InitializeComponent();
			mainWindowController = new MainWindowController(this);
			DataController dataController = new DataController();
			dataController.LoadGerman();
			scoreManager = new ScoreManager();
			gameTimer = new GameTimer(remainingTimeLabel, StopGame);
			gameScore = new GameScore(OutputScore);
			findableWords = new FindableWords(this, allWords);
			gameGrid = new GameGrid(dataController, gameScore, findableWords, LetterGrid);
			UpdateScore();
		}

		public void SetGameField(int size, char[] letters)
        {
			SetField(size);
			SetLetters(size, letters);
		}

		private void SetField(int size)
        {
			LetterGrid.RowDefinitions.Clear();
			LetterGrid.ColumnDefinitions.Clear();
			for (int i = 0; i < size; i++)
			{
				LetterGrid.RowDefinitions.Add(
					new RowDefinition()
					{
						Height = new GridLength(1, GridUnitType.Star)
					});
				LetterGrid.ColumnDefinitions.Add(
					new ColumnDefinition()
					{
						Width = new GridLength(1, GridUnitType.Star)
					});
			}
		}
		private void SetLetters(int size, char[] letters)
        {
			for (int i = 0; i < letters.Length; i++)
			{
				Viewbox mainBox = new Viewbox();
				Grid boxGrid = new Grid();
				Viewbox letterBox = new Viewbox() { Name = "letter" };
				Viewbox hitBox = new Viewbox();
				TextBlock letter = new TextBlock() { Text = letters[i].ToString() };
				Ellipse hitCircle = new Ellipse()
				{
					Name = (letters[i].ToString() + "_" + (i / size).ToString() + "_" + ((i % size)).ToString()).ToString(),
					Width = 100,
					Height = 100,
					Stroke = new SolidColorBrush(Colors.Transparent),
					Fill = new SolidColorBrush(Colors.Transparent)
				};
				hitCircle.PreviewMouseDown += new MouseButtonEventHandler(ClickLetter);
				hitCircle.MouseEnter += new MouseEventHandler(HoverLetter);

				letterBox.Child = letter;
				hitBox.Child = hitCircle;

				boxGrid.Children.Add(letterBox);
				boxGrid.Children.Add(hitBox);
				mainBox.Child = boxGrid;

				LetterGrid.Children.Add(mainBox);
				Grid.SetRow(mainBox, i / size);
				Grid.SetColumn(mainBox, (i % size));
			}
		}
		private void ClickLetter(object sender, MouseButtonEventArgs e)
		{
			Ellipse ellipse = sender as Ellipse;
			selectedWord = ellipse.Name.Split('_')[0];
			int coordRow = int.Parse(ellipse.Name.Split('_')[1]);
			int coordCol = int.Parse(ellipse.Name.Split('_')[2]);
			selectedFields = new List<int[]>() { new int[] { coordRow, coordCol } };
		}
		private void HoverLetter(object sender, MouseEventArgs e)
		{
            if (selectedWord != "") { 
				Ellipse ellipse = sender as Ellipse;
				string letter = ellipse.Name.Split('_')[0];
				int coordRow = int.Parse(ellipse.Name.Split('_')[1]);
				int coordCol = int.Parse(ellipse.Name.Split('_')[2]);
				bool clickValid = true;
				foreach(int[] i in selectedFields)
				{
					int row = i[0];
					int col = i[1];
					if (row == coordRow && col == coordCol)
					{
						EndClick();
						clickValid = false;
						break;
					}
				}
				if (Math.Abs(selectedFields[^1][0] - coordRow) > 1 ||
					Math.Abs(selectedFields[^1][1] - coordCol) > 1)
                {
					EndClick();
					clickValid = false;
				}
				if (clickValid)
                {
					selectedWord += letter;
					selectedFields.Add(new int[] { coordRow, coordCol });
					Console.WriteLine(selectedWord);
				}
			}
		}
		private void ReleaseMouse(object sender, MouseButtonEventArgs e)
		{
			EndClick();
		}
		private void EndClick()
        {
			mainWindowController.TryWord(selectedWord);
			selectedFields.Clear();
			selectedWord = "";
		}

		private void NewGame(object sender, RoutedEventArgs e) {
			RadioButton sizeRadioButton = FieldSizeSelection.Children.OfType<RadioButton>().ToList().Where(r => r.IsChecked == true).Single();
			RadioButton gameTimeButton = GameTimeSelection.Children.OfType<RadioButton>().ToList().Where(r => r.IsChecked == true).Single();
			int fieldSize = int.Parse(sizeRadioButton.Tag.ToString());
			int gameTime = int.Parse(gameTimeButton.Tag.ToString());

			mainWindowController.NewGame(fieldSize, gameTime);

			//findableWords.ClearAllWords();
			//gameScore.ResetScore();
			//gameScore.SetDifficulty(fieldSize, gameTime);
			//gameGrid.NewGrid(fieldSize);
			//gameTimer.StartTimerInMinutes(gameTime);
		}

		public bool StopGame()
		{
			gameGrid.DeactivateAllLetter();
			findableWords.ShowAllWords();
			RadioButton sizeRadioButton = FieldSizeSelection.Children.OfType<RadioButton>().ToList().Where(r => r.IsChecked == true).Single();
			RadioButton gameTimeButton = GameTimeSelection.Children.OfType<RadioButton>().ToList().Where(r => r.IsChecked == true).Single();
			int fieldSize = int.Parse(sizeRadioButton.Tag.ToString());
			int gameTime = int.Parse(gameTimeButton.Tag.ToString());
			SaveScoreWindow saveScoreWindow = new SaveScoreWindow(this, scoreManager, int.Parse(OutputScore.Content.ToString()), fieldSize, gameTime);
			saveScoreWindow.ShowDialog();
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

		public void UpdateScore()
		{
			Highscores.ItemsSource = scoreManager.GetTopScores(10);
		}

        
    }
}