using System.Windows;

namespace Wortfinder
{
	/// <summary>
	/// Interaktionslogik für WordMissingWindow.xaml
	/// </summary>
	public partial class WordMissingWindow : Window
	{

		public WordMissingWindow()
		{
			InitializeComponent();
			scraper = new WebScraper();
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
	}
}