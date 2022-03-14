﻿using System;
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

            ComplexNumber result = a + b;
            Assert.Equal(3, result.Re);
            Assert.Equal(3, result.Im);
        }

        [Fact]
        public void SubtractComplexNumber()
        {
            ComplexNumber a = new ComplexNumber(8, 6);
            ComplexNumber b = new ComplexNumber(5, 2);

            ComplexNumber result = a - b;
            Assert.Equal(3, result.Re);
            Assert.Equal(4, result.Im);
        }

        [Fact]
        public void MultiplyComplexNumber()
        {
            ComplexNumber a = new ComplexNumber(1, 1);
            ComplexNumber b = new ComplexNumber(3, 1);

            ComplexNumber result = a * b;
            Assert.Equal(2, result.Re);
            Assert.Equal(4, result.Im);
        }

        [Fact]
        public void MultiplyComplexNumber2()
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

            ComplexNumber result = a / b;  
            Assert.Equal(4.0 / 17.0, result.Re);
            Assert.Equal(19.0 / 34.0, result.Im);
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
    }
}
