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
		private readonly Grid letterGrid = null;
		private readonly GuessController guessController = null;
		private List<Coordinate> coordinates = new List<Coordinate>();

		public WordBuilder(GuessController guessCtr, Grid grid)
		{
			letterGrid = grid;
			guessController = guessCtr;
		}
		
		internal SolidColorBrush ClickLetter(char letter, Coordinate coordinate)
		{
			if (coordinates.Count == 0 || coordinates[^1].IsNeighbour(coordinate))
			{
				wordName += letter.ToString();
				coordinates.Add(new Coordinate(coordinate));
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
			ResetBuilder();
			guessController.TryWord(word);
			
		}

		private void ResetBuilder()
		{
			wordName = "";
			coordinates.Clear();
		}
	}
}