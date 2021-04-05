using System;
using System.Collections.Generic;
using System.Text;
using Wortfinder.Interfaces;

namespace Wortfinder
{
	public class WordMissingController : IWordMissingController
	{
		private readonly IWordMissingWindow window;
		public WordMissingController(IWordMissingWindow wordMissingWindow)
		{
			window = wordMissingWindow;
		}
		public void Open()
		{
			window.ShowWindow();
		}
	}
}
