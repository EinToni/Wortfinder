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
		private readonly int score;
		private readonly int fieldSize;
		private readonly int gameTimeInMintes;
		private readonly Func<int, int, int, string, bool> saveFunction;

		public SaveScoreWindow(Func<int, int, int, string, bool> saveFunction, int score, int fieldSize, int gameTime)
		{
			this.saveFunction = saveFunction;
			this.score = score;
			this.fieldSize = fieldSize;
			this.gameTimeInMintes = gameTime;
			InitializeComponent();
			ScoreDisplay.Content = score;
		}

		private void SaveScore(object sender, RoutedEventArgs e)
		{
			saveFunction(score, fieldSize, gameTimeInMintes, NameInput.Text.ToString());
			Close();
		}

		private void CloseWindow(object sender, RoutedEventArgs e)
		{
			Close();
		}
	}
}
