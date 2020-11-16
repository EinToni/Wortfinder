using Xunit;

namespace Wortfinder.XUnitTests
{
	public class CoordinateTests
	{
		[Fact]
		public void SameCoordinate_Same()
		{
			int row = 5;
			int column = 4;
			Coordinate testCoordinate = new Coordinate(row, column);
			Coordinate secondCoordinate = new Coordinate(row, column);

			Assert.True(testCoordinate.SameCoordinate(secondCoordinate));
		}

		[Fact]
		public void SameCoordinate_Different()
		{
			int row = 5;
			int column = 4;
			Coordinate testCoordinate = new Coordinate(row, column);
			Coordinate secondCoordinate = new Coordinate(row + 1, column);

			Assert.False(testCoordinate.SameCoordinate(secondCoordinate));
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

			Assert.False(testCoordinate.SameCoordinate(secondCoordinate));
		}
	}
}
