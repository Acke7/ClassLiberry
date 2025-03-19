using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Strategy
{
    public class SubtractionStrategy : IStrategy
    {
        public double Execute(double a, double b)
        {
            return a - b;
        }
    }
}

