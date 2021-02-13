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
	public partial class SaveScoreWindow : Window
	{
		private readonly Func<bool, string, bool> saveScore;
		public SaveScoreWindow(Func<bool, string, bool> saveScore, string score)
		{
			InitializeComponent();
			ScoreDisplay.Content = score;
			this.saveScore = saveScore;
		}

		private void SaveScore(object sender, RoutedEventArgs e)
		{
			saveScore(true, NameInput.Text);
			Close();
		}

		private void CloseWindow(object sender, RoutedEventArgs e)
		{
			saveScore(false, NameInput.Text);
			Close();
		}
	}
}
