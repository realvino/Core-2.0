using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Threading.Tasks;

namespace tibs.stem.Web.Controllers
{
    public class NumberToWordsConverter
    {
        public static readonly string[] UnitsMap = { "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
        public static readonly string[] TensMap = { "Zero", "Ten", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };

        public static readonly Dictionary<long, string> OrdinalExceptions = new Dictionary<long, string>
        {
            {1, "First"},
            {2, "Second"},
            {3, "Third"},
            {4, "Fourth"},
            {5, "Fifth"},
            {8, "eighth"},
            {9, "Ninth"},
            {12, "Twelfth"},
        };

        public string Convert(long number)
        {
            return Convert(number, false);
        }

        public string ConvertToOrdinal(int number)
        {
            return Convert(number, true);
        }

        public string Convert(long number, bool isOrdinal)
        {
            if (number == 0)
                return GetUnitValue(0, isOrdinal);

            if (number < 0)
                return string.Format("minus {0}", Convert(-number));

            var parts = new List<string>();

            if ((number / 1000000000000000000) > 0)
            {
                parts.Add(string.Format("{0} Quintillion", Convert(number / 1000000000000000000)));
                number %= 1000000000000000000;
            }

            if ((number / 1000000000000000) > 0)
            {
                parts.Add(string.Format("{0} Quadrillion", Convert(number / 1000000000000000)));
                number %= 1000000000000000;
            }

            if ((number / 1000000000000) > 0)
            {
                parts.Add(string.Format("{0} Trillion", Convert(number / 1000000000000)));
                number %= 1000000000000;
            }

            if ((number / 1000000000) > 0)
            {
                parts.Add(string.Format("{0} Billion", Convert(number / 1000000000)));
                number %= 1000000000;
            }

            if ((number / 1000000) > 0)
            {
                parts.Add(string.Format("{0} Million", Convert(number / 1000000)));
                number %= 1000000;
            }

            if ((number / 1000) > 0)
            {
                parts.Add(string.Format("{0} Thousand", Convert(number / 1000)));
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                parts.Add(string.Format("{0} Hundred", Convert(number / 100)));
                number %= 100;
                // parts.Add(" and ");
            }

            if (number > 0)
            {
                if (parts.Count != 0)
                    parts.Add(" ");

                if (number < 20)
                    parts.Add(GetUnitValue(number, isOrdinal));
                else
                {
                    var lastPart = TensMap[number / 10];
                    if ((number % 10) > 0)
                        lastPart = lastPart + " " + string.Format("{0}", GetUnitValue(number % 10, isOrdinal));
                    else if (isOrdinal)
                        lastPart = lastPart.TrimEnd('y') + "ieth";

                    parts.Add(lastPart);
                }
            }
            else if (isOrdinal)
                parts[parts.Count - 1] += "th";

            var toWords = string.Join(" ", parts.ToArray());

            if (isOrdinal)
                toWords = RemoveOnePrefix(toWords);

            return toWords;
        }

        public static string GetUnitValue(long number, bool isOrdinal)
        {
            if (isOrdinal)
            {
                string exceptionString;
                if (ExceptionNumbersToWords(number, out exceptionString))
                    return exceptionString;
                else
                    return UnitsMap[number] + "th";
            }
            else
                return UnitsMap[number];
        }

        public static string RemoveOnePrefix(string toWords)
        {
            // one hundred => hundredth
            if (toWords.IndexOf("one", StringComparison.Ordinal) == 0)
                toWords = toWords.Remove(0, 4);

            return toWords;
        }

        public static bool ExceptionNumbersToWords(long number, out string words)
        {
            return OrdinalExceptions.TryGetValue(number, out words);
        }
    }
}




        