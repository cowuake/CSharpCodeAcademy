using System;
using Xunit;
using MathLibrary;

namespace MathLibrary.Test
{
    public class SecondDegreeEquationTest
    {
        [Fact]
        public void TestZeroDelta()
        {
            // Arrange
            Equation eq = new Equation();

            // Act
            var result = eq.SolveSecondDegreeEquation(1, 2, 1);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Length == 1);
            Assert.Equal(result, new double[] { -1 });
        }

        [Fact]
        public void TestNegativeDelta()
        {
            // Arrange
            Equation eq = new Equation();

            // Act
            var result = eq.SolveSecondDegreeEquation(2, 1, 2);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Length == 0);
        }

        [Fact]
        public void TestPositiveDelta()
        {
            // Arrange
            Equation eq = new Equation();

            // Act
            var result = eq.SolveSecondDegreeEquation(1, 2, 0);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Length == 2);
            Assert.Equal(result, new double[] { -2, 0 });
        }
    }
}
