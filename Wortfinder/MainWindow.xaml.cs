using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
	public partial class MainWindow : INotifyPropertyChanged
	{
		private readonly MainWindowController mainWindowController;
        #region Colors
        private readonly Brush unselectedLetter = new SolidColorBrush(Colors.Transparent);
		private readonly Brush selectedLetter	= new SolidColorBrush(Color.FromArgb(120,   0,  50, 180));
		private readonly Brush foundLetter		= new SolidColorBrush(Color.FromArgb(120,   0,  50,   0));
		private readonly Brush notFoundLetter	= new SolidColorBrush(Color.FromArgb(120,  50,   0,   0));
		private readonly Brush gameInactive		= new SolidColorBrush(Color.FromArgb(180, 150, 150, 150));
		private readonly Brush gameActive		= new SolidColorBrush(Color.FromArgb( 30, 150, 150, 150));
		#endregion
		public MainWindow()
		{
			InitializeComponent();
			LettersInactive();
			DataContext = this;
			mainWindowController = new MainWindowController(this);
		}
		#region Databindings
		private string time = "";
		public string Time
        {
            get => time;
            set { time = value; OnPropertyChanged(); }
        }
        private string findableWordsAmount = "";
		public string FindableWordsAmount
        {
            get => findableWordsAmount;
            set { findableWordsAmount = value; OnPropertyChanged(); }
        }
        private string foundWordsAmount = "";
		public string FoundWordsAmount
        {
            get => foundWordsAmount;
            set { foundWordsAmount = value; OnPropertyChanged(); }
        }
        private string actualScore = "";
		public string ActualScore
        {
            get => actualScore;
            set { actualScore = value; OnPropertyChanged(); }
        }
		private string mainButton = "Start Game";
		public string MainButton
		{
			get => mainButton;
			set { mainButton = value; OnPropertyChanged(); }
		}
		#endregion
		
		public void SetGameField(int size, char[] letters)
        {
			SetField(size);
			SetLetters(size, letters);
		}
        #region BuildLetterGridUiElements
        private void SetField(int size)
        {
			LetterGrid.RowDefinitions.Clear();
			LetterGrid.ColumnDefinitions.Clear();
			LetterGrid.Children.Clear();
			for (int i = 0; i < size; i++)
			{
				GridLength format = new GridLength(1, GridUnitType.Star);
                LetterGrid.RowDefinitions.Add(new RowDefinition() { Height = format });
                LetterGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = format });
			}
		}
		private void SetLetters(int size, char[] letters)
        {
			for (int i = 0; i < letters.Length; i++)
			{
				Viewbox letter = CreateUiLetter(i, size, letters[i].ToString());
				LetterGrid.Children.Add(letter);
				Grid.SetRow(letter, i / size);
				Grid.SetColumn(letter, (i % size));
			}
		}
		private Viewbox CreateUiLetter(int position, int size, string letter)
        {
			Viewbox mainBox = new Viewbox();
			Grid boxGrid = new Grid();
			Viewbox letterBox = new Viewbox()
			{
				Name = "letterbox",
				Child = new TextBlock() { Text = letter }
			};
			Viewbox hitBox = new Viewbox() { Name = "hitbox" };
			Ellipse hitCircle = new Ellipse()
			{
				Name = (letter + "_" + (position / size).ToString() + "_" + ((position % size)).ToString()).ToString(),
				Width = 100,
				Height = 100,
				Stroke = unselectedLetter,
				Fill = unselectedLetter
			};
			hitCircle.PreviewMouseDown += new MouseButtonEventHandler(ClickLetter);
			hitCircle.MouseEnter += new MouseEventHandler(HoverLetter);

			hitBox.Child = hitCircle;

			boxGrid.Children.Add(letterBox);
			boxGrid.Children.Add(hitBox);
			mainBox.Child = boxGrid;
			return mainBox;
		}
        #endregion BuildLetterGridUiElements
        public void ColorLetter(Ellipse ellipse) => ellipse.Fill = selectedLetter;
        private void ClickLetter(object sender, MouseButtonEventArgs e)
		{
			Ellipse ellipse = sender as Ellipse;
			string[] parts = ellipse.Name.Split('_');
			if (mainWindowController.ClickLetter(parts[0], parts[1], parts[2]))
            {
				ColorLetter(ellipse);
            }
		}
		private void HoverLetter(object sender, MouseEventArgs e)
		{
			Ellipse ellipse = sender as Ellipse;
			string[] parts = ellipse.Name.Split('_');
			if (mainWindowController.HoverLetter(parts[0], parts[1], parts[2]))
			{
				ColorLetter(ellipse);
            }
		}
        private void ReleaseMouse(object sender, MouseButtonEventArgs e) => mainWindowController.ReleaseMouse();
        public void DeselectAllLetters()
        {
			foreach (Viewbox viewbox in LetterGrid.Children)
			{
				Grid grid = viewbox.Child as Grid;
				foreach (Viewbox v in grid.Children)
				{
					if (v.Name.Equals("hitbox"))
					{
						Ellipse ellipse = v.Child as Ellipse;
						ellipse.Fill = unselectedLetter;
						ellipse.Stroke = unselectedLetter;
						break;
					}
				}
			}
		}
        private void NewGame(object sender, RoutedEventArgs e) => mainWindowController.NewGame(GetFieldSizeSelected(), GetTimeSelectedMinutes());

		public bool StopGame()
		{
			mainWindowController.StopGame();
			return true;
		}
		public int GetTimeSelectedMinutes()
		{
			RadioButton gameTimeButton = GameTimeSelection.Children.OfType<RadioButton>().ToList().Where(r => r.IsChecked == true).Single();
			return int.Parse(gameTimeButton.Tag.ToString());
		}
		public int GetFieldSizeSelected()
        {
			RadioButton sizeRadioButton = FieldSizeSelection.Children.OfType<RadioButton>().ToList().Where(r => r.IsChecked == true).Single();
			return int.Parse(sizeRadioButton.Tag.ToString());
		}
		private void WordMissing(object sender, RoutedEventArgs e)
		{
			WordMissingWindow wmw = new WordMissingWindow();
			wmw.Show();
		}
		private bool HoverWord(Word word)
        {
			mainWindowController.ReleaseMouse();
			int fieldSize = mainWindowController.GetFieldSize();
			Brush brush = notFoundLetter;
			if (word.Found)
            {
				brush = foundLetter;
			}
			foreach (Coordinate coordinate in word.Coordinates)
            {
				int value = coordinate.Row * fieldSize + coordinate.Column;
				Grid grid = (LetterGrid.Children[value] as Viewbox).Child as Grid;
				foreach (Viewbox v in grid.Children)
				{
					if (v.Name.Equals("hitbox"))
					{
						Ellipse ellipse = v.Child as Ellipse;
						ellipse.Fill = brush;
						ellipse.Stroke = brush;
						break;
					}
				}
			}
			return true;
        }
		private bool StopHoverWord()
		{
			DeselectAllLetters();
			return true;
		}
        public void LettersActive() => LetterGrid.Background = gameActive;
        public void LettersInactive() => LetterGrid.Background = gameInactive;
        public void ClearWords() => allWords.Children.Clear();
        public void AddFoundWord(Word word)
		{
			WordDisplay wd = new WordDisplay(word, HoverWord, StopHoverWord);
			wd.WordGotFound();
			allWords.Children.Add(wd);
		}
		public void ShowWords(List<Word> words)
		{
			foreach (Word word in words)
			{
				AddFoundWord(word);
			}
		}
        public void SetBestScores(List<Score> itemSource) => Highscores.ItemsSource = itemSource;

        public event PropertyChangedEventHandler PropertyChanged;
		private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
	}
}