using System;
using System.Diagnostics.CodeAnalysis;
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
    }
}