using System;
using System.Collections.Generic;
using System.IO;

namespace CryptographyLibrary
{
	public class LetterFrequency
	{
		public Dictionary<string, int> ProduceFrequencies (string cipherFileName, string sep = " ")
		{
            string formattedCipher = "";
            using (StreamReader reader = new StreamReader(cipherFileName))
            {
                string cipher = "";
                while ((cipher = reader.ReadLine()) != null)
                {
                    formattedCipher += cipher.Replace(" ", "");
                }
            }

            string returnStr = "";

            for(int i = 0; i < formattedCipher.Length; ++i)
            {
                if(i != 0 && i % 2 == 0)
                {
                    returnStr += " ";  
                }

                returnStr += formattedCipher[i];
            }

            var freqs = new Dictionary<string, int>();
            foreach (var part in returnStr.Split(' '))
            {
                if (!freqs.ContainsKey(part))
                {
                    freqs.Add(part, 0);
                }

                ++freqs[part];
            }

            return freqs;
		}
	}
}
