using Xunit;

namespace Wortfinder.XUnitTests
{
	public class PointsWordLengthTests
	{
		[Fact]
		public void ZeroLength()
		{
			PointsWordLength pointsWordLength = new PointsWordLength();
			int result = pointsWordLength.GetPoints(0, 0, 0);
			Assert.Equal(0, result);
		}
		[Fact]
		public void NormalLength()
		{
			PointsWordLength pointsWordLength = new PointsWordLength();
			int result = pointsWordLength.GetPoints(10, 0, 0);
			Assert.Equal(8, result);
		}
	}
}
