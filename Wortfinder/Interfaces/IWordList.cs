using System;
using System.Collections.Generic;
using System.Text;

namespace Wortfinder
{
    public interface IWordList
    {
        bool Loaded();
        int FindIndexOfBeginning(string word, int startIndex);
        bool CheckWord(string word, int startIndex);
    }
}
