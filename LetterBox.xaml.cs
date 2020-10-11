using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Wortfinder
{
	/// <summary>
	/// Interaktionslogik für LetterBox.xaml
	/// </summary>
	public partial class LetterBox : UserControl
	{
		private readonly LetterController letterController = null;
		private readonly SolidColorBrush clickedColor = new SolidColorBrush(Color.FromRgb(255, 0, 255));
		private readonly SolidColorBrush unclickedColor = new SolidColorBrush(Color.FromRgb(100, 100, 100));
		public LetterBox(LetterController letterContr, int size, int circleSize, int row, int column, char letter)
		{
			InitializeComponent();
			DataContext = this;

			Size = size;
			CircleSize = circleSize;
			Row = row;
			Column = column;
			Letter = letter;
			Clicked = false;
			letterController = letterContr;
			Background.Fill = unclickedColor;
		}
		private int size;
		public int Size
		{
			get { return size; }
			set { size = value; CircleMargin = (int)((value - CircleSize) / 2); }
		}
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
			if (!Clicked)
			{
				Clicked = true;
				letterController.ClickLetter(Letter, Row, Column);
				Background.Fill = clickedColor;
			}

		}
		private void HoverLetter(object sender, RoutedEventArgs e)
		{
			if (!Clicked && Mouse.LeftButton == MouseButtonState.Pressed)
			{
				Clicked = true;
				letterController.ClickLetter(Letter, Row, Column);
				Background.Fill = clickedColor;
			}
		}
		public void MouseRelease()
		{
			Clicked = false;
			Background.Fill = unclickedColor;
		}
	}
}
