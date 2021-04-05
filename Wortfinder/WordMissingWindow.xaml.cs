using System.Windows;
using Wortfinder.Interfaces;

namespace Wortfinder
{
	/// <summary>
	/// Interaktionslogik für WordMissingWindow.xaml
	/// </summary>
	public partial class WordMissingWindow : Window, IWordMissingWindow
	{
		private readonly WebScraper scraper;
		public WordMissingWindow(WebScraper webScraper)
		{
			InitializeComponent();
			scraper = webScraper;
		}

		private async void ReportMissingWord(object sender, RoutedEventArgs e)
		{
			SuccessMessage.Content = "";
			ReportButton.IsEnabled = false;
			string word = ReportedWord.Text;
			bool wordExist = scraper.SearchWordAsync(word);
			if (wordExist)
			{
				SuccessMessage.Content = "Your word was found and added.";
			}
			else
			{
				SuccessMessage.Content = "Your word could not be found.";
				ReportButton.IsEnabled = true;
			}
		}
		public void ShowWindow()
		{
			ShowDialog();
		}
		public void HideWindow()
		{
			Hide();
		}
	}
}