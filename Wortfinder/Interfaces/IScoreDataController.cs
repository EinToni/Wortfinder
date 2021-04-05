using System;
using System.Collections.Generic;
using System.Text;

namespace Wortfinder
{
    public interface IScoreDataController
    {
        public List<Score> LoadScores();
        public void SaveScores(List<Score> scores);
    }
}
