using System;
using System.Windows.Controls;
using System.Windows.Media;

namespace Wortfinder
{
	// Class to manage all letters that get connected and the resulting word.
	public class LetterController
	{
		private string word = "";
		private int lastRow = -1;
		private int lastColumn = -1;
		private TextBox outputTextBox = null;
		private readonly Grid letterGrid = null;
		private readonly WordController wordController = null;
		private readonly MainWindow mainWindow = null;
		public LetterController(MainWindow main, Grid grid, TextBox textBox)
		{
			outputTextBox = textBox;
			letterGrid = grid;
			wordController = new WordController();
			mainWindow = main;
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
			return new SolidColorBrush(Color.FromRgb((byte)(100 + (5 * word.Length)), 0, 0));
		}

		internal void MouseRelease()
		{
			foreach (LetterBox child in letterGrid.Children)
			{
				child.MouseRelease();
			}
			if (wordController.CheckWord(word))
			{
				mainWindow.AddPoints(1);
			}
			word = "";
			lastRow = -1;
			lastColumn = -1;
		}
	}
}
