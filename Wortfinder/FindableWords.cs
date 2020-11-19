using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Wortfinder
{
	public class FindableWords
	{
		private readonly WrapPanel panel;
		private readonly MainWindow mainWindow;


		public FindableWords(MainWindow mainWindow, WrapPanel allWords)
		{
			panel = allWords;
			this.mainWindow = mainWindow;
		}

		public void ClearAllWords()
		{
			panel.Children.Clear();
			mainWindow.amountOfFoundWords.Content = 0;
		}

		public void ShowAllWords()
		{
			foreach(WordDisplay displayableWord in panel.Children)
			{
				displayableWord.ShowWord();
			}
		}

		public void AddNewWord(Word word)
		{
			mainWindow.AddWordToList(word);
		}

		public void WordFound(Word word)
		{
			foreach (WordDisplay wordDisplay in panel.Children)
			{
				if (wordDisplay.Word.Name.Equals(word.Name))
				{
					wordDisplay.WordGotFound();
					mainWindow.amountOfFoundWords.Content = int.Parse(mainWindow.amountOfFoundWords.Content.ToString()) + 1;
					break;
				}
			}
			
		}

		internal void ShowWord(Word word)
		{
			foreach(WordDisplay wordDisplay in panel.Children)
			{
				if (wordDisplay.Word.Name.Equals(word.Name))
				{
					wordDisplay.ShowWord();
					break;
				}
			}
		}
	}
}
