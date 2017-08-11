using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptographyLibrary
{
    class DictionarySearch
    {
        public void Search()
        {
            string adfgx = "IUNOLSOGTNZNSSMSSMNTMAOOWPPOWXNTLL".ToLower();
            string word;

            using (StreamWriter writer = new StreamWriter("Test.txt"))
            {
                using (StreamReader reader = new StreamReader("dictionary_pruned.txt"))
                {
                    while ((word = reader.ReadLine()) != null)
                    {
                        // End of ciphertext
                        if (word.Length < 8 && word.Length > 2 && word[word.Length - 1] == word[word.Length - 2])
                        {
                            int length = (adfgx.Length - 1) - (word.Length - 1);
                            string temp = adfgx.Substring(length);

                            string alpha_word = Utilities.ConvertToAlphaCharacters(word);
                            string alpha_temp = Utilities.ConvertToAlphaCharacters(temp);

                            if (alpha_word == alpha_temp)
                            {
                                writer.WriteLine(word);
                            }
                        }
                    }
                }
            }
        }

        public void Search(string pattern)
        {
            string adfgx = "IUNOLSOGTNZNSSMSSMNTMAOOWPPOWXNTLL".ToLower();
            string word;

            using (StreamWriter writer = new StreamWriter("Test.txt"))
            { 
                using (StreamReader reader = new StreamReader("dictionary_pruned.txt"))
                {
                    while ((word = reader.ReadLine()) != null)
                    {
                        // End of ciphertext
                        if (word.Length < 8 && word.Length > 2 && word[word.Length - 1] == word[word.Length - 2])
                        {
                            int length = (adfgx.Length - 1) - (word.Length - 1);
                            string temp = adfgx.Substring(length);

                            string alpha_word = Utilities.ConvertToAlphaCharacters(word);
                            string alpha_temp = Utilities.ConvertToAlphaCharacters(temp);

                            if (alpha_word == alpha_temp)
                            {
                                writer.WriteLine(word);
                            }
                        }
                    }
                }
            }
        }

        public void Prune()
        {
            string word;
            using (StreamWriter writer = new StreamWriter("dictionary_pruned.txt"))
            {
                using (StreamReader reader = new StreamReader("dictionary.txt"))
                {
                    while ((word = reader.ReadLine()) != null)
                    {
                        if (!Utilities.ContainsSpecialCharacters(word))
                        {
                            writer.WriteLine(word);
                        }
                    }
                }
            }
        }

        public void FillInCipherText(string cipher, string dictionary)
        {
            string word = "";
            using (StreamWriter writer = new StreamWriter("PartialSolutions.txt"))
            {
                using (StreamReader reader = new StreamReader(dictionary))
                {
                    while ((word = reader.ReadLine()) != null)
                    {
                        string pt_partial = "";
                        // End of cipher
                        int length = (cipher.Length - 1) - (word.Length - 1);
                        string temp = cipher.Substring(length);
                        Dictionary<char, char> holder = new Dictionary<char, char>();

                        for (int i = 0; i < temp.Length; ++i)
                        {
                            if (!holder.ContainsKey(temp[i]))
                            {
                                holder.Add(temp[i], word[i]);
                            }
                        }

                        foreach (char c in cipher)
                        {
                            if (holder.ContainsKey(c))
                            {
                                pt_partial += holder[c].ToString().ToUpper();
                            }
                            else
                            {
                                pt_partial += c.ToString().ToLower();
                            }
                        }
                        writer.WriteLine(pt_partial);

                    }
                }
            }
        }
    }
}
