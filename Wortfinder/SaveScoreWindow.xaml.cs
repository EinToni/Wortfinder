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
		private readonly ScoreManager scoreManager;
		private readonly MainWindow mainWindow;
		private readonly int score;
		private readonly int fieldSize;
		private readonly int gameTimeInMintes;

		public SaveScoreWindow(MainWindow mainWindow, ScoreManager scoreManager, int score, int fieldSize, int gameTime)
		{
			this.mainWindow = mainWindow;
			this.scoreManager = scoreManager;
			this.score = score;
			this.fieldSize = fieldSize;
			this.gameTimeInMintes = gameTime;
			InitializeComponent();
			ScoreDisplay.Content = score;
		}

		private void SaveScore(object sender, RoutedEventArgs e)
		{
			scoreManager.AddScore(score, fieldSize, gameTimeInMintes, NameInput.Text.ToString());
			mainWindow.UpdateScore();
			Close();
		}

		private void CloseWindow(object sender, RoutedEventArgs e)
		{
			Close();
		}
	}
}
