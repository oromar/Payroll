using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payroll.Models
{
    public class Currency: Basic
    {
        public String Name { get; set; }
        public double Exchange { get; set; }
    }
}
