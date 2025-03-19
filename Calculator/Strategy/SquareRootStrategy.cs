using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Strategy
{
    public class SquareRootStrategy : IStrategy
    {
        public double Execute(double a, double b)
        {
            if (a < 0)
                throw new ArgumentException("Kvadratrot av negativa tal är inte definierat i detta sammanhang.");

            return Math.Round(Math.Sqrt(a), 2);
        }
    }
}

