using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using System.IO;
using System.Net.Http;
using System.Threading;

namespace Wortfinder
{
	class WebScraper
	{
		public WebScraper()
		{

		}

		public bool SearchWordAsync(string word)
		{
			HttpClient httpClient = new HttpClient();
			var request = httpClient.GetStringAsync("https://duden.de/suchen/dudenonline/" + word);
			request.Wait();
			string result = request.Result;
			if (result.Contains("liefert keine Ergebnisse. Wir haben stattdessen nach"))
			{
				return false;
			}
			string startOfList = result.Substring(result.IndexOf("<main"));

			string completeFile = startOfList.Substring(startOfList.IndexOf(@"<a class=""vignette__label"""));
			string textRegion = completeFile.Substring(0, completeFile.IndexOf("</a>"));
			if (textRegion.Contains(word))
			{
				return true;
			}
			return false;
		}


	}
}
