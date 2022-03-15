using System;

namespace MathLibrary.Model
{
    public static class Equation
    {
        public static double[] SolveSecondDegreeEquation(double a , double b, double c)
        {
            double delta = b * b - 4 * a * c;
            
            if (delta < 0)
            {
                return new double[0];
            } else if (delta == 0)
            {
                return new double[] { -b / 2 / a };
            } else
            {
                return new double[] { (-b - Math.Sqrt(delta)) / 2 / a,
                                        (-b + Math.Sqrt(delta)) / 2 / a };
            }
        }
    }
}
