using System;
using System.ComponentModel;
using System.Windows;

namespace Wortfinder
{
	/// <summary>
	/// Interaktionslogik für SaveScoreWindow.xaml
	/// </summary>
	public partial class SaveScoreWindow : Window, IScoreWindow
	{
		private Func<bool, string, bool> saveScore;
		public SaveScoreWindow()
		{
			InitializeComponent();
			Closing += OnWindowClosing;
		}

		public void SetCallback(Func<bool, string, bool> saveScore)
		{
			this.saveScore = saveScore;
		}
		public void SetScore(int score)
		{
			ScoreDisplay.Content = score;
		}
		public void ShowWindow()
		{
			ShowDialog();
		}
		public void OnWindowClosing(object sender, CancelEventArgs e)
		{
			HideDialog();
		}
		public void HideDialog()
		{
			Hide();
		}
		private void SaveScore(object sender, RoutedEventArgs e)
		{
			saveScore(true, NameInput.Text);
			Hide();
		}

		private void CloseWindow(object sender, RoutedEventArgs e)
		{
			saveScore(false, NameInput.Text);
			Hide();
		}
	}
}
