using System;
using System.Collections.Generic;
using System.Text;

namespace Wortfinder
{
	public interface IMainWindow
	{
		string Time { get; set; }
		string ActualScore { get; set; }
		string FindableWordsAmount { get; set; }
		string FoundWordsAmount { get; set; }

		void DeselectAllLetters();
		void SetGameField(int size, char[] letters);
		void AddWordToShow(Word word);
		void SetWordsToShow(List<Word> words);
		void ClearWords();
		void LettersActive();
		void LettersInactive();
		void SetBestScores(List<Score> scores);
	}
}
