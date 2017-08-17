using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptographyLibrary.CipherImplementations
{
    class Transposition
    {
        /// <summary>
        /// Transposes the ciphertext according to the key.
        /// </summary>
        /// <param name="cipher"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public string Encrypt(string cipher, string key)
        {
            string encryptedString = "";

            // Convert the key to an int array
            var intArr_key = KeyToIntArr(key);
            var tempDict = new Dictionary<int, string>();

            // Goes through each column and assigns the current key 
            // value for the column to a dictionary
            for (int i = 0; i < intArr_key.Length; ++i)
            {
                string tempString = "";
                for (int j = i; j < cipher.Length; j += key.Length)
                {
                    tempString += cipher[j];
                }
                tempDict.Add(intArr_key[i], tempString);
            }

            // Gets the correct columns and saves it to the encrypted string
            for(int i = 0; i < key.Length; ++i)
            {
                encryptedString += tempDict[i];
            }

            return encryptedString;
        }
        
        /// <summary>
        /// Decrypts the ciphertext given with the key given.
        /// </summary>
        /// <param name="cipher"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public string Decrypt(string cipher, string key)
        {
            // Precompute values
            string decryptedString = "";
            int offset = cipher.Length % key.Length;
            int length = cipher.Length / key.Length;
            
            // Convert the key to an int array
            var intArr_key = KeyToIntArr(key);
            var tempDict = new Dictionary<int, string>();
            int index_dict = 0;

            // Goes through each column and assigns the current key 
            // value for the column to a dictionary
            for (int i = 0; i < cipher.Length; i += length)
            {
                string tempString = "";
                bool contained = ContainedInColumn(index_dict, intArr_key, offset);
                
                if (contained)
                {
                    tempString += cipher.Substring(i, length + 1);
                    ++i;
                }
                else
                {
                    tempString += cipher.Substring(i, length);
                }

                tempDict.Add(index_dict, tempString);
                index_dict++;
            }

            // Pulls out the ith character in each column iteratively
            for (int i = 0; i < length; ++i)
            {
                for(int j = 0; j < intArr_key.Length; ++j)
                {
                    decryptedString += tempDict[intArr_key[j]][i];
                }
            }

            // Grabbing the last row since we may not have a full row
            for(int i = 0; i < intArr_key.Length; ++i)
            {
                bool contained = ContainedInColumn(intArr_key[i], intArr_key, offset);
                if (contained)
                {
                    decryptedString += tempDict[intArr_key[i]][length];
                }
                else
                {
                    break;
                }
            }

            return decryptedString;
        }

        /// <summary>
        /// Checks to see if the letter should be in this column.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="keyArr"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public bool ContainedInColumn(int index, int[] keyArr, int offset)
        {
            bool contained = false;

            for (int i = 0; i < keyArr.Length; ++i)
            {
                if(i < offset && index == keyArr[i])
                {
                    contained = true;
                    break;
                }
                else if(i >= offset)
                {
                    break;
                }
            }

            return contained;
        }

        /// <summary>
        /// Retrieves the numerical values associated with the key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public int[] KeyToIntArr(string key)
        {
            int[] returnArr = new int[key.Length];

            var orderedKey = key.ToList();
            orderedKey.Sort();

            for (int i = 0; i < key.Length; ++i)
            {
                returnArr[i] = orderedKey.IndexOf(key[i]);
                orderedKey[returnArr[i]] = '-';
            }

            return returnArr;
        }
        
    }
}
