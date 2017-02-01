using System;
using System.IO;
using System.Collections.Generic;

namespace CryptographyLibrary
{
	class MainClass
	{
		public static void Main (string[] args)
		{
            string message = "8,1 12,11 21,20 15,6 22,16 0,4 13,6 18,11 23,4 19,19 4,1 16,4 1,7 11,20 17,17 10,25 6,1 7,1 24,7 25,20 9,11 3,10 20,21 5,16 14,26 2,19 8,19 12,23 21,19 15,12 22,18 0,4 13,16 18,20 23,12 19,20 4,24 16,19 1,19 11,8 17,3 10,17 6,6 7,5 24,7 25,20 9,11 3,17 20,24 5,26 14,17";
			string alphabet = " abcdefghijklmnopqrstuvwxyz";

			var container = message.Split (' ');
			var pt = "";

			foreach(var l in container)
			{
				var arr = l.Split (',');

				pt += alphabet[(int.Parse(arr [0]) + int.Parse(arr [1])) % 26];
			}

			Console.WriteLine (pt);
            Console.ReadLine();
        }

		public static void test(string message)
		{
			var endings = File.OpenText ("ending.txt").ReadToEnd().Split('\n');

			foreach(var word in endings)
			{
				Console.WriteLine (word);
				int length = message.Length - 1;
				var subDict = new Dictionary<char, char> ();

				for (int i = word.Length - 1; i >= 0; --i)
				{
					if(subDict.ContainsKey(message[length]))
					{
						if(subDict[message[length]] != word[i])
						{
							break;
						}
					}
					else
					{
						subDict.Add (message [length], word [i]);
					}

					--length;
				}

				string pt = "";
				foreach(var c in message)
				{
					if(subDict.ContainsKey(c))
					{
						pt += subDict [c].ToString ().ToLower ();
					}
					else
					{
						pt += c;
					}
				}

				Console.WriteLine (pt);
				Console.ReadLine ();
			}
		}
	}
}
