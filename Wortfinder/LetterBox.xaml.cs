using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Wortfinder
{
	/// <summary>
	/// Interaktionslogik für LetterBox.xaml
	/// </summary>
	partial class LetterBox : UserControl
	{
		private readonly WordBuilder guessController = null;
		private readonly SolidColorBrush clickedColor = new SolidColorBrush(Color.FromRgb(255, 0, 100));
		private readonly SolidColorBrush unclickedColor = new SolidColorBrush(Color.FromRgb(100, 100, 100));

		public LetterBox(WordBuilder guessContr, int size, int circleSize, int row, int column, char letter)
		{
			InitializeComponent();
			DataContext = this;

			Size = size;
			CircleSize = circleSize;
			Row = row;
			Column = column;
			Letter = letter;
			Clicked = false;
			guessController = guessContr;
			Background.Fill = unclickedColor;
			
		}
		private int size;
		public int Size
		{
			get { return size; }
			set { size = value; CircleMargin = (int)((value - CircleSize) / 2); }
		}

		public bool Activated { get; set; }
		public int Row { get; set; }
		public int Column { get; set; }
		public int Test { get; set; }

		private int circleSize;
		public int CircleSize
		{
			get { return circleSize; }
			set { circleSize = value; CircleMargin = (int)((Size - value) / 2); }
		}

		public int CircleMargin { get; set; }

		private char letter;
		public char Letter
		{
			get { return letter; }
			set { letter = char.ToUpper(value); TextLetter.Text = char.ToUpper(value).ToString(); }
		}

		private bool Clicked { get; set; }

		private void ClickLetter(object sender, RoutedEventArgs e)
		{
			if (Activated && !Clicked && Mouse.LeftButton == MouseButtonState.Pressed)
			{
				Clicked = true;
				//clickedColor.Color = letterController.ClickLetter(Letter, Row, Column);
				Background.Fill = guessController.ClickLetter(Letter, Row, Column);
			}
		}
		
		public void MouseRelease()
		{
			Clicked = false;
			Background.Fill = unclickedColor;
		}
		public void MouseRelease(object sender, MouseButtonEventArgs e)
		{
			guessController.MouseRelease();
			Clicked = false;
			Background.Fill = unclickedColor;
		}
	}
}