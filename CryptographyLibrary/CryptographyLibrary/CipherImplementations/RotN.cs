using System;

namespace CryptographyLibrary
{
	public class RotN
	{
		string alphabet_lower = "abcdefghijklmnopqrstuvwxyz";
		string alphabet_upper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
		string digits = "0123456789";

		/// <summary>
		/// Encrypt the specified plaintext.
		/// </summary>
		/// <param name="plaintext">Plaintext.</param>
		public string Encrypt(string plaintext)
		{
			int N = new Random().Next(0, 25);

			string ciphertext = this.Encrypt(plaintext, N);

			return ciphertext;
		}

		/// <summary>
		/// Encrypt the specified plaintext and N.
		/// </summary>
		/// <param name="plaintext">Plaintext.</param>
		/// <param name="N">N.</param>
		public string Encrypt(string plaintext, int N)
		{
			string ciphertext = "";

			for(int i = 0; i < plaintext.Length; ++i)
			{
				char letter = plaintext[i];
				char newLetter = this.DetermineChar(letter, N, true);

				if(newLetter != '~')
				{
					ciphertext += newLetter;
				}
				else
				{
					ciphertext += plaintext[i];
				}
			}

			return ciphertext;
		}
		/// <summary>
		/// Decrypt the specified ciphertext.
		/// </summary>
		/// <param name="ciphertext">Ciphertext.</param>
		public string[] Decrypt(string ciphertext)
		{
			string[] allPlaintexts = new string[26];

			for(int outer = 0; outer < allPlaintexts.Length; ++outer)
			{
				string plaintext = this.Decrypt(ciphertext, outer);

				allPlaintexts[outer] = plaintext;
			}

			return allPlaintexts;
		}
		/// <summary>
		/// Decrypt the specified ciphertext and N.
		/// </summary>
		/// <param name="ciphertext">Ciphertext.</param>
		/// <param name="N">N.</param>
		public string Decrypt(string ciphertext, int N)
		{
			string plaintext = "";
			for(int i = 0; i < ciphertext.Length; ++i)
			{
				char letter = ciphertext[i];
				char newLetter = this.DetermineChar(letter, N, false);

				if(newLetter != '~')
				{
					plaintext += newLetter;
				}
				else
				{
					plaintext += ciphertext[i];
				}	
			}

			return plaintext;
		}
		/// <summary>
		/// Determines the char.
		/// </summary>
		/// <returns>The char.</returns>
		/// <param name="letter">Letter.</param>
		/// <param name="N">N.</param>
		/// <param name="encrypt">If set to <c>true</c> encrypt.</param>
		private char DetermineChar(char letter, int N, bool encrypt)
		{
			char newLetter = '~';

			if(encrypt)
			{
				if(isAlpha_lower(letter))
				{
					newLetter = alphabet_lower[(alphabet_lower.IndexOf(letter) + N) % alphabet_lower.Length];
				}
				else if(isAlpha_upper(letter))
				{
					newLetter = alphabet_upper[(alphabet_upper.IndexOf(letter) + N) % alphabet_upper.Length];
				}
				else if(isDigit(letter))
				{
					newLetter = digits[(digits.IndexOf(letter) + N) % digits.Length];
				}
			}
			else
			{
				if(isAlpha_lower(letter))
				{
					newLetter = alphabet_lower[((alphabet_lower.IndexOf(letter) - N) + alphabet_lower.Length) % alphabet_lower.Length];
				}
				else if(isAlpha_upper(letter))
				{
					newLetter = alphabet_upper[((alphabet_upper.IndexOf(letter) - N) + alphabet_upper.Length) % alphabet_upper.Length];
				}
				else if(isDigit(letter))
				{
					// TODO UGLY!!! Fix immediately					
					newLetter = digits[((digits.IndexOf(letter) - N) + 2 * digits.Length) % digits.Length];
				}
			}

			return newLetter;
		}
		/// <summary>
		/// Ises the digit.
		/// </summary>
		/// <returns><c>true</c>, if digit was ised, <c>false</c> otherwise.</returns>
		/// <param name="c">C.</param>
		private bool isDigit(char c)
		{
			if(c >= '0' && c <= '9')
			{
				return true;
			}
			return false;
		}
		/// <summary>
		/// Ises the alpha lower.
		/// </summary>
		/// <returns><c>true</c>, if alpha lower was ised, <c>false</c> otherwise.</returns>
		/// <param name="c">C.</param>
		private bool isAlpha_lower(char c)
		{
			if(c >= 'a' && c <= 'z')
			{
				return true;
			}
			return false;
		}
		/// <summary>
		/// Ises the alpha upper.
		/// </summary>
		/// <returns><c>true</c>, if alpha upper was ised, <c>false</c> otherwise.</returns>
		/// <param name="c">C.</param>
		private bool isAlpha_upper(char c)
		{
			if(c >= 'A' && c <= 'Z')
			{
				return true;
			}
			return false;
		}
	}
}

