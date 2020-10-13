using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;

namespace Wortfinder
{
	// Class to manage all letters that get connected and the resulting word.
	public class GuessController
	{
		private string word = "";
		private int lastRow = -1;
		private int lastColumn = -1;
		private TextBox outputTextBox = null;
		private List<string> guessedWords;
		private readonly Grid letterGrid = null;
		private readonly WordController wordController = null;
		private readonly DataController dataController = null;
		private readonly MainWindow mainWindow = null;
		private readonly GameController gameController = null;

		public GuessController(MainWindow main, DataController dataCtr, Grid grid, TextBox textBox)
		{
			outputTextBox = textBox;
			letterGrid = grid;
			dataController = dataCtr;
			wordController = new WordController(dataController);
			mainWindow = main;
			guessedWords = new List<string>();
		}

		internal SolidColorBrush ClickLetter(char letter, int row, int column)
		{
			if (Math.Abs(lastRow - row) <= 1 && Math.Abs(lastColumn - column) <= 1 || lastRow == -1 && lastColumn == -1)
			{
				word += letter.ToString();
				outputTextBox.Text = word;
				lastRow = row;
				lastColumn = column;
			}
			else
			{
				MouseRelease();
			}
			return new SolidColorBrush(Color.FromRgb((byte)(100 + (7 * word.Length)), 0, 0));
		}

		internal void MouseRelease()
		{
			foreach (LetterBox child in letterGrid.Children)
			{
				child.MouseRelease();
			}
			if (dataController.CheckWordInList(word) && !guessedWords.Contains(word))
			{
				guessedWords.Add(word);
				mainWindow.AddPoints(1);
			}
			word = "";
			lastRow = -1;
			lastColumn = -1;
		}
	}
}