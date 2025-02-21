﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shapes.Strategy
{
    public class RectangleStrategy : IStrategy
    {
        public AreaPerimiter Execute(double length, double width, double unused)
        {
            var returnValues = new AreaPerimiter();
            returnValues.Area = length * width;
            returnValues.Perimiter = 2 * (length + width);

            return returnValues;
        }
    }
}
