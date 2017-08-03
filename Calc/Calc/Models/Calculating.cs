using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Calc.Models
{
    public class Calculating
    {
        public int Id { get; set; }
        public string Term { get; set; }
        public decimal Res { get; set; }
        public string Operands { get; set; }
        public string Operations { get; set; }        
        public DateTime TimeofCalc { get; set; }
        public string UserIp { get; set; }

    }
}