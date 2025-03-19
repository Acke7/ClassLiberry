using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Strategy
{
    public class DivisionStrategy : IStrategy
    {
        public double Execute(double a, double b)
        {
            if (b == 0)
                throw new DivideByZeroException("Division by zero is not allowed.");
            return a / b;
        }
    }
}

