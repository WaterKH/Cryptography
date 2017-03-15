using System;
using System.IO;
using System.Collections.Generic;

namespace CryptographyLibrary
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			//TODO Fix this - We are reading into columns instead of reading into rows
			string ciphertext = "";
			string alphabet = "abcdefghiklmnopqrstuvwxyz";
			string key = "WSIPAC";

			ADFGX adfgx = new ADFGX (alphabet, key);

			var pt = adfgx.Decrypt (ciphertext);
			Console.WriteLine ("Plaintext: " + pt + "\tLetter Count: " + pt.Length);

            Console.ReadLine();
        }

	}
}
