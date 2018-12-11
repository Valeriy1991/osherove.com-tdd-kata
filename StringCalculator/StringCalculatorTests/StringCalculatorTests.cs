using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Xunit;
using StringCalculator = StringCalculatorTests.StringCalculator;

namespace StringCalculatorTests
{
    [ExcludeFromCodeCoverage]
    [Trait("Category", "Unit")]
    public class StringCalculatorTests
    {
        private StringCalculator CreateTestedComponent()
        {
            return new StringCalculator();
        }

        [Fact]
        public void Add__InputStringIsEmpty__ReturnEmptyString()
        {
            // Arrange
            var calculator = CreateTestedComponent();
            // Act
            var sum = calculator.Add("");
            // Assert
            Assert.Equal(0, sum);
        }

        [Theory]
        [InlineData("1")]
        [InlineData("10")]
        [InlineData("100")]
        public void Add__InputStringContainsOneNumber__ReturnThisNumber(string inputNumberAsText)
        {
            // Arrange
            var inputNumber = Convert.ToInt32(inputNumberAsText);
            var calculator = CreateTestedComponent();
            // Act
            var sum = calculator.Add(inputNumberAsText);
            // Assert
            Assert.Equal(inputNumber, sum);
        }

        [Theory]
        [InlineData("1,2")]
        [InlineData("10,20")]
        [InlineData("100,200")]
        public void Add__InputStringContainsTwoNumbers_SeparatorIsComma__ReturnSumOfThisNumbers(string inputNumbersAsText)
        {
            // Arrange
            var separator = ",";
            var inputNumbers = inputNumbersAsText.Split(separator).Select(e => Convert.ToInt32(e));
            var validSum = inputNumbers.Sum(e => e);

            var calculator = CreateTestedComponent();
            // Act
            var sum = calculator.Add(inputNumbersAsText);
            // Assert
            Assert.Equal(validSum, sum);
        }

        [Theory]
        [InlineData("1,2,3,4,5,6,7,8,9,10")]
        [InlineData("10,20,30,40,50,60")]
        [InlineData("100,200,300")]
        public void Add__InputStringContainsManyNumbers_SeparatorIsComma__ReturnSumOfThisNumbers(string inputNumbersAsText)
        {
            // Arrange
            var separator = ",";
            var inputNumbers = inputNumbersAsText.Split(separator).Select(e => Convert.ToInt32(e));
            var validSum = inputNumbers.Sum(e => e);

            var calculator = CreateTestedComponent();
            // Act
            var sum = calculator.Add(inputNumbersAsText);
            // Assert
            Assert.Equal(validSum, sum);
        }


        [Theory]
        [InlineData("1\n2\n3\n4\n5\n6\n7\n8\n9\n10")]
        [InlineData("1\n2,3")]
        [InlineData("100\n200\n300")]
        public void Add__InputStringContainsManyNumbers_SeparatorIsNewLine__ReturnSumOfThisNumbers(
            string inputNumbersAsText)
        {
            // Arrange
            var newLineSeparator = '\n';
            var commaSeparator = ',';
            var inputNumbersWithCommaSeparator = inputNumbersAsText.Replace(newLineSeparator, commaSeparator);
            var inputNumbers = inputNumbersWithCommaSeparator.Split(commaSeparator).Select(e => Convert.ToInt32(e));
            var validSum = inputNumbers.Sum(e => e);

            var calculator = CreateTestedComponent();
            // Act
            var sum = calculator.Add(inputNumbersAsText);
            // Assert
            Assert.Equal(validSum, sum);
        }
    }
}