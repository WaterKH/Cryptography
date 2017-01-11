using System;
using System.Collections.Generic;

namespace CryptographyLibrary
{
	class MainClass
	{
		public static void Main (string[] args)
		{
            string message = "AZSXS POOJL GDHDW RZRMA ZYIRB RBIRN MLEFZ CYHML DIIX";

            for (int i = 1; i <= 10; ++i)
            {
                double ioc = Utilities.CalculateIOC(message, i);

                Console.WriteLine(ioc);
            }
            Console.ReadLine();
        }
	}
}
