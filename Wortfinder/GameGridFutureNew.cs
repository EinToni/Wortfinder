using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace Wortfinder
{
	public class GameGridFutureNew
	{
		private readonly Grid grid;
		private readonly LetterGenerator letterGenerator;
		private readonly GuessController guessController;
		private readonly FieldGenerator fieldGenerator;

		public GameGridFutureNew(Grid letterGrid)
		{
			grid = letterGrid;
			letterGenerator = new LetterGenerator();
			//guessController = new GuessController();
			//fieldGenerator = new FieldGenerator();
		}

		public void NewGrid(int fieldSize)
		{
			char[] letters = letterGenerator.GetNewLetters(fieldSize);
			GameGrid gameGrid = new GameGrid(fieldSize, letters);
			guessController.LoadAllFindableWords(gameGrid);
			fieldGenerator.NewGameField(gameGrid);
			//mainWindow.ActivateAllLetters();
		}
	}
}
