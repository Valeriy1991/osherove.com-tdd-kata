using System;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace StringCalculatorTests.Kata2
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
        [InlineData("1,2", 3)]
        [InlineData("10,20", 30)]
        [InlineData("100,200", 300)]
        public void Add__InputStringContainsTwoNumbers_SeparatorIsComma__ReturnSumOfThisNumbers(
            string inputNumbersAsText, int validSum)
        {
            // Arrange
            var calculator = CreateTestedComponent();
            // Act
            var sum = calculator.Add(inputNumbersAsText);
            // Assert
            Assert.Equal(validSum, sum);
        }

        [Theory]
        [InlineData("1,2,3,4,5,6,7,8,9,10", 55)]
        [InlineData("10,20,30,40,50,60", 210)]
        [InlineData("100,200,300", 600)]
        public void Add__InputStringContainsManyNumbers_SeparatorIsComma__ReturnSumOfThisNumbers(
            string inputNumbersAsText, int validSum)
        {
            // Arrange
            var calculator = CreateTestedComponent();
            // Act
            var sum = calculator.Add(inputNumbersAsText);
            // Assert
            Assert.Equal(validSum, sum);
        }


        [Theory]
        [InlineData("1\n2\n3\n4\n5\n6\n7\n8\n9\n10", 55)]
        [InlineData("1\n2,3", 6)]
        [InlineData("100\n200\n300", 600)]
        public void Add__InputStringContainsManyNumbers_SeparatorIsNewLine__ReturnSumOfThisNumbers(
            string inputNumbersAsText, int validSum)
        {
            // Arrange
            var calculator = CreateTestedComponent();
            // Act
            var sum = calculator.Add(inputNumbersAsText);
            // Assert
            Assert.Equal(validSum, sum);
        }

        [Theory]
        [InlineData("//;\n5", 5)]
        [InlineData("//;\n1;2", 3)]
        [InlineData("//-\n100-200-300", 600)]
        public void Add__InputStringContainsDelimiterAtFirstLineAndAlsoContainsSomeNumbers__ReturnSumOfThisNumbers(
            string inputNumbersAsText, int validSum)
        {
            // Arrange
            var calculator = CreateTestedComponent();
            // Act
            var sum = calculator.Add(inputNumbersAsText);
            // Assert
            Assert.Equal(validSum, sum);
        }

        [Theory]
        [InlineData("//;\n-5", "-5")]
        [InlineData("//;\n-1;-2", "-1, -2")]
        [InlineData("//;\n-1;-2;3", "-1, -2")]
        [InlineData("1\n2,-3", "-3")]
        [InlineData("100,-200,300", "-200")]
        [InlineData("10\n20\n30\n-40\n-50", "-40, -50")]
        public void Add__InputStringContainsNegativeNumbers__ThrowExceptionWithThisNegativeNumbers(
            string inputNumbersAsText, string validNegativeNumbersAsText)
        {
            // Arrange
            var calculator = CreateTestedComponent();
            Action act = () => calculator.Add(inputNumbersAsText);
            // Act
            var ex = Record.Exception(act);
            // Assert
            Assert.IsType<ArgumentException>(ex);
            Assert.Contains("negatives not allowed", ex.Message);
            Assert.Contains(validNegativeNumbersAsText, ex.Message);
        }

        [Theory]
        [InlineData("1,2,3,4,1001,1002", 10)]
        [InlineData("1001,1002,1003", 0)]
        [InlineData("1000,1001,1002,1003", 1000)]
        public void
            Add__InputStringContainsNumbersThatBiggerThan1000__ReturnSumOfThisNumbersWithoutNumbersThatBiggerThan1000(
                string inputNumbersAsText, int validSum)
        {
            // Arrange
            var calculator = CreateTestedComponent();
            // Act
            var sum = calculator.Add(inputNumbersAsText);
            // Assert
            Assert.Equal(validSum, sum);
        }

        [Theory]
        [InlineData("//***\n1***2***3***4", 10)]
        [InlineData("//**\n10**20**30", 60)]
        [InlineData("//++++\n999++++1000++++1001", 1999)]
        public void Add__SeparatorsMayHaveAnyLength__ReturnSumOfInputNumbers(
            string inputNumbersAsText, int validSum)
        {
            // Arrange
            var calculator = CreateTestedComponent();
            // Act
            var sum = calculator.Add(inputNumbersAsText);
            // Assert
            Assert.Equal(validSum, sum);
        }

        [Theory]
        [InlineData("//[*][%]\n1*2*3%4%5", 15)]
        [InlineData("//[#][^]\n10#20^30", 60)]
        [InlineData("//[*][%]\n999*1000%1001", 1999)]
        public void Add__InputStringContainsNumbersAndMultipleDelimiters__ReturnSumOfInputNumbers(
            string inputNumbersAsText, int validSum)
        {
            // Arrange
            var calculator = CreateTestedComponent();
            // Act
            var sum = calculator.Add(inputNumbersAsText);
            // Assert
            Assert.Equal(validSum, sum);
        }

        [Theory]
        [InlineData("//[***][%%]\n1***2***3%%4%%5", 15)]
        [InlineData("//[##][^]\n10##20^30", 60)]
        [InlineData("//[*][%%%]\n999*1000%%%1001", 1999)]
        public void Add__InputStringContainsNumbersAndMultipleDelimitersWithAnyLength__ReturnSumOfInputNumbers(
            string inputNumbersAsText, int validSum)
        {
            // Arrange
            var calculator = CreateTestedComponent();
            // Act
            var sum = calculator.Add(inputNumbersAsText);
            // Assert
            Assert.Equal(validSum, sum);
        }
    }
}