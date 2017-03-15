using System;
namespace CryptographyLibrary
{
	public class Cryptanalysis
	{
		public string alphabet { get; set; }
		public string ciphertext { get; set; }

		public Cryptanalysis(string alpha, string cipher)
		{
			alphabet = alpha;
			ciphertext = cipher;
		}

		public void Analysis()
		{
			
		}
	}
}
