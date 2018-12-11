using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace StringCalculatorTests
{
    public class StringCalculator
    {
        public int Add(string inputNumbersAsText)
        {
            if (string.Equals(inputNumbersAsText, ""))
                return 0;

            string[] numbersAsText;

            var separatorStartChar = "//";
            var separatorSignStartPosition = inputNumbersAsText.IndexOf(separatorStartChar, StringComparison.Ordinal);
            if (separatorSignStartPosition == 0)
            {
                var separatorRegex = new Regex(@"(?<=//)[^\n]+");
                var separator = separatorRegex.Match(inputNumbersAsText).Value;

                var inputNumbersAsTextWithoutSeparator =
                    inputNumbersAsText.Substring(separatorStartChar.Length + separator.Length);
                var inputNumbersAsTextWithoutFirstNewLine = inputNumbersAsTextWithoutSeparator.Substring(1);
                numbersAsText = inputNumbersAsTextWithoutFirstNewLine.Split(separator);
            }
            else
            {
                var numbersAsTextWithOnlyCommaSeparator = inputNumbersAsText.Replace('\n', ',');
                numbersAsText = numbersAsTextWithOnlyCommaSeparator.Split(",");
            }

            var numbers = ConvertToNumbers(numbersAsText);

            RemoveNumbersThatBiggerThan1000(numbers);
            CheckThatExistsNegativeNumbers(numbers);

            return numbers.Sum();
        }

        private List<int> ConvertToNumbers(string[] numbersAsText)
        {
            return numbersAsText.Select(e => Convert.ToInt32(e)).ToList();
        }

        private void RemoveNumbersThatBiggerThan1000(List<int> allNumbers)
        {
            allNumbers.RemoveAll(e => e > 1000);
        }

        private void CheckThatExistsNegativeNumbers(IEnumerable<int> allNumbers)
        {
            var allNegativeNumbers = allNumbers.Where(e => e < 0).ToList();
            if (allNegativeNumbers.Any())
            {
                var messageBuilder = new StringBuilder();
                messageBuilder.Append("negatives not allowed: ");
                messageBuilder.Append(string.Join(", ", allNegativeNumbers));

                throw new ArgumentException(messageBuilder.ToString());
            }
        }
    }
}
