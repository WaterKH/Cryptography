using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using CryptographyLibrary.CipherFunctions_Utilities;
using CryptographyLibrary.CipherImplementations;
using CryptographyLibrary.CryptanalysisProjects;

namespace CryptographyLibrary
{
	class MainClass
	{
        public static void Main (string[] args)
		{
            DictionaryAttack ds = new DictionaryAttack("dictionary_pruned.txt", 8);

            var lines = File.ReadAllLines("space.txt");

            foreach(var l in lines)
            {
                ds.BeginAnalysis(l);    
            }

            Console.Read();

            SpacingPermutations sp = new SpacingPermutations(8, 1, 8);
            sp.BeginPerms("IUNOLSOGTNZNSSMSSMNTMAOOWPPOWXNTLL");
            //sp.TrimSentences("space.txt");
            Console.Read();

            ConsonantLine cl = new ConsonantLine();
            var alphabet = "abcdefghijklmnopqrstuvwxyz ";

            foreach (var c in alphabet)
            {
                cl.before_and_after_percs[c] = new Dictionary<Tuple<int, int>, int>();
            }
            cl.Analyze("IUNOLSOGTNZNSSMSSMNTMAOOWPPOWXNTLL");
            Console.Read();
            /*var path = "MobyDick_FORMAT.txt";
            var lines = File.ReadAllText(path);
            var format_lines = Utilities.RemoveSpecialCharacters(lines.Replace('\n', ' ')).ToLower();


            var variableLength = 0;
            for (int i = 0; i < format_lines.Length - variableLength; i += variableLength)
            {
                var temp = format_lines.Substring(i, 34);

                var tempCounter = 0;
                foreach(var c in temp)
                {
                    if(c == ' ')
                    {
                        tempCounter++;
                    }
                }
                variableLength = 34 + tempCounter;
                temp = format_lines.Substring(i, variableLength);

                //Console.WriteLine(temp);
                cl.Analyze(temp);
            }

            cl.PrintData();
            Console.Read();*/
        }
    }
}
