using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptographyLibrary.Utilities
{
    class BruteForceUtils
    {
        public void AllPossibilities(string fileName, string pattern = "abbabb", string dictionary = "dictionary_pruned.txt")
        {
            using (StreamWriter writer = new StreamWriter(fileName))
            using (StreamReader reader = new StreamReader(dictionary))
            {
                string line = "";
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.Length > 8)
                        continue;

                    using (StreamReader sub_reader = new StreamReader(dictionary))
                    {
                        string sub_line = "";
                        while((sub_line = reader.ReadLine()) != null)
                        {
                            string combined_lines = line + sub_line;

                            if (combined_lines.Length > 8)
                                continue;


                        }
                    }
                }

                Console.WriteLine("Poss. finished");
            }
        }
    }
}
