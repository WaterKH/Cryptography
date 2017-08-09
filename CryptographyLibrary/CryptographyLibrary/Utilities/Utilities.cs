using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptographyLibrary
{
    static class Utilities
	{
		public static void PrintDoubleArray<T>(IEnumerable<T>[][] arr)
		{
			foreach(var outer in arr)
			{
				foreach(var inner in outer)
				{
					Console.Write (inner + " ");
				}
				Console.WriteLine ();
			}
		}

		public static string RemoveRepLetters(string text)
		{
			return new string (text.ToCharArray ().Distinct ().ToArray ());
		}

        public static Dictionary<string, int> Frequency(string text, char parseBy)
        {
            var freq = new Dictionary<string, int>();

            foreach (var l in text.Split(parseBy))
            {
                if (!freq.ContainsKey(l))
                {
                    freq.Add(l, 0);
                }
                ++freq[l];
            }

            return freq;
        }

        public static bool ContainsSpecialCharacters(string word)
        {
            foreach (char c in word)
            {
                if (!(c >= 'A' && c <= 'Z') && !(c >= 'a' && c <= 'z'))
                {
                    return true;
                }
            }
            return false;
        }

        public static double CalculateIOC_Polygraphic(Dictionary<string, int> freq)
        {
            double ioc = 0.0;
            int totalPolygraphs = 0;

            foreach(KeyValuePair<string, int> kv in freq)
            {
                ioc += kv.Value * (kv.Value - 1);
                totalPolygraphs += kv.Value;
            }
            Console.WriteLine(totalPolygraphs);
            ioc /= totalPolygraphs * (totalPolygraphs - 1);

            return ioc;
        }

        public static double CalculateIOC(string text, int keyLength)
        {
            text = RemoveSpecialCharacters(text).ToLower();

            //double[] ioc = new double[keyLength];
            string[] sepText = new string[keyLength];
            double value = 0.0;

            for(int i = 0; i < text.Length; ++i)
            {
                sepText[i % keyLength] += text[i];
            }

            for(int i = 0; i < sepText.Length; ++i)
            {
                value += IndexOfCoincidence(sepText[i]);
            }
            value /= sepText.Length;

            return value;
        }

        public static double IndexOfCoincidence(string text)
        {
            text = RemoveSpecialCharacters(text).ToLower();

            double ioc = 0.0;
            double sum = 0.0;
            double div = text.Length * (text.Length - 1);
            string alphabet = new String(text.Distinct().ToArray());

            foreach (var c in alphabet)
            {
                int freq = 0;
                for (int index = text.IndexOf(c); index >= 0; index = text.IndexOf(c, index + 1))
                {
                    ++freq;
                }

                sum += freq * (freq - 1);
            }

            ioc = sum / div;

            return ioc;
        }

        //http://stackoverflow.com/questions/1120198/most-efficient-way-to-remove-special-characters-from-string
        public static string RemoveSpecialCharacters(string str)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in str)
            {
                if ((c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z'))
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }

        public static IEnumerable<List<T>> GetPermutations<T>(this List<T> items, bool repeating = false)
        {
            foreach (var i in GetPermutations(items, new List<T>(), 0, repeating))
            {
                yield return i;
            }
        }
        public static IEnumerable<List<T>> GetPermutations<T>(this List<T> items, int limit, bool repeating = false)
        {
            foreach (var i in GetPermutations(items, new List<T>(), limit, repeating))
            {
                yield return i;
            }
        }

        public static string ConvertToAlphaCharacters(string word)
        {
            string alphabet = "abcdefghijklmnopqrstuvwxyz";
            Dictionary<char, char> holder = new Dictionary<char, char>();
            int index = 0;
            string alphaChars = "";

            foreach (char c in word)
            {
                if(!holder.ContainsKey(c))
                {
                    holder.Add(c, alphabet[index]);
                    ++index;
                }
                alphaChars += holder[c];
            }

            return alphaChars;
        }

        //OXIN STUFF
        private static IEnumerable<List<T>> GetPermutations<T>(this List<T> items, List<T> currentItems, int limit, bool repeating = false)
        {
            if (items.Count() == 0 || (limit > 0 && currentItems.Count == limit))
            {
                yield return currentItems;
            }
            else
            {
                if (repeating)
                {
                    foreach (var item in items)
                    {
                        currentItems.Add(item);
                        foreach (var i in GetPermutations(items, currentItems, limit, repeating))
                        {
                            yield return i;
                        }
                        currentItems.RemoveAt(currentItems.Count - 1);
                    }
                }
                else
                {
                    var availableItems = items.ToList();
                    foreach (var item in availableItems)
                    {
                        currentItems.Add(item);
                        items.Remove(item);
                        foreach (var i in GetPermutations(items, currentItems, limit, repeating))
                        {
                            yield return i;
                        }
                        items.Add(item);
                        currentItems.RemoveAt(currentItems.Count - 1);
                    }
                }
            }
        }

        public static IEnumerable<T[]> GetPermutations<T>(this T[] items, bool repeating = false)
        {
            foreach (var i in GetPermutations(items.ToList(), new List<T>(), 0, repeating))
            {
                yield return i.ToArray();
            }
        }
        public static IEnumerable<T[]> GetPermutations<T>(this T[] items, int limit, bool repeating = false)
        {
            foreach (var i in GetPermutations(items.ToList(), new List<T>(), limit, repeating))
            {
                yield return i.ToArray();
            }
        }
    }
}
