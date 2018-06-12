using System;

namespace CryptographyLibrary.CipherImplementations
{
	public class Affine
	{
		string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
		string affine_Alpha = "";

		public string Encrypt(string plaintext, int a, int b)
		{
			if (Math.Abs((a / alphabet.Length) % 1) > Double.Epsilon)
			{
				Console.WriteLine("a is not coprime with the alphabet length. Returning Null value.");
				return "";
			}
			string ciphertext = "";

			this.setAffineAlpha(a, b);

			foreach(char c in plaintext)
			{
				ciphertext += affine_Alpha[alphabet.IndexOf(c)];
			}

			return ciphertext;
		}

		public string Decrypt(string ciphertext, int a, int b)
		{
			if (Math.Abs((a / alphabet.Length) % 1) > Double.Epsilon)
			{
				Console.WriteLine("a is not coprime with the alphabet length. Returning Null value.");
				return "";
			}
			string plaintext = "";

			this.setAffineAlpha(a, b);

			foreach(char c in ciphertext)
			{
				plaintext += affine_Alpha[alphabet.IndexOf(c)];
			}

			return plaintext;
		}

		public void setAffineAlpha(int a, int b)
		{
			for(int i = 0; i < alphabet.Length; ++i)
			{
				affine_Alpha += (a * alphabet.IndexOf(alphabet[i]) + b) % alphabet.Length;
			}
		}
	}
}

