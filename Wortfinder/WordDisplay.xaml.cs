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
	/// Interaktionslogik für WordDisplay.xaml
	/// </summary>
	public partial class WordDisplay : UserControl
	{
		public Word Word { get; }
		public bool Found { get; private set; } = false;
		private GameGrid gameGrid;
		public WordDisplay(GameGrid gameGrid, Word word)
		{
			this.Word = new Word(word);
			InitializeComponent();
			this.NameLabel.Content = this.Word.Name;
			this.gameGrid = gameGrid;
		}

		public void WordGotFound()
		{
			Found = true;
			Background.Fill = new SolidColorBrush(Color.FromArgb(200, 0, 200, 0));
			ShowWord();
		}

		public void ShowWord()
		{
			Visibility = Visibility.Visible;
		}

		private void MouseHovers(object sender, MouseEventArgs e)
		{
			gameGrid.DisplayWord(Word, Found);
		}

		private void MouseStopHover(object sender, MouseEventArgs e)
		{
			gameGrid.StopDisplayWord();
		}
	}
}
