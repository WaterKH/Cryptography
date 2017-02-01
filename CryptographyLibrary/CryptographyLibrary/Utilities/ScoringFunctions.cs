using System;
using System.Collections.Generic;
using System.Linq;

namespace CryptographyLibrary
{
	public class ScoringFunctions
	{
		/* Holder for Bi-, Tri-, and Quadgrams */
		public List<Dictionary<string, double>> scores = new List<Dictionary<string, double>>();
		/* Bigrams, Trigrams, And Quadgrams */
		public Dictionary<string, double> trigramScores = new Dictionary<string, double>();
		public Dictionary<string, double> bigramScores = new Dictionary<string, double>(); 
		public Dictionary<string, double> quadgramScores = new Dictionary<string, double>();
		/* Letter Frequencies */
		public Dictionary<string, double> monogramScores = new Dictionary<string, double>();
		/* Contact Letters */
		//private static Dictionary<string, Dictionary<string, double>> contactLetters = new Dictionary<string, string>();
		public Dictionary<char, Dictionary<char, double>> contactLetters_AFTER = new Dictionary<char, Dictionary<char, double>>();
		public Dictionary<char, Dictionary<char, double>> contactLetters_BEFORE = new Dictionary<char, Dictionary<char, double>>();
		/* MACCs */
		public Dictionary<char, double> totalMaccs = new Dictionary<char, double>();

		/**********************************************************************
		 * Constructor: ScoringFunctions
		 *
		 * Description:
		 *  We add all of our ngram scores to the list to index into later.
		 */
		public ScoringFunctions()
		{
			scores.Add(monogramScores);
			scores.Add(bigramScores);
			scores.Add(trigramScores);
			scores.Add(quadgramScores);
		}

		/**********************************************************************
		 * Function: GetNgram
		 * Parameters: string ngram
		 *	ngram - One of the tri, bi, quad or monograms
		 * Return: double value
		 * 	value - Either the frequency of the ngram or 0.0 if it doesn't 
		 *	 exist.
		 *
		 * Description:
		 * 	We setup an int N that is the length of the ngram. We then index
		 * 	 into the scores List of Dictionaries to find the frequency of the
		 * 	 ngram.
		 */
		public double GetNgram(string ngram)
		{
			int N = ngram.Length;
			if(scores[N - 1].ContainsKey(ngram))
			{
				return scores[N - 1][ngram];
			}
			else
			{
				return 0.0;
			}
		}

		/**********************************************************************
		 * Function: GetBeforeContact
		 * Parameters: string currLetter, string nextLetter
		 * 	currLetter - The current letter we are using
		 *	nextLetter - The previous letter (in this case)
		 * Return: double value
		 * 	value - Either the frequency of the letter pair or 0.0 if it 
		 * 	 doesn't exist.
		 *
		 * Description:
		 * 	We index into the Dictionary to retrieve our letter frequency.
		 */
		public double GetBeforeContact(char currLetter, char nextLetter)
		{
			if(contactLetters_BEFORE[currLetter].ContainsKey(nextLetter))
			{
				return contactLetters_BEFORE[currLetter][nextLetter];
			}
			else
			{
				return 0.0;
			}
		}

		/**********************************************************************
		 * Function: GetAfterContact
		 * Parameters: string currLetter, string nextLetter
		 * 	currLetter - The current letter we are using
		 *	nextLetter - The following letter (in this case)
		 * Return: double value
		 * 	value - Either the frequency of the letter pair or 0.0 if it 
		 * 	 doesn't exist.
		 *
		 * Description:
		 * 	We index into the Dictionary to retrieve our letter frequency.
		 */
		public double GetAfterContact(char currLetter, char nextLetter)
		{
			if(contactLetters_AFTER[currLetter].ContainsKey(nextLetter))
			{
				return contactLetters_AFTER[currLetter][nextLetter];
			}
			else
			{
				return 0.0;
			}
		}

		/**********************************************************************
		 * Function: Ngrams
		 * Parameters: string line, int N
		 * 	line - The current line we are using to find Ngrams
		 *	N - The number that we will be searching for (ie - 1 = mono, 2 = bi, 
		 *	 3 = tri, & 4 = quad)
		 * Return: void
		 *
		 * Description:
		 * 	We obtain our current score for the ngram in question. We then run
		 * 	 a loop on the length minus our N - 1 to find all the ngrams in the
		 * 	 line. If it's not contained, we add it and then increase the score
		 * 	 everytime we iterate. We then reassign the score to our currScore.
		 */
		public void Ngrams(string line, int N)
		{
			Dictionary<string, double> currScore = scores[N - 1];
			string ngram = "";
			for(int i = 0; i < line.Length - (N - 1); ++i)
			{
				ngram = line.Substring(i, N);
				if(!currScore.ContainsKey(ngram))
				{
					currScore.Add(ngram, 0.0);
				}
				++currScore[ngram];
			}
			scores[N - 1] = currScore;
		}

