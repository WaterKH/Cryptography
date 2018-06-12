using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptographyLibrary.CipherImplementations
{
    class Vigenere
    {
        public string Encrypt(string text, string key)
        {
            string alphabet = "abcdefghijklmnopqrstuvwxyz";
            StringBuilder ct = new StringBuilder();

            for(int i = 0; i < text.Length; ++i)
            {
                if (alphabet.IndexOf(char.ToLower(text[i])) < 0)
                {
                    ct.Append(text[i]);
                    continue;
                }

                int index = (alphabet.IndexOf(char.ToLower(text[i])) + alphabet.IndexOf(key[i % key.Length])) % alphabet.Length;
                if (char.IsUpper(text[i]))
                    ct.Append(char.ToUpper(alphabet[index]));
                else
                    ct.Append(alphabet[index]);
            }

            return ct.ToString();
        }

        public string Decrypt(string text, string key)
        {
            string alphabet = "abcdefghijklmnopqrstuvwxyz";
            StringBuilder ct = new StringBuilder();

            for (int i = 0; i < text.Length; ++i)
            {
                if(alphabet.IndexOf(char.ToLower(text[i])) < 0)
                {
                    ct.Append(text[i]);
                    continue;
                }

                int index = (alphabet.IndexOf(char.ToLower(text[i])) - alphabet.IndexOf(key[i % key.Length]));

                if(index < 0)
                {
                    index += alphabet.Length;
                }

                if (char.IsUpper(text[i]))
                    ct.Append(char.ToUpper(alphabet[index]));
                else
                    ct.Append(alphabet[index]);
            }

            return ct.ToString();
        }
    }
}
