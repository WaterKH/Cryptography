using System;
using CryptographyLibrary.CipherImplementations;
namespace CryptographyLibrary.CryptanalysisProjects
{
    public class TicketSolution_REV
    {
        string[] cipher = new string[7];
        string[] trans_cipher = new string[7];

        public void Solve()
        {
            var text = @"dfagxagaffgxdaxfgfgdgdxgdafdggaaxfgdgdxffffxafggfxggdfaaaadxgfaffxggaaafadfgfxdxfagxadffaafgaffaggxfddafaxaxfdfgagfffgdxxfxdagggdggfafgaagfafgadaadgffxaadxaxdxddxgafdggggxxffffffgaxgddxdxdaagadgggxgfaffgaxxdxfgfggxdgxggfffxdfaaagagdxfgfaggafaxdafgxdaaddgggxaxgadgfaaggfgffaaxdxfggadddffgadaaafdffdfddafdxgadgggggfafgagaaxxfggfgdxd";

            var key = "6430215";

            var counter = 0;
            for (int i = 0; i < text.Length; ++i)
            {
                cipher[i % 7] += text[i].ToString();
                ++counter;
            }

            counter = 0;
            foreach(var c in key)
            {
                var index = int.Parse(c.ToString());
                trans_cipher[index] = cipher[counter];
                ++counter;
            }

            int row = 0;
            var pt = "";
            for (int i = 0; i < text.Length; ++i)
            {
                if(i != 0 && i % 7 == 0)
                {
                    row++;
                }
                //Console.WriteLine(pt);
                pt += trans_cipher[i % 7][row];
            }

            var final_pt = "";
            for (int i = 0; i < pt.Length - 1; i += 2)
            {
                if(i + 2 >= pt.Length)
                {
                    break;
                }

                final_pt += Convert(pt.Substring(i, 2).ToUpper());
            }

            Console.WriteLine(final_pt);
        }

        public string Convert(string pair)
        {
            string returnString = "";
            switch(pair)
            {
                case "AA":
                    returnString = "a";
                    break;
                case "AD":
                    returnString = "b";
                    break;
                case "AF":
                    returnString = "c";
                    break;
                case "AG":
                    returnString = "d";
                    break;
                case "AX":
                    returnString = "e";
                    break;
                case "DA":
                    returnString = "f";
                    break;
                case "DD":
                    returnString = "g";
                    break;
                case "DF":
                    returnString = "h";
                    break;
                case "DG":
                    returnString = "i";
                    break;
                case "DX":
                    returnString = "k";
                    break;
                case "FA":
                    returnString = "l";
                    break;
                case "FD":
                    returnString = "m";
                    break;
                case "FF":
                    returnString = "n";
                    break;
                case "FG":
                    returnString = "o";
                    break;
                case "FX":
                    returnString = "p";
                    break;
                case "GA":
                    returnString = "q";
                    break;
                case "GD":
                    returnString = "r";
                    break;
                case "GF":
                    returnString = "s";
                    break;
                case "GG":
                    returnString = "t";
                    break;
                case "GX":
                    returnString = "u";
                    break;
                case "XA":
                    returnString = "v";
                    break;
                case "XD":
                    returnString = "w";
                    break;
                case "XF":
                    returnString = "x";
                    break;
                case "XG":
                    returnString = "y";
                    break;
                case "XX":
                    returnString = "z";
                    break;
                default:
                    break;
            }
            return returnString;
        }
    }
}
