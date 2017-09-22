using System;
using System.Collections.Generic;
using System.Linq;

namespace CryptographyLibrary
{
	public class ADFGX
	{
		public string[,] alphabet { get; set; }
		public string key { get; set; }

		private readonly string typeToADFGX = "ToADFGX";
		private readonly string typeToNormal = "ToNormal";
		private readonly string typeEncrypt = "Encrypt";
		private readonly string typeDecrypt = "Decrypt";

		//public ADFGX (string[,] anAlpha, string aKey)
		//{
		//	alphabet = anAlpha;
		//	key = aKey;
		//}

		public ADFGX (string unformAlpha, string aKey)
		{
			alphabet = this.ConstructAlphabet(unformAlpha);
			key = aKey;
		}

		/// <summary>
		/// Encrypt the specified text.
		/// </summary>
		/// <param name="text">Text.</param>
		public string Encrypt(string text)
		{
			string transposedEncryptedText = "";

			string encryptedText = this.SubstituteAlphabet (text, typeEncrypt);
			transposedEncryptedText = this.Transpose (encryptedText, typeToADFGX);

			return transposedEncryptedText;
		}

		/// <summary>
		/// Decrypt the specified text.
		/// </summary>
		/// <param name="text">Text.</param>
		public string Decrypt(string text)
		{
			string decryptedText = "";

			string transposedText = this.Transpose (text, typeToNormal);
			decryptedText = this.SubstituteAlphabet (transposedText, typeDecrypt);

			return decryptedText;
		}


		private string Transpose(string text, string type_alphabet)
		{
			string transposedText = "";
			string sortedKey = "";
			var listOfIndexesUsed = new List<int> ();
			var listOfLengths = new List<int> ();

			var tempKey = key.ToList ();
			tempKey.Sort ();
			tempKey.ForEach (x => sortedKey += x); // Assign sorted key to variable

			var unformattedText = SetUpColumns (text);

			int [] indexesToUse = new int [key.Length];

			switch (type_alphabet) 
			{
			case "ToADFGX":

				for (int i = 0; i < sortedKey.Length; ++i)
				{
					indexesToUse [i] = key.IndexOf (sortedKey [i]);
				}

				break;
			case "ToNormal":
				for (int i = 0; i < sortedKey.Length; ++i) 
				{
					indexesToUse [i] = sortedKey.IndexOf (key [i]);
				}
				break;
			default:
				break;
			}

			for (int i = 0; i < sortedKey.Length; ++i) 
			{
				if (!listOfIndexesUsed.Contains (i)) 
				{
					var indexOfKeyLetter = indexesToUse [i];

					var temp = unformattedText [i];
					unformattedText [i] = unformattedText [indexOfKeyLetter];
					unformattedText [indexOfKeyLetter] = temp;

					listOfIndexesUsed.Add (i);
					listOfIndexesUsed.Add (indexOfKeyLetter);
				}

				listOfLengths.Add (unformattedText [i].Length);
			}

			int max = 0;

			for (int i = 0; i < unformattedText.Length; ++i) 
			{
				if (unformattedText[i].Length > max)
				{
					max = unformattedText [i].Length;
				}
			}
			//TODO Horrible way; fix later
			for (int i = 0; i < max; ++i)
			{
				for (int j = 0; j < unformattedText.Length; ++j)
				{
					if (i >= unformattedText[j].Length)
						break;
					transposedText += unformattedText [j] [i];
				}
			}

			return transposedText;
		}

		/// <summary>
		/// Sets up columns.
		/// </summary>
		/// <returns>The up columns.</returns>
		/// <param name="text">Text.</param>
		private string[][] SetUpColumns(string text)
		{
			int numberOfRows = (text.Length / key.Length);
			int rem = text.Length % key.Length;

			//Console.WriteLine (numberOfRows);
			string [] [] unformattedText = new string [key.Length] [];

			for (int i = 0; i < unformattedText.GetLength (0); ++i) 
			{
				if (i >= rem) 
				{
					unformattedText [i] = new string [numberOfRows];
				} 
				else
				{
					unformattedText [i] = new string [numberOfRows + 1];
				}
			}
			int j = 0;

			//TODO FIX THIS

			for (int i = 0; i < text.Length; ++i) 
			{
				if (i % key.Length == 0 && i != 0) 
				{
					++j;
				}
				unformattedText [i % key.Length][j] = text.Substring (i, 1);
			}

			Console.WriteLine ("Test");
			WaterkhUtilities.PrintDoubleArray (unformattedText);

			return unformattedText;
		}

