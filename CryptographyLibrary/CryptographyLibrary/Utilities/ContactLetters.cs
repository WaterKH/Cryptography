using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptographyLibrary
{
    class ContactLetters
    {
        public Dictionary<string, Dictionary<string, int>> before = new Dictionary<string, Dictionary<string, int>>();
        public Dictionary<string, Dictionary<string, int>> after = new Dictionary<string, Dictionary<string, int>>();
        public string alphabet = "abcdefghiklmnopqrstuvwxyz ";


		public ContactLetters ()
		{
			
		}

		public ContactLetters(string message)
		{
			this.FindContactLetters (message);
		}

        public void FindContactLetters(string file)
        {
            string prevLetter, nextLetter, currLetter = "";

            using (StreamReader reader = new StreamReader(file))
            {
                string line = "";
                while ((line = reader.ReadLine()) != null)
                {
                    var sb = new StringBuilder(line.ToLower());
                    
                    for (int i = 0; i < sb.Length; ++i)
                    {
                        if(sb.Length < 2)
                        {
                            break;
                        }

                        currLetter = sb[i].ToString();// line.Substring(i, 1);

                        if ((i - 1) < 0)
                        {
                            prevLetter = " ";
                            nextLetter = sb[i + 1].ToString();// line.Substring(i + 1, 1);
                        }
                        else if ((i + 1) == sb.Length)
                        {
                            prevLetter = sb[i - 1].ToString();// line.Substring(i - 1, 1);
                            nextLetter = " ";
                        }
                        else
                        {
                            prevLetter = sb[i - 1].ToString();// line.Substring(i - 1, 1);
                            nextLetter = sb[i + 1].ToString();// line.Substring(i + 1, 1);
                        }

                        if (!alphabet.Contains(currLetter))
                        {
                            sb.Remove(i, 1);
                            --i;
                            continue;
                        }
                        else if (!alphabet.Contains(prevLetter))
                        {
                            sb.Remove(i - 1, 1);
                            --i;
                            continue;
                        }
                        else if (!alphabet.Contains(nextLetter))
                        {
                            sb.Remove(i + 1, 1);
                            --i;
                            continue;
                        }

                        this.setAfter(currLetter, nextLetter);
                        this.setBefore(currLetter, prevLetter);
                    }
                }
            }
        }

        /*public void FindContactLetters(string line, string alphabet = "abcdefghijklmnopqrstuvwxyz ")
		{
            string prevLetter, nextLetter, currLetter = "";
            var sb = new StringBuilder(line.ToLower());

            for (int i = 0; i < sb.Length; ++i)
            {
                currLetter = sb[i].ToString();// line.Substring(i, 1);
                if (!alphabet.Contains(currLetter))
                {
                    sb.Remove(i, 1);
                    --i;
                    continue;
                }

                if ((i - 1) < 0)
                {
                    prevLetter = " ";
                    nextLetter = sb[i + 1].ToString();// line.Substring(i + 1, 1);
                }
                else if ((i + 1) == sb.Length)
                {
                    prevLetter = sb[i - 1].ToString();// line.Substring(i - 1, 1);
                    nextLetter = " ";
                }
                else
                {
                    prevLetter = sb[i - 1].ToString();// line.Substring(i - 1, 1);
                    nextLetter = sb[i + 1].ToString();// line.Substring(i + 1, 1);
                }

                if (!alphabet.Contains(currLetter))
                {
                    sb.Remove(i, 1);
                    --i;
                    continue;
                }
                else if (!alphabet.Contains(prevLetter))
                {
                    sb.Remove(i - 1, 1);
                    --i;
                    continue;
                }
                else if (!alphabet.Contains(nextLetter))
                {
                    sb.Remove(i + 1, 1);
                    --i;
                    continue;
                }


                this.setAfter(currLetter, nextLetter);
                this.setBefore(currLetter, prevLetter);
            }
        }*/

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

        public void PrintToFile(string file)
        {
            using (StreamWriter writer = new StreamWriter(file))
            {
                writer.WriteLine();
                writer.WriteLine("BEFORE CONTACT LETTERS");
                writer.WriteLine();

                foreach (var outer_key in before)
                {
                    writer.Write(outer_key.Key + ": ");
                    foreach (var inner_key in outer_key.Value.OrderByDescending(x => x.Value))
                    {
                        writer.Write(inner_key.Key + "[" + inner_key.Value + "], ");
                    }
                    writer.WriteLine();
                }

                writer.WriteLine();
                writer.WriteLine("AFTER CONTACT LETTERS");
                writer.WriteLine();

                foreach (var outer_key in after)
                {
                    writer.Write(outer_key.Key + ": ");
                    foreach (var inner_key in outer_key.Value.OrderByDescending(x => x.Value))
                    {
                        writer.Write(inner_key.Key + "[" + inner_key.Value + "], ");
                    }
                    writer.WriteLine();
                }
            }
        }

		public void PrintContactLetters()
		{
            Console.WriteLine ();
			Console.WriteLine ("BEFORE CONTACT LETTERS");
			Console.WriteLine ();

			foreach (var outer_key in before)
			{
                Console.Write(outer_key.Key + ": ");
				foreach(var inner_key in outer_key.Value.OrderBy(x => x.Value))
				{
					Console.Write(inner_key.Key + "[" + inner_key.Value + "], "); 
				}
                Console.WriteLine();
			}

			Console.WriteLine ();
			Console.WriteLine ("AFTER CONTACT LETTERS");
			Console.WriteLine ();

            foreach (var outer_key in after)
            {
                Console.Write(outer_key.Key + ": ");
                foreach (var inner_key in outer_key.Value.OrderBy(x => x.Value))
                {
                    Console.Write(inner_key.Key + "[" + inner_key.Value + "], ");
                }
                Console.WriteLine();
            }
        }
    }
}
