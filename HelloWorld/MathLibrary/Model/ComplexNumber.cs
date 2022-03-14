using System;

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

        #region ====================   Sum   ====================
        public ComplexNumber Sum(ComplexNumber c)
        {
            return new ComplexNumber(Re + c.Re, Im + c.Im);
        }

        public ComplexNumber Sum(double d)
        {
            return new ComplexNumber(Re + d, Im);
        }

        public static ComplexNumber operator +(ComplexNumber a, ComplexNumber b)
            => a.Sum(b);

        public static ComplexNumber operator +(ComplexNumber a, double d)
            => a.Sum(d);
        #endregion

        #region ====================   Subtract   ====================
        public ComplexNumber Subtract(ComplexNumber c)
        {
            return Sum(new ComplexNumber(-c.Re, -c.Im));
        }

        public ComplexNumber Subtract(double d)
        {
            return Sum(new ComplexNumber(-d, 0));
        }

        public static ComplexNumber operator -(ComplexNumber a, ComplexNumber b)
            => a.Subtract(b);

        public static ComplexNumber operator -(ComplexNumber a, double d)
            => a.Subtract(d);
        #endregion

        #region ==================== Multiply ====================
        public ComplexNumber Multiply(ComplexNumber c)
        {
            // (a+bi)(c+di) = ac + adi + bci + bdi2 = (ac-bd) + (ad+bc)i
            return new ComplexNumber(Re * c.Re - Im * c.Im, Re * c.Im + c.Re * Im);
        }

        public ComplexNumber Multiply(double d)
        {
            return new ComplexNumber(Re * d, Im * d);
        }

        public static ComplexNumber operator *(ComplexNumber a, ComplexNumber b)
            => a.Multiply(b);

        public static ComplexNumber operator *(ComplexNumber a, double d)
            => a.Multiply(d);
        #endregion

        #region ====================   Divide   ====================
        public ComplexNumber Divide(ComplexNumber c)
        {
            double denominator = Math.Pow(c.Re, 2) + Math.Pow(c.Im, 2);

            double newRe = (Re * c.Re + Im * c.Im);
            double newIm = (Im * c.Re - Re * c.Im);

            newRe /= denominator;
            newIm /= denominator;

            return new ComplexNumber(newRe, newIm);
        }

        public ComplexNumber Divide(double d)
        {
            return new ComplexNumber(Re / d, Im / d);
        }

        public static ComplexNumber operator /(ComplexNumber a, ComplexNumber b)
            => a.Divide(b);

        public static ComplexNumber operator /(ComplexNumber a, double d)
            => a.Divide(d);
        #endregion

        #region ==================== Special cases ====================
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
        #endregion

        public static ComplexNumber NeutralElementForSum => new ComplexNumber(0, 0);
        public static ComplexNumber NeutralElementForMult => new ComplexNumber(1, 0);

        public override string ToString()
        {
            return $"({Re})+({Im})i";
        }
    }
}