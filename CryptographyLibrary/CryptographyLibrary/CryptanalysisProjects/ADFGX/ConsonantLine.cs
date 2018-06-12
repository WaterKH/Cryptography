using System;
using System.Linq;
using System.Collections.Generic;

namespace CryptographyLibrary.CryptanalysisProjects
{
    public class ConsonantLine
    {
        //public Dictionary<char, Dictionary<char, int>> contacts = new Dictionary<char, Dictionary<char, int>>();

        // index by letter, and then by number of frequency, value will be incremented by 1 each time it is found
        public Dictionary<char, Dictionary<Tuple<int, int>, int>> before_and_after_percs = new Dictionary<char, Dictionary<Tuple<int, int>, int>>();
        public Dictionary<int, int> spaces = new Dictionary<int, int>();
        //public Dictionary<char, Dictionary<int, int>> after_percs = new Dictionary<char, Dictionary<int, int>>();
        //public int counter = 0;

        public void Analyze(string text)
        {
            text = Utilities.RemoveSpecialCharacters(text.ToLower());

            // Check space count to determine the total number of spaces in the text
            int space_count = 0;
            var letters = new ContactLetters(text);

            foreach(var c in text)
            {
                if(c == ' ')
                {
                    space_count++;
                }
            }
            if (!spaces.ContainsKey(space_count))
                spaces.Add(space_count, 0);
            spaces[space_count]++;

            Dictionary<char, int> freqs = new Dictionary<char, int>();
            Dictionary<char, int> contacts = new Dictionary<char, int>();

            // Calculate frequencies of text
            for (int i = 0; i < text.Length; ++i)
            {
                if (!freqs.ContainsKey(text[i]))
                {
                    freqs.Add(text[i], 0);
                }
                ++freqs[text[i]];
            }
            var posOfDemarcation = FindLineOfDemarcation(letters, freqs, contacts);

            // Find the lowest number and work up until you find the line of demarcation
            var desc_contacts = contacts.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);


            // Separate the lower half and create contact letters for every letter to the right of the line of demarcation
            var total = 0;
            var lineOfDemarcation_List = new List<char>();
            // Last group is equal to the next variety of contacts
            var lastGroup = 1;
            var groupList = new List<char>();

            // Look at each key value pair in the descending contacts list
            foreach(var kv in desc_contacts)
            {
                bool groupEnd = false;
                total += kv.Value;

                if(lastGroup == kv.Value)
                {
                    groupList.Add(kv.Key);
                }
                else
                {
                    lastGroup = kv.Value;
                    groupEnd = true;
                }

                if (total > posOfDemarcation)
                {
                    lastGroup = kv.Value;
                    break;
                }

                if (groupEnd)
                {
                    lineOfDemarcation_List.AddRange(groupList);

                    groupList.Clear();
                    groupList.Add(kv.Key);
                }
            }

            // Pick the number of characters in that group that seem to be consonants (step down method)
            // If nothing is there, discard the group
            var allowed_chars = groupList.Count;
            var allowedGroup = new List<char>();
            var counter = 0;

            foreach(var kv in desc_contacts)
            {
                Console.WriteLine(kv.Key + " " + kv.Value + " " + freqs[kv.Key]);
                if (kv.Value == lastGroup && !groupList.Contains(kv.Key) && kv.Key != ' ' && kv.Key != '-')
                {
                    Console.WriteLine("Success: " + kv.Key + " " + kv.Value + " " + freqs[kv.Key]);
                    groupList.Add(kv.Key);
                }
                else if (kv.Value > lastGroup)
                {
                    break;
                }
            }

            foreach(var c in groupList)
            {
                if (counter == allowed_chars)
                    break;
                Console.WriteLine(c + " " + desc_contacts[c] + " " + freqs[c]);
                if(desc_contacts[c] - freqs[c] <= 0) 
                {
                    Console.WriteLine(c);
                    allowedGroup.Add(c);
                    counter++;
                }
            }

            lineOfDemarcation_List.AddRange(allowedGroup);

            // Keeps track of all letters 
            var trackList = new List<char>();

            trackList.AddRange(lineOfDemarcation_List);
            //lineOfDemarcation_List.Reverse();

            var after = new Dictionary<string, int>();
            var before = new Dictionary<string, int>();
            var alphabet = "abcdefghijklmnopqrstuvwxyz";

            foreach(var a in alphabet)
            {
                after.Add(a.ToString(), 0);
                before.Add(a.ToString(), 0);
            }

            foreach(var i in lineOfDemarcation_List)
            {
                var a = letters.getAfter(i.ToString());
                var b = letters.getBefore(i.ToString());

                foreach(var kv in a)
                {
                    if (kv.Key == "-" || kv.Key == " ")
                        continue;

                    after[kv.Key] += kv.Value;

                    if (!trackList.Contains(kv.Key[0])) 
                    {
                        trackList.Add(kv.Key[0]);
                    }
                }

                foreach (var kv in b)
                {
                    if (kv.Key == "-" || kv.Key == " ")
                        continue;
                    
                    before[kv.Key] += kv.Value;

                    if (!trackList.Contains(kv.Key[0]))
                    {
                        trackList.Add(kv.Key[0]);
                    }
                }
            }

