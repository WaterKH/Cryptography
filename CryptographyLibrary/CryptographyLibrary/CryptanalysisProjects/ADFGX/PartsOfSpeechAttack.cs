using System;
using System.Collections.Generic;
using System.IO;
using CryptographyLibrary;

namespace CryptographyLibrary.CryptanalysisProjects
{
    public class PartsOfSpeechAttack
    {
        public Dictionary<string, string> ReadInFile(string fileName)
        {
            var pos = new Dictionary<string, string>();

            using(StreamReader reader = new StreamReader(fileName))
            {
                string line = "";
                while((line = reader.ReadLine()) != null)
                {
                    var data = line.Split('\t');

                    //var speech_obj = new PartOfSpeech(int.Parse(data[0]), 
                    //                                  data[1], data[2][0], 
                    //                                  long.Parse(data[3]),
                    //                                  double.Parse(data[4]));

                    if (!pos.ContainsKey(data[1]))
                    {
                        pos.Add(data[1], data[2]);
                    }
                    else
                    {
                        pos[data[1]] += ", " + data[2];
                    }

                }
            }

            return pos;
        }

        public string ConvertToPartsOfSpeech(string message, Dictionary<string, string> data)
        {
            string pos_message = "";
            string formatted_message = Utilities.RemoveSpecialCharacters(message).ToLower();
            //Console.WriteLine(formatted_message);

            foreach(var s in formatted_message.Split(' '))
            {
                if(data.ContainsKey(s))
                {
                    if(data[s].Length > 1)
                    {
                        // TODO Create smarter decisions here
                        // ie - if there is an adjective or noun here that 
                        // precedes a noun, we know it's an adjective
                        //pos_message += "{" + data[s] + "} ";
                        pos_message += data[s][0];
                    }
                    else
                    {
                        pos_message += data[s] + " ";
                    }
                }
                else
                {
                    pos_message += "u ";
                }
            }

            return pos_message;
        }

        public Dictionary<string, int> AnalyzePartsOfSpeech(string message)
        {
            var analysis = new Dictionary<string, int>();
            var truncatedMessage = Utilities.RemoveSpecialCharactersAndSpaces(message);
            Console.WriteLine(truncatedMessage);
            foreach(var part in truncatedMessage.Split('\n'))
            {
                for (int i = 0; i < part.Length - 1; ++i) // NOTE: does not complete entirely
                {
                    string pair = part[i] + " " + part[i + 1];

                    if(!analysis.ContainsKey(pair))
                    {
                        analysis.Add(pair, 0);
                    }

                    ++analysis[pair];
                }
            }

            return analysis;
        }
    }
}
