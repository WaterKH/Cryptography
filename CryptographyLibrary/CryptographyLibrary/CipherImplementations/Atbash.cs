using System;

namespace CryptographyLibrary
{
	public class Atbash
	{
		string alphabet = "abcdefghijklmnopqrstuvwxyz";
		string atbash_Alpha = "zyxwvutsrqponmlkjihgfedcba";

		/// <summary>
		/// Encrypt the specified plaintext.
		/// </summary>
		/// <param name="plaintext">Plaintext.</param>
		public string Encrypt(string plaintext)
		{
			string ciphertext = "";

			foreach(char c in plaintext)
			{
				if(c >= 'A' && c <= 'Z')
				{
					ciphertext += char.ToUpper(atbash_Alpha[alphabet.IndexOf(char.ToLower(c))]);
				}
				else if(c >= 'a' && c <= 'z')
				{
					ciphertext += atbash_Alpha[alphabet.IndexOf(c)];
				}
				else
				{
					ciphertext += c;
				}
			}

			return ciphertext;
		}

		/// <summary>
		/// Decrypt the specified ciphertext.
		/// </summary>
		/// <param name="ciphertext">Ciphertext.</param>
		public string Decrypt(string ciphertext)
		{
			string plaintext = "";

			foreach(char c in ciphertext)
			{
				if(c >= 'A' && c <= 'Z')
				{
					plaintext += char.ToUpper(alphabet[atbash_Alpha.IndexOf(char.ToLower(c))]);
				}
				else if(c >= 'a' && c <= 'z')
				{
					plaintext += alphabet[atbash_Alpha.IndexOf(c)];
				}
				else
				{
					plaintext += c;
				}
			}

			return plaintext;
		}
	}
}

