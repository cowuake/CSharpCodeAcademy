using System;
using System.Collections.Generic;
using System.Text;

namespace MathLibrary.Model
{
    public class ComplexNumber
    {
        private double _realPart;
        private double _imagPart;

        public double Re
        {
            get { return _realPart; }
            set { _realPart = value; }
        }

        public double Im
        {
            get { return _imagPart; }
            set { _imagPart = value; }
        }

        public double Modulus
        {
            get => Math.Sqrt(Math.Pow(_realPart, 2) + Math.Pow(_imagPart, 2));
        }

        public ComplexNumber Conjugate
        {
            get => new ComplexNumber(_realPart, -_imagPart);
        }

        public double? Argument
        {
            get
            {
                if (IsOrigin())
                    return null;

                if (IsReal()) // Purely real number
                {
                    return 0;
                }
                else if (IsImaginary()) // Purely imaginary number
                {
                    return Math.PI / 2;
                }
                else
                {
                    return Math.Atan(Im / Re);
                }
            }
        }

        public ComplexNumber(double re, double im)
        {
            _realPart = re;
            _imagPart = im;
        }

        public ComplexNumber Sum(ComplexNumber c)
        {
            return new ComplexNumber(Re + c.Re, Im + c.Im);
        }

        public ComplexNumber Subtract(ComplexNumber c)
        {
            return Sum(new ComplexNumber(-c.Re, -c.Im));
        }

        public ComplexNumber Multiply(ComplexNumber c)
        {
            // (a+bi)(c+di) = ac + adi + bci + bdi2 = (ac-bd) + (ad+bc)i
            return new ComplexNumber(Re * c.Re - Im * c.Im, Re * c.Im + c.Re * Im);
        }

        public ComplexNumber Divide(ComplexNumber c)
        {
            double denominator = Math.Pow(c.Re, 2) + Math.Pow(c.Im, 2);

            double newRe = (Re * c.Re + Im * c.Im);
            double newIm = (Im * c.Re - Re * c.Im);

            newRe /= denominator;
            newIm /= denominator;

            return new ComplexNumber(newRe, newIm);
        }

        public static ComplexNumber operator +(ComplexNumber a, ComplexNumber b)
        {
            return a.Sum(b);
        }

        public static ComplexNumber operator -(ComplexNumber a, ComplexNumber b)
        {
            return a.Subtract(b);
        }

        public static ComplexNumber operator *(ComplexNumber a, ComplexNumber b)
        {
            return a.Multiply(b);
        }

        public static ComplexNumber operator /(ComplexNumber a, ComplexNumber b)
        {
            return a.Divide(b);
        }

        public bool IsReal()
        {
            if (Re != 0 && Im == 0)
                return true;

            return false;
        }

        public bool IsImaginary()
        {
            if (Re == 0 && Im != 0)
                return true;

            return false;
        }

        public bool IsOrigin()
        {
            if (Re == 0 && Im == 0)
                return true;

            return false;
        }

        public override string ToString()
        {
            return $"({Re})+({Im})i";
        }
    }
}
