using System;
using System.Collections.Generic;

namespace CryptographyLibrary
{
	public class CertainSolution
	{
        public int startIndex = 5;
        public int endIndex = 7;
        
        public List<string> TestFunction(string ciphertext, List<string> dictionary)
        {
            var poss = new List<string>();

            for(int i = startIndex; i < endIndex + 1; ++i)
            {
                foreach(var word in dictionary)
                {
                    var letterMap = new Dictionary<char, char>();
                    var cipherMap = new Dictionary<char, char>();
                    int cipherIndex = 0;
                    var incomplete = false;

                    if (word.Length == i && !WaterkhUtilities.ContainsSpecialCharacters(word))
                    {
                        foreach(var c in word)
                        {
                            if(!cipherMap.ContainsKey(ciphertext[cipherIndex]))
                            {
                                cipherMap.Add(ciphertext[cipherIndex], c);
                            }

                            if(!letterMap.ContainsKey(c))
                            {
                                letterMap.Add(c, ciphertext[cipherIndex]);
                            }
                            else
                            {
                                if (letterMap[c] != ciphertext[cipherIndex])
                                {
                                    incomplete = true;
                                    break;
                                }
                            }
                            
                            ++cipherIndex;
                        }
                        
                        if(incomplete)
                        {
                            continue;
                        }

                        string pt = "";
                        foreach(var c in ciphertext)
                        {
                            if(cipherMap.ContainsKey(c))
                            {
                                pt += cipherMap[c];
                            }
                            else
                            {
                                pt += c;
                            }
                        }
                        poss.Add(pt);
                    }
                }
            }

            return poss;
        }
	}
}

