using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyML.Functions
{
    public class ReLu : IActFunction
    {
        public ReLu() { }

        public double DerivativeAtX(double x)
        {
            if (x >= 0) 
                return 1;
            return 0;
        }

        public double ValueAtX(double x)
        {
            return double.Max(0, x);
        }
    }
}
