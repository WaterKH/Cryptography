using System;

namespace CryptographyLibrary
{
	public class ADFGX
	{
		public string[,] alphabet { get; set; }
		public string key { get; set; }

		private readonly string typeADFGX = "ADFGX";
		private readonly string typeNormal = "Normal";
		private readonly string typeEncrypt = "Encrypt";
		private readonly string typeDecrypt = "Decrypt";

		public ADFGX (string[,] anAlpha, string aKey)
		{
			alphabet = anAlpha;
			key = aKey;
		}

		public ADFGX (string unformAlpha, string aKey)
		{
			alphabet = this.constructAlphabet(unformAlpha);
			key = aKey;
		}

		/// <summary>
		/// Encrypt the specified text.
		/// </summary>
		/// <param name="text">Text.</param>
		public string encrypt(string text)
		{
			string encryptedText = "";

			string transposedText = this.transpose (text, typeEncrypt);
			encryptedText = this.substituteAlphabet (transposedText, typeADFGX);

			return encryptedText;
		}

		/// <summary>
		/// Decrypt the specified text.
		/// </summary>
		/// <param name="text">Text.</param>
		public string decrypt(string text)
		{
			string decryptedText = "";

			string transposedText = this.transpose (text, typeDecrypt);
			decryptedText = this.substituteAlphabet (transposedText, typeNormal);

			return decryptedText;
		}


		private string transpose(string text, string type_alphabet)
		{
			string transposedText = "";

			switch (type_alphabet) 
			{
			case "ADFGX":
				//TODO implement
				break;
			case "Normal":
				//TODO implement
				break;
			default:
				break;
			}

			// TODO Perform transposition

			return transposedText;
		}

		private string substituteAlphabet(string text, string type_crypt)
		{
			string encryptedText = "";

			switch (type_crypt) 
			{
			case "Encrypt":
				//TODO implement
				break;
			case "Decrypt":
				//TODO implement
				break;
			default:
				break;
			}

			// TODO Perform substitution

			return encryptedText;
		}

		/// <summary>
		/// Constructs the alphabet from a string.
		/// </summary>
		/// <returns>The alphabet.</returns>
		/// <param name="unformAlpha">Unform alpha.</param>
		private string[,] constructAlphabet(string unformAlpha)
		{
			string[,] formAlpha = new string[5, 5];

			int counter = 0;

			for(int i = 0; i < 5; ++i)
			{
				for(int j = 0; j < 5; ++j)
				{
					formAlpha[i,j] = unformAlpha[counter].ToString();
					++counter;
				}
			}

			return formAlpha;
		}
	}
}

