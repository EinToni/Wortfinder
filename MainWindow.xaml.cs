using System.Windows;

namespace Wortfinder
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private readonly FieldGenerator fieldGenerator;
		private readonly LetterController letterController;
		public MainWindow()
		{
			InitializeComponent();
			letterController = new LetterController(this, LetterGrid, OutputWord);
			fieldGenerator = new FieldGenerator(LetterGrid, letterController);
			fieldGenerator.InitializeField(4);
			//LetterGrid.ShowGridLines = true;
		}

		public void MouseRelease(object sender, RoutedEventArgs e) => letterController.MouseRelease();
		private void NewGame(object sender, RoutedEventArgs e) => fieldGenerator.NewLetters();
		public void AddPoints(int points)
		{
			OutputScore.Text = (int.Parse(OutputScore.Text.ToString()) + points).ToString();
		}

		private void WordMissing(object sender, RoutedEventArgs e)
		{
			WordMissingWindow wmw = new WordMissingWindow();
			wmw.Show();
		}
	}
}
