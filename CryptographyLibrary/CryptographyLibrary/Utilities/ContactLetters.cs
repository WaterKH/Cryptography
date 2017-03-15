using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptographyLibrary
{
    class ContactLetters
    {
        public Dictionary<string, Dictionary<string, int>> before = new Dictionary<string, Dictionary<string, int>>();
        public Dictionary<string, Dictionary<string, int>> after = new Dictionary<string, Dictionary<string, int>>();

		public ContactLetters ()
		{
			
		}

		public ContactLetters(string message)
		{
			this.FindContactLetters (message);
		}

		public void FindContactLetters(string text)
		{
			for (int i = 0; i < text.Length; ++i)
			{
				string currLetter = text.Substring (i, 1);
				string prevLetter, nextLetter;

				if((i - 1) < 0)
				{
					prevLetter = "-";
					nextLetter = text.Substring (i + 1, 1);
				} 
				else if((i + 1) == text.Length)
				{
					prevLetter = text.Substring (i - 1, 1);
					nextLetter = "-";
				} 
				else
				{
					prevLetter = text.Substring (i - 1, 1);
					nextLetter = text.Substring (i + 1, 1);
				}

				this.setAfter (currLetter, nextLetter);
				this.setBefore (currLetter, prevLetter);
			}
		}

        public Dictionary<string, int> getBefore(string letter)
        {
            if(before.ContainsKey(letter))
                return before[letter];
            return null;
        }
        public Dictionary<string, int> getAfter(string letter)
        {
            if (after.ContainsKey(letter))
                return after[letter];
            return null;
        }

        public void setBefore(string outer_letter, string inner_letter)
        {
            if(before.ContainsKey(outer_letter))
            {
                if(before[outer_letter].ContainsKey(inner_letter))
                {
                    ++before[outer_letter][inner_letter];
                }
                else
                {
                    before[outer_letter].Add(inner_letter, 1);
                }
            }
            else
            {
                var t = new Dictionary<string, int>();
                t.Add(inner_letter, 1);

                before.Add(outer_letter, t);
            }
        }
        public void setAfter(string outer_letter, string inner_letter)
        {
            if (after.ContainsKey(outer_letter))
            {
                if (after[outer_letter].ContainsKey(inner_letter))
                {
                    ++after[outer_letter][inner_letter];
                }
                else
                {
                    after[outer_letter].Add(inner_letter, 1);
                }
            }
            else
            {
                var t = new Dictionary<string, int>();
                t.Add(inner_letter, 1);

                after.Add(outer_letter, t);
            }
        }

		public void PrintContactLetters()
		{
			Console.WriteLine ();
			Console.WriteLine ("BEFORE CONTACT LETTERS");
			Console.WriteLine ();

			foreach (var outer_key in before)
			{
				foreach(var inner_key in outer_key.Value)
				{
					Console.WriteLine ("CURR LETTER: " + outer_key.Key + "\tPREV LETTER: " + inner_key.Key + "\tCOUNT: " + inner_key.Value); 
				}
			}

			Console.WriteLine ();
			Console.WriteLine ("AFTER CONTACT LETTERS");
			Console.WriteLine ();

			foreach (var outer_key in after)
			{
				foreach (var inner_key in outer_key.Value) 
				{
					Console.WriteLine ("CURR LETTER: " + outer_key.Key + "\tNEXT LETTER: " + inner_key.Key + "\tCOUNT: " + inner_key.Value);
				}
			}
		}
    }
}
