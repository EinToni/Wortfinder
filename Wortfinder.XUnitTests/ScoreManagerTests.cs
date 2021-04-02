using Xunit;
using Moq;
using System.Collections.Generic;
using System;

namespace Wortfinder.XUnitTests
{
	public class ScoreManagerTests
	{
		[Fact]
		public void LoadingTest()
		{
			Mock<IScoreDataController> scoreDataCtr = new Mock<IScoreDataController>();
			Mock<IScoreWindowController> scoreWindowCtr = new Mock<IScoreWindowController>();

			scoreDataCtr.Setup(x => x.LoadScores()).Returns(new List<Score>()).Verifiable();

			ScoreManager scoreManager = new ScoreManager(scoreWindowCtr.Object, scoreDataCtr.Object);

			scoreDataCtr.Verify(x => x.LoadScores());
		}

		[Fact]
		public void GetTopScores_EmptyList()
		{
			Mock<IScoreDataController> scoreDataCtr = new Mock<IScoreDataController>();
			Mock<IScoreWindowController> scoreWindowCtr = new Mock<IScoreWindowController>();

			scoreDataCtr.Setup(x => x.LoadScores()).Returns(new List<Score>()).Verifiable();

			ScoreManager scoreManager = new ScoreManager(scoreWindowCtr.Object, scoreDataCtr.Object);
			var result = scoreManager.GetTopScores(5);

			scoreDataCtr.Verify(x => x.LoadScores());
			Assert.Empty(result);
		}

		[Fact]
		public void GetTopScores_listTooSmall()
		{
			Mock<IScoreDataController> scoreDataCtr = new Mock<IScoreDataController>();
			Mock<IScoreWindowController> scoreWindowCtr = new Mock<IScoreWindowController>();
			Score scoreSmall = new Score(1,0,0,"",DateTime.Now);
			Score scoreLarge = new Score(10, 0, 0, "", DateTime.Now);

			scoreDataCtr.Setup(x => x.LoadScores()).Returns(new List<Score>() { scoreSmall, scoreLarge}).Verifiable();

			ScoreManager scoreManager = new ScoreManager(scoreWindowCtr.Object, scoreDataCtr.Object);
			List<Score> result = scoreManager.GetTopScores(5);

			scoreDataCtr.Verify(x => x.LoadScores());
			Assert.True(result.Capacity == 2);
		}

		[Fact]
		public void GetTopScores_valid()
		{
			Mock<IScoreDataController> scoreDataCtr = new Mock<IScoreDataController>();
			Mock<IScoreWindowController> scoreWindowCtr = new Mock<IScoreWindowController>();
			Score scoreSmall = new Score(1, 0, 0, "", DateTime.Now);
			Score scoreLarge = new Score(10, 0, 0, "", DateTime.Now);

			scoreDataCtr.Setup(x => x.LoadScores()).Returns(new List<Score>() { scoreSmall, scoreLarge }).Verifiable();

			ScoreManager scoreManager = new ScoreManager(scoreWindowCtr.Object, scoreDataCtr.Object);
			List<Score> result = scoreManager.GetTopScores(1);

			scoreDataCtr.Verify(x => x.LoadScores());
			Assert.True(result.Capacity == 1);
			Assert.Contains(scoreLarge, result);
		}
	}
}
