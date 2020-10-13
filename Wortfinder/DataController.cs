using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;

namespace Wortfinder
{
	public class DataController
	{
		private List<string> wordList = new List<string>();
		private string[] wordArray = null;
		private string pathGerman = "E:\\Coding\\AdvangedSWMeinProjekt\\Wortfinder\\Wortfinder\\wordListGerman.txt";
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
			wordList = NormaliseList(wordList);
			wordList.Sort();
			wordArray = wordList.ToArray();
		}

		public List<string> NormaliseList(List<string> list)
		{
			for(int i = 0; i < list.Count; i++)
			{
				list[i] = NormaliseWord(list[i]);
			}
			return list;
		}

		public string NormaliseWord(string word)
		{
			word = word.ToUpper();
			word = word.Replace("Ä", "AE");
			word = word.Replace("Ö", "OE");
			word = word.Replace("Ü", "UE");
			return word;
		}

		public bool AddWord(string word)
		{
			return false;
		}

		public bool CheckWord(string word, int startIndex)
		{
			if(wordList.BinarySearch(startIndex, wordList.Count-startIndex-1, word, null) >= 0)
			{
				return true;
			}
			return false;
		}

		public bool CheckBeginning(string word, int startIndex)
		{
			if (wordList.BinarySearch(startIndex, wordList.Count - startIndex - 1, word, new BeginningComparer()) >= 0)
			{
				return true;
			}
			return false;
		}
	}
}