		/**********************************************************************
		 * Function: ContactLetter
		 * Parameters: string line
		 * 	line - The current line we are using to find Ngrams
		 * Return: void
		 *
		 * Description:
		 * 	We run a loop on our current line's length and obtain our key, 
		 * 	 which is simply our current letter in the iteration. If it is not
		 * 	 zero, then we grab our before letter and add it to our dictionary.
		 * 	 If it's less than the second to last letter, we grab our after
		 * 	 letter and add it to our dictionary.
		 */
		public void ContactLetters(string line)
		{
			int end = line.Length - 1;
			for(int i = 0; i < line.Length; ++i)
			{
				//string key = line.Substring(i, 1);
				char key = line[i];

				if(i != 0)
				{
					//string before = line.Substring(i - 1, 1);
					char before = line[i - 1];
					if(!contactLetters_BEFORE.ContainsKey(key))
					{
						Dictionary<char, double> before_Contacts = new Dictionary<char, double>();
						before_Contacts.Add(before, 0.0);
						contactLetters_BEFORE.Add(key, before_Contacts);

					}
					else if(!contactLetters_BEFORE[key].ContainsKey(before))
					{
						contactLetters_BEFORE[key].Add(before, 0.0);
					}

					++contactLetters_BEFORE[key][before];
				}
				if(i < end)
				{
					//string after = line.Substring(i + 1, 1);
					char after = line[i + 1];
					if(!contactLetters_AFTER.ContainsKey(key))
					{
						Dictionary<char, double> after_Contacts = new Dictionary<char, double>();
						after_Contacts.Add(after, 0.0);
						contactLetters_AFTER.Add(key, after_Contacts);
					}
					else if(!contactLetters_AFTER[key].ContainsKey(after))
					{
						contactLetters_AFTER[key].Add(after, 0.0);
					}

					++contactLetters_AFTER[key][after];
				}
			}
		}

		/**********************************************************************
		 * Function: GetMACCs
		 * Parameters: string cipher
		 * 	
		 * Return: void
		 *
		 * Description:
		 * 	This is Oxin's Function, so if he wants to update this description
		 * 	 that's cool, but it basically finds the vowels of the plaintext if 
		 * 	 possible. Vowels will usually have the lowest double value.
		 */
		public void MACCs(string cipher)
		{
			char[] uniqueChars = cipher.ToCharArray().Distinct().ToArray();
			Dictionary<char, string> adjacentLetters = new Dictionary<char, string>();
			foreach (char c in uniqueChars)
			{
				adjacentLetters[c] = "";
			}
			adjacentLetters[cipher[0]] += cipher[1];
			adjacentLetters[cipher[cipher.Length - 1]] += cipher[cipher.Length - 2];
			for (int i = 1; i < cipher.Length - 1; i++)
			{
				adjacentLetters[cipher[i]] += string.Format("{0}{1}",cipher[i - 1],cipher[i + 1]);
			}
			Dictionary<char, string> adjacentLettersClean = new Dictionary<char, string>();
			foreach (var item in adjacentLetters)
			{
				if (item.Key != ' ')
				{
					adjacentLettersClean[item.Key] = new string(item.Value.Replace(" ", "").ToCharArray().Distinct().ToArray());
				}
			}
			//adjacentLettersClean.Dump();
			Dictionary<char, double> vccs = new Dictionary<char, double>();
			foreach (var item in adjacentLettersClean)
			{
				vccs[item.Key] = item.Value.ToCharArray().Where(x => x != ' ').Distinct().Count();
			}
			//vccs.Dump();
			Dictionary<char, double> maccs = new Dictionary<char, double>();
			foreach (var item in vccs)
			{
				double total = 0.0;
				foreach (char c in adjacentLettersClean[item.Key].Where(x => x != ' '))
				{
					total += vccs[c];
				}
				maccs[item.Key] = total / vccs[item.Key];
			}
			totalMaccs = maccs;
		}

		/* Print Functions */
		public void PrintMACCs()
		{
			Console.WriteLine("Entering MACCs");
			var keys = totalMaccs.Keys.ToList();
			var maccList = totalMaccs.ToList();

			maccList.Sort((x,y) => x.Value.CompareTo(y.Value));
			maccList.ForEach(x => Console.WriteLine(x));

			Console.WriteLine("Exiting MACCs");
			Console.WriteLine();
		}

		public void PrintContactLetters()
		{
			Console.WriteLine("Entering Contact Letters");
			Console.WriteLine("Before Contact Letters");
			var outerKeys_B = contactLetters_BEFORE.Keys.ToList();
			foreach(var o_key in outerKeys_B)
			{
				var innerKeys_B = contactLetters_BEFORE[o_key].Keys.ToList();
				foreach(var i_key in innerKeys_B)
				{
					Console.WriteLine("Outer: " + o_key + " Inner: " + i_key + " Value: " + contactLetters_BEFORE[o_key][i_key]);
				}
			}
			Console.WriteLine();
			Console.WriteLine("After Contact Letters");
			var outerKeys_A = contactLetters_AFTER.Keys.ToList();
			foreach(var o_key in outerKeys_A)
			{
				var innerKeys_A = contactLetters_AFTER[o_key].Keys.ToList();
				foreach(var i_key in innerKeys_A)
				{
					Console.WriteLine("Outer: " + o_key + " Inner: " + i_key + " Value: " + contactLetters_AFTER[o_key][i_key]);
				}
			}
			Console.WriteLine("Exiting Contact Letters");
			Console.WriteLine();
		}

		public void PrintNgrams()
		{
			Console.WriteLine("Entering Ngrams");
			foreach(Dictionary<string, double> d in scores)
			{
				var keys = d.Keys.ToList();
				foreach(var key in keys)
				{
					Console.WriteLine("Key: " + key + " Value: " + d[key]);
				}
			}
			Console.WriteLine("Exiting Ngrams");
			Console.WriteLine();
		}
	}
}

