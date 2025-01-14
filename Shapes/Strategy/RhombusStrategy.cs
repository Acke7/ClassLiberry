using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shapes.Strategy
{
    public class RhombusStrategy :IStrategy
    {
        public AreaPerimiter Execute(double diagonal1, double diagonal2, double unused)
        {
            var returnValues = new AreaPerimiter();
            returnValues.Area = (diagonal1 * diagonal2) / 2;
            returnValues.Perimiter = 2 * Math.Sqrt(Math.Pow(diagonal1 / 2, 2) + Math.Pow(diagonal2 / 2, 2));

            return returnValues;
        }
    }
}
