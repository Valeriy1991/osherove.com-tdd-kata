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
        public void Add__InputStringContainsTwoNumbers__ReturnThisNumber(string inputNumbersAsText)
        {
            // Arrange
            var inputNumbers = inputNumbersAsText.Split(",").Select(e => Convert.ToInt32(e));
            var validSum = inputNumbers.Sum(e => e);

            var calculator = CreateTestedComponent();
            // Act
            var sum = calculator.Add(inputNumbersAsText);
            // Assert
            Assert.Equal(validSum, sum);
        }
    }
}