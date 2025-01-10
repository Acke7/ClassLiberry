using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shapes.Strategy
{
    public class Context
    {
        private IStrategy _strategy;

        public void SetStrategy(IStrategy strategy)
        {
            _strategy = strategy;
        }

        public AreaPerimiter ExecuteStrategy(double a, double b, double c)
        {
            return _strategy.Execute(a, b, c);
        }
    }
}
