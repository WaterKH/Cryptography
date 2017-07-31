using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptographyLibrary
{
    class Conversion
    {
        public static string B64ToBinary(string b64)
        {
            string binaryString = "";

            foreach(char c in b64)
            {
                int index = Base64.lookup[c];
                string result = Convert.ToString(index, 2);
                Console.WriteLine(result);
            }

            return binaryString;
        }
    }
}
