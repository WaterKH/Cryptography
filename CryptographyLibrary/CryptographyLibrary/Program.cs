using System;

namespace CryptographyLibrary
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			string alphabet = "abcdefghiklmnopqrstuvwxyz";
			string key = "zombie";

			ADFGX adfgx = new ADFGX(alphabet, key);
		}
	}
}
