using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary.Model;
using Xunit;

namespace MathLibrary.Test
{
    public class ComplexNumberTest
    {
        [Fact]
        public void SumComplexNumber()
        {
            ComplexNumber a = new ComplexNumber(1, 1);
            ComplexNumber b = new ComplexNumber(2, 2);

            ComplexNumber result = a.Sum(b);
            Assert.Equal(3, result.Re);
            Assert.Equal(3, result.Im);
        }

        [Fact]
        public void SubtractComplexNumber()
        {
            ComplexNumber a = new ComplexNumber(8, 6);
            ComplexNumber b = new ComplexNumber(5, 2);

            ComplexNumber result = a.Subtract(b);
            Assert.Equal(3, result.Re);
            Assert.Equal(4, result.Im);
        }

        [Fact]
        public void MultiplyComplexNumber()
        {
            ComplexNumber a = new ComplexNumber(1, 1);
            ComplexNumber b = new ComplexNumber(3, 1);

            ComplexNumber result = a.Multiply(b);
            Assert.Equal(2, result.Re);
            Assert.Equal(4, result.Im);
        }

        [Fact]
        public void MultiplyComplexNumber2()
        {
            ComplexNumber a = new ComplexNumber(3, 2);
            ComplexNumber b = new ComplexNumber(1, 7);

            ComplexNumber result = a.Multiply(b);
            Assert.Equal(-11, result.Re);
            Assert.Equal(23, result.Im);
        }

        [Fact]
        public void DivideComplexNumber()
        {
            ComplexNumber a = new ComplexNumber(3, 4);
            ComplexNumber b = new ComplexNumber(8, -2);

            ComplexNumber result = a.Divide(b);
            // NOTE: The asserts are going to fail if the expected values are not
            //       explicitly passed as a division over doubles!
            Assert.Equal(4.0d / 17.0d, result.Re);
            Assert.Equal(19.0d / 34.0d, result.Im);
        }

        [Fact]
        public void ComplexNumberModule()
        {
            ComplexNumber c = new ComplexNumber(2, 2);

            Assert.Equal(Math.Sqrt(8), c.Modulus);
        }

        [Fact]
        public void ComplexNumberConjugate()
        {
            ComplexNumber a = new ComplexNumber(1, 1);
            ComplexNumber b = new ComplexNumber(1, -1);

            Assert.Equal(b.Re, a.Conjugate.Re);
            Assert.Equal(b.Im, a.Conjugate.Im);
        }
    }
}
