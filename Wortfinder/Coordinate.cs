using System;
using System.Collections.Generic;
using System.Text;

namespace Wortfinder
{
	public class Coordinate
	{
		public int Row { get; }
		public int Column { get; }
		public Coordinate(int row, int column)
		{
			Row = row;
			Column = column;
		}

		public Coordinate(Coordinate coordinate)
		{
			Row = coordinate.Row;
			Column = coordinate.Column;
		}

		public bool Equals(Coordinate coordinate)
		{
			if (Row == coordinate.Row && Column == coordinate.Column)
			{
				return true;
			}
			return false;
		}

		public bool IsNeighbour(Coordinate coordinate)
		{
			if (Math.Abs(coordinate.Row - Row) <= 1 && Math.Abs(coordinate.Column - Column) <= 1)
			{
				return true;
			}
			return false;
		}
	}
}
