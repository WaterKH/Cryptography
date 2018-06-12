using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptographyLibrary.CipherImplementations
{
    class XOR
    {
        public string Encrypt(string text, string key)
        {
            StringBuilder ct = new StringBuilder();
            
            for(int i = 0; i < text.Length; ++i)
            {
                ct.Append((char)(text[i] ^ key[i % key.Length]));
            }
            
            return ct.ToString();
        }

        public string Decrypt(string text, string key)
        {
            StringBuilder pt = new StringBuilder();

            for (int i = 0; i < text.Length; ++i)
            {
                Console.Write((text[i] ^ key[i % key.Length]));
                pt.Append((char)(text[i] ^ key[i % key.Length]));
            }
            
            return pt.ToString();
        }
    }
}
