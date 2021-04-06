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
		[Fact]
		public void AddScore()
		{
			Mock<IScoreDataController> scoreDataCtr = new Mock<IScoreDataController>();
			Mock<IScoreWindowController> scoreWindowCtr = new Mock<IScoreWindowController>();
			scoreDataCtr.Setup(x => x.LoadScores()).Returns(new List<Score>());
			int testNumber = 10;
			int testSize = 5;
			int testTime = 3;
			string testName = "helloWorld";
			ScoreManager scoreManager = new ScoreManager(scoreWindowCtr.Object, scoreDataCtr.Object);

			scoreManager.AddScore(testNumber, testSize, testTime, testName);

			Assert.Single(scoreManager.scores);
			Assert.Equal(testNumber, scoreManager.scores[0].Number);
			Assert.Equal(testSize, scoreManager.scores[0].FieldSize);
			Assert.Equal(testTime, scoreManager.scores[0].GameTimeInMinutes);
			Assert.Equal(testName, scoreManager.scores[0].PlayerName);
		}
		[Fact]
		public void SaveScores()
		{
			Mock<IScoreDataController> scoreDataCtr = new Mock<IScoreDataController>();
			Mock<IScoreWindowController> scoreWindowCtr = new Mock<IScoreWindowController>();
			scoreDataCtr.Setup(x => x.SaveScores(It.IsAny<List<Score>>())).Verifiable();
			scoreDataCtr.Setup(x => x.LoadScores()).Returns(new List<Score>());

			ScoreManager scoreManager = new ScoreManager(scoreWindowCtr.Object, scoreDataCtr.Object);

			scoreManager.SaveScores();

			scoreDataCtr.Verify();
		}
	}
}
