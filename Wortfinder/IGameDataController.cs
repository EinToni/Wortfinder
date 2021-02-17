using System;
using System.Collections.Generic;
using System.Text;

namespace Wortfinder
{
    public interface IGameDataController
    {
        public Dictionary<int, List<Game>> LoadGames();
        public void SaveGames(Dictionary<int, List<Game>> games);
    }
}
