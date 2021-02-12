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
		private readonly Func<Word, bool> hover;
		private readonly Func<bool> stopHover;
		public WordDisplay(Word word, Func<Word,bool> hover, Func<bool> stopHover)
		{
			this.Word = new Word(word);
			InitializeComponent();
			this.NameLabel.Content = this.Word.Name;
			this.hover = hover;
			this.stopHover = stopHover;
			Visibility = Visibility.Visible;
			if (Word.Found)
            {
				Background.Fill = new SolidColorBrush(Color.FromArgb(200, 0, 200, 0));
			}
		}
		private void MouseHovers(object sender, MouseEventArgs e)
		{
			hover(Word);
		}

		private void MouseStopHover(object sender, MouseEventArgs e)
		{
			stopHover();
		}
	}
}
