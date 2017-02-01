using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CryptographyLibrary
{
    class BookUtilities
    {
        public static List<ContactLetters> CreateContacts(string fileName)
        {
            var listOfContacts = new List<ContactLetters>();
            string book = File.ReadAllText(fileName);
            int textSize = 34;

            book = Utilities.RemoveSpecialCharacters(book).ToLower();

            for(int i = 0; i < book.Length - textSize; ++i)
            {
                ContactLetters cls = new ContactLetters();
                string text = book.Substring(i, textSize);

                for(int j = 0; j < text.Length; ++j)
                {
                    if(j - 1 >= 0)
                        cls.setBefore(char.ToString(text[j]), char.ToString(text[j - 1]));
                    else
                        cls.setBefore(char.ToString(text[j]), "*");
                    if(j + 1 < text.Length)
                        cls.setAfter(char.ToString(text[j]), char.ToString(text[j + 1]));
                    else
                        cls.setAfter(char.ToString(text[j]), "*");

                }
                listOfContacts.Add(cls);
            }

            return listOfContacts;
        }
    }
}
