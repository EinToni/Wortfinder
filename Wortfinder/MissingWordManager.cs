using Wortfinder.Interfaces;

namespace Wortfinder
{
	class MissingWordManager : IMissingWordManager
	{
		private readonly IScraperController scraperController;
		private readonly IWordMissingWindowController windowController;
		public MissingWordManager(IScraperController scraperController, IWordMissingWindowController windowController)
		{
			this.scraperController = scraperController;
			this.windowController = windowController;
			windowController.SetCallback(WordExist);
		}

		public void OpenWindow()
		{
			windowController.OpenWindow();
		}
		public bool WordExist(string word)
		{
			return scraperController.WordExist(word);
		}
	}
}
