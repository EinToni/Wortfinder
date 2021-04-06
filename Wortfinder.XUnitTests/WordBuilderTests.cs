using Xunit;

namespace Wortfinder.XUnitTests
{
	public class WordBuilderTests
	{
		[Fact]
		public void ClearTest()
		{
			WordBuilder wordBuilder = new WordBuilder();
			wordBuilder.Clear();
			Assert.Equal("", wordBuilder.Word);
		}

		[Fact]
		public void Hover_GameNotRunning()
		{
			WordBuilder wordBuilder = new WordBuilder();
			bool result = wordBuilder.HoverLetter("", new Coordinate(0, 0), false);
			Assert.False(result);
		}

		[Fact]
		public void ClickLetter_GameNotRunning()
		{
			WordBuilder wordBuilder = new WordBuilder();
			bool result = wordBuilder.ClickLetter("", new Coordinate(0, 0), false);
			Assert.False(result);
			Assert.Equal("", wordBuilder.Word);
		}

		[Fact]
		public void ClickLetter()
		{
			WordBuilder wordBuilder = new WordBuilder();
			bool result = wordBuilder.ClickLetter("A", new Coordinate(0, 0), true);
			Assert.True(result);
			Assert.Equal("A", wordBuilder.Word);
		}
	}
}
