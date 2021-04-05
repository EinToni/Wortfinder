using System.Windows;
using Wortfinder.Interfaces;

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
			ILetterProbability		letterProbalilitys		= new LettersGerman();
			LetterGenerator			letterGenerator			= new LetterGenerator(letterProbalilitys);
			IGameGenerator			gameGenerator			= new GameGenerator(wordGenerator, letterGenerator);
			GameDataController		gameDataController		= new GameDataController();
			GameLibrary				gameLibrary				= new GameLibrary(gameGenerator, gameDataController);
			GameScoreCalculator		gameScoreCalculator		= new GameScoreCalculator();
			GameScore				gameScore				= new GameScore(gameScoreCalculator);
			ScoreDataController		scoreDataController		= new ScoreDataController();
			SaveScoreWindow			saveScoreWindow			= new SaveScoreWindow();
			ScoreWindowController	scoreWindowController	= new ScoreWindowController(saveScoreWindow);
			ScoreManager			scoreManager			= new ScoreManager(scoreWindowController, scoreDataController);
			MainWindow				mainWindow				= new MainWindow();
			WebScraper				webScraper				= new WebScraper();
			IWordMissingWindow		wordMissingWindow		= new WordMissingWindow(webScraper);
			IWordMissingController	wordMissingController	= new WordMissingController(wordMissingWindow);
			MainWindowController	mainWindowController	= new MainWindowController(mainWindow, wordMissingController);
			GameTimer				gameTimer				= new GameTimer(new System.Windows.Threading.DispatcherTimer());
			GameManager				gameManager				= new GameManager(mainWindowController, scoreManager, gameLibrary, gameScore, gameTimer);

			mainWindowController.SetGameManager(gameManager);
			mainWindow.SetController(mainWindowController);
			mainWindow.Show();
		}
	}
}