using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payroll.Models
{
    public class Basic
    {
        public Guid Id { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime LastUpdateTime { get; set; }
        public DateTime DeleteTime { get; set; }
        public Boolean Deleted { get; set; }
    }
}
