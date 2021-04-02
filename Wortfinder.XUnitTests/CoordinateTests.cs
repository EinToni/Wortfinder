using Xunit;

namespace Wortfinder.XUnitTests
{
	public class CoordinateTests
	{
		[Fact]
		public void Coordinate_Duplicate()
		{
			int row = 5;
			int column = 4;
			Coordinate testCoordinate = new Coordinate(row, column);
			Coordinate secondCoordinate = new Coordinate(testCoordinate);

			Assert.True(testCoordinate.Row == secondCoordinate.Row);
			Assert.True(testCoordinate.Column == secondCoordinate.Column);
		}
		[Fact]
		public void Equals_Same()
		{
			int row = 5;
			int column = 4;
			Coordinate testCoordinate = new Coordinate(row, column);
			Coordinate secondCoordinate = new Coordinate(row, column);

			Assert.True(testCoordinate.Equals(secondCoordinate));
		}

		[Fact]
		public void Equals_Different()
		{
			int row = 5;
			int column = 4;
			Coordinate testCoordinate = new Coordinate(row, column);
			Coordinate coordinate2 = new Coordinate(row + 1, column);
			Coordinate coordinate3 = new Coordinate(row, column - 1);
			Coordinate coordinate4 = new Coordinate(0, 0);

			Assert.False(testCoordinate.Equals(coordinate2));
			Assert.False(testCoordinate.Equals(coordinate3));
			Assert.False(testCoordinate.Equals(coordinate4));
		}

		[Fact]
		public void IsNeighbour_True()
		{
			int row = 5;
			int column = 4;
			Coordinate testCoordinate = new Coordinate(row, column);
			Coordinate secondCoordinate = new Coordinate(row + 1, column);

			Assert.True(testCoordinate.IsNeighbour(secondCoordinate));
		}

		[Fact]
		public void IsNeighbour_False()
		{
			int row = 5;
			int column = 4;
			Coordinate testCoordinate = new Coordinate(row, column);
			Coordinate secondCoordinate = new Coordinate(row + 2, column);

			Assert.False(testCoordinate.IsNeighbour(secondCoordinate));
		}
	}
}
