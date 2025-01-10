using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shapes.Strategy
{
    public interface IStrategy
    {
        AreaPerimiter Execute(double a, double b, double c);
    }
}
