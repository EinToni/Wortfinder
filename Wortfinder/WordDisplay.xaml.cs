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
		private bool WordFound { get; set; } = false;
		private bool DisplayWord { get; set; } = false;
		public WordDisplay(Word word)
		{
			this.Word = word;
			InitializeComponent();
			this.NameLabel.Content = this.Word.Name;
		}

		public void WordGotFound()
		{
			WordFound = true;
			Background.Fill = new SolidColorBrush(Color.FromArgb(200, 0, 200, 0));
			ShowWord();
		}

		public void ShowWord()
		{
			Visibility = Visibility.Visible;
		}

		private void MouseHovers(object sender, MouseEventArgs e)
		{

		}
	}
}
