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
		private bool wordFound { get; set; } = false;
		private bool displayWord { get; set; } = false;
		public WordDisplay(Word word)
		{
			this.Word = word;
			InitializeComponent();
			this.NameLabel.Content = this.Word.Name;
		}
		public void WordGotFound()
		{
			this.wordFound = true;
			this.Background.Fill = new SolidColorBrush(Color.FromArgb(200, 0, 200, 0));
			this.Visibility = Visibility.Visible;
		}

		private void Grid_MouseEnter(object sender, MouseEventArgs e)
		{

		}
	}
}
