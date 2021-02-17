using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Wortfinder
{
	[Serializable]
	public class Score : IComparable<Score>
	{
		public int Number { get; }
		public int FieldSize { get; }
		public int GameTimeInMinutes { get; }
		public string PlayerName { get; }
		public DateTime Timestamp { get; }

		public Score(int score, int fieldSize, int gameTimeInMinutes, string name, DateTime dateTime) 
		{
			Number = score;
			FieldSize = fieldSize;
			GameTimeInMinutes = gameTimeInMinutes;
			PlayerName = name;
			Timestamp = dateTime;
		}

		public string Date
		{
			get
			{
				return Timestamp.Date.ToString("dd/MM/yy");
			}
		}

		public string GameInfo
		{
			get
			{
				return FieldSize + "x" + FieldSize + " in " + GameTimeInMinutes + "min";
			}
		}

		public int CompareTo(Score other)
		{
			int samePosition = 0;
			int otherIsSmaller = 1;

			if (other == null) { 
				return otherIsSmaller;
			}
			else { 
				int returnValue = other.Number.CompareTo(Number);
				if (returnValue == samePosition)
				{
					return other.Timestamp.CompareTo(Timestamp);
				}
				return returnValue;
			}
		}
	}
}
