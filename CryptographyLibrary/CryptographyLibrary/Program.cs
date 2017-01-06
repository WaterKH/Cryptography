using System;
using System.Collections.Generic;

namespace CryptographyLibrary
{
	class MainClass
	{
		public static void Main (string[] args)
		{
            Vigenere v = new Vigenere();
            string message = "Hello World";
            string key = "waterkh";

            string ct = v.Encrypt(message, key);
            string pt = v.Decrypt(ct, key);

            Console.WriteLine(ct);
            Console.WriteLine(pt);

            Console.ReadLine();
        }
	}
}
