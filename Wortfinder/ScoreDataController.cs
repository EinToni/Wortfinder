﻿using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;


namespace Wortfinder
{
    public class ScoreDataController : IScoreDataController
    {
        private readonly Aes aes;
        private readonly string highscoreLocation = ".\\Highscores.bin";
        private readonly byte[] key = { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16 };
        private readonly EnDecrypter enDecrypter;
        public ScoreDataController(EnDecrypter enDecrypter)
        {
            aes = Aes.Create();
            aes.Key = key;
            this.enDecrypter = enDecrypter;
        }
        public List<Score> LoadScores()
        {
            if (File.Exists(highscoreLocation))
            {
                using FileStream myStream = new FileStream(highscoreLocation, FileMode.Open);
                using Stream file = enDecrypter.Decrypt(myStream, aes, key);
                BinaryFormatter bf = new BinaryFormatter();
                List<Score> scores = (List<Score>)bf.Deserialize(file);
                return scores;
			}
            return new List<Score>();
        }

        public void SaveScores(List<Score> scores)
        {
            using FileStream myStream = new FileStream(highscoreLocation, FileMode.Create);
            using Stream file = enDecrypter.Encrypt(myStream, aes);
            new BinaryFormatter().Serialize(file, scores);
        }
    }
}
