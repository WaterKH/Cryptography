using System;
namespace CryptographyLibrary
{
	public class GroupCipher
	{
		public GroupCipher ()
		{
		}

		public string Encode(string plaintext, string key, string alphabet, string sequence)
		{
			string ct = "";

			for (int i = 0; i < plaintext.Length; ++i)
			{
				if (alphabet.Contains (char.ToString(plaintext [i]))) 
				{
					int index = alphabet.IndexOf (plaintext [i]);
					int seqIndex = sequence.IndexOf (sequence [i % sequence.Length]);

					switch (key [i % key.Length]) 
					{
					case '+':
						ct += alphabet [(index + seqIndex) % alphabet.Length];
						break;
					case '-':
						ct += alphabet [((index - seqIndex) + alphabet.Length) % alphabet.Length];
						break;
					case '%':
						if (index == 0)
							ct += alphabet [0];
						else
							ct += alphabet [index % seqIndex];
						break;
					case '*':
						ct += alphabet [(index * seqIndex) % alphabet.Length];
						break;
					default:
						break;
					}
				}
				else
				{
					ct += plaintext [i];
				}
			}

			return ct;
		}

		public string Decode (string ciphertext, string key, string alphabet, string sequence)
		{
			string pt = "";

			for (int i = 0; i < ciphertext.Length; ++i) 
			{
				if (alphabet.Contains (char.ToString (ciphertext [i]))) 
				{
					int index = alphabet.IndexOf (ciphertext [i]);
					int seqIndex = sequence.IndexOf (sequence [i % sequence.Length]);

					switch (key [i % key.Length]) 
					{
					case '+':
						Console.WriteLine (index + " - " + seqIndex + " = " + (index - seqIndex));
						pt += alphabet [((index - seqIndex) + alphabet.Length) % alphabet.Length];
						break;
					case '-':
						pt += alphabet [(index + seqIndex) % alphabet.Length];
						break;
					case '%':
						pt += alphabet [(index * seqIndex) % alphabet.Length];
						break;
					case '*':
						pt += alphabet [index % seqIndex];
						break;
					default:
						break;
					}
				}
				else 
				{
					pt += ciphertext [i];
				}
			}

			return pt;
		}
	}
}
