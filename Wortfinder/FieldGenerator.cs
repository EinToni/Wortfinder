﻿using System;
using System.Windows;
using System.Windows.Controls;

namespace Wortfinder
{
	// Class to controll the field. Generate a variable sizes field and fill them with letters.
	internal class FieldGenerator
	{
		private readonly Grid letterGrid;
		private readonly WordBuilder wordBuilder;
		private int fieldSize = 4;

		public FieldGenerator(GuessController guessCtr, Grid grid)
		{
			letterGrid = grid;
			wordBuilder = new WordBuilder(guessCtr, letterGrid);
		}

		public void NewGameField(GameGrid gameGrid)
		{
			InitializeField(gameGrid.FieldSize);
			NewLetters(gameGrid.Letters);
		}

		public void InitializeField(int fieldSize)
		{
			// Delete All Fields if any exist
			letterGrid.Children.Clear();
			letterGrid.RowDefinitions.Clear();
			letterGrid.ColumnDefinitions.Clear();
			//
			for (int row = 0; row < fieldSize; row++)
			{
				var rowDefinition = new RowDefinition();
				var columnDefinition = new ColumnDefinition();
				rowDefinition.Height = new GridLength(1, GridUnitType.Star);
				columnDefinition.Width = new GridLength(1, GridUnitType.Star);
				letterGrid.RowDefinitions.Add(rowDefinition);
				letterGrid.ColumnDefinitions.Add(columnDefinition);
				for (int column = 0; column < fieldSize; column++)
				{
					var letter = new LetterBox(wordBuilder, 100, 50, row, column, '-');
					letterGrid.Children.Add(letter);
					Grid.SetRow(letter, row);
					Grid.SetColumn(letter, column);
				}
			}
			
		}

		public void NewLetters(char[] letters)
		{
			if(letters.Length != letterGrid.Children.Count)
			{
				throw new ArgumentException("Letter count does not match the amount of fields.");
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