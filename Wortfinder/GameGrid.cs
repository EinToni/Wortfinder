using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace Wortfinder
{
	public class GameGrid
	{

		private readonly Grid grid;
		private readonly LetterGenerator letterGenerator;
		private readonly GuessController guessController;
		private readonly FieldGenerator fieldGenerator;

		public int FieldSize { get; private set; }
		public char[] Letters { get; private set; }

		public GameGrid(DataController dataCtr, GameScore score, FindableWords findableWords, Grid letterGrid)
		{
			grid = letterGrid;
			letterGenerator = new LetterGenerator();
			guessController = new GuessController(dataCtr, findableWords, score);
			fieldGenerator = new FieldGenerator(guessController, letterGrid);
		}

		public void NewGrid(int fieldSize)
		{
			FieldSize = fieldSize;
			Letters = letterGenerator.GetNewLetters(fieldSize);
			guessController.LoadAllFindableWords(this);
			fieldGenerator.NewGameField(this);
			//mainWindow.ActivateAllLetters();
		}

		internal void DeactivateAllLetter()
		{
			foreach (LetterBox letter in grid.Children)
			{
				letter.Activated = false;
			}
		}

		internal void StopDisplayWord()
		{
			foreach (LetterBox letterBox in grid.Children)
			{
				letterBox.StopDisplay();
			}
		}

		internal void DisplayWord(Word word, bool found)
		{
			int index = 0;
			foreach(Coordinate coordinate in word.Coordinates)
			{
				foreach(LetterBox letterBox in grid.Children)
				{
					if(letterBox.coordinate.Equals(coordinate))
					{
						if (found)
						{
							letterBox.DisplayFound(index);
						} else
						{
							letterBox.DisplayNotFound(index);
						}
					}
				}
				index += 1;
			}
		}
	}
}
