using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shapes.Strategy
{
    public class ParallelogramStrategy : IStrategy
    {
        public AreaPerimiter Execute(double baseLength, double height, double sideLength)
        {
            var returnValues = new AreaPerimiter();
            returnValues.Area = baseLength * height;
            returnValues.Perimiter = 2 * (baseLength + sideLength);

            return returnValues;
        }
    }
}