            // Add whatever is not in the list to the above and recalculate contact letters
            var leftoverList = new List<char>();
            foreach(var kv in desc_contacts)
            {
                if(!trackList.Contains(kv.Key) && kv.Key != ' ')
                {
                    leftoverList.Add(kv.Key);
                }
            }

            foreach (var i in leftoverList)
            {
                var a = letters.getAfter(i.ToString());
                var b = letters.getBefore(i.ToString());

                foreach (var kv in a)
                {
                    if (kv.Key == "-" || kv.Key == " ")
                        continue;

                    after[kv.Key] += kv.Value;

                    if (!trackList.Contains(kv.Key[0]))
                    {
                        trackList.Add(kv.Key[0]);
                    }
                }

                foreach (var kv in b)
                {
                    if (kv.Key == "-" || kv.Key == " ")
                        continue;

                    before[kv.Key] += kv.Value;

                    if (!trackList.Contains(kv.Key[0]))
                    {
                        trackList.Add(kv.Key[0]);
                    }
                }
            }

            // Analyze the frequency count on both sides to determine if the letter is a vowel or consonant
            foreach(var x in trackList)
            {
                var temp = new Tuple<int, int>(before[x.ToString()], after[x.ToString()]);
                if(!before_and_after_percs[x].ContainsKey(temp))
                {
                    before_and_after_percs[x].Add(temp, 0);
                }
                ++before_and_after_percs[x][temp];
            }

            ++counter;

            // Print?
            lineOfDemarcation_List.ForEach(x => Console.Write(x + " "));
            leftoverList.ForEach(x => Console.Write(x + " "));
            Console.WriteLine();
            var lineCount = (lineOfDemarcation_List.Count + leftoverList.Count) * 2;
            for (int i = 0; i < lineCount - 1; ++i)
                Console.Write("-");
            Console.WriteLine();

            foreach(var t in trackList)
            {
                int tempBefore = 0;
                int tempAfter = 0;

                if (lineOfDemarcation_List.Contains(t) || leftoverList.Contains(t))
                    continue;
                if (before.ContainsKey(t.ToString()))
                {
                    Console.Write("\t" + t + " " + before[t.ToString()] + "| ");
                    tempBefore = before[t.ToString()];
                    if (after.ContainsKey(t.ToString()))
                    {
                        Console.Write(t + " " + after[t.ToString()]);
                        tempAfter = after[t.ToString()];
                    }
                }
                else if (after.ContainsKey(t.ToString()))
                {
                    Console.Write("\t   | " + t + " " + after[t.ToString()]);
                    tempAfter = after[t.ToString()];
                }
                else
                    continue;
                
                Console.WriteLine();
            }
        }

        private float FindLineOfDemarcation(ContactLetters letters, Dictionary<char, int> freqs, Dictionary<char, int> contacts)
        {
            var total = 0;
            foreach (var kv in freqs)
            {
                var tempList = new List<string>();
                var after = letters.getAfter(kv.Key.ToString());
                var before = letters.getBefore(kv.Key.ToString());

                foreach (var kv2 in after)
                {
                    if (!tempList.Contains(kv2.Key) && kv2.Key != "-" && kv2.Key != " ")
                    {
                        tempList.Add(kv2.Key);
                    }
                }
                foreach (var kv2 in before)
                {
                    if (!tempList.Contains(kv2.Key) && kv2.Key != "-" && kv2.Key != " ")
                    {
                        tempList.Add(kv2.Key);
                    }
                }
                total += tempList.Count;
                //tempList.ForEach(x => Console.Write(x + " "));
                //Console.WriteLine();
                if (!contacts.ContainsKey(kv.Key))
                {
                    contacts[kv.Key] = tempList.Count;
                }
            }

            return .2f * total;
        }

        public void PrintData()
        {
            //Console.WriteLine("Before");
            string alphabet = "abcdefghijklmnopqrstuvwxyz";
            foreach(var a in alphabet)
            {
                var total = 0.0f;
                //before_percs[a] = before_percs[a].OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
                //after_percs[a] = after_percs[a].OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);

                foreach(var kv in before_and_after_percs[a])
                {
                    total += kv.Value;
                }

                Console.Write(a + ":");
                for (int i = 0; i < 11; ++i)
                {
                    for (int j = 0; j < 11; ++j)
                    {
                        var temp = new Tuple<int, int>(i, j);
                        if (before_and_after_percs[a].ContainsKey(temp))
                        {
                            Console.WriteLine("\t" + temp + " - " + Math.Round((float)before_and_after_percs[a][temp] / total, 5) * 100);
                        }
                    }
                }
                Console.WriteLine();
            }
            Console.Write("space:");
            foreach(var s in spaces.OrderBy(x => x.Value))
            {
                Console.WriteLine("\t" + s.Key + " - " + s.Value);
            }
        }
    }
}
