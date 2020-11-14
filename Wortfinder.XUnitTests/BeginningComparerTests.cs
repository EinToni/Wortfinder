using System.Diagnostics;
using Xunit;

namespace Wortfinder.XUnitTests
{
	public class BeginningComparerTests
	{
		[Fact]
		public void Compare_SameString()
		{
			BeginningComparer beginningComparer = new BeginningComparer();
			var result = beginningComparer.Compare("Test", "Test");

			Assert.Equal(0, result);
		}

		[Fact]
		public void Compare_FirstEmpty()
		{
			BeginningComparer beginningComparer = new BeginningComparer();
			var result = beginningComparer.Compare("", "Test");
			
			Assert.True(result < 0);
		}

		[Fact]
		public void Compare_SecondEmpty()
		{
			BeginningComparer beginningComparer = new BeginningComparer();
			var result = beginningComparer.Compare("Test", "");

			Assert.Equal(0, result);
		}

		[Fact]
		public void Compare_LargerString()
		{
			BeginningComparer beginningComparer = new BeginningComparer();
			var result = beginningComparer.Compare("Apfel", "Birne");

			Assert.True(result < 0);
		}

		[Fact]
		public void Compare_BeginsWith()
		{
			BeginningComparer beginningComparer = new BeginningComparer();
			var result = beginningComparer.Compare("Apfelbaum", "Apfel");

			Assert.Equal(0, result);
		}
	}
}
