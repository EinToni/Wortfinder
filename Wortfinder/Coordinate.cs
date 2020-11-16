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

		public bool SameCoordinate(Coordinate coordinate)
		{
			return Row == coordinate.Row && Column == coordinate.Column;
		}

		public bool IsNeighbour(Coordinate coordinate)
		{
			return RowDistanceTo(coordinate) <= 1 && ColumnDistanceTo(coordinate) <= 1;
		}

		private int RowDistanceTo(Coordinate coordinate)
		{
			return Math.Abs(coordinate.Row - Row);
		}

		private int ColumnDistanceTo(Coordinate coordinate)
		{
			return Math.Abs(coordinate.Row - Row);
		}
	}
}
