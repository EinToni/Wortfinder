﻿using System;
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
	public partial class MainWindow : INotifyPropertyChanged, IMainWindow
	{
		private MainWindowController mainWindowController;
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
		}
		public void SetController(MainWindowController mainWindowController)
		{
			this.mainWindowController = mainWindowController;
		}
		#region Databindings
		public event PropertyChangedEventHandler PropertyChanged;
		private void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
		public void SetTime(string value) => remainingTimeLabel.Content = value;
		public void SetFindableWordsAmount(string value) => amountOfWords.Content = value;
		public void SetFoundWordsAmount(string value) => amountOfFoundWords.Content = value;
		public void SetCurrentScore(string value) => OutputScore.Content = value;

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
				foreach (Viewbox v in (viewbox.Child as Grid).Children)
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
		public string GetTimeSelectedMinutes()
		{
			RadioButton gameTimeButton = GameTimeSelection.Children.OfType<RadioButton>().ToList().Where(r => r.IsChecked == true).Single();
			return gameTimeButton.Tag.ToString();
		}
		public string GetFieldSizeSelected()
        {
			RadioButton sizeRadioButton = FieldSizeSelection.Children.OfType<RadioButton>().ToList().Where(r => r.IsChecked == true).Single();
			return sizeRadioButton.Tag.ToString();
		}
		private void WordMissing(object sender, RoutedEventArgs e)
		{
			mainWindowController.WordMissing();
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
				int value = coordinate.PositionInGrid(fieldSize);
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
        public void AddWordToShow(Word word)
		{
			allWords.Children.Add(new WordDisplay(word, HoverWord, StopHoverWord));
		}
		public void SetWordsToShow(List<Word> words)
		{
			ClearWords();
			foreach (Word word in words)
            {
				AddWordToShow(word);
			}
		}
        public void SetBestScores(List<Score> itemSource) => Highscores.ItemsSource = itemSource;
	}
}