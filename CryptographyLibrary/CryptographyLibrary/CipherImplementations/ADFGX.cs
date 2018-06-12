using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CryptographyLibrary.CipherImplementations
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
        public ADFGX(){}
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
			var indexPairedToLetter = new Dictionary<char, int> ();

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
					indexPairedToLetter.Add (key [i], indexesToUse [i]);
					Console.WriteLine (indexesToUse [i]+ " " + key[i]);
				}
				break;
			default:
				break;
			}

			int nextIndex = 0;

			for (int i = 0; i < sortedKey.Length; ++i) 
			{
				if (!listOfIndexesUsed.Contains (i) && indexesToUse[i] == nextIndex) 
				{
					var indexOfKeyLetter = indexesToUse [i];

					// ref used here so we do not return anything, but manipulate the matrix
					SwapColumns (ref unformattedText, i, indexOfKeyLetter);

					listOfIndexesUsed.Add (i);
					++nextIndex;
					i = nextIndex;
				}
			}

			Console.WriteLine ("Print Columns");
			Utilities.PrintWithSpacesBtwnChars (sortedKey);
			Console.WriteLine ("---------------------------------");
			Utilities.PrintDoubleArray (unformattedText);

			for (int i = 0; i < unformattedText.Length; ++i)
			{
				for (int j = 0; j < unformattedText[i].Length; ++j)
				{
					transposedText += unformattedText [i] [j];
				}
			}
			Console.WriteLine (transposedText);
			return transposedText;
		}

		/// <summary>
		/// Swaps the columns.
		/// </summary>
		/// <param name="matrix">ADFGX matrix.</param>
		/// <param name="index1">Index of the first column</param>
		/// <param name="index2">Index of the second column</param>
		private void SwapColumns(ref string[][] matrix, int index1, int index2)
		{
			for (int i = 0; i < matrix.Length; ++i)
			{
				var temp = matrix [i][index1];
				matrix [i][index1] = matrix [i][index2];
				matrix [i][index2] = temp;
			}
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
			if (rem > 0) 
			{
				numberOfRows += 1;
			}

			string [] [] unformattedText = new string [numberOfRows] [];

			for (int i = 0; i < unformattedText.GetLength (0); ++i) 
			{
				unformattedText [i] = new string [key.Length];
			}
			int j = 0;

			//TODO FIX THIS

			for (int i = 0; i < text.Length; ++i) {
				if (i % numberOfRows == 0 && i != 0) {
					++j;
				}

				if (j >= rem)
				{
					unformattedText [i % (numberOfRows - 1)] [j] = text.Substring (i, 1);
				} 
				else 
				{
					unformattedText [i % numberOfRows] [j] = text.Substring (i, 1);
				}
			}

			Console.WriteLine ("Print Columns");
			Utilities.PrintWithSpacesBtwnChars (key);
			Console.WriteLine ("---------------------------------");
			Utilities.PrintDoubleArray (unformattedText);

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
				for (int i = 0; i < text.Length; i += 2) // NOTE!! += 2 instead of +
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

