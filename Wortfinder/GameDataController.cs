using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;

namespace Wortfinder
{
    class GameDataController
    {
        private readonly byte[] key = { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16 };
        private readonly string highscoreLocation = ".\\PreLoadedGames.bin";
        private readonly Aes aes;

        public GameDataController()
        {
            aes = Aes.Create();
            aes.Key = key;
            //aes.IV = yourByteArrayIV;
        }

        public Dictionary<int, List<Game>> LoadGames()
        {
            Dictionary<int, List<Game>> Games = new Dictionary<int, List<Game>>();
            if (File.Exists(highscoreLocation))
            {
                try
                {
                    //Create a file stream.
                    using FileStream myStream = new FileStream(highscoreLocation, FileMode.Open);

                    //Reads IV value from beginning of the file.
                    byte[] iv = new byte[aes.IV.Length];
                    myStream.Read(iv, 0, iv.Length);

                    //Create a CryptoStream, pass it the file stream, and decrypt
                    //it with the Aes class using the key and IV.
                    using CryptoStream cryptStream = new CryptoStream(
                       myStream,
                       aes.CreateDecryptor(key, iv),
                       CryptoStreamMode.Read);

                    //Read the stream.
                    BinaryFormatter bf = new BinaryFormatter();
                    //using StreamReader sReader = new StreamReader(cryptStream);

                    Games = (Dictionary<int, List<Game>>)bf.Deserialize(cryptStream);
                }
                catch
                {
                    Console.WriteLine("The decryption failed.");
                }
            }
            return Games;
        }

        public void SaveLoadedGames(Dictionary<int, List<Game>> games)
        {
            try
            {
                //Create a file stream
                using FileStream myStream = new FileStream(highscoreLocation, FileMode.Create);
                byte[] iv = aes.IV;
                myStream.Write(iv, 0, iv.Length);

                //Create a CryptoStream, pass it the FileStream, and encrypt
                //it with the Aes class.  
                using CryptoStream cryptStream = new CryptoStream(
                    myStream,
                    aes.CreateEncryptor(),
                    CryptoStreamMode.Write);

                new BinaryFormatter().Serialize(cryptStream, games);
            }
            catch
            {
                //Inform the user that an exception was raised.  
                Console.WriteLine("The encryption failed.");
                throw;
            }
        }
    }
}
