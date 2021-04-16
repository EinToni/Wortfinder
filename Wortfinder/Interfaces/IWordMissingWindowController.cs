using System;
using System.Collections.Generic;
using System.Text;

namespace Wortfinder.Interfaces
{
	interface IWordMissingWindowController
	{
		void OpenWindow();
		void SetCallback(Func<string, bool> func);
	}
}
