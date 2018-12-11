using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace StringCalculatorTests
{
    public class StringCalculator
    {
        public int Add(string numbersAsText)
        {
            if (string.Equals(numbersAsText, ""))
                return 0;

            string[] numbers;

            var separatorStartChar = "//";
            var separatorSignStartPosition = numbersAsText.IndexOf(separatorStartChar, StringComparison.Ordinal);
            if (separatorSignStartPosition == 0)
            {
                var separatorRegex = new Regex(@"(?<=//)[^\n]+");
                var separator = separatorRegex.Match(numbersAsText).Value;

                var inputNumbersAsTextWithoutSeparator =
                    numbersAsText.Substring(separatorStartChar.Length + separator.Length);
                var inputNumbersAsTextWithoutFirstNewLine = inputNumbersAsTextWithoutSeparator.Substring(1);
                numbers = inputNumbersAsTextWithoutFirstNewLine.Split(separator);
            }
            else
            {
                var numbersAsTextWithOnlyCommaSeparator = numbersAsText.Replace('\n', ',');
                numbers = numbersAsTextWithOnlyCommaSeparator.Split(",");
            }

            var allNumbers = numbers.Select(e => Convert.ToInt32(e)).ToList();

            var allNumbersThatBiggerThan1000 = allNumbers.Where(e => e > 1000).ToList();
            if (allNumbersThatBiggerThan1000.Any())
            {
                allNumbers.RemoveAll(e => e > 1000);
            }
            
            var allNegativeNumbers = allNumbers.Where(e => e < 0).ToList();
            if (allNegativeNumbers.Any())
            {
                var messageBuilder = new StringBuilder();
                messageBuilder.Append("negatives not allowed: ");
                messageBuilder.Append(string.Join(", ", allNegativeNumbers));

                throw new ArgumentException(messageBuilder.ToString());
            }

            return allNumbers.Sum();
        }
    }
}
