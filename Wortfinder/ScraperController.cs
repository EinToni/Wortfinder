using System;
using System.Collections.Generic;
using System.Text;
using Wortfinder.Interfaces;

namespace Wortfinder
{
	class ScraperController : IScraperController
	{
		private readonly IWebScraper webScraper;
		public ScraperController(IWebScraper webScraper)
		{
			this.webScraper = webScraper;
		}

		public bool WordExist(string word)
		{
			return webScraper.SearchWordAsync(word);
		}
	}
}
