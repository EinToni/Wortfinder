using System;
using System.Collections.Generic;
using System.Text;

namespace Wortfinder
{
	public class WordBuilder
	{
        public string Word { get; private set; } = "";
        private readonly List<Coordinate> wordCoords = new List<Coordinate>();

        public void Clear()
		{
            Word = "";
            wordCoords.Clear();
        }

        internal bool HoverLetter(string letter, Coordinate coordinate, bool gameRunning)
        {
            if (gameRunning && Word != "" && !AlreadyClicked(coordinate, wordCoords))
            {
                if (wordCoords.Count > 0 && !coordinate.IsNeighbour(wordCoords[^1]))
				{ }
                else
                {
                    Word += letter;
                    wordCoords.Add(coordinate);
                    return true;
                }
            }
            return false;
        }

        internal bool AlreadyClicked(Coordinate coordinate, List<Coordinate> coordinates)
        {
            foreach (Coordinate clickedCoord in coordinates)
            {
                if (coordinate.Equals(clickedCoord))
                {
                    return true;
                }
            }
            return false;
        }

        internal bool ClickLetter(string letter, Coordinate coordinate, bool gameRunning)
        {
            if (gameRunning)
            {
                Word = letter;
                wordCoords.Clear();
                wordCoords.Add(coordinate);
                return true;
            }
            return false;
        }
    }
}
