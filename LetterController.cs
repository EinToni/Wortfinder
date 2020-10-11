using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace Wortfinder
{
	public class LetterController
	{
		private string word = "";
		private int lastRow = -1;
		private int lastColumn = -1;
		private Label Label = null;
		private readonly Grid letterGrid;
		public LetterController(Grid grid, Label label)
		{
			Label = label;
			letterGrid = grid;
		}

		internal void ClickLetter(char letter, int row, int column)
		{
			if (Math.Abs(lastRow - row) <= 1 && Math.Abs(lastColumn - column) <= 1 || lastRow == -1 && lastColumn == -1)
			{
				word += letter.ToString();
				Label.Content = word;
				lastRow = row;
				lastColumn = column;
			}
			else
			{
				MouseRelease();
			}
		}

		internal void MouseRelease()
		{
			word = "";
			foreach (LetterBox child in letterGrid.Children)
			{
				child.MouseRelease();
			}
		}
	}
}
