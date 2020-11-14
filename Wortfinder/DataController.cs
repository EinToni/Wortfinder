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
		private List<string> wordList = new List<string>();
		private string[] wordArray = null;
		private string pathGerman = "E:\\Coding\\AdvangedSWMeinProjekt\\Wortfinder\\Wortfinder\\wordListGerman.txt";

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
