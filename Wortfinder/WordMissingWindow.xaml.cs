using System;
using System.Windows;
using Wortfinder.Interfaces;

namespace Wortfinder
{
	/// <summary>
	/// Interaktionslogik für WordMissingWindow.xaml
	/// </summary>
	public partial class WordMissingWindow : Window, IWordMissingWindow
	{
		private Func<string, bool> reportWord;
		public WordMissingWindow()
		{
			InitializeComponent();
		}
		private async void ReportMissingWord(object sender, RoutedEventArgs e)
		{
			ReportButton.IsEnabled = false;
			string word = ReportedWord.Text;
			bool wordExist = reportWord(word);
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

		public void SetCallback(Func<string,bool> func)
		{
			reportWord = func;
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