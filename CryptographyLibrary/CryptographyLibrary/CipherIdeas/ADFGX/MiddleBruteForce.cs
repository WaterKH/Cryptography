using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Timers;

namespace CryptographyLibrary.CipherIdeas.ADFGX
{
    class MiddleBruteForce
    {
        string alphabet = "abcdefghiklmnopqrstuvwxyz";
        string consonants = "bcdfghklmnpqrstvwxz";
        string vowels = "aeiouy";

        public void AllPossibilities(string fileName)
        {
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                foreach (var a in consonants)
                foreach (var b in vowels)
                foreach(var c in vowels)
                {
                    if (c == b) continue;

                    string writeTo = a.ToString() + b.ToString() + c.ToString();
                    writer.WriteLine(writeTo);
                }
                
            }
        }

        public void FillInCiphertext(string fileName, string ciphertext)
        {
            using(StreamWriter writer = new StreamWriter("Test.txt"))
            using (StreamReader reader = new StreamReader(fileName))
            {
                string line = "";
                while ((line = reader.ReadLine()) != null)
                {
                    var dict = new Dictionary<char, char>()
                    {
                        { 's', line[0] }, { 'n', line[1] }, { 'r', line[2] }
                    };

                    string pt = "";
                    foreach(var c in ciphertext)
                    {
                        if(dict.ContainsKey(c))
                        {
                            pt += dict[c];
                        }
                        else
                        {
                            pt += "-";
                        }
                    }
                    writer.WriteLine(pt);
                }
            }
        }

        public void ConstrainDictionary(int length = 7)
        {
            string word = "";

            using (StreamWriter writer = new StreamWriter("dictionary_7letter.txt"))
            using (StreamReader reader = new StreamReader("dictionary_pruned.txt"))
            {
                while((word = reader.ReadLine()) != null)
                {
                    if(word.Length <= 7)
                    {
                        writer.WriteLine(word);
                    }
                }
            }
        }

        public void AllPatterns(int index = 0)
        {
            string word = "";

            using (StreamWriter writer = new StreamWriter("AllPossibleWords_index" + index + ".txt"))
            using (StreamReader reader = new StreamReader("dictionary_7letter.txt"))
            {
                while((word = reader.ReadLine()) != null)
                {
                    string sub_word = "";


                    
                    using (StreamReader sub_reader = new StreamReader("patterns.txt"))
                    {
                        while((sub_word = sub_reader.ReadLine()) != null)
                        {
                            if(sub_word.Contains(word))
                            {
                                writer.WriteLine(word);
                            }
                        }
                    }
                }
                Console.WriteLine("Finished");
            }
        }

        public void FillInCipherText(string cipher, string partialSolution, string patterns, string writeTo)
        {
            Timer time = new Timer();
            
            time.Start();

            using (StreamWriter writer = new StreamWriter(writeTo))
            { 
                //using (StreamReader reader = new StreamReader(partialSolutionFile))
                //{

                //    while ((partialSolution = reader.ReadLine()) != null)
                //    {
                // Prelim - Grab all previous partial solutions
                Dictionary<char, char> holder = new Dictionary<char, char>();
                for (int i = 0; i < partialSolution.Length; ++i)
                {
                    if (char.IsUpper(partialSolution[i]))
                    {
                        // No error checking since this is already precomputed
                        if(!holder.ContainsKey(cipher[i]))
                            holder.Add(cipher[i], partialSolution[i]);
                    }
                }


                // Start at length 12 (index 11)
                int length = 11;
                string temp = cipher.Substring(length, 4); // Only get what we need
                string pattern = "";

                // Tack onto previous string
                using (StreamReader sub_reader = new StreamReader(patterns))
                {
                    while ((pattern = sub_reader.ReadLine()) != null)
                    {
                        bool nonmatch = false;
                        string pt_partial = "";
                        Dictionary<char, char> sub_holder = new Dictionary<char, char>();
                           
                        for (int i = 0; i < temp.Length; ++i)
                        {
                            if (!sub_holder.ContainsKey(temp[i]))
                            {
                                sub_holder.Add(temp[i], pattern[i]);
                            }
                        }
                        

                        foreach (char c in cipher)
                        {
                            if(holder.ContainsKey(c) && sub_holder.ContainsKey(c))
                            {
                                if (char.ToLower(holder[c]) != sub_holder[c])
                                {
                                    nonmatch = true;
                                    break;
                                }
                            }
                            else if(holder.ContainsKey(c))
                            {
                                if(sub_holder.ContainsValue(char.ToLower(holder[c])))
                                {
                                    nonmatch = true;
                                    break;
                                }
                            }

                            if (holder.ContainsKey(c))
                            {
                                pt_partial += holder[c].ToString().ToUpper();
                            }
                            else if(sub_holder.ContainsKey(c))
                            {
                                pt_partial += sub_holder[c].ToString().ToUpper();
                            }
                            else
                            {
                                pt_partial += c.ToString();
                            }
                        }
                        if (nonmatch)
                            continue;
                        writer.WriteLine(pt_partial);
                    }
                }
            }
            time.Stop();
            //Console.WriteLine(time.Interval);
        }
    }
}
