using System;
using System.Collections.Generic;
using System.Text;

namespace Wortfinder
{
    interface IWordList
    {
        bool Loaded();
        int FindBeginningLinear(string word, int startIndex);
        bool CheckWord(string word, int startIndex);
    }
}
