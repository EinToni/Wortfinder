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
	/// Interaktionslogik für SaveScoreWindow.xaml
	/// </summary>
	public partial class SaveScoreWindow : Window, IScoreWindow
	{
		private Func<bool, string, bool> saveScore;
		public SaveScoreWindow()
		{
			InitializeComponent();
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
