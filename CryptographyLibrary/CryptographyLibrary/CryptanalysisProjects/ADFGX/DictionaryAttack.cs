using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CryptographyLibrary.CryptanalysisProjects
{
    public class DictionaryAttack
    {
        public Dictionary<int, List<string>> wordDictionary = new Dictionary<int, List<string>>();

        public DictionaryAttack(string aFile, int maximumWordLength)
        {
            CreateDictionary(aFile, maximumWordLength);
        }

        public void CreateDictionary(string file, int maximumWordLength = 8)
        {
            var lines = File.ReadAllLines(file);

            foreach(var l in lines)
            {
                if (l.Length > maximumWordLength)
                    continue;
                
                var newL = Utilities.RemoveSpecialCharacters(l.ToLower());

                if(!wordDictionary.ContainsKey(newL.Length))
                {
                    wordDictionary.Add(newL.Length, new List<string>());
                }
                wordDictionary[newL.Length].Add(newL);
            }
        }

        public void BeginAnalysis(string text)
        {
            var split_text = text.Split(' ');
            var first = split_text[0];
            var alpha_first = Utilities.ConvertToAlphaPatterns(first);

            StreamWriter writer = new StreamWriter("word_attack.txt");
            foreach(var t in wordDictionary[first.Length])
            {
                var alpha_t = Utilities.ConvertToAlphaPatterns(t);

                if(alpha_first == alpha_t)
                {
                    ContinueAnalysis(split_text, 1, writer);
                }
            }
            writer.Close();
            Console.WriteLine("Finished");
        }

        public void ContinueAnalysis(string[] split_text, int depth, StreamWriter writer)
        {
            if(depth == split_text.Length)
            {
                var x = "";
                split_text.ToList().ForEach(y => x += y + " ");
                writer.WriteLine(x);

                return;
            }

            var part = split_text[depth];
            var alpha_part = Utilities.ConvertToAlphaPatterns(part);

            foreach (var t in wordDictionary[part.Length])
            {
                var alpha_t = Utilities.ConvertToAlphaPatterns(t);

                if (alpha_part == alpha_t)
                {
                    ContinueAnalysis(split_text, depth + 1, writer);
                }
            }
        }
    }
}
