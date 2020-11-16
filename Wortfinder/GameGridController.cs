using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace Wortfinder
{
	public class GameGridController
	{
		private readonly Grid gameGrid;
		private readonly LetterGenerator letterGenerator;
		private readonly GuessController guessController;
		private readonly FieldGenerator fieldGenerator;

		public event EventHandler<Word> DisplayWordEvent;

		public GameGridController(Grid gameGrid, DataController dataController, GameController gameController)
		{
			/*
			this.gameGrid = gameGrid;
			DisplayWordEvent += DisplayWord;
			letterGenerator = new LetterGenerator();
			guessController = new GuessController(gameController, dataController);
			fieldGenerator = new FieldGenerator(guessController, gameGrid);
			*/
		}

		public void DisplayWord(object sender, Word word)
		{

		}

		internal void GenerateNewGame(GameGrid gameGrid)
		{
			/*
			guessController.LoadAllFindableWords(gameGrid);
			fieldGenerator.NewGameField(gameGrid);
			*/
		}
	}
}
