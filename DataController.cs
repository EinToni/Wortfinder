using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Wortfinder
{
	public class DataController
	{
		private List<string> wordList = new List<string>();
		private string pathGerman = "E:\\Coding\\AdvangedSWMeinProjekt\\Wortfinder\\wordListGerman.txt";
		public DataController()
		{
			LoadLanguage(pathGerman);
		}

		private void LoadLanguage(string path)
		{
			using (StreamReader file = new StreamReader(path))
			{
				string line;
				while ((line = file.ReadLine()) != null)
				{
					wordList.Add(line);
				}
				file.Close();
			}
		}

		public bool AddWord(string word)
		{
			return false;
		}

		public bool CheckWordInList(string word)
		{
			if (word.Length > 0)
			{
				foreach (string line in wordList)
				{
					char firstLetter = word[0];
					if (line[0] != 'Ä' && line[0] != 'Ö' && line[0] != 'Ü' && firstLetter < line[0])
					{
						return false;
					}
					if (word.Equals(line))
					{
						return true;
					}
				}
			}
			return false;
		}
	}
}
