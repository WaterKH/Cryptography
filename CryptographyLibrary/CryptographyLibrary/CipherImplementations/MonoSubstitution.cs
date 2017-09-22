using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptographyLibrary.CipherImplementations
{
    class MonoSubstitution
    {
        public string Encrypt()
        {
            return "";
        }
        
        public string Decrypt(string cipher, string cipherAlphabet = "abcdefghijklmnopqrstuvwxyz", string alphabet = "abcdefghijklmnopqrstuvwxyz")
        {
            string decryptString = "";

            foreach(var c in cipher)
            {
                decryptString += alphabet[cipherAlphabet.IndexOf(c)];
            }

            return decryptString;
        }
    }
}
