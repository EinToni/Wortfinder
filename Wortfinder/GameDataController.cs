using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;

namespace Wortfinder
{
    class GameDataController : IGameDataController
    {
        private readonly byte[] key = { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16 };
        private readonly string highscoreLocation = ".\\PreLoadedGames.bin";
        private readonly Aes aes;
        private readonly EnDecrypter enDecrypter;
        private readonly object mylock = new object();

        public GameDataController()
        {
            aes = Aes.Create();
            aes.Key = key;
            enDecrypter = new EnDecrypter();
        }

        public Dictionary<int, List<Game>> LoadGames()
        {
            Dictionary<int, List<Game>> Games = new Dictionary<int, List<Game>>();
            if (File.Exists(highscoreLocation))
            {
                using FileStream myStream = new FileStream(highscoreLocation, FileMode.Open);
                using Stream file = enDecrypter.Decrypt(myStream, aes, key);
                BinaryFormatter bf = new BinaryFormatter();
				try
				{
					Games = (Dictionary<int, List<Game>>)bf.Deserialize(file);
                }
				catch (SerializationException)
				{}

            }
            return Games;
        }

        public void SaveGames(Dictionary<int, List<Game>> games)
        {
			lock (mylock)
			{
                using FileStream myStream = new FileStream(highscoreLocation, FileMode.Create);
                using Stream file = enDecrypter.Encrypt(myStream, aes);
                new BinaryFormatter().Serialize(file, games);
            }
        }
    }
}
