using System;
using System.Collections.Generic;
using System.Text;

namespace Encoder
{
    public class EncoderProcessor
    {
        public string Encode(string message)
        {
            message = message.ToLower();
            StringBuilder sb = new StringBuilder();
            int length = message.Length;

            var vowelDictionary = new Dictionary<char, char>
            {
                { 'a', '1' },
                { 'e', '2' },
                { 'i', '3' },
                { 'o', '4' },
                { 'u', '5' }
            };

            for (int i = 0; i < length; i++)
            {
                if (message[i] == 'y')
                {
                    sb.Append(' ');
                }
                else if (message[i] == ' ')
                {
                    sb.Append('y');
                }
                else if (vowelDictionary.ContainsKey(message[i])) //Vowels
                {
                    sb.Append(vowelDictionary[message[i]]);
                }
                else if (char.IsLetter(message[i])) // Consonants
                {
                    int ascii = message[i] - 'a';
                    ascii = (ascii == 0) ? 26 : ascii - 1;
                    sb.Append(Convert.ToChar(ascii + 'a'));
                }
                else if (char.IsDigit(message[i]))
                {
                    //Get numbers
                    string num = "";
                    while (i < length && char.IsDigit(message[i]))
                    {
                        num = message[i] + num;
                        i++;
                    }
                    sb.Append(num);
                    i--;
                }
                else
                {
                    sb.Append(message[i]);
                }
            }

            return sb.ToString();
        }
    }
}