using System;
using MathLibrary.Model;
using Xunit;

namespace MathLibrary.Test
{
    public class ComplexNumberTest
    {
        [Fact]
        public void ComplexNumberSum()
        {
            ComplexNumber a = new ComplexNumber(1, 1);
            ComplexNumber b = new ComplexNumber(2, 2);
            double d = 10;

            ComplexNumber result;
            
            result = a + b;
            Assert.Equal(3, result.Re);
            Assert.Equal(3, result.Im);

            result = a + d;
            Assert.Equal(11, result.Re);
            Assert.Equal(a.Im, result.Im);
        }

        [Fact]
        public void ComplexNumberSubtract()
        {
            ComplexNumber a = new ComplexNumber(8, 6);
            ComplexNumber b = new ComplexNumber(5, 2);
            double d = 10;

            ComplexNumber result;
            
            result = a - b;
            Assert.Equal(3, result.Re);
            Assert.Equal(4, result.Im);

            result = a - d;
            Assert.Equal(-2, result.Re);
            Assert.Equal(a.Im, result.Im);
        }

        [Fact]
        public void ComplexNumberMultiply()
        {
            ComplexNumber a = new ComplexNumber(1, 1);
            ComplexNumber b = new ComplexNumber(3, 1);
            double d = 10;

            ComplexNumber result;
            
            result = a * b;
            Assert.Equal(2, result.Re);
            Assert.Equal(4, result.Im);

            result = a * d;
            Assert.Equal(a.Re * d, result.Re);
            Assert.Equal(a.Im * d, result.Im);
        }

        [Fact]
        public void ComplexNumberMultiply2()
        {
            ComplexNumber a = new ComplexNumber(3, 2);
            ComplexNumber b = new ComplexNumber(1, 7);

            ComplexNumber result = a * b;
            Assert.Equal(-11, result.Re);
            Assert.Equal(23, result.Im);
        }

        [Fact]
        public void DivideComplexNumber()
        {
            ComplexNumber a = new ComplexNumber(3, 4);
            ComplexNumber b = new ComplexNumber(8, -2);
            double d = 10;

            ComplexNumber result;
            
            result = a / b;  
            Assert.Equal(4.0 / 17.0, result.Re);
            Assert.Equal(19.0 / 34.0, result.Im);

            result = a / d;
            Assert.Equal(a.Re / d, result.Re);
            Assert.Equal(a.Im / d, result.Im);
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

        [Fact]
        public void ComplexNumberArgument()
        {
            ComplexNumber c;

            c = new ComplexNumber(0, 0);
            Assert.Null(c.Argument);

            c = new ComplexNumber(0, 999);
            Assert.Equal(Math.PI / 2, c.Argument);

            c = new ComplexNumber(999, 0);
            Assert.Equal(0, c.Argument);

            c = new ComplexNumber(999, 999);
            Assert.Equal(Math.PI / 4, c.Argument);
        }

        [Fact]
        public void ComplexNumberSpecialCases()
        {
            ComplexNumber c;

            c = new ComplexNumber(0, 0);
            Assert.False(c.IsReal());
            Assert.False(c.IsImaginary());
            Assert.True(c.IsOrigin());

            c = new ComplexNumber(1, 0);
            Assert.True(c.IsReal());
            Assert.False(c.IsImaginary());
            Assert.False(c.IsOrigin());

            c = new ComplexNumber(0, 1);
            Assert.False(c.IsReal());
            Assert.True(c.IsImaginary());
            Assert.False(c.IsOrigin());
        }

        [Fact]
        public void ComplexNumberToString()
        {
            ComplexNumber c;

            c = new ComplexNumber(0, 0);
            Assert.Equal("0", c.ToString());

            c = new ComplexNumber(999, 0);
            Assert.Equal("999", c.ToString());

            c = new ComplexNumber(0, 999);
            Assert.Equal("999i", c.ToString());

            c = new ComplexNumber(999, 999);
            Assert.Equal("999+999i", c.ToString());

            c = new ComplexNumber(-999, -999);
            Assert.Equal("-999-999i", c.ToString());
        }

        [Fact]
        public void ComplexNumberFromStringConstructor()
        {
            string sourceString;
            ComplexNumber c;
            
            sourceString = "(1,1)";
            c = new ComplexNumber(sourceString);
            Assert.Equal(1, c.Re);
            Assert.Equal(1, c.Im);

            sourceString = "(-1,-1)";
            c = new ComplexNumber(sourceString);
            Assert.Equal(-1, c.Re);
            Assert.Equal(-1, c.Im);

            sourceString = "(1.999,-999.1)";
            c = new ComplexNumber(sourceString);
            Assert.Equal(1.999, c.Re);
            Assert.Equal(-999.1, c.Im);
        }
    }
}