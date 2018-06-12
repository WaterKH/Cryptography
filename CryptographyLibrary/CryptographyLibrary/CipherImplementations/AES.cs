using System.Text;
using System.Linq;
using System;
using System.IO;
using System.Security.Cryptography;

namespace CryptographyLibrary.CipherImplementations {
	
	public static class EncryptionHelper
	{
		private static byte [] key1AndIvBytes;
		private static byte [] key2AndIvBytes;
		private static byte [] key192AndIvBytes;

		static EncryptionHelper ()
		{
			string key1 = "548BD1FA96F9BAD594072B257552494128FE0E1CEEC4F60A9B6AFCAAF17421A2";
			string key2 = "4C308858683572444091D49ADC18A9EE53334214E9FAC8E70BC7430930747120";
			string key192 = "9192D4DEF2EA3758E74A00C1162126460B93D0ED6EEDDC64D821ABA9145E2FC94A1BB102";

			//TODO
			//key1AndIvBytes = key1.HexToByteArray ();
			//key2AndIvBytes = key2.HexToByteArray ();
			//key192AndIvBytes = key192.HexToByteArray ();
		}

		public static string ByteArrayToHexString (byte [] ba)
		{
			return BitConverter.ToString (ba).Replace ("-", "");
		}

		public static byte [] StringToByteArray (string hex)
		{
			return Enumerable.Range (0, hex.Length)
							 .Where (x => x % 2 == 0)
							 .Select (x => Convert.ToByte (hex.Substring (x, 2), 16))
							 .ToArray ();
		}

		public static string DecodeAndDecrypt (byte [] cipherText)
		{
			string DecodeAndDecrypt = AesDecrypt (cipherText);
			return (DecodeAndDecrypt);
		}

		public static string EncryptAndEncode (string plaintext)
		{
			return ByteArrayToHexString (AesEncrypt (plaintext));
		}

		public static string AesDecrypt (Byte [] inputBytes)
		{
			Console.WriteLine (inputBytes.Length % 16);
			Byte [] outputBytes = inputBytes;

			string plaintext = string.Empty;

			using (MemoryStream memoryStream = new MemoryStream (outputBytes)) {
				using (CryptoStream cryptoStream = new CryptoStream (memoryStream, GetCryptoAlgorithm ().CreateDecryptor (key1AndIvBytes, key1AndIvBytes), CryptoStreamMode.Read)) {
					using (StreamReader srDecrypt = new StreamReader (cryptoStream)) {
						plaintext = srDecrypt.ReadToEnd ();
					}
				}
			}

			return plaintext;
		}

		public static byte [] AesEncrypt (string inputText)
		{
			byte [] inputBytes = UTF8Encoding.UTF8.GetBytes (inputText);//AbHLlc5uLone0D1q

			byte [] result = null;
			using (MemoryStream memoryStream = new MemoryStream ()) {
				using (CryptoStream cryptoStream = new CryptoStream (memoryStream, GetCryptoAlgorithm ().CreateEncryptor (key1AndIvBytes, key1AndIvBytes), CryptoStreamMode.Write)) {
					cryptoStream.Write (inputBytes, 0, inputBytes.Length);
					cryptoStream.FlushFinalBlock ();

					result = memoryStream.ToArray ();
				}
			}

			return result;
		}


		private static RijndaelManaged GetCryptoAlgorithm ()
		{
			RijndaelManaged algorithm = new RijndaelManaged ();
			//set the mode, padding and block size
			algorithm.Padding = PaddingMode.PKCS7;
			algorithm.Mode = CipherMode.ECB;
			algorithm.KeySize = 256;
			algorithm.BlockSize = 256;
			return algorithm;
		}
	}
}