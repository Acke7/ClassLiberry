using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClassLibrary.Models
{
    public class CalculationData
    {
        public int Id { get; set; }
        public double Operand1 { get; set; }
        public double Operand2 { get; set; }
        public string Operator { get; set; }
        public double Result { get; set; }
        public DateTime Date { get; set; }
    }
}
