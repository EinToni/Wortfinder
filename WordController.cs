using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Wortfinder
{
	// Class to controll 
	class WordController
	{
		public WordController()
		{

		}
		public bool CheckWord(string word)
		{
			bool returnValue = false;
			if(word.Length > 0)
			{
				using (StreamReader file = new StreamReader("E:\\Coding\\AdvangedSWMeinProjekt\\Wortfinder\\wordListGerman.txt"))
				{
					string line;
					char firstLetter = word[0];
					while ((line = file.ReadLine()) != null)
					{
						if (line[0] != 'Ä' && line[0] != 'Ö' && line[0] != 'Ü' && firstLetter < line[0])
						{
							break;
						}
						if (word.Equals(line))
						{
							returnValue = true;
							break;
						}
					}
					file.Close();
				}
			}
			return returnValue;
		}
		
	}
}
