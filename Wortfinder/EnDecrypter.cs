using System;
using System.IO;
using System.Security.Cryptography;

namespace Wortfinder
{
	public class EnDecrypter
	{
		public Stream Decrypt(Stream toDecrypt, Aes aes, byte[] key)
		{
			try
			{
				byte[] iv = new byte[aes.IV.Length];
				toDecrypt.Read(iv, 0, iv.Length);

				CryptoStream decrypted = new CryptoStream(
				   toDecrypt,
				   aes.CreateDecryptor(key, iv),
				   CryptoStreamMode.Read);
				return decrypted;
			}
            catch
            {
                Console.WriteLine("The decryption failed.");
                throw;
            }
		}

		public Stream Encrypt(Stream toEncrypt, Aes aes)
		{
			try
			{
				byte[] iv = aes.IV;
				toEncrypt.Write(iv, 0, iv.Length);
				CryptoStream encrypted = new CryptoStream(
					toEncrypt,
					aes.CreateEncryptor(),
					CryptoStreamMode.Write);
				return encrypted;
			}
			catch
			{
				Console.WriteLine("The encryption failed.");
				throw;
			}
		}
	}
}
