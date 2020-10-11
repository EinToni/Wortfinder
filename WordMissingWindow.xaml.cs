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
using System.Windows.Shapes;

namespace Wortfinder
{
	/// <summary>
	/// Interaktionslogik für WordMissingWindow.xaml
	/// </summary>
	public partial class WordMissingWindow : Window
	{
		private readonly WebScraper scraper = null;
		public WordMissingWindow()
		{
			InitializeComponent();
			scraper = new WebScraper();
		}

		private async void ReportMissingWord(object sender, RoutedEventArgs e)
		{
			string word = ReportedWord.Text;
			bool wordExist = scraper.SearchWordAsync(word);
			if (wordExist)
			{
				SuccessMessage.Opacity = 1.0;
			}
		}
	}
}