		/// <summary>
		/// Substitutes the letters from ADFGX to plaintext or vice versa.
		/// </summary>
		/// <returns>The converted text from plaintext to ADFGX or vice versa.</returns>
		/// <param name="text">Text that you want to convert.</param>
		/// <param name="type_crypt">Type crypt determines if it is Encrypting or Decrypting.</param>
		private string SubstituteAlphabet(string text, string type_crypt)
		{
			string substitutedText = "";

			switch (type_crypt) 
			{
			case "Encrypt":
				foreach (var letter in text) 
				{
					substitutedText += LetterToADFGX (letter.ToString ().ToLower ());
				}
				break;
			case "Decrypt":
				for (int i = 0; i < text.Length; i += 2) // NOTE!! += 2 instead of ++
				{
					var letterPair = text.Substring(i, 2);

					substitutedText += ADFGXToLetter (letterPair);
				}
				break;
			default:
				break;
			}

			return substitutedText;
		}

		/// <summary>
		/// Converts a plaintext letter to an ADFGX letter pair.
		/// </summary>
		/// <returns>The letter pairs that index to a letter in the ADFGX square.</returns>
		/// <param name="letter">Letter from plaintext.</param>
		private string LetterToADFGX(string letter)
		{
			string letterPair = "";

			int [] indexOfLetter = IndexOfLetter (letter);

			foreach(var i in indexOfLetter)
			{
				switch(i)
				{
				case 0:
					letterPair += "A";
					break;
				case 1:
					letterPair += "D";
					break;
				case 2:
					letterPair += "F";
					break;
				case 3:
					letterPair += "G";
					break;
				case 4:
					letterPair += "X";
					break;
				default:
					break;
				}
			}

			return letterPair;
		}

		/// <summary>
		/// Indexes of letter.
		/// </summary>
		/// <returns>The indexes of the letter.</returns>
		/// <param name="letter">Letter(s) that are either plaintext or ciphertext.</param>
		private int[] IndexOfLetter(string letter)
		{
			int [] indexes = new int [2];
			int j = 0;
			int modLength = alphabet.GetLength (0);

			for (int i = 0; i < alphabet.Length; ++i) 
			{
				if (i % modLength == 0 && i != 0) 
				{
					++j;
				}

				if (alphabet [j, i % modLength] == letter.ToLower())
				{
					indexes [0] = j;
					indexes [1] = i % modLength;
					break;
				}
			}

			return indexes;
		}

		/// <summary>
		/// Converts an ADFGX letter pair to a letter.
		/// </summary>
		/// <returns>The letter comes from the ADFGX letter pairs.</returns>
		/// <param name="letter">ADFGX letter pair.</param>
		private string ADFGXToLetter (string letterPair)
		{
			//string plaintextLetter = "";

			int [] indexes = new int [2];

			for (int i = 0; i < letterPair.Length; ++i)
			{
				string letter = letterPair [i].ToString();

				switch (letter) 
				{
				case "A":
					indexes [i] = 0;
					break;
				case "D":
					indexes [i] = 1;
					break;
				case "F":
					indexes [i] = 2;
					break;
				case "G":
					indexes [i] = 3;
					break;
				case "X":
					indexes [i] = 4;
					break;
				default:
					break;
				}
			}

			return alphabet [indexes [0], indexes [1]];
			//plaintextLetter += alphabet [indexes [0], indexes [1]];

			//return plaintextLetter;
		}

		/// <summary>
		/// Constructs the alphabet from a string.
		/// </summary>
		/// <returns>The alphabet.</returns>
		/// <param name="unformAlpha">Unform alpha.</param>
		private string[,] ConstructAlphabet(string unformAlpha)
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

