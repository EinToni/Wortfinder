using System;
using System.Collections.Generic;
using System.Text;

namespace Wortfinder
{
	// Controlls the running game
	class GameController
	{
		private int score = 0;
		private List<string> foundWords = null;
		public GameController()
		{
			foundWords = new List<string>();
		}

		public void FoundCorrectWord(string word)
		{
			foundWords.Add(word);
			AddPoints(word.Length);
		}

		private void AddPoints(int wordLenth)
		{
			score += wordLenth - 3;
		}
	}
}
