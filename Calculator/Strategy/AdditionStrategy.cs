﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyCalculator.Strategy
{
    public class AdditionStrategy : IStrategy
    {
        public double Execute(double a, double b)
        {
            return a + b;
        }
    }
}

