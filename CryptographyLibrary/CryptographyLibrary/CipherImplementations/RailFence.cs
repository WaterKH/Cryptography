using System;

namespace CryptographyLibrary
{
	public class RailFence
	{
		public RailFence ()
		{
		}

		public string Encrypt(string plaintext, int rails)
		{
			string[,] railArr = new string[rails,plaintext.Length];

			string ciphertext = "";
			int rail = 0;
			int post = 0;
			bool switchD = false;

			while(true)
			{
				railArr[rail,post] = plaintext.Substring(post, 1);

				if(!switchD)//switchD == false
				{
					++rail;
				}
				else // switchD == true
				{
					--rail;
				}

				if(rail == rails)
				{
					rail -= 2;
					switchD = true;
				}
				else if(rail < 0)
				{
					rail += 2;
					switchD = false;
				}

				++post;
				if(post == plaintext.Length)
				{
					break;
				}
			}

			for(int i = 0; i < rails; ++i)
			{
				for(int j = 0; j < post; ++j)
				{
					string part = railArr[i,j];
					if(part != null)
					{
						ciphertext += part;
					}
				}
			}

			Console.WriteLine(ciphertext);
			return ciphertext;
		}

		public string Decrypt(string ciphertext, int rails)
		{
			int incrementor = (rails - 1) * 2;
			int start = 0;
			int index = 0;
			int charIndex = 0;

			string[,] railArr = new string[rails,ciphertext.Length];

			while(true)
			{
				if(start == rails)
				{
					break;
				}

				if(index > ciphertext.Length)
				{
					++start;
					index = 0;
					incrementor -= 2;
				}
				else
				{
					railArr[start, index] = ciphertext.Substring(charIndex, 1);
					index += incrementor;
				}

				++charIndex;

			}

			return "";

		}
	}
}

