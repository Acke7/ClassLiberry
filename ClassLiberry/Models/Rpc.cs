using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClassLibrary.Models
{
    public class Rpc
    {
        public int Id { get; set; }
        public int PlayerMove { get; set; }
        public int ComputerMove { get; set; }
        public string Result { get; set; }
        public DateTime Date { get; set; }

        public decimal AverageResult { get; set; }
    }
}
