using System;
using System.Collections.Generic;
using System.Text;

namespace Wortfinder
{
	public interface IMainWindow
	{
		void SetCurrentScore(string value);
		void SetFoundWordsAmount(string value);
		void SetFindableWordsAmount(string value);
		void SetTime(string value);
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
