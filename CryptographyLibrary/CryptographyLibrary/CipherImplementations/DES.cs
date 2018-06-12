using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptographyLibrary.CipherImplementations
{
    class DES
    {
        #region Tables and Matrixes
        // 8 x 7 (# of rows x # of cols)
        int[] PC_1 = new int[]
        {
            57, 49, 41, 33, 25, 17,  9,
             1, 58, 50, 42, 34, 26, 18,
            10,  2, 59, 51, 43, 35, 27,
            19, 11,  3, 60, 52, 44, 36,
            63, 55, 47, 39, 31, 23, 15,
             7, 62, 54, 46, 38, 30, 22,
            14,  6, 61, 53, 45, 37, 29,
            21, 13,  5, 28, 20, 12,  4
        };

        // 8 x 6 (# of rows x # of cols)
        int[] PC_2 = new int[]
        {
            14, 17, 11, 24,  1,  5,
             3, 28, 15,  6, 21, 10,
            23, 19, 12,  4, 26,  8,
            16,  7, 27, 20, 13,  2,
            41, 52, 31, 37, 47, 55,
            30, 40, 51, 45, 33, 48,
            44, 49, 39, 56, 34, 53,
            46, 42, 50, 36, 29, 32
        };

        int[] IP_Matrix = new int[]
        {
            58, 50, 42, 34, 26, 18, 10,  2,
            60, 52, 44, 36, 28, 20, 12,  4,
            62, 54, 46, 38, 30, 22, 14,  6,
            64, 56, 48, 40, 32, 24, 16,  8,
            57, 49, 41, 33, 25, 17,  9,  1,
            59, 51, 43, 35, 27, 19, 11,  3,
            61, 53, 45, 37, 29, 21, 13,  5,
            63, 55, 47, 39, 31, 23, 15,  7,
        };

        int[] Inv_IP_Matrix = new int[]
        {
            40,  8, 48, 16, 56, 24, 64, 32,
            39,  7, 47, 15, 55, 23, 63, 31,
            38,  6, 46, 14, 54, 22, 62, 30,
            37,  5, 45, 13, 53, 21, 61, 29,
            36,  4, 44, 12, 52, 20, 60, 28,
            35,  3, 43, 11, 51, 19, 59, 27,
            34,  2, 42, 10, 50, 18, 58, 26,
            33,  1, 41,  9, 49, 17, 57, 25,
        };

        int[] E_Table = new int[]
        {
            32,  1,  2,  3,  4,  5,
             4,  5,  6,  7,  8,  9,
             8,  9, 10, 11, 12, 13,
            12, 13, 14, 15, 16, 17,
            16, 17, 18, 19, 20, 21,
            20, 21, 22, 23, 24, 25,
            24, 25, 26, 27, 28, 29,
            28, 29, 30, 31, 32,  1,
        };

        int[] P_Table = new int[]
        {
            16,  7, 20, 21,
            29, 12, 28, 17,
             1, 15, 23, 26,
             5, 18, 31, 10,
             2,  8, 24, 14,
            32, 27,  3,  9,
            19, 13, 30,  6,
            22, 11,  4, 25,
        };

        // Key: Iteration number, Value: Number of Left Shifts
        Dictionary<int, int> Iter_LeftShifts = new Dictionary<int, int>()
        {
            { 1,  1 },
            { 2,  1 },
            { 3,  2 },
            { 4,  2 },
            { 5,  2 },
            { 6,  2 },
            { 7,  2 },
            { 8,  2 },
            { 9,  1 },
            { 10, 2 },
            { 11, 2 },
            { 12, 2 },
            { 13, 2 },
            { 14, 2 },
            { 15, 2 },
            { 16, 1 },
        };
        #endregion

        #region S-Blocks
        static int[][] S1 = new int[][]
        {
            new int[] { 14,  4, 13,  1,  2, 15, 11,  8,  3, 10,  6, 12,  5,  9,  0,  7 },
            new int[] {  0, 15,  7,  4, 14,  2, 13,  1, 10,  6, 12, 11,  9,  5,  3,  8 },
            new int[] {  4,  1, 14,  8, 13,  6,  2, 11, 15, 12,  9,  7,  3, 10,  5,  0 },
            new int[] { 15, 12,  8,  2,  4,  9,  1,  7,  5, 11,  3, 14, 10,  0,  6, 13 },
        };

        static int[][] S2 = new int[][]
        {
            new int[] { 15,  1,  8, 14,  6, 11,  3,  4,  9,  7,  2, 13, 12,  0,  5, 10 },
            new int[] {  3, 13,  4,  7, 15,  2,  8, 14, 12,  0,  1, 10,  6,  9, 11,  5 },
            new int[] {  0, 14,  7, 11, 10,  4, 13,  1,  5,  8, 12,  6,  9,  3,  2, 15 },
            new int[] { 13,  8, 10,  1,  3, 15,  4,  2, 11,  6,  7, 12,  0,  5, 14,  9 },
        };

        static int[][] S3 = new int[][]
        {
            new int[] { 10,  0,  9, 14,  6,  3, 15,  5,  1, 13, 12,  7, 11,  4,  2,  8 },
            new int[] { 13,  7,  0,  9,  3,  4,  6, 10,  2,  8,  5, 14, 12, 11, 15,  1 },
            new int[] { 13,  6,  4,  9,  8, 15,  3,  0, 11,  1,  2, 12,  5, 10, 14,  7 },
            new int[] {  1, 10, 13,  0,  6,  9,  8,  7,  4, 15, 14,  3, 11,  5,  2, 12 },
        };

        static int[][] S4 = new int[][]
        {
            new int[] {  7, 13, 14,  3,  0,  6,  9, 10,  1,  2,  8,  5, 11, 12,  4, 15 },
            new int[] { 13,  8, 11,  5,  6, 15,  0,  3,  4,  7,  2, 12,  1, 10, 14,  9 },
            new int[] { 10,  6,  9,  0, 12, 11,  7, 13, 15,  1,  3, 14,  5,  2,  8,  4 },
            new int[] {  3, 15,  0,  6, 10,  1, 13,  8,  9,  4,  5, 11, 12,  7,  2, 14 },
        };

        static int[][] S5 = new int[][]
        {
            new int[] {  2, 12,  4,  1,  7, 10, 11,  6,  8,  5,  3, 15, 13,  0, 14,  9 },
            new int[] { 14, 11,  2, 12,  4,  7, 13,  1,  5,  0, 15, 10,  3,  9,  8,  6 },
            new int[] {  4,  2,  1, 11, 10, 13,  7,  8, 15,  9, 12,  5,  6,  3,  0, 14 },
            new int[] { 11,  8, 12,  7,  1, 14,  2, 13,  6, 15,  0,  9, 10,  4,  5,  3 },
        };

        static int[][] S6 = new int[][]
        {
            new int[] { 12,  1, 10, 15,  9,  2,  6,  8,  0, 13,  3,  4, 14,  7,  5, 11 },
            new int[] { 10, 15,  4,  2,  7, 12,  9,  5,  6,  1, 13, 14,  0, 11,  3,  8 },
            new int[] {  9, 14, 15,  5,  2,  8, 12,  3,  7,  0,  4, 10,  1, 13, 11,  6 },
            new int[] {  4,  3,  2, 12,  9,  5, 15, 10, 11, 14,  1,  7,  6,  0,  8, 13 },
        };

        static int[][] S7 = new int[][]
        {
            new int[] {  4, 11,  2, 14, 15,  0,  8, 13,  3, 12,  9,  7,  5, 10,  6,  1 },
            new int[] { 13,  0, 11,  7,  4,  9,  1, 10, 14,  3,  5, 12,  2, 15,  8,  6 },
            new int[] {  1,  4, 11, 13, 12,  3,  7, 14, 10, 15,  6,  8,  0,  5,  9,  2 },
            new int[] {  6, 11, 13,  8,  1,  4, 10,  7,  9,  5,  0, 15, 14,  2,  3, 12 },
        };

        static int[][] S8 = new int[][]
        {
            new int[] { 13,  2,  8,  4,  6, 15, 11,  1, 10,  9,  3, 14,  5,  0, 12,  7 },
            new int[] {  1, 15, 13,  8, 10,  3,  7,  4, 12,  5,  6, 11,  0, 14,  9,  2 },
            new int[] {  7, 11,  4,  1,  9, 12, 14,  2,  0,  6, 10, 13, 15,  3,  5,  8 },
            new int[] {  2,  1, 14,  7,  4, 10,  8, 13, 15, 12,  9,  0,  3,  5,  6, 11 },
        };

        List<int[][]> SBoxes = new List<int[][]>
        {
            S1, S2, S3, S4,
            S5, S6, S7, S8
        };
        #endregion


        public DES()
        {
            // Not used
        }

        public void TestImplementation(string message, string key)
        {
			//string encryptMes = "kCmIgFi6GUJNgkNI1Q41fbfyLoCFTCvIqkZiI0KIAXAzP1U1uy1BE4UfPBfpKmmL0bjYnQNRBaPtKiVWzc5A4v0w3xIe8F0hAGJZ7g4in0wndJxM0v03dc1M82at2T6935roTqyWDgtGD/hwwRF3oHqFM5Vcw1JtINbsgWRm4o4/quEDkZ7x1B275bX3/Fo1";
			//string key = "TheGiant";
			DES des = new DES();

			/*
             * Message Conversion
             */
			byte[] convert_message = Convert.FromBase64String(message);
			string binMes_prePad = "";

			// USE 6 from b64 and 8 from ascii
			foreach (var b in convert_message)
			{
				binMes_prePad += Convert.ToString(b, 2).PadLeft(6, '0');
			}

			int pad = binMes_prePad.Length + (64 - (binMes_prePad.Length % 64));
			string binMes = binMes_prePad.PadRight(pad, '0');

			/*
             * Key Conversion
             */
			string convert_key = "";

			foreach (char ch in key)
			{
				convert_key += Convert.ToString((int)ch, 2).PadLeft(8, '0');
			}


			/*
             * DES Encryption
             *
            var encryptedMessage = "";

            for (int i = 0; i < binMes.Length; i += 64)
            {
                var temp = binMes.Substring(i, 64);
                encryptedMessage += des.Encrypt(temp, convert_key);
            }*/

			/*
             * DES Decryption
             */
			var decryptedMessage = "";

			for (int i = 0; i < binMes.Length; i += 64)
			{
				var temp = binMes.Substring(i, 64);
				decryptedMessage += des.Decrypt(temp, convert_key);
			}

			Console.WriteLine(decryptedMessage);
        }

        /// <summary>
        /// Encryption method for DES.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="key"></param>
        /// <returns>Encrypted message.</returns>
        public string Encrypt(string message, string key)
        {
            string encryptedString = "";

            /* PART ONE - Key Generation */
            // Permutae K (key) according to the PC-1 table (Converts 64bits to 56bits)
            // Split key into left/ right halves, C0 and D0 (each 28bits)
            // Create sixteen blocks Cn and Dn, 1<=n<=16, using the Iter_LeftShift Dictionary and Cn-1 and Dn-1
            // Form Kn, 1<=n<=16, by using PC-2 to permute the concatenated pairs CnDn (56bits to 48bits)
            var Kn = SubKeys(key);

            /* PART TWO */
            // Use IP_Matrix to permute the message M to a variable IP (64bits to 64bits)
            // Split IP into left/ right halves, L0 and R0 (each 32bits)
            // Proceed through 16 iterations using the function_f on two blocks, a data block of 32 bits
            //    and a key Kn of 48 bits, to produce a block of 32 bits
            //    for n, 1<=n<=16, where + is XOR:
            //        Ln = R_n-1_
            //        Rn = L_n-1_ + f(R_n-1_, Kn)
            // At the end, we will have L16 and R16, reverse the order and concat: R16L16
            // Finally, apply the Inv_IP_Matrix to obtain the ciphertext
            int[] IP = new int[message.Length];
            for(int i = 0; i < IP_Matrix.Length; ++i)
            {
                IP[i] = int.Parse(message[IP_Matrix[i] - 1].ToString());
            }
            
            var L0 = IP.Take(IP.Length / 2).ToArray();
            var R0 = IP.Skip(IP.Length / 2).ToArray();

            int[][] Ln = new int[17][];
            int[][] Rn = new int[17][];
            Ln[0] = L0;
            Rn[0] = R0;

            for (int i = 1; i <= 16; ++i)
            {
                Ln[i] = Rn[i - 1];
                Rn[i] = XOR(Ln[i - 1], function_f(Rn[i - 1], Kn[i - 1]));
            }

            var RL = new int[64];

            Rn[16].CopyTo(RL, 0);
            Ln[16].CopyTo(RL, Rn[15].Length);

            int[] Inv_IP = new int[64];
            for (int i = 0; i < Inv_IP_Matrix.Length; ++i)
            {
                Inv_IP[i] = RL[Inv_IP_Matrix[i] - 1];
            }

            Inv_IP.ToList().ForEach(x => encryptedString += x);

            return encryptedString;
        }

        /// <summary>
        /// Similar to DES Encryption, however the keys are inversed.
        /// </summary>
        /// <returns></returns>
        public string Decrypt(string message, string key)
        {
            string decryptedString = "";
            
            /* PART ONE - Key Generation */
            // Permutae K (key) according to the PC-1 table (Converts 64bits to 56bits)
            // Split key into left/ right halves, C0 and D0 (each 28bits)
            // Create sixteen blocks Cn and Dn, 1<=n<=16, using the Iter_LeftShift Dictionary and Cn-1 and Dn-1
            // Form Kn, 1<=n<=16, by using PC-2 to permute the concatenated pairs CnDn (56bits to 48bits)
            var Kn = SubKeys(key);

            /* PART TWO */
            // Use IP_Matrix to permute the message M to a variable IP (64bits to 64bits)
            // Split IP into left/ right halves, L0 and R0 (each 32bits)
            // Proceed through 16 iterations using the function_f on two blocks, a data block of 32 bits
            //    and a key Kn of 48 bits, to produce a block of 32 bits
            //    for n, 1<=n<=16, where + is XOR:
            //        Ln = R_n-1_
            //        Rn = L_n-1_ + f(R_n-1_, Kn)
            // At the end, we will have L16 and R16, reverse the order and concat: R16L16
            // Finally, apply the Inv_IP_Matrix to obtain the ciphertext
            int[] IP = new int[message.Length];
            for (int i = 0; i < IP_Matrix.Length; ++i)
            {
                IP[i] = int.Parse(message[IP_Matrix[i] - 1].ToString());
            }

            var L0 = IP.Take(IP.Length / 2).ToArray();
            var R0 = IP.Skip(IP.Length / 2).ToArray();

            int[][] Ln = new int[17][];
            int[][] Rn = new int[17][];
            Ln[0] = L0;
            Rn[0] = R0;

            int counter = 15;

            for (int i = 1; i <= 16; ++i)
            {
                Ln[i] = Rn[i - 1];
                Rn[i] = XOR(Ln[i - 1], function_f(Rn[i - 1], Kn[counter]));

                --counter;
            }

            var RL = new int[64];

            Rn[16].CopyTo(RL, 0);
            Ln[16].CopyTo(RL, Rn[15].Length);

            int[] Inv_IP = new int[64];
            for (int i = 0; i < Inv_IP_Matrix.Length; ++i)
            {
                Inv_IP[i] = RL[Inv_IP_Matrix[i] - 1];
            }

            Inv_IP.ToList().ForEach(x => decryptedString += x);

            return decryptedString;
        }

        /// <summary>
        /// Generate SubKeys.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public int[][] SubKeys(string key)
        {
            int[] K = new int[56];
            for (int i = 0; i < PC_1.Length; ++i)
            {
                K[i] = int.Parse(key[PC_1[i] - 1].ToString());
            }

            var C0 = K.Take(K.Length / 2).ToArray();
            var D0 = K.Skip(K.Length / 2).ToArray();

            int[][] Cn = new int[17][];
            int[][] Dn = new int[17][];
            Cn[0] = C0;
            Dn[0] = D0;

            for (int i = 1; i <= 16; ++i)
            {
                var shifts = Iter_LeftShifts[i];

                var tempC_init = Cn[i - 1].Take(shifts).ToArray();
                var tempC_final = Cn[i - 1].Skip(shifts).ToList<int>();
                tempC_final.AddRange(tempC_init);

                var tempD_init = Dn[i - 1].Take(shifts).ToArray();
                var tempD_final = Dn[i - 1].Skip(shifts).ToList<int>();
                tempD_final.AddRange(tempD_init);

                Cn[i] = tempC_final.ToArray();
                Dn[i] = tempD_final.ToArray();
            }

            int[][] Kn = new int[16][];
            for (int i = 1; i <= 16; ++i)
            {
                Kn[i - 1] = new int[48];
                var CD = new int[56];

                Cn[i].CopyTo(CD, 0);
                Dn[i].CopyTo(CD, Cn[i].Length);
                
                for (int j = 0; j < PC_2.Length; ++j)
                {
                    Kn[i - 1][j] = CD[PC_2[j] - 1];
                }
                
            }

            return Kn;
        }

        /// <summary>
        /// Perform the function f on the datablock and the keyblock.
        /// </summary>
        /// <param name="dataBlock"></param>
        /// <param name="keyBlock"></param>
        /// <returns></returns>
        public int[] function_f(int[] dataBlock, int[] keyBlock)
        {
            // Use E to extend R_n-1_ from 32bits to 48bits: E(R_n-1_)
            // XOR the output E(R_n-1_) with the key K_n_: K_n_ + E(R_n-1_)
            // Index into the *S Boxes* with the 8 groups of six bits
            //   We do this by taking the first and last bits to form a 2bit number
            //   that we will use to index into the row (ie: 0-3) and the middle 4 bits
            //   to index into the column (ie: 0-15)
            // Use the P_Table to permute the previous result
            int[] P = new int[32];

            int[] E = new int[48];
            for(int i = 0; i < E_Table.Length; ++i)
            {
                E[i] = dataBlock[E_Table[i] - 1];
            }
            
            var xorResult = XOR(E, keyBlock);
           
            var counter = 0;
            List<int> S_Result = new List<int>();

            for(int i = 0; i < xorResult.Length; i += 6)
            {
                int[] row = new int[] { xorResult[i], xorResult[i + 5] };
                int[] col = new int[4];
                Array.Copy(xorResult, i + 1, col, 0, 4);

                int rowNumb = IntArrBinaryToInt(row);
                int colNumb = IntArrBinaryToInt(col);
            
                int intResult = SBoxes[counter][rowNumb][colNumb];
                S_Result.AddRange(IntToBinary(intResult));
               
                ++counter;
            }

            int[] tempS = S_Result.ToArray<int>();

            for(int i = 0; i < P_Table.Length; ++i)
            {
                P[i] = tempS[P_Table[i] - 1];
            }

            return P;
        }

        /// <summary>
        /// Simple XOR Function.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public int[] XOR(int[] a, int[] b)
        {
            int[] xorResult = new int[a.Length];

            if(a.Length != b.Length)
            {
                return null;
            }

            for(int i = 0; i < a.Length; ++i)
            {
                xorResult[i] = a[i] ^ b[i];
            }

            return xorResult;
        }

        /// <summary>
        /// Converts a binary number represented by an array into a number.
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public int IntArrBinaryToInt(int[] a)
        {
            int sum = 0;
            int counter = 0;
            for(int i = a.Length - 1; i >= 0; --i)
            {
                sum += a[i] * (int)Math.Pow(2, counter);
                ++counter;
            }

            return sum;
        }
        
        /// <summary>
        /// Converts a number into a binary number represented by an array.
        /// !NOTE! This is always a 4 bit number
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public int[] IntToBinary(int a)
        {
            int[] result = new int[4];
            int temp = a;

            for(int i = result.Length - 1; i >= 0; --i)
            {
                result[i] = temp % 2;
                temp /= 2;
            }

            return result;
        }
    }
}
