using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shapes.Strategy
{
    public class RectangleStrategy : IStrategy
    {
        public AreaPerimiter Execute(double a, double b, double c)
        {
            var returnValues = new AreaPerimiter();
            returnValues.Area = a * b;
            returnValues.Perimiter = (a * 2) + (b * 2);

            return returnValues;
        }
    }
}
