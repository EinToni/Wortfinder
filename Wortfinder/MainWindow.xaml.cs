using System.Windows;
using System.Windows.Controls;

namespace Wortfinder
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private readonly GameController		gameController = null;

		public MainWindow()
		{
			InitializeComponent();
			gameController = new GameController(this, LetterGrid);
			gameController.Time = 180;
			gameController.FieldSize = 4;

			//wordController.CheckAllWords(test);
			
			
			
			//gameTimer.SetTimeoutFunc(guessController.TimeOver);
			//LetterGrid.ShowGridLines = true;
		}

		public void MouseRelease(object sender, RoutedEventArgs e) => gameController.MouseRelease();

		private void NewGame(object sender, RoutedEventArgs e) => gameController.NewGame();

		public void SetPoints(int points)
		{
			OutputScore.Text = points.ToString();
		}

		private void WordMissing(object sender, RoutedEventArgs e)
		{
			WordMissingWindow wmw = new WordMissingWindow();
			wmw.Show();
		}

		private void SetGameTime(object sender, RoutedEventArgs e)
		{
			RadioButton radioButton = sender as RadioButton;
			if (gameController != null)
			{
				gameController.Time = int.Parse(radioButton.Tag.ToString()) * 60;
			}
		}

		private void SetFieldSize(object sender, RoutedEventArgs e)
		{
			RadioButton radioButton = sender as RadioButton;
			if (gameController != null)
			{
				gameController.FieldSize = int.Parse(radioButton.Tag.ToString());
			}
		}
	}
}