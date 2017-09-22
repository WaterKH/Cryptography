using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptographyLibrary.CipherIdeas.ADFGX
{
    class BruteForceAttempt
    {
        string vowels = "aeiouy".ToUpper();
        //string consonants = "cdfghklmnrstw".ToUpper();
        string consonants = "dfghlnrst";
        string alphabet = "--------------";
        string cipher_alphabet = "IUNOLSGTZMAWPX";
        string vowel_mask      = "CUVVCCCVCVCUCC";

        public void VowelMask_Attempt(string ciphertext)//, string vowelmask = "")
        {
            ////////////////////////////////////////////////////////////// 
            // Generate all possibilities
            // 
            var all_consonants = WaterkhUtilities.GetPermutations(consonants.ToList());
            var all_vowels = WaterkhUtilities.GetPermutations(vowels.ToList());
            int length = 0;

            ////////////////////////////////////////////////////////////// 
            // Generate all consonant plaintexts
            // 
            using (StreamWriter writer = new StreamWriter("Stuff.txt"))
            foreach (var c in all_consonants)
            {
                ++length;
                var dict = new Dictionary<char, char>();
                int counter = 0;
               

                for(int i = 0; i < cipher_alphabet.Length; ++i)
                {
                    if(!dict.ContainsKey(cipher_alphabet[i]) && vowel_mask[i] == 'C')
                    {
                        dict.Add(cipher_alphabet[i], c[counter]);
                        ++counter;
                    }
                }

                string plaintext = "";
                for(int i = 0; i < ciphertext.Length; ++i)
                {
                    if (dict.ContainsKey(ciphertext[i]))
                    {
                        plaintext += dict[ciphertext[i]];
                    }
                    else
                    {
                        plaintext += vowel_mask[cipher_alphabet.IndexOf(ciphertext[i])];
                    }
                }
                writer.WriteLine(plaintext);
            }
            Console.WriteLine("Finished");
        }
    }
}
