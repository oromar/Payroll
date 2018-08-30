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
    public class CurrenciesController : GenericController<Currency>
    {
        public CurrenciesController(CurrenciesBO currenciesBO, Message message): 
            base(currenciesBO, message, Resource.Currency) {}
    }
}
