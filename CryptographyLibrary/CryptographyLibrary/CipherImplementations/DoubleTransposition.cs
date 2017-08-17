using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptographyLibrary.CipherImplementations
{
    class DoubleTransposition
    {

        public string Encrypt(string cipher, string firstKey, string secondKey = "")
        {
            string encryptedString = "";

            if (secondKey == "")
                secondKey = firstKey;

            Transposition transposition = new Transposition();
            var firstTransposition = transposition.Encrypt(cipher, firstKey);
            var secondTransposition = transposition.Encrypt(firstTransposition, secondKey);

            encryptedString = secondTransposition;

            return encryptedString;
        }

        /// <summary>
        /// Decrypts the ciphertext given with the key given.
        /// </summary>
        /// <param name="cipher"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public string Decrypt(string cipher, string firstKey, string secondKey = "")
        {
            string decryptedString = "";

            if (secondKey == "")
                secondKey = firstKey;

            Transposition transposition = new Transposition();
            var firstTransposition = transposition.Decrypt(cipher, firstKey);
            var secondTransposition = transposition.Decrypt(firstTransposition, secondKey);


            decryptedString = secondTransposition;

            return decryptedString;
        }
    }
}
