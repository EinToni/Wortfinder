using System.Windows;

namespace Wortfinder
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		private void Application_Startup(object sender, StartupEventArgs e)
		{
			WordList				wordList				= new WordList();
			WordGenerator			wordGenerator			= new WordGenerator(wordList);
			LetterProbalilitys		letterProbalilitys		= new LetterProbalilitys();
			LetterGenerator			letterGenerator			= new LetterGenerator(letterProbalilitys);
			GameGenerator			gameGenerator			= new GameGenerator(wordGenerator, letterGenerator);
			GameDataController		gameDataController		= new GameDataController();
			GameLibrary				gameLibrary				= new GameLibrary(gameGenerator, gameDataController);
			GameScoreCalculator		gameScoreCalculator		= new GameScoreCalculator();
			GameScore				gameScore				= new GameScore(gameScoreCalculator);
			ScoreDataController		scoreDataController		= new ScoreDataController();
			ScoreWindowController	scoreWindowController	= new ScoreWindowController();
			ScoreManager			scoreManager			= new ScoreManager(scoreWindowController, scoreDataController);
			MainWindow				mainWindow				= new MainWindow();
			MainWindowController	mainWindowController	= new MainWindowController(mainWindow);
			GameManager				gameManager				= new GameManager(mainWindowController, scoreManager, gameLibrary, gameScore);


			
			mainWindowController.SetGameManager(gameManager);
			mainWindow.SetController(mainWindowController);
			mainWindow.Show();
		}
	}
}