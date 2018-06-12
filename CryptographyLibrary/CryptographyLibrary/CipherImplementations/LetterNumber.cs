using System;
namespace CryptographyLibrary.CipherImplementations
{
    public class LetterNumber
    {
        string alphabet = "abcdefghijklmnopqrstuvwxyz";

        public string Encrypt(string message, int addition = 0)
        {
			string encryptString = "";

			foreach (var m in message)
			{
                encryptString += alphabet.IndexOf(m) + addition;
			}

			return encryptString;
        }

        public string Decrypt(string message, int addition = 0)
        {
            string decryptString = "";

            foreach(var m in message)
            {
                int parsedInt = int.Parse(m.ToString()) - addition;
                decryptString += alphabet[parsedInt];
            }

            return decryptString;
        }
    }
}
