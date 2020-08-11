using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Modwana.Core.Extensions
{
    public static class StringExtension
    {
        public static string Brief(this string word, int maxSize)
        {
            if (String.IsNullOrWhiteSpace(word))
                return "";


            if (word.Length > maxSize)
                word = word.Substring(0, maxSize) + " ...";

            return word;
        }

        public static string StripHtml(this string text)
        {
            if (text == null)
                return text;

            string result = Regex.Replace(text, "<(.|\\n)*?>", string.Empty);
            result = result.Replace("&nbsp;", " ");
            return result;
        }

        public static string ConvertNumbersToEnglish(this string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return text;


            text = text.Replace('١', '1')
                        .Replace('٢', '2')
                        .Replace('٣', '3')
                        .Replace('٤', '4')
                        .Replace('٥', '5')
                        .Replace('٦', '6')
                        .Replace('٧', '7')
                        .Replace('٨', '8')
                        .Replace('٩', '9')
                        .Replace('٠', '0');

            
            return text;

        }

        public static string ConvertNumbersToArabic(this string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return text;


            text = text.Replace('1', '١')
                        .Replace('2', '٢')
                        .Replace('3', '٣')
                        .Replace('4', '٤')
                        .Replace('5', '٥')
                        .Replace('6', '٦')
                        .Replace('7', '٧')
                        .Replace('8', '٨')
                        .Replace('9', '٩')
                        .Replace('0', '٠');


            return text;

        }

        public static string AppendZeroToTheLeft(this int number, int digitCount = 9)
        {
            int numberOfDigit = number.ToString().Length;

            digitCount = digitCount - numberOfDigit;

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < digitCount; i++)
            {
                sb.Append("0");
            }

            sb.Append(number);

            return sb.ToString();
        }

    }
}
