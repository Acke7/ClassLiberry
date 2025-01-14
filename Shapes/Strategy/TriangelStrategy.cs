using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shapes.Strategy
{
    public class TriangelStrategy : IStrategy
    {
        public AreaPerimiter Execute(double sideA, double sideB, double sideC)
        {
            var areaPerimeterResult = new AreaPerimiter();

            // Calculate the semi-perimeter
            double semiPerimeter = (sideA + sideB + sideC) / 2;

            // Calculate the area using Heron's formula
            areaPerimeterResult.Area = Math.Sqrt(semiPerimeter * (semiPerimeter - sideA) * (semiPerimeter - sideB) * (semiPerimeter - sideC));

            // Calculate the perimeter
            areaPerimeterResult.Perimiter = sideA + sideB + sideC;

            return areaPerimeterResult;
        }
    }
}
