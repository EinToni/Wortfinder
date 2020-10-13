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

		public void NewLetters(char[] letters)
		{
			if (newSize) 
			{
				newSize = false;
				InitializeField();
			}

			if(letters.Length != letterGrid.Children.Count)
			{
				throw new System.ArgumentException("Letter count does not match the amount of fields.");
			}
			else
			{
				int i = 0;
				foreach (LetterBox letterBox in letterGrid.Children)
				{
					letterBox.Letter = letters[i];
					i++;
				}
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