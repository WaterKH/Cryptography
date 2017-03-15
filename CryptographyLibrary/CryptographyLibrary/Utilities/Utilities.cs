using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptographyLibrary
{
    class Utilities
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
    }
}
