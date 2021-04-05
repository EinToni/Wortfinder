using System;
using System.Net.Http;

namespace Wortfinder
{
	public class WebScraper
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
			if (result.Contains("liefert keine Ergebnisse. Wir haben stattdessen nach") || result.Contains("Leider gibt es für Ihre Suchanfrage im"))
			{
				return false;
			}
			string completeFile = result.Substring(result.IndexOf("<main"));
			while (true)
			{
				try
				{
					completeFile = completeFile.Substring(1);
					completeFile = completeFile.Substring(completeFile.IndexOf(@"<a class=""vignette__label"""));
					string textRegion = completeFile.Substring(0, completeFile.IndexOf("</a>"));
					if (textRegion.Contains(word))
					{
						return true;
					}
				}
				catch (Exception)
				{
					throw new Exception();
				}
			}
		}
	}
}