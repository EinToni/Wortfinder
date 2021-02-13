using System;
using System.Collections.Generic;
using System.Text;

namespace Wortfinder
{
    interface IFactory
    {
        IWordList GetWordList();
    }
}
