using QuizApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace QuizApp.Tests
{
    public class QuizWindowTests
    {
        private readonly QuizService _sut;

        public QuizWindowTests()
        {
            _sut = new QuizService(new(new()));
        }

        [Theory]
        [InlineData(1, 3, 33.33)]
        [InlineData(6, 9, 66.67)]
        [InlineData(1, 5, 20)]
        [InlineData(1, 1, 100)]
        [InlineData(0, 1, 0)]
        [InlineData(1, 0, -1)]
        public void ResultShouldReturnRoundValue(
            int correctCheckedCount, int questionCount, decimal expected)
        {
            // Arrange

            // Act
            var result = _sut.CalculateResult(correctCheckedCount, questionCount);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}
