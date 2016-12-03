using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace Education
{
    class Program
    {
        static void Main(string[] args)
        {
            Program p = new Program();
            p.DoEverything();

            Console.ReadLine();
        }

        private void DoEverything()
        {
            Console.WriteLine(string.Format(new WordyFormatProvider(), "Number {0:G5} is pronounced like {0:W}", -735.1));
        }
    }

    public class WordyFormatProvider : IFormatProvider, ICustomFormatter
    {
        private IFormatProvider parent;

        private string[] digitWords => "zero one two three four five six seven eight nine minus point".Split();

        private string digitList => "0123456789-.";

        public WordyFormatProvider() : this(CultureInfo.CurrentCulture) { }

        public WordyFormatProvider(IFormatProvider parent)
        {
            this.parent = parent;
        }

        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            if (arg == null || format != "W")
            {
                return string.Format(parent, "{0:" + format + "}", arg);
            }
            string numberString = string.Format(CultureInfo.InvariantCulture, "{0}", arg);
            StringBuilder result = new StringBuilder();

            for (int i = 0; i < numberString.Length; i++)
            {
                var digit = numberString[i];
                var digitIndex = digitList.IndexOf(digit);
                var digitWord = digitWords[digitIndex];
                if (result.Length > 0)
                {
                    result.Append(" ");
                }
                result.Append(digitWord);
            }

            return result.ToString();
        }

        public object GetFormat(Type formatType)
        {
            if (formatType == typeof(ICustomFormatter))
            {
                return this;
            }

            return null;
        }
    }
}