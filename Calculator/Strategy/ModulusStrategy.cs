using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Strategy
{
    public class ModulusStrategy : IStrategy
    {
        public double Execute(double a, double b)
        {
            if (b == 0)
                throw new DivideByZeroException("Modulus-division med noll är inte tillåtet.");

            return Math.Round(a % b, 2);
        }
    }
}

