using System;
using System.Linq;

namespace StringCalculatorTests
{
    public class StringCalculator
    {
        public int Add(string numbersAsText)
        {
            if (string.Equals(numbersAsText, ""))
                return 0;

            string[] numbers;

            var separatorSign = "//";
            var separatorSignStartPosition = numbersAsText.IndexOf(separatorSign, StringComparison.Ordinal);
            if (separatorSignStartPosition == 0)
            {
                var separator = numbersAsText.Substring(separatorSign.Length, 1);
                var inputNumbersAsTextWithoutSeparator =
                    numbersAsText.Substring(separatorSign.Length + separator.Length);
                var inputNumbersAsTextWithoutFirstNewLine = inputNumbersAsTextWithoutSeparator.Substring(1);
                numbers = inputNumbersAsTextWithoutFirstNewLine.Split(separator);
            }
            else
            {
                var numbersAsTextWithOnlyCommaSeparator = numbersAsText.Replace('\n', ',');
                numbers = numbersAsTextWithOnlyCommaSeparator.Split(",");
            }

            return numbers.Select(e => Convert.ToInt32(e)).Sum();
        }
    }
}
