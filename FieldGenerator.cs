using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Wortfinder
{
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
					Random rnd = new Random();

					char letterChar = (char)rnd.Next(65, 90);
					var letter = new LetterBox(letterController, 100, 50, j, i, letterChar);
					letterGrid.Children.Add(letter);
					Grid.SetRow(letter, j);
					Grid.SetColumn(letter, i);
				}
			}
		}
	}
}
