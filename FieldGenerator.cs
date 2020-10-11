using System;
using System.Windows;
using System.Windows.Controls;

namespace Wortfinder
{
	// Class to controll the field. Generate a variable sizes field and fill them with letters.
	class FieldGenerator
	{
		private readonly Grid letterGrid;
		private readonly LetterController letterController;
		public FieldGenerator(Grid grid, LetterController letterContr)
		{
			letterGrid = grid;
			letterController = letterContr;
		}

		public void InitializeField(int size)
		{
			// Delete All Fields if any exist
			letterGrid.Children.Clear();
			// 
			for (int i = 0; i < size; i++)
			{
				var rowDefinition = new RowDefinition();
				var columnDefinition = new ColumnDefinition();
				rowDefinition.Height = new GridLength(1, GridUnitType.Star);
				columnDefinition.Width = new GridLength(1, GridUnitType.Star);
				letterGrid.RowDefinitions.Add(rowDefinition);
				letterGrid.ColumnDefinitions.Add(columnDefinition);
				for (int j = 0; j < size; j++)
				{
					var letter = new LetterBox(letterController, 100, 50, j, i, '-');
					letterGrid.Children.Add(letter);
					Grid.SetRow(letter, j);
					Grid.SetColumn(letter, i);
				}
			}
			NewLetters();
		}

		public void NewLetters()
		{
			//TODO: Shuffle findable Words.
			foreach(LetterBox letterBox in letterGrid.Children)
			{
				Random rnd = new Random();
				char letterChar = (char)rnd.Next(65, 90);
				letterBox.Letter = letterChar;
			}
		}
	}
}
