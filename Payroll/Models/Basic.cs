using Microsoft.AspNetCore.Identity;
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
        public string CreationUser{ get; set; }
        public string LastUpdateUser { get; set; }
        public string DeleteUser { get; set; }
    }
}
