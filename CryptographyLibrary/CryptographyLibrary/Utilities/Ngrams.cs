/* Oxin's */
using CryptographyLibrary;
using System;
using System.Collections.Generic;
using System.Linq;

public class Ngrams
{
    public Dictionary<string, double> Grams = new Dictionary<string, double>();
    public int L;
    public double N;
    public double Floor;

    public Ngrams(string File, char sep = ' ')
    {
        foreach (string line in System.IO.File.ReadAllLines(File))
        {
            string[] split = line.Split(sep);
            Grams.Add(split[0].ToUpper(), int.Parse(split[1]));
        }
        ProcessGrams();
    }

    public Ngrams(List<string> lines, int length)
    {
        var allSubstrings = lines.Where(x => !String.IsNullOrEmpty(x)).SelectMany(x => x.GetSubstringsByLength(length));
        Grams = allSubstrings.GroupBy(x => x).ToDictionary(x => x.Key, x => (double)x.LongCount());
        ProcessGrams();
    }

    private void ProcessGrams()
    {
        L = Grams.Keys.First().Length;
        N = Grams.Sum(x => x.Value);

        foreach (var key in Grams.Keys.ToList())
        {
            Grams[key] = Math.Log10(Grams[key] / N);
        }
        Floor = Math.Log10(0.01 / N);
    }

    public double Score(string text)
    {
        double temp = 0;
        return text.GetSubstringsByLength(L).Sum(x => Grams.TryGetValue(x, out temp) ? temp : Floor);
    }

    public static Ngrams _standardTrigrams;
    public static Ngrams StandardTrigrams
    {
        get
        {
            if (_standardTrigrams == null) _standardTrigrams = new Ngrams("english_trigrams_DF.txt");
            return _standardTrigrams;
        }
    }

    public static Ngrams _standardQuadgrams;
    public static Ngrams StandardQuadgrams
    {
        get
        {
            if (_standardQuadgrams == null) _standardQuadgrams = new Ngrams("english_quadgrams_DF.txt");
            return _standardQuadgrams;
        }
    }
}