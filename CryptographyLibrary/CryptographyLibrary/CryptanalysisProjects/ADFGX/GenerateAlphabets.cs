using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CryptographyLibrary
{
	public class GenerateAlphabets
	{
		/*public static void Main (string[] args)
		{
			List<string> input = new List<string>();
			//List<string> output = new List<string>();
			input.AddRange(File.ReadAllLines("cryptograms.txt"));
			string alphabet = "ABCDEFGHIKLMNOPQRSTUVWXYZ".ToLower();
			//string cipherAlpha = "lamtzobnycfpvkgqwehrxdisy";

			ADFGX_Tools adfgx = new ADFGX_Tools();
			adfgx.solveADFGX(alphabet);
			File.WriteAllLines("plainText.txt", adfgx.ptList.ToArray());
			//string nextAlpha = "cdefghiklmnopqrstuvwxyzab";
			while(true)
			{
				string searchFor = Console.ReadLine();
				foreach(string ct in input)
				{
					Console.WriteLine(ct);
					Utilities.TestWord(searchFor, ct);
				}
			}
			/*int i = 0;
			foreach(string cipherText in adfgx.ptList)
			{
				Console.WriteLine(cipherText + " #" + i);
				Dictionary<string, ContactLetters> varList = Utilities.FindContactLetters(cipherText);
				Console.WriteLine("PRINT PRECEDING: ");
				foreach(char L in alphabet)
				{
					if(varList.ContainsKey(L.ToString()))
					{
						varList[L.ToString()].printPreceding();
					}
				}
				Console.WriteLine();
				Console.WriteLine("PRINT LEADING: ");
				foreach(char L in alphabet)
				{
					if(varList.ContainsKey(L.ToString()))
					{
						varList[L.ToString()].printLeading();
					}
				}
				Console.WriteLine();
				//Console.Read();
				++i;
			}*/
		//}
	}
}

