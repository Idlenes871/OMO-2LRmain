using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyML.Functions
{
    internal class Tanh : IActFunction
    {
        public Tanh() { }

        public double DerivativeAtX(double x)
        {
            return 1 - Math.Sqrt(ValueAtX(x));
        }

        public double ValueAtX(double x)
        {
            double a = Math.Exp(x); // e^x
            double b = 1 / a; // e^-x
            return (a - b) / (a + b);
        }
    }
}
