using Xunit;

namespace Wortfinder.XUnitTests
{
	public class PointsForSizeTests
	{
		[Fact]
		public void SizeZero()
		{
			PointsForSize pointsForSize = new PointsForSize();
			int result = pointsForSize.GetPoints(0, 0, 0);
			Assert.Equal(0, result);
		}
		[Fact]
		public void MaxSize()
		{
			PointsForSize pointsForSize = new PointsForSize();
			int result = pointsForSize.GetPoints(0, 6, 0);
			Assert.Equal(0, result);
		}
		[Fact]
		public void LargeSize()
		{
			PointsForSize pointsForSize = new PointsForSize();
			int result = pointsForSize.GetPoints(0, 100, 0);
			Assert.Equal(0, result);
		}
		[Fact]
		public void NormalSize()
		{
			PointsForSize pointsForSize = new PointsForSize();
			int result = pointsForSize.GetPoints(0, 3, 0);
			Assert.Equal(3, result);
		}
	}
}
