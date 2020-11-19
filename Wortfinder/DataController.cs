using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;

namespace Wortfinder
{
	public class DataController
	{
		public List<string> wordList = new List<string>();
		private readonly string pathGerman = "E:\\Coding\\AdvangedSWMeinProjekt\\Wortfinder\\Wortfinder\\wordListGerman.txt";
		private readonly int minimumWordLength = 3;

		public DataController()
		{
			
		}

		public void LoadGerman()
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
					if (HasMinimumLength(line))
					{
						wordList.Add(line);
					}
				}
				file.Close();
			}
			wordList = NormaliseList(wordList);
			wordList.Sort();
		}

		private bool HasMinimumLength(string word)
		{
			return word.Length - minimumWordLength >= 0;
		}

		public List<string> NormaliseList(List<string> list)
		{
			for(int i = 0; i < list.Count; i++)
			{
				list[i] = NormaliseToUpper(NormaliseVowels(list[i]));
			}
			return list;
		}

		public string NormaliseVowels(string word)
		{
			word = word.Replace("Ä", "Ae");
			word = word.Replace("Ö", "Oe");
			word = word.Replace("Ü", "Ue");
			word = word.Replace("ä", "ae");
			word = word.Replace("ö", "oe");
			word = word.Replace("ü", "ue");
			return word;
		}

		public string NormaliseToUpper(string word)
		{
			return word.ToUpper();
		}

		public void AddWord(string word)
		{
			InsertWord(word, wordList);
			wordList = NormaliseList(wordList);
		}

		public void InsertWord(string word, List<string> list)
		{
			if(word.Length > 0)
			{
				list.Add(word);
			}
		}

		public bool CheckWord(string word, int startIndex)
		{
			if(wordList.BinarySearch(startIndex, wordList.Count-startIndex, word, null) >= 0)
			{
				return true;
			}
			return false;
		}

		public int FindBeginningLinear(string word, int startIndex)
		{
			if (word == "")
			{
				return 0;
			}
			for (int i = startIndex; i < wordList.Count; i++)
			{
				string wordFromList = wordList[i];
				if (wordFromList.StartsWith(word))
				{
					return i;
				}else if (wordFromList[0] > word[0])
				{
					return -1;
				}
			}
			return -1;
		}
	}
}
