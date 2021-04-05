using System;
using System.Collections.Generic;
using System.Text;

namespace Wortfinder
{
	public interface IScoreWindow
	{
		void SetScore(int score);
		void SetCallback(Func<bool, string, bool> setReturn);
		void ShowWindow();
		void HideDialog();
	}
}
