using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyCalculator.Strategy
{
    public interface IStrategy
    {
        double Execute(double a, double b);
    }
}
