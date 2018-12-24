using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace StringCalculatorTests.Kata2
{
    public class StringCalculator
    {
        public const string CustomSeparatorStartChar = "//";

        public int Add(string inputNumbersAsText)
        {
            if (string.Equals(inputNumbersAsText, ""))
                return 0;

            List<int> numbers;

            if (IsCustomSeparatorExists(inputNumbersAsText))
            {
                numbers = GetNumbersWithCustomSeparator(inputNumbersAsText).ToList();
            }
            else
            {
                numbers = GetNumbersWithDefaultSeparators(inputNumbersAsText).ToList();
            }

            RemoveNumbersThatBiggerThan1000(numbers);
            CheckThatExistsNegativeNumbers(numbers);

            return numbers.Sum();
        }

        private bool IsCustomSeparatorExists(string inputNumbersAsText)
        {
            var separatorSignStartPosition =
                inputNumbersAsText.IndexOf(CustomSeparatorStartChar, StringComparison.Ordinal);
            return separatorSignStartPosition == 0;
        }

        private IEnumerable<int> GetNumbersWithCustomSeparator(string inputNumbersAsText)
        {
            var customSeparatorRegex = new Regex(@"(?<=//)[^\n]+");
            var separator = customSeparatorRegex.Match(inputNumbersAsText).Value;
            var inputNumbersAsTextWithoutSeparator =
                inputNumbersAsText.Substring(CustomSeparatorStartChar.Length + separator.Length);
            
            const string separatorGroupName = "separator";
            var multipleCustomSeparatorsRegex = new Regex($@"(?<=\[)(?<{separatorGroupName}>[^\]]+)");
            if (multipleCustomSeparatorsRegex.IsMatch(separator))
            {
                var foundSeparators = multipleCustomSeparatorsRegex.Matches(separator).Select(e => e.Value).ToList();
                if (foundSeparators.Any())
                {
                    var firstCustomSeparator = foundSeparators[0];
                    var otherCustomSeparators = foundSeparators.Skip(1);

                    foreach (var otherCustomSeparator in otherCustomSeparators)
                    {
                        inputNumbersAsTextWithoutSeparator =
                            inputNumbersAsTextWithoutSeparator.Replace(otherCustomSeparator, firstCustomSeparator);
                    }

                    separator = firstCustomSeparator;
                }
            }

            var inputNumbersAsTextWithoutFirstNewLine = inputNumbersAsTextWithoutSeparator.Substring(1);
            var numbersAsText = inputNumbersAsTextWithoutFirstNewLine.Split(separator);
            return ConvertToNumbers(numbersAsText);
        }

        private IEnumerable<int> GetNumbersWithDefaultSeparators(string inputNumbersAsText)
        {
            var numbersAsTextWithOnlyCommaSeparator = inputNumbersAsText.Replace('\n', ',');
            var numbersAsText = numbersAsTextWithOnlyCommaSeparator.Split(",");
            return ConvertToNumbers(numbersAsText);
        }

        private IEnumerable<int> ConvertToNumbers(string[] numbersAsText)
        {
            return numbersAsText.Select(e => Convert.ToInt32(e));
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