using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;

namespace Wortfinder
{
	public class WordList : IWordList
	{
		public List<string> wordList = new List<string>();
		private readonly string pathGerman = "wordListGerman.txt";
		private readonly int minimumWordLength = 3;
		private bool loaded = false;

		public WordList()
		{
			LoadGerman();	
		}

		public void LoadGerman()
		{
            if (!File.Exists(pathGerman))
            {
				MessageBox.Show("File with words could not be loaded.");
			}
			LoadLanguage(pathGerman);
		}

		internal void LoadLanguage(string path)
		{
			Thread thread = new Thread(() => LoadThreadFunction(path));
			thread.Start();
		}

		internal void SetList(List<string> wordList) => this.wordList = wordList;

		internal void LoadThreadFunction(string path)
        {
			List<string> list = new List<string>();
			using (StreamReader file = new StreamReader(path))
			{
				string line;
				while ((line = file.ReadLine()) != null)
				{
					if (HasMinimumLength(line))
					{
						list.Add(line);
					}
				}
				file.Close();
			}
			list = NormaliseList(list);
			list.Sort();
			SetList(list);
			loaded = true;
		}

        public bool Loaded() => loaded;

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

		public int FindIndexOfBeginning(string word, int startIndex)
		{
			int index = 0;
			if (word == "")
			{
				return index;
			}
			for (index = startIndex; index < wordList.Count; index++)
			{
				string wordFromList = wordList[index];
				if (wordFromList.StartsWith(word))
				{
					return index;
				}
				else if (wordFromList[0] > word[0])
				{
					break;
				}
			}
			throw new Exception("No word begins with this string");
		}
	}
}
