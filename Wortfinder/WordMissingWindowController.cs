using System;
using System.Collections.Generic;
using System.Text;
using Wortfinder.Interfaces;

namespace Wortfinder
{
	public class WordMissingWindowController : IWordMissingWindowController
	{
		private readonly IWordMissingWindow window;
		public WordMissingWindowController(IWordMissingWindow wordMissingWindow)
		{
			window = wordMissingWindow;
		}

		public void OpenWindow()
		{
			window.ShowWindow();
		}

		public void SetCallback(Func<string, bool> func) => window.SetCallback(func);
	}
}
