using System;
using System.Collections.Generic;
using System.Text;

namespace Wortfinder.Interfaces
{
	interface IWebScraper
	{
		bool SearchWordAsync(string word);
	}
}
