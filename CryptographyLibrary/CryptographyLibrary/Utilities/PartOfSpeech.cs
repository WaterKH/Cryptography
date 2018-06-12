using System;
namespace CryptographyLibrary
{
    public class PartOfSpeech
    {
        public int Rank { get; set; }
        public string Word { get; set; }
        public char Speech {get; set; }
        public long Frequency { get; set; }
        public double Dispersion { get; set; }

        public PartOfSpeech(int aRank, string aWord, char aSpeech, long aFreq, double aDisp)
        {
            Rank = aRank;
            Word = aWord;
            Speech = aSpeech;
            Frequency = aFreq;
            Dispersion = aDisp;
        }

        public string ToString()
        {
            return Rank + "\t" + Word + "\t" + Speech + "\t" + Frequency + "\t" + Dispersion;
        }
    }
}
