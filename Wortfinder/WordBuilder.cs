using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;

namespace Wortfinder
{
	// Class to manage all letters that get connected and the resulting word.
	public class WordBuilder
	{
		private string wordName = "";
		private int lastRow = -1;
		private int lastColumn = -1;
		private readonly Label outputTextBox = null;
		private readonly Grid letterGrid = null;
		private readonly GuessController guessController = null;
		private List<int[]> coordinates = new List<int[]>();

		public WordBuilder(GuessController guessCtr, Grid grid)
		{
			letterGrid = grid;
			guessController = guessCtr;
		}

		internal SolidColorBrush ClickLetter(char letter, int row, int column)
		{
			if (Math.Abs(lastRow - row) <= 1 && Math.Abs(lastColumn - column) <= 1 || lastRow == -1 && lastColumn == -1)
			{
				wordName += letter.ToString();
				lastRow = row;
				lastColumn = column;
				coordinates.Add(new int[] { row, column });
			}
			else
			{
				MouseRelease();
			}
			return new SolidColorBrush(Color.FromRgb((byte)(100 + (7 * wordName.Length)), 0, 0));
		}

		internal void MouseRelease()
		{
			foreach (LetterBox child in letterGrid.Children)
			{
				child.MouseRelease();
			}
			Word word = new Word(wordName, coordinates);
			guessController.TryWord(word);
			wordName = "";
			coordinates.Clear();
			lastRow = -1;
			lastColumn = -1;
		}
	}
}