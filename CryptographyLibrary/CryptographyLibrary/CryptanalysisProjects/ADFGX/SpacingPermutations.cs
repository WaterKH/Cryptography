using System;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;

namespace CryptographyLibrary.CryptanalysisProjects
{
    public class SpacingPermutations
    {
        int numberOfSpaces { get; set; }
        int minimumWordLength { get; set; }
        int maximumWordLength { get; set; }

        public SpacingPermutations(int numbOfSpaces, int minWordLen, int maxWordLen)
        {
            numberOfSpaces = numbOfSpaces;
            minimumWordLength = minWordLen;
            maximumWordLength = maxWordLen;
        }

        public void BeginPerms(string text)
        {
            Console.WriteLine("Start");
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            StreamWriter writer = new StreamWriter("space.txt");
            for (int i = minimumWordLength; i <= maximumWordLength; ++i)
            {
                var newText = text.Substring(0, i) + " " + text.Substring(i, text.Length - i);
                //Console.WriteLine(newText + "    " + 1);
                //writer.WriteLine(newText);// + "\t\t" + 1);
                ContinuePerms(newText, (i + 2), 2, writer);
            }

            writer.Close();
            stopWatch.Stop();
            // Get the elapsed time as a TimeSpan value.
            TimeSpan ts = stopWatch.Elapsed;

            // Format and display the TimeSpan value.
            string elapsedTime = String.Format("{0:00}:{1:00}.{2:00}",
                ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            Console.WriteLine("RunTime " + elapsedTime);
        }

        /// <summary>
        /// Continues the perms.
        /// </summary>
        /// <param name="text">Text.</param>
        /// <param name="offsetSpace">Offset space = The length of the word until the next space.</param>
        /// <param name="depth">Depth = The number of spaces we are currently at.</param>
        public void ContinuePerms(string text, int offsetSpace, int depth, StreamWriter writer)
        {
            if(depth > numberOfSpaces)
            {
                return;
            }
            else if(depth == numberOfSpaces)
            {
                var tempSplit = text.Split(' ');
                var tempBool = false;
                for (int j = 0; j < tempSplit.Length - 1; ++j)
                {
                    if ((tempSplit[j].Length == 1 && tempSplit[j + 1].Length == 1))
                    {
                        tempBool = true;
                        break;
                    }
                    else if (tempSplit[j].Length > maximumWordLength || tempSplit[j + 1].Length > maximumWordLength)
                    {
                        tempBool = true;
                        break;
                    }

                }
                if (tempBool)
                    return;
            }

            for (int i = offsetSpace; i <= offsetSpace + maximumWordLength; ++i)
            {
                if (i < text.Length)
                {
                    var newText = text.Substring(0, i) + " " + text.Substring(i, text.Length - i);
                    //Console.WriteLine(newText + "    " + depth);
                    if(depth == numberOfSpaces)
                        writer.WriteLine(newText);// + "\t\t" + depth);
                    ContinuePerms(newText, (i + 2), depth + 1, writer);
                }
            }
        }
    }
}
