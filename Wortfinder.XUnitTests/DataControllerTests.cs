using Xunit;

namespace Wortfinder.XUnitTests
{
	public class DataControllerTests
	{
		[Fact]
		public void CheckBeginningTest()
		{
			DataController dataController = new DataController();
			var result = dataController.CheckWord("AALGLATT", 0);
			Assert.True(result);
		}

		[Fact]
		public void CheckBeginnTest()
		{
			DataController dataController = new DataController();
			var result = dataController.CheckBeginning("ABAENDERUNGSVEREINBARUM", 0);
			Assert.True(result);
		}
	}
}
