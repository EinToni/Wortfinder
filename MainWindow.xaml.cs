using System.Windows;
using System.Windows.Controls;

namespace Wortfinder
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private readonly FieldGenerator fieldGenerator;
		private readonly GuessController guessController;
		private readonly DataController dataController;
		private readonly WordController wordController;
		private readonly GameTimer gameTimer;

		public MainWindow()
		{
			InitializeComponent();
			gameTimer		= new GameTimer();
			dataController	= new DataController();
			wordController	= new WordController(dataController);
			guessController = new GuessController(this, dataController, LetterGrid, OutputWord);
			fieldGenerator	= new FieldGenerator(LetterGrid, guessController);

			char[,] test = new char[,] { { 'A', 'B', 'C' }, { 'F', 'B', 'C' }, { 'F', 'E', 'C' } };
			//wordController.CheckAllWords(test);
			
			fieldGenerator.InitializeField();
			gameTimer.SetDisplayFunc(DisplayTime);
			//gameTimer.SetTimeoutFunc(guessController.TimeOver);
			//LetterGrid.ShowGridLines = true;
		}

		public void MouseRelease(object sender, RoutedEventArgs e) => guessController.MouseRelease();

		private void NewGame(object sender, RoutedEventArgs e)
		{
			fieldGenerator.NewLetters();
			gameTimer.StartTimer();
		}

		public void AddPoints(int points)
		{
			OutputScore.Text = (int.Parse(OutputScore.Text.ToString()) + points).ToString();
		}

		private void WordMissing(object sender, RoutedEventArgs e)
		{
			WordMissingWindow wmw = new WordMissingWindow();
			wmw.Show();
		}

		private bool DisplayTime(string time)
		{
			remainingTime.Text = time;
			return true;
		}

		private void SetGameTime(object sender, RoutedEventArgs e)
		{
			if (gameTimer != null)
			{
				RadioButton radioButton = sender as RadioButton;
				gameTimer.SetTime(int.Parse(radioButton.Tag.ToString())*60);
			}
		}

		private void SetFieldSize(object sender, RoutedEventArgs e)
		{
			if (fieldGenerator != null)
			{
				RadioButton radioButton = sender as RadioButton;
				fieldGenerator.SetFieldSize(int.Parse(radioButton.Tag.ToString()));
			}
		}
	}
}