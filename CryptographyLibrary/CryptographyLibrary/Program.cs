using System;

namespace CryptographyLibrary
{
	class MainClass
	{
		public static void Main (string[] args)
		{
            /*string alphabet = "abcdefghiklmnopqrstuvwxyz";
			string key = "zombie";

			ADFGX adfgx = new ADFGX(alphabet, key);
            */

            string ciphertext = "Aazek haafx, eh bo o ckzeu uhkr wwm, keuapzr oc?";
            int maxKeyLength = 3;

            for (int i = 1; i <= maxKeyLength; ++i)
            {
                double ioc = Utilities.CalculateIOC(ciphertext, i);

                Console.WriteLine("Key size: " + i);
                Console.WriteLine(ioc);
                Console.WriteLine();
            }
            Console.ReadLine();
        }
	}
}
