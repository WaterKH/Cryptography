using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace CryptographyLibrary
{
	public class HillClimbingSolution
	{
		//public static string alphabet_MAIN = "etaoinsrhldcumf";//p g w y b v k x j q z;

		/* Function: HillClimb
		 * Params: string alphabet - The alphabet that will be used to produce the ciphertext. This will be changed
		 *	due to the hill climbing cipher.
		 *		   string ciphertext - The current iteration of the transposition created cryptograms
		 *		   Dictionary<char, double> frequencies - The frequencies of each of the ciphertext letters. This
		 *	will hopefully give a better score as we are multiplying the scores by this number.
		 *
		 *
		 * Description: Incorporates the hill climbing algorithm. Essentially, we choose two random letters in our
		 *	alphabet and swap them. This will yield a different plaintext. We then test the trigrams in the 
		 *	plaintext to see if we have better "sounding" words than we had before. Another added bonus is that
		 *	we tell the algorithm which letters can be what based on the letter frequencies, contact letters,
		 *	and MACCs. This will limit the plaintext to be of a more targeted, controlled sequence.
		 */
		public static void HillClimb (string alphabet, string ciphertext//, Dictionary<char, double> frequencies 
																		/*Dictionary<string, int> trigrams/*, Dictionary<char, string> sets*/)
		{
			int iteration = 0;
			int totalIters = 40;
			List<string> output = new List<string> ();
			string ctToAdd = "";
			ciphertext = Utilities.RemoveSpecialCharacters (ciphertext);

			while (iteration <= totalIters) {
				Console.WriteLine ("ITERATION: " + iteration + " OF " + totalIters + "; CIPHERTEXT: " + ciphertext);
				Random rand = new Random ();
				StringBuilder ctAlphabet = new StringBuilder (alphabet);
				double bestScore = -2;
				int count = 0;

				// We limit our search to 100,000 iterations since its last kept score
				while (count != 10000) {
					// Grab two random letters
					char a = ctAlphabet [rand.Next (0, alphabet.Length)];
					char b = ctAlphabet [rand.Next (0, alphabet.Length)];

					Dictionary<char, char> ct = new Dictionary<char, char> ();

					// If these letters are not the same
					if (a != b) {
						StringBuilder copy = new StringBuilder (ctAlphabet.ToString ());
						string copyStr = copy.ToString ();

						// Get the index of what letters we want to swap
						int indexA = copyStr.IndexOf (a);
						int indexB = copyStr.IndexOf (b);

						// Swap 'b' for 'a'
						copy.Remove (indexA, 1);
						copy.Insert (indexA, b);

						// Swap 'a' for 'b'
						copy.Remove (indexB, 1);
						copy.Insert (indexB, a);

						// TODO Assign a set of letters to each ciphertext letter to limit the search
						string newCipherText = "";
						string tempA = Utilities.RemoveRepLetters (ciphertext);

						// Grab all of the letters of the alphabet we are using...
						for (int i = 0; i < tempA.Length; ++i) {
							ct.Add (tempA [i], copy.ToString () [i]);
						}

						// And use them to create a new alphabet
						for (int i = 0; i < ciphertext.Length; ++i) {
							newCipherText += ct [ciphertext [i]];
						}

						/* Get Score of Ciphertext */
						double score = fitnessFunction (newCipherText);
						//double score = GenericScoring.ScoringFunctions.CalculateChiSquaredScore(newCipherText);
						//double score = GenericScoring.ScoringFunctions.DetectTopZombieWordsScore(newCipherText);

						if (score > bestScore) {
							bestScore = score;
							ctAlphabet = copy;
							count = 0;
							ctToAdd = newCipherText;

							Console.WriteLine (newCipherText);
						} else {
							//Console.WriteLine("SCORE IS LESS THAN BEST SCORE " + score + " " + bestScore);
							++count;
						}

					}
				}
				output.Add (ctToAdd);
				++iteration;
			}

			File.AppendAllLines ("final_output.txt", output);
		}

		// TODO Write what this does - TODO Multiply each letter of the Ngram by the monogram?
		public static double fitnessFunction (string newCipherText)
		{
			double score = 0.0;

			// Quadgrams
			for (int i = 0; i < newCipherText.Length - 3; ++i) {
				string temp = newCipherText.Substring (i, 4);
				if (quadgrams.ContainsKey (temp))
					score += quadgrams [temp];
			}

			return score;
		}
		static Dictionary<string, double> quadgrams = new Dictionary<string, double> ();
		public void CreateQuadgrams ()
		{
			List<string> l = new List<string> ();
			l.AddRange (File.ReadAllLines ("quadgrams.txt"));

			foreach (string s in l) {
				string [] sA = s.Split (' ');

				quadgrams.Add (sA [0], double.Parse (sA [1]));
			}
		}
	}
}

