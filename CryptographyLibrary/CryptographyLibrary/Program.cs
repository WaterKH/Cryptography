using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using ADFGX;

namespace CryptographyLibrary
{
	class MainClass
	{
        public static string alphabet = "abcdefghijklmnopqrstuvwxyz";

        public static void Main (string[] args)
		{
            DictionarySearch ds = new DictionarySearch();
            //ds.Prune();
            //ds.Search();
            string adfgx = "IUNOLSOGTNZNSSMSSMNTMAOOWPPOWXNTLL".ToLower();
            ds.FillInCipherText(adfgx, "Test.txt");

            Console.ReadLine();

            string encryptMes = "kCmIgFi6GUJNgkNI1Q41fbfyLoCFTCvIqkZiI0KIAXAzP1U1uy1BE4UfPBfpKmmL0bjYnQNRBaPtKiVWzc5A4v0w3xIe8F0hAGJZ7g4in0wndJxM0v03dc1M82at2T6935roTqyWDgtGD/hwwRF3oHqFM5Vcw1JtINbsgWRm4o4/quEDkZ7x1B275bX3/Fo1";
            string key = "TheGiant";
            DES des = new DES();

            /*
             * Message Conversion
             */
            byte[] convert_message = Convert.FromBase64String(encryptMes);
            string binMes_prePad = "";

            // USE 6 from b64 and 8 from ascii
            foreach(var b in convert_message)
            {
                binMes_prePad += Convert.ToString(b, 2).PadLeft(6, '0');
            }

            int pad = binMes_prePad.Length + (64 - (binMes_prePad.Length % 64));
            string binMes = binMes_prePad.PadRight(pad, '0');

            /*
             * Key Conversion
             */
            string convert_key = "";

            foreach (char ch in key)
            {
                convert_key += Convert.ToString((int)ch, 2).PadLeft(8, '0');
            }
            

            /*
             * DES Encryption
             *
            var encryptedMessage = "";

            for (int i = 0; i < binMes.Length; i += 64)
            {
                var temp = binMes.Substring(i, 64);
                encryptedMessage += des.Encrypt(temp, convert_key);
            }*/
            
            /*
             * DES Decryption
             */
            var decryptedMessage = "";

            for(int i = 0; i < binMes.Length; i += 64)
            {
                var temp = binMes.Substring(i, 64);
                decryptedMessage += des.Decrypt(temp, convert_key);
            }

            Console.WriteLine(decryptedMessage);

            /*var key = new List<int> { 1, 2, 3, 4, 5 };
            var ct = "XDGDGDFDXFXDXAAAGGAFXXFDXFGXGDFDGFFAFGXFXXAGDAGXAFDAFDGDGDGAGGDADAFAFADAFFGAGDADXAGADAXAGXDDGAGAGAFFAGDXAGAGGDFDGGDDGAAFXGXDFAFDGDAXAADAFFAXGD";

            var l = Utilities.GetPermutations(key);

            foreach(var k in l)
            {
                var t = "";

                k.ForEach(x => t += x);
                var adfgx = new ADFGX_Test(t);
                Console.WriteLine(adfgx.DecryptMessage(ct));

                Console.WriteLine();
            }*/
            Console.WriteLine("Finished");
            Console.ReadLine();
        }

        public static void WriteToFileCribs(string ciphertext)
        {
            List<string> dictionary = File.ReadLines("dictionary.txt").ToList<string>();
            var newDict = new List<string>();

            // Shorten dictionary to two letter ending words
            foreach(var word in dictionary)
            {
                if (word.Length > 2 && word.Length < 9)
                {
                    if (word[word.Length - 1] == word[word.Length - 2])
                    {
                        newDict.Add(word);
                    }
                }
            }

            File.WriteAllLines("two_letter_endings.txt", newDict);

            // Grab all cribs
            //CertainSolution cs = new CertainSolution();
            //var ctList = cs.TestFunction(ciphertext, dictionary);

            //File.WriteAllLines("crib_results.txt", ctList);

            //List<string> ctList = File.ReadLines("crib_results.txt").ToList<string>();
            //List<string> dictionary = File.ReadLines("dictionary.txt").ToList<string>();

            //Console.WriteLine(list.Count);
            //string alphabet = "abcdefghiklmnopqrstuvwxyz";
            //foreach(var c in alphabet)
            //{
            //    var tempL = new List<string>();

            //                foreach(var item in list)
            //                {
            //                    if(item[0] == c)
            //                    {
            //                        tempL.Add(item);
            //                    }
            //                }

            //    File.WriteAllLines("crib_results_for_" + c + ".txt", tempL);
            //    Console.WriteLine(c + " " + tempL.Count); 
            //}

            // Grab particular cribs
            var condensedList = new List<string>();
            for (int i = 0; i < newDict.Count; ++i)
            {
                var word = newDict[i];

                if (Utilities.ContainsSpecialCharacters(word)) continue;
                
                var index = ciphertext.Length - word.Length;
                var tempDict = new Dictionary<char, char>();
                var tempCipherDict = new Dictionary<char, char>();
                var counter = 0;
                var incomplete = false;

                for(int k = index; k < ciphertext.Length; ++k)
                {
                    if (!tempCipherDict.ContainsKey(ciphertext[k]))
                    {
                        tempCipherDict.Add(ciphertext[k], word[counter]);
                    }

                    if (!tempDict.ContainsKey(word[counter]))
                    {
                        tempDict.Add(word[counter], ciphertext[k]);
                    }
                    else
                    {
                        if (tempDict[word[counter]] != ciphertext[k])
                        {
                            incomplete = true;
                            break;
                        }
                    }
                       
                    ++counter;
                }

                if(incomplete) continue;

                var pt = "";
                foreach (var c in ciphertext)
                {
                    if (tempCipherDict.ContainsKey(c))
                    {
                        pt += tempCipherDict[c].ToString().ToUpper();
                    }
                    else
                    {
                        pt += "_";//c.ToString().ToLower();
                    }
                }
                condensedList.Add(pt);
                
            }
            File.WriteAllLines("condensed_crib_results_Liz.txt", condensedList);
        }

        public static void WriteToFileAlphabets(string ciphertext)
        {
            List<string> list = File.ReadAllLines("use_for_alpha.txt").ToList<string>();
            var condensedList = new List<string>();
            foreach (var ct in list)
            {
                StringBuilder newCT = new StringBuilder(ct);
                for (int i = 0; i < newCT.Length; ++i)
                {
                    if (char.IsLower(newCT[i]))
                    {
                        newCT[i] = '_';
                    }
                }
                condensedList.Add(newCT.ToString());
            }

            var listPrint = new List<string>();
            condensedList.ForEach(x => listPrint.Add(printCipherAlphaWithAlpha(ciphertext, x)));

            File.WriteAllLines("alphabets.txt", listPrint);
        }
        
        public static string printCipherAlphaWithAlpha(string ciphertext, string partialPlain)
        {

            StringBuilder cipheralphabet = new StringBuilder("--------------------------");
            string print = "\r\n===========================================\r\n";

            print += alphabet + "\r\n";

            for (int i = 0; i < partialPlain.Length; ++i)
            {
                if (char.IsUpper(partialPlain[i]))
                {
                    cipheralphabet[alphabet.IndexOf(partialPlain[i].ToString().ToLower())] = ciphertext[i];
                }
            }
            Console.WriteLine();
            print += cipheralphabet + "\r\n";
            print += ciphertext.ToLower() + "\r\n" + partialPlain;
            print += "\r\n===========================================";

            return print;
        }
    }

}
