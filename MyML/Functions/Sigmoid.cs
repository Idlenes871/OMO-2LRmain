using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyML.Functions
{
        public class Sigmoid : IActFunction { 

        public Sigmoid() { }

        public double DerivativeAtX(double x)
        {
            double val = ValueAtX(x);
            return val * (1 - val);
        }

        public double ValueAtX(double x)
        {
            return 1 / (1 + Math.Exp(-x));
        }
    }
}
