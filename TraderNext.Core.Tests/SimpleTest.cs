using Shouldly;
using Xunit;

namespace TraderNext.Core.Tests
{
    public class SimpleTest
    {
        [Fact]
        public void ShouldAddNumbersCorrectly()
        {
            // Arrange
            var x = 1;
            var y = 1;
            var expectedResult = 2;

            // Act
            var actualResult = x + y;

            // Assert
            expectedResult.ShouldBe(actualResult);
        }

        [Theory]
        [InlineData(0, 5)]
        [InlineData(1, 4)]
        [InlineData(2, 3)]
        public void ShouldAddAllNumbersCorrectly(int x, int y)
        {
            // Arrange
            var expectedResult = 5;

            // Act
            var actualResult = x + y;

            // Assert
            Assert.Equal(expectedResult, actualResult);
        }
    }
}
