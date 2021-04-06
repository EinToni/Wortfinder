using Xunit;

namespace Wortfinder.XUnitTests
{
	public class PointsForTimeTests
	{
		[Fact]
		public void TimeZero()
		{
			PointsForTime pointsForTime = new PointsForTime();
			int result = pointsForTime.GetPoints(0, 0, 0);
			Assert.Equal(0, result);
		}
		[Fact]
		public void LargeTime()
		{
			PointsForTime pointsForTime = new PointsForTime();
			int result = pointsForTime.GetPoints(0, 0, 10000);
			Assert.Equal(0, result);
		}
		[Fact]
		public void NormalTime()
		{
			PointsForTime pointsForTime = new PointsForTime();
			int result = pointsForTime.GetPoints(0, 0, 60);
			Assert.Equal(2, result);
		}
		[Fact]
		public void RoundTest()
		{
			PointsForTime pointsForTime = new PointsForTime();
			int result = pointsForTime.SecondsToRoundedMins(65);
			Assert.Equal(1, result);
		}
		[Fact]
		public void RoundTest_Zero()
		{
			PointsForTime pointsForTime = new PointsForTime();
			int result = pointsForTime.SecondsToRoundedMins(0);
			Assert.Equal(0, result);
		}
	}
}
