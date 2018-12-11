using System;

namespace StringCalculatorTests
{
    public class StringCalculator
    {
        public int Add(string numbersAsText)
        {
            if (string.Equals(numbersAsText, ""))
                return 0;

            var numbers = numbersAsText.Split(",");
            if (numbers.Length == 1)
                return Convert.ToInt32(numbers[0]);

            if (numbers.Length == 2)
            {
                var firstNumber = Convert.ToInt32(numbers[0]);
                var secondNumber = Convert.ToInt32(numbers[1]);

                return firstNumber + secondNumber;
            }

            throw new InvalidOperationException();
        }
    }
}
