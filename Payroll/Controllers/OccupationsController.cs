using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Payroll.Business;
using Payroll.Common;
using Payroll.Data;
using Payroll.Models;

namespace Payroll.Controllers
{
    public class OccupationsController : GenericController<Occupation>
    {
        public OccupationsController(OccupationsBO occupationsBO, Message message): 
            base(occupationsBO, message, Resource.Occupation) {}
    }
}
