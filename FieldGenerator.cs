using System;
using System.Windows;
using System.Windows.Controls;

namespace Wortfinder
{
	// Class to controll the field. Generate a variable sizes field and fill them with letters.
	internal class FieldGenerator
	{
		private readonly Grid letterGrid;
		private readonly GuessController guessController;
		private int fieldSize = 4;
		private bool newSize = false;

		public FieldGenerator(Grid grid, GuessController guessContr)
		{
			letterGrid = grid;
			guessController = guessContr;
		}

		public void InitializeField()
		{
			// Delete All Fields if any exist
			letterGrid.Children.Clear();
			letterGrid.RowDefinitions.Clear();
			letterGrid.ColumnDefinitions.Clear();
			//
			for (int i = 0; i < fieldSize; i++)
			{
				var rowDefinition = new RowDefinition();
				var columnDefinition = new ColumnDefinition();
				rowDefinition.Height = new GridLength(1, GridUnitType.Star);
				columnDefinition.Width = new GridLength(1, GridUnitType.Star);
				letterGrid.RowDefinitions.Add(rowDefinition);
				letterGrid.ColumnDefinitions.Add(columnDefinition);
				for (int j = 0; j < fieldSize; j++)
				{
					var letter = new LetterBox(guessController, 100, 50, j, i, '-');
					letterGrid.Children.Add(letter);
					Grid.SetRow(letter, j);
					Grid.SetColumn(letter, i);
				}
			}
			
		}

		public void NewLetters()
		{
			if (newSize) 
			{
				newSize = false;
				InitializeField();
			}

			//TODO: Shuffle findable Words.
			foreach (LetterBox letterBox in letterGrid.Children)
			{
				Random rnd = new Random();
				char letterChar = (char)rnd.Next(65, 90);
				letterBox.Letter = letterChar;
			}
		}

		public void SetFieldSize(int size)
		{
			if (fieldSize != size)
			{
				fieldSize = size;
				newSize = true;
			}
		}

		public void StopGame()
		{
			foreach(var letter in letterGrid.Children)
			{

			}
		}
	}
}