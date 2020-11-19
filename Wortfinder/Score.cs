using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Wortfinder
{
	[Serializable]
	public class Score : IEquatable<Score>, IComparable<Score>
	{
		public int Number { get; }
		public int FieldSize { get; }
		public int GameTimeInMinutes { get; }
		public string PlayerName { get; }
		public DateTime Timestamp { get; }

		public Score(int score, int fieldSize, int gameTimeInMinutes, string name) 
		{
			Number = score;
			FieldSize = fieldSize;
			GameTimeInMinutes = gameTimeInMinutes;
			PlayerName = name;
			Timestamp = DateTime.Now;
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
			if (other == null) { 
				return 1;
			}
			else { 
				int returnValue = other.Number.CompareTo(Number);
				if (returnValue == 0)
				{
					return other.Timestamp.CompareTo(Timestamp);
				}
				return returnValue;
			}
		}

		public override bool Equals(Object other)
		{
			if (other == null) return false;
			if (!(other is Score objAsPart)) return false;
			else return Equals(objAsPart);
		}

		public bool Equals(Score other)
		{
			if (other == null) return false;
			return (Number.Equals(other.Number)
				&& PlayerName.Equals(other.PlayerName)
				&& Timestamp.Equals(other.Timestamp));
		}
	}
}
